// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Nabble.Web.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using Nabble.Core;
	using Nabble.Core.Builder;
	using Nabble.Core.Preview;
	using Nabble.Web.Models;

	[Route("api/v1/preview")]
	public class PreviewController : Controller
	{
		[HttpGet("aggregate/{analyzer}")]
		public async Task<IActionResult> GetAggregate(AnalyzerEnum analyzer,
			[FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			// Property is set through API parameter
			////properties.AggregateValues = true;

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumErrors = 1, NumWarnings = 1 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("error/{analyzer}")]
		public async Task<IActionResult> GetError(AnalyzerEnum analyzer, [FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumErrors = 1 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("inaccessible/{analyzer}")]
		public async Task<IActionResult> GetInaccessible(AnalyzerEnum analyzer,
			[FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { Inaccessible = true });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("info/{analyzer}")]
		public async Task<IActionResult> GetInfo(AnalyzerEnum analyzer, [FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			// Property is set through API parameter
			////properties.CountInfos = true;

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumInfos = 1 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("success/{analyzer}")]
		public async Task<IActionResult> GetSuccess(AnalyzerEnum analyzer,
			[FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			IAnalyzerResultAccessor analyzerResultAccessor = Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings());

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("warning/{analyzer}")]
		public async Task<IActionResult> GetWarning(AnalyzerEnum analyzer,
			[FromQuery] BadgeBuilderProperties properties = null)
		{
			properties = GetProperties(analyzer, properties);

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumWarnings = 1 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		private BadgeBuilderProperties GetProperties(AnalyzerEnum analyzer, BadgeBuilderProperties properties)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			switch (analyzer)
			{
				case AnalyzerEnum.StyleCop:
					properties.Label = "StyleCop";
					break;

				case AnalyzerEnum.FxCop:
					properties.Label = "FxCop";
					break;
			}

			if (properties.Format == "json")
			{
				properties.Format = "svg";
			}

			return properties;
		}
	}
}