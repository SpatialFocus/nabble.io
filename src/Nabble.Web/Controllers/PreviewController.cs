// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Nabble.Web.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using Nabble.Core;
	using Nabble.Core.Builder;
	using Nabble.Core.Preview;

	[Route("api/v1/preview")]
	public class PreviewController : Controller
	{
		[HttpGet("aggregate")]
		public async Task<IActionResult> GetAggregate([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			properties.AggregateValues = true;

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumErrors = 3, NumWarnings = 5 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("error")]
		public async Task<IActionResult> GetError([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumErrors = 3 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("inaccessible")]
		public async Task<IActionResult> GetInaccessible([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { Inaccessible = true });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("info")]
		public async Task<IActionResult> GetInfo([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			properties.CountInfos = true;

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumInfos = 7 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("success")]
		public async Task<IActionResult> GetSuccess([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			IAnalyzerResultAccessor analyzerResultAccessor = Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings());

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}

		[HttpGet("warning")]
		public async Task<IActionResult> GetWarning([FromQuery] BadgeBuilderProperties properties = null)
		{
			if (properties == null)
			{
				properties = new BadgeBuilderProperties();
			}

			IAnalyzerResultAccessor analyzerResultAccessor =
				Factory.CreatePreviewAnalyzerResultAccessor(new PreviewSettings { NumWarnings = 5 });

			IBadgeBuilder badgeBuilder = Factory.CreateBadgeBuilder();
			Badge badge = await badgeBuilder.BuildBadgeAsync(properties, analyzerResultAccessor);

			return File(badge.Stream, badge.ContentType);
		}
	}
}