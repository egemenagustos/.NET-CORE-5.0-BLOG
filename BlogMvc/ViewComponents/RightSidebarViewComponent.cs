using BlogMvc.Models;
using BlogServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogMvc.ViewComponents
{
    public class RightSidebarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public RightSidebarViewComponent(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();

            var articleResult = await _articleService.GetAllByViewCountAsync(isAscending:false, takeSize:5);

            return View(new RightSidebarViewModel
            {
                Categories = categoriesResult.Data.Categories,
                Articles = articleResult.Data.Articles,
            });
        }
    }
}
