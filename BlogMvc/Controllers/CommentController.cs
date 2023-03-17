using BlogEntities.Dtos;
using BlogServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogShared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using BlogMvc.Models;
using BlogShared.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace BlogMvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<JsonResult> Add(CommentAddDto commentAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentService.AddAsync(commentAddDto);
                if (result.ResultStates == ResultStates.Success)
                {
                    var commentaddAjaxViewModel = JsonSerializer.Serialize(new CommentAddAjaxViewModel
                    {
                        CommentDto = result.Data,
                        CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto)
                    }, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return Json(commentaddAjaxViewModel);
                }
                ModelState.AddModelError("", result.Message);
            }
            var commentaddAjaxErrorModel = JsonSerializer.Serialize(new CommentAddAjaxViewModel
            {
                CommentAddDto = commentAddDto,
                CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto)
            });
            return Json(commentaddAjaxErrorModel);
        }
    }
}
