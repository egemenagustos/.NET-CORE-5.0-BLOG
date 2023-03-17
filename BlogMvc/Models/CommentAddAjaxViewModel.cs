using BlogEntities.Dtos;

namespace BlogMvc.Models
{
    public class CommentAddAjaxViewModel
    {
        public CommentAddDto CommentAddDto { get; set; }

        public string CommentAddPartial { get; set; }

        public CommentDto CommentDto { get; set; }
    }
}
