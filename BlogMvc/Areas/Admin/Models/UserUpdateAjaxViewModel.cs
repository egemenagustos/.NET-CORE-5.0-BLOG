using BlogEntities.Dtos;

namespace BlogMvc.Areas.Admin.Models
{
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }

        public string UserUpdatePartial { get; set; }

        public UserDto UserDto { get; set; }
    }
}
