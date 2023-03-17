using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Controllers
{
	public class DefaultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
