﻿using AutoMapper;
using BlogData.Concrete.EntitiyFramework.Context;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogMvc.Areas.Admin.Models;
using BlogMvc.Helpers.Abstract;
using BlogServices.Abstract;
using BlogServices.Concrete;
using BlogShared.Utilities.Extensions;
using BlogShared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper ) : base(userManager, mapper, imageHelper)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            return View(result.Data);
        }

        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }

        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(categoryAddDto, LoggedInUser.UserName);
                if (result.ResultStates == ResultStates.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                    });
                    return Json(categoryAddAjaxModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
            });
            return Json(categoryAddAjaxErrorModel);
        }

        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetCategoryUpdateDtoAsync(id);
            if(result.ResultStates == ResultStates.Success)
            {
                return PartialView("_CategoryUpdatePartial",result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(categoryUpdateDto, LoggedInUser.UserName);
                if (result.ResultStates == ResultStates.Success)
                {
                    var categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
                    });
                    return Json(categoryUpdateAjaxModel);
                }
            }
            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
            });
            return Json(categoryUpdateAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [Authorize(Roles = "SuperAdmin,Category.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id, LoggedInUser.UserName);
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }

        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            return View(result.Data);
        }

        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int id)
        {
            var result = await _categoryService.UndoDeleteAsync(id, LoggedInUser.UserName);
            var undoDeleteCategory = JsonSerializer.Serialize(result.Data);
            return Json(undoDeleteCategory);
        }

        [Authorize(Roles = "SuperAdmin,Category.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int id)
        {
            var result = await _categoryService.HardDeleteAsync(id);
            var deletedCategory = JsonSerializer.Serialize(result);
            return Json(deletedCategory);
        }
    }
}
