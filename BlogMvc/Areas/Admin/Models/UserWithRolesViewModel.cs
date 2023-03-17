using BlogEntities.Concrete;
using System.Collections.Generic;

namespace BlogMvc.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }

        public IList<string> Roles { get; set; }
    }
}
