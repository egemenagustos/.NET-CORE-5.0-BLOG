using BlogEntities.Concrete;
using BlogMvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogMvc.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _usermanager;

        public UserMenuViewComponent(UserManager<User> usermanager)
        {
            _usermanager = usermanager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _usermanager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return Content("Kullanıcı Bulunamadı");
            }
            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}
