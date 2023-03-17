using BlogServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogShared.Utilities.Results.ComplexTypes;
using BlogMvc.Models;
using BlogEntities.ComplexType;
using System;
using BlogMvc.Areas.Admin.Models;
using Microsoft.Extensions.Options;
using BlogEntities.Concrete;
using BlogMvc.Attributes;
using BlogData.Abstract;

namespace BlogMvc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;

        public ArticleController(IArticleService articleService, IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions)
        {
            _articleService = articleService;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var searchResult = await _articleService.SearchAsync(keyword,currentPage,pageSize,isAscending);
            if(searchResult.ResultStates == ResultStates.Success)
            {
                return View(new ArticleSearchViewModel
                {
                    ArticleListDto = searchResult.Data,
                    Keyword = keyword,
                });
            }
            return NotFound();
        }

        [HttpGet]
        [ViewCountFilterAttiribute]
        public async Task<IActionResult> Detail(int id)
        {
            var articleResult = await _articleService.GetAsync(id);
            if (articleResult.ResultStates == ResultStates.Success)
            {
                var userArticles = await _articleService.GetAllByUserIdOnFilter(articleResult.Data.Article.UserId,
                _articleRightSideBarWidgetOptions.FilterBy,
                _articleRightSideBarWidgetOptions.OrderBy, _articleRightSideBarWidgetOptions.IsAscending,
                _articleRightSideBarWidgetOptions.TakeSize, _articleRightSideBarWidgetOptions.CategoryId,
                _articleRightSideBarWidgetOptions.StartAt, _articleRightSideBarWidgetOptions.EndAt,
                _articleRightSideBarWidgetOptions.MinViewCount, _articleRightSideBarWidgetOptions.MaxViewCount,
                _articleRightSideBarWidgetOptions.MinCommentCount, _articleRightSideBarWidgetOptions.MaxCommentCount);
                //await _articleService.IncreaseViewCountAsync(id);
                return View(new ArticleDetailViewModel
                {
                    ArticleDto = articleResult.Data,
                    ArticleDetailRightSidebarViewModel = new ArticleDetailRightSidebarViewModel
                    {
                        ArticleListDto = userArticles.Data,
                        Header = _articleRightSideBarWidgetOptions.Header,
                        User = articleResult.Data.Article.User
                    }
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
