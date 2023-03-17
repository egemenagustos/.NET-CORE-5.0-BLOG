using BlogEntities.Dtos;

namespace BlogMvc.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto UserAddDto { get; set; }

        public string UserAddPartial { get; set; }

        public UserDto UserDto { get; set; }
    }
}
