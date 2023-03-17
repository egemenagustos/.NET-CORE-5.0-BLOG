using BlogEntities.Dtos;
using BlogMvc.Helpers.Abstract;
using BlogShared.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using BlogShared.Utilities.Extensions;
using BlogShared.Utilities.Results.ComplexTypes;
using BlogShared.Utilities.Results.Concrete;
using BlogEntities.ComplexType;
using System.Text.RegularExpressions;

namespace BlogMvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}", pictureName);
            if (File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path= fileInfo.FullName,
                    Size = fileInfo.Length
                };
                File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStates.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStates.Error, "Böyle bir resim bulunamadı",null);
            }
        }

        public async Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            /* Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (PictureType) klasör adı ataması yapılır. */
            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

            /* Eğer folderName değişkeni ile gelen klasör adı sistemimizde mevcut değilse, yeni bir klasör oluşturulur. */
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            /* Resimin yüklenme sırasındaki ilk adı oldFileName adlı değişkene atanır. */
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName); //uzantısız resimi alıyoruz.

            /* Resimin uzantısı fileExtension adlı değişkene atanır. */
            string fileExtensions = Path.GetExtension(pictureFile.FileName); //uzantıyı aldık.


            DateTime dateTime = DateTime.Now;

            /* Makale başlığımızın ismini vereceğimiz için resim yolunda özel semboller olmaması gerekiyor. Bu yüzden replace işlemi uyguluyoruz. */
            Regex regex = new Regex("[|+*'\",._&#^@++||]");
            name = regex.Replace(name,string.Empty);

            /*
           // Parametre ile gelen değerler kullanılarak yeni bir resim adı oluşturulur.
           // Örn: adminuser_587_5_38_12_3_10_2020.png
           */
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtensions}"; //yeni bir isim oluşturdum.

            /* Kendi parametrelerimiz ile sistemimize uygun yeni bir dosya yolu (path) oluşturulur. */
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            /* Sistemimiz için oluşturulan yeni dosya yoluna resim kopyalanır. */
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            /* Resim tipine göre kullanıcı için bir mesaj oluşturulur. */
            string message = pictureType == PictureType.User ?
            $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir." : $"{name} adlı makalenin resmi başarıyla yüklenmiştir.";

            return new DataResult<ImageUploadedDto>(ResultStates.Success, message, new ImageUploadedDto
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtensions,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });

        }
    }
}
