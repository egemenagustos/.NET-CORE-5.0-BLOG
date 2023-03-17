using BlogEntities.Concrete;
using BlogEntities.Dtos;

namespace BlogMvc.Models
{
    public class ArticleDetailRightSidebarViewModel
    {
        public string Header { get; set; }

        public ArticleListDto ArticleListDto { get; set; }

        public User User { get; set; }
    }
}
