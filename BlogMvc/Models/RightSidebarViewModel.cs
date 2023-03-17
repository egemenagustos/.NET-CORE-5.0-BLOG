using BlogEntities.Concrete;
using System.Collections;
using System.Collections.Generic;

namespace BlogMvc.Models
{
    public class RightSidebarViewModel
    {
        public IList<Category> Categories { get; set; }

        public IList<Article> Articles { get; set; }
    }
}
