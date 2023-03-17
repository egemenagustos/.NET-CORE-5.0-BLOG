using AutoMapper;
using BlogEntities.Concrete;
using BlogMvc.Areas.Admin.Models;
using BlogServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using ProgrammersBlog.Shared.Utilities.Helpers.Abstract;
using System.Threading.Tasks;

namespace BlogMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsInfoWriter;
        private readonly IToastNotification _toastNotification;
        private readonly WebSiteInfo _webSiteInfo;
        private readonly IWritableOptions<WebSiteInfo> _webSiteInfoWriter;
        private readonly SmtpSettings _smptSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingInfoWriter;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;
        private readonly IWritableOptions<ArticleRightSideBarWidgetOptions> _articleRightSideBarWidgetOptionsWriter;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public OptionsController(IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IWritableOptions<AboutUsPageInfo> aboutUsInfoWriter, IToastNotification toastNotification, IOptionsSnapshot<WebSiteInfo> webSiteInfo, IWritableOptions<WebSiteInfo> webSiteWriterInfo, IOptionsSnapshot<SmtpSettings> smptSettings, IWritableOptions<SmtpSettings> smtpSettingWriterInfo, IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions, IWritableOptions<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptionsWriter, ICategoryService categoryService, IMapper mapper)
        {
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _aboutUsInfoWriter = aboutUsInfoWriter;
            _toastNotification = toastNotification;
            _webSiteInfo = webSiteInfo.Value;
            _webSiteInfoWriter = webSiteWriterInfo;
            _smptSettings = smptSettings.Value;
            _smtpSettingInfoWriter = smtpSettingWriterInfo;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
            _articleRightSideBarWidgetOptionsWriter = articleRightSideBarWidgetOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsInfoWriter.Update(x =>
                {
                    x.Header = aboutUsPageInfo.Header;
                    x.Content = aboutUsPageInfo.Content;
                    x.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    x.SeoDescription= aboutUsPageInfo.SeoDescription;
                    x.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Hakkımızda sayfa içerikleri başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(aboutUsPageInfo);
            }
            return View(aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_webSiteInfo);
        }

        [HttpPost]
        public IActionResult GeneralSettings(WebSiteInfo webSiteInfo)
        {
            if (ModelState.IsValid)
            {
                _webSiteInfoWriter.Update(x =>
                {
                    x.Title = webSiteInfo.Title;
                    x.MenuTitle = webSiteInfo.MenuTitle;
                    x.SeoAuthor = webSiteInfo.SeoAuthor;
                    x.SeoTags = webSiteInfo.SeoTags;
                    x.SeoDescription = webSiteInfo.SeoDescription;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin genel ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(webSiteInfo);
            }
            return View(webSiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smptSettings);
        }

        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingInfoWriter.Update(x =>
                {
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.SenderName = smtpSettings.SenderName;
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.Password = smtpSettings.Password;
                    x.Username = smtpSettings.Username;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin e-posta ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleRightSidebarWidgetSettings()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var articleRightSidebarWidgetOptionsViewModel = _mapper.Map<ArticleRightSideBarWidgetOptionsViewModel>(_articleRightSideBarWidgetOptions);
            articleRightSidebarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(articleRightSidebarWidgetOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ArticleRightSidebarWidgetSettings(ArticleRightSideBarWidgetOptionsViewModel articleRightSideBarWidgetOptionsViewModel)
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            if (ModelState.IsValid)
            {
                _articleRightSideBarWidgetOptionsWriter.Update(x =>
                {
                    x.Header = articleRightSideBarWidgetOptionsViewModel.Header;
                    x.TakeSize = articleRightSideBarWidgetOptionsViewModel.TakeSize;
                    x.CategoryId = articleRightSideBarWidgetOptionsViewModel.CategoryId;
                    x.FilterBy = articleRightSideBarWidgetOptionsViewModel.FilterBy;
                    x.OrderBy = articleRightSideBarWidgetOptionsViewModel.OrderBy;
                    x.IsAscending = articleRightSideBarWidgetOptionsViewModel.IsAscending;
                    x.StartAt = articleRightSideBarWidgetOptionsViewModel.StartAt;
                    x.EndAt = articleRightSideBarWidgetOptionsViewModel.EndAt;
                    x.MaxViewCount = articleRightSideBarWidgetOptionsViewModel.MaxViewCount;
                    x.MinViewCount = articleRightSideBarWidgetOptionsViewModel.MinViewCount;
                    x.MaxCommentCount = articleRightSideBarWidgetOptionsViewModel.MaxCommentCount;
                    x.MinCommentCount = articleRightSideBarWidgetOptionsViewModel.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage("Makale sayfalarınızın widget ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(articleRightSideBarWidgetOptionsViewModel);
            }
            return View(articleRightSideBarWidgetOptionsViewModel);
        }
    }
}
