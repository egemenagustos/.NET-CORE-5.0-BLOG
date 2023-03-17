using AutoMapper;
using BlogEntities.Dtos;
using BlogMvc.Areas.Admin.Models;
using BlogMvc.Helpers.Concrete;
using BlogServices.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogEntities.ComplexType;
using BlogMvc.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using BlogEntities.Concrete;
using System.Text.Json;
using System.Text.Json.Serialization;
using NToastNotify;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace BlogMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }

        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStates == ResultStates.Success) return View(result.Data);
            return NotFound();
        }

        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (result.ResultStates == ResultStates.Success)
            {
                return View(new ArticleAddViewModel
                {
                    Category = result.Data.Categories
                });
            }
            return NotFound();
        }

        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await ImageHelper.Upload(articleAddViewModel.Title, articleAddViewModel.ThumbNail, PictureType.Post);
                articleAddDto.ThumbNail = imageResult.Data.FullName;

                var result = await _articleService.AddAsync(articleAddDto, LoggedInUser.UserName, LoggedInUser.Id);
                if (result.ResultStates == ResultStates.Success)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message,new ToastrOptions
                    {
                       Title = "Başarılı İşlem" 
                    });
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleAddViewModel.Category = categories.Data.Categories;
            return View(articleAddViewModel);
        }

        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var articleResult = await _articleService.GetArticleUpdateAsync(id);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (articleResult.ResultStates == ResultStates.Success && categoriesResult.ResultStates == ResultStates.Success)
            {
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                articleUpdateViewModel.Category = categoriesResult.Data.Categories;
                return View(articleUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnail = false;
                var oldThumbNail = articleUpdateViewModel.ThumbNail;
                if (articleUpdateViewModel.ThumbNailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(articleUpdateViewModel.Title, articleUpdateViewModel.ThumbNailFile, PictureType.Post);
                    articleUpdateViewModel.ThumbNail = uploadedImageResult.ResultStates == ResultStates.Success ? uploadedImageResult.Data.FullName : "postImages/defaultThumbnail.jpg";
                    if (oldThumbNail != "postImages/default.Thumbnail.jpg")
                    {
                        isNewThumbnail = true;
                    }
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                var result = await _articleService.UpdateAsync(articleUpdateDto, LoggedInUser.UserName);
                if (result.ResultStates == ResultStates.Success)
                {
                    if (isNewThumbnail)
                    {
                        ImageHelper.Delete(oldThumbNail);
                    }
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Başarılı İşlem"
                    });
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleUpdateViewModel.Category = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }

        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _articleService.DeleteAsync(id, LoggedInUser.UserName);
            var articleResult = JsonSerializer.Serialize(result);
            return Json(articleResult);
        }

        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllByNonDeletedActiveAsync();
            var articlesResult = JsonSerializer.Serialize(articles,new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articlesResult);
        }

        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            return View(result.Data);

        }

        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            var articles = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }

        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int id)
        {
            var result = await _articleService.UndoDeleteAsync(id, LoggedInUser.UserName);
            var undoDeleteArticleResult = JsonSerializer.Serialize(result);
            return Json(undoDeleteArticleResult);
        }

        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int id)
        {
            var result = await _articleService.HardDeleteAsync(id);
            var hardDeletedArticleResult = JsonSerializer.Serialize(result);
            return Json(hardDeletedArticleResult);
        }

        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllByViewCount(bool isAscending, int takeSize)
        {
            var result = await _articleService.GetAllByViewCountAsync(isAscending,takeSize);
            var articles = JsonSerializer.Serialize(result.Data.Articles, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }
    }
}
