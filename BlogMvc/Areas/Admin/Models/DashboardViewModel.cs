using BlogEntities.Concrete;
using BlogEntities.Dtos;
using System.Collections;
using System.Collections.Generic;

namespace BlogMvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }

        public int CommentsCount { get; set; }

        public int UsersCount { get; set; }

        public int ArticlesCount { get; set; }

        public ArticleListDto Articles { get; set; }
    }
}
