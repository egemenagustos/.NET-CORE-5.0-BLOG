using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogMvc.Models;
using BlogServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogShared.Utilities.Results.ComplexTypes;
using NToastNotify;
using ProgrammersBlog.Shared.Utilities.Helpers.Abstract;

namespace BlogMvc.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        
        public HomeController(IArticleService articleService, IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification notification, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter)
        {
            _articleService = articleService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _mailService = mailService;
            _toastNotification = notification;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
        }
        [Route("index")]
        [Route("anasayfa")]
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var articlesResult = await (categoryId == null ? _articleService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending) : _articleService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(articlesResult.Data);
        }

        [Route("hakkimizda")]
        [HttpGet]
        public IActionResult About()
        {
         return View(_aboutUsPageInfo);
        }

        [Route("iletisim")]
        [HttpGet]
        public IActionResult Contact()
        { 
            return View();
        }

        [Route("iletisim")]
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactMail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message,new ToastrOptions
                {
                    Title = "Başarılı İşlem !"
                });
                return View();
            }
            return View();
        }
    }
}
