﻿using BlogEntities.Dtos;

namespace BlogMvc.Models
{
    public class ArticleSearchViewModel
    {
        public ArticleListDto ArticleListDto { get; set; }

        public string Keyword { get; set; }
    }
}
