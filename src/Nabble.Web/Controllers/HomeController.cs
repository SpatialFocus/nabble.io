namespace Nabble.Web.Controllers
{
	using Microsoft.AspNet.Mvc;

	public class HomeController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
	}
}