namespace Nabble.Web.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using Nabble.Core;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;

	public class HomeController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Test()
		{
			IAnalyzerResultAccessor appVeyorAnalyzerResultAccessor = Factory.CreateAppVeyorAnalyzerResultAccessor(
				new[] { "SA" },
				"Dresel",
				"SampleProject",
				"report.json",
				string.Empty);

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(new BadgeBuilderProperties(), appVeyorAnalyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}
	}
}