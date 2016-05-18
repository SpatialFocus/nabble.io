namespace Nabble.Web.Controllers
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using Microsoft.Data.Entity;
	using Microsoft.Data.Entity.Storage;
	using Microsoft.Extensions.Logging;
	using Nabble.Core;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Data;
	using Nabble.Core.Exceptions;
	using Nabble.Web.Core;
	using Nabble.Web.Models;

	[Route("api/v1")]
	[NoCache]
	public class ApiV1Controller : Controller
	{
		public ApiV1Controller(ILogger<ApiV1Controller> logger)
		{
			Logger = logger;
		}

		public ILogger<ApiV1Controller> Logger { get; set; }

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

			Badge badge = null;

			NabbleUnitOfWork nabbleUnitOfWork = new NabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);
			IAnalyzerResultAccessor analyzerResultAccessor;

			switch (vendor)
			{
				case VendorEnum.AppVeyor:

					analyzerResultAccessor = Factory.CreateAppVeyorAnalyzerResultAccessor(
						analyzerRules,
						account,
						project,
						branch,
						"report.json",
						statisticsService);
					break;

				default:
					return HttpBadRequest("Selected vendor is not supported.");
			}

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();

			for (int i = 0; i < 3; i++)
			{
				try
				{
					using (IRelationalTransaction beginTransactionAsync = await nabbleUnitOfWork.BeginTransactionAsync())
					{
						badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);
						await statisticsService.AddRequestEntryAsync();

						beginTransactionAsync.Commit();
					}

					break;
				}
				catch (DbUpdateException exception)
				{
					Logger.LogError("Badge building errored.", exception);

					// Retry for database exceptions, like locked, unique constraint exceptions, etc.
					nabbleUnitOfWork.RevertChanges();

					await Task.Delay(100);
				}
				catch (BuildPendingException)
				{
					// Break on pending builds but generate pending badge
					badge = await badgeBuilder.BuildErrorBadgeAsync(properties, properties.StatusTemplatePending);
					break;
				}
				catch (Exception exception)
				{
					// Break on all other types of errors
					Logger.LogError("Badge building errored.", exception);
					break;
				}
			}

			if (badge == null)
			{
				badge = await badgeBuilder.BuildErrorBadgeAsync(properties, properties.StatusTemplateInaccessible);
			}

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("statistics")]
		public async Task<StatisticsViewModel> Statistics()
		{
			StatisticsService statisticsService = new StatisticsService(new NabbleUnitOfWork());

			// TODO: Consistent naming of statistical values
			StatisticsViewModel viewModel = new StatisticsViewModel
			{
				Projects = await statisticsService.GetTotalProjectEntriesAsync(),
				Builds = await statisticsService.GetTotalBadgeEntriesAsync(),
				Requests = await statisticsService.GetTotalRequestEntriesAsync()
			};

			return viewModel;
		}
	}
}