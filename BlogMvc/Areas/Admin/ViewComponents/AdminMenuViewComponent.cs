using BlogEntities.Concrete;
using BlogMvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogMvc.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return Content("Kullanıcı Bulunamadı");
            }
            if (roles == null)
            {
                return Content("Roller Bulunamadı");
            }
            return View(new UserWithRolesViewModel
            {
                Roles = roles,
                User = user,
            });
        }
    }
}
