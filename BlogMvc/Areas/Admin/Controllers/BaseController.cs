using AutoMapper;
using BlogEntities.Concrete;
using BlogMvc.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper;
        }

        protected UserManager<User> UserManager { get; }

        protected IMapper Mapper { get; }

        protected IImageHelper ImageHelper { get; }

        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;



    }
}
