using BlogEntities.Dtos;

namespace BlogMvc.Models
{
    public class ArticleDetailViewModel
    {
        public ArticleDto ArticleDto { get; set; }

        public ArticleDetailRightSidebarViewModel ArticleDetailRightSidebarViewModel { get; set; }
    }
}
