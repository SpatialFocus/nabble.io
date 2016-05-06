namespace Nabble.Web.Controllers
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Hosting;
	using Microsoft.AspNet.Mvc;
	using Nabble.Core;
	using Nabble.Core.Builder;
	using Nabble.Web.Models;

	[Route("api/v1")]
	[ResponseCache(NoStore = true)]
	public class ApiV1Controller : Controller
	{
		// GET api/v1/appveyor/Dresel/SampleProject/StyleCop
		[HttpGet("{vendor}/{account}/{project}/{branch}/{analyzer}")]
		[HttpGet("{vendor}/{account}/{project}/{analyzer}")]
		public async Task<IActionResult> Get(VendorEnum vendor, string account, string project, AnalyzerEnum analyzer,
			string branch = null, [FromQuery] string rules = null, [FromQuery] BadgeBuilderProperties properties = null)
		{
			if (!ModelState.IsValid)
			{
				return HttpBadRequest(ModelState);
			}

			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			string[] analyzerRules;
			switch (analyzer)
			{
				case AnalyzerEnum.StyleCop:
					analyzerRules = new[] { "SA", "SX" };
					properties.Label = "StyleCop";
					break;

				case AnalyzerEnum.FxCop:
					analyzerRules = new[] { "CA", "RS", "Async" };
					properties.Label = "FxCop";
					break;

				case AnalyzerEnum.Custom:
					if (string.IsNullOrEmpty(rules))
					{
						analyzerRules = new[] { string.Empty };
					}
					else
					{
						analyzerRules = rules.Split(',');
					}

					break;

				default:
					return HttpBadRequest("Selected analyzer is not supported.");
			}

			IAnalyzerResultAccessor analyzerResultAccessor;
			switch (vendor)
			{
				case VendorEnum.AppVeyor:
					analyzerResultAccessor = Factory.CreateAppVeyorAnalyzerResultAccessor(analyzerRules, account, project, branch);
					break;

				default:
					return HttpBadRequest("Selected vendor is not supported.");
			}

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("statistics")]
		public async Task<StatisticsViewModel> Statistics()
		{
			Random random = new Random();
			StatisticsViewModel viewModel = new StatisticsViewModel
			{
				Projects = 111 + random.Next(0,100),
				Builds = 222 + random.Next(0, 100),
				Requests = 300 + random.Next(0, 100)
			};

			return await Task.Run(() => viewModel);
		}
	}
}