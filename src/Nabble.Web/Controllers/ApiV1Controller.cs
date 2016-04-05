namespace Nabble.Web.Controllers
{
	using System.IO;
	using Microsoft.AspNet.Hosting;
	using Microsoft.AspNet.Mvc;
	using Nabble.Web.Models;

	[Route("api/v1")]
	public class ApiV1Controller : Controller
	{
		private readonly IHostingEnvironment environment;

		public ApiV1Controller(IHostingEnvironment environment)
		{
			this.environment = environment;
		}

		// GET: api/v1
		[HttpGet]
		public IActionResult Get()
		{
			return HttpBadRequest("Usage: /api/v1/{vendor}/{account}/{project}/{branch (opt)}/{analyzer}?{parameters}");
		}

		// GET api/v1/appveyor/pergerch/demoproject1/development/fxcop
		[HttpGet("{vendor}/{account}/{project}/{branch}/{analyzer}")]
		[HttpGet("{vendor}/{account}/{project}/{analyzer}")]
		public IActionResult Get(VendorEnum vendor, string account, string project, AnalyzerEnum analyzer,
			string branch = null, [FromQuery] string parameters = null)
		{
			if (!ModelState.IsValid)
			{
				return HttpBadRequest(ModelState);
			}

			// TODO: All the magic to map parameters to CORE functions that returns the local path to the generated/cached file (svg, png, ...)
			string localpath = "images/favicon/favicon-96x96.png";

			// Write image to response stream: http://stackoverflow.com/questions/5629251/c-sharp-outputting-image-to-response-output-stream-giving-gdi-error
			// Or maybe: https://jamessdixon.wordpress.com/2013/10/01/handling-images-in-webapi/
			string fullpath = Path.Combine(this.environment.WebRootPath, localpath);

			Stream badgeStream = new FileStream(fullpath, FileMode.Open);

			return new FileStreamResult(badgeStream, "image/png");
		}
	}
}