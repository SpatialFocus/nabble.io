namespace Nabble.Web.Controllers
{
	using Microsoft.AspNet.Mvc;

	[Route("api/v1")]
	public class ApiV1Controller : Controller
	{
		// GET: api/v1
		[HttpGet]
		public ActionResult Get()
		{
			return HttpBadRequest("Usage: /api/v1/{vendor}/{account}/{project}/{branch (opt)}/{analyzer}?{parameters}");
		}

		// GET api/v1/appveyor/cperger/demoproject1/development/fxcop
		[HttpGet("{vendor}/{account}/{project}/{branch}/{analyzer}")]
		[HttpGet("{vendor}/{account}/{project}/{analyzer}")]
		public string Get(string vendor, string account, string project, string analyzer, string branch = null,
			[FromQuery] string parameters = null)
		{
			// TODO: All the magic to map parameters to CORE functions

			// Write image to response stream: http://stackoverflow.com/questions/5629251/c-sharp-outputting-image-to-response-output-stream-giving-gdi-error
			////using (Bitmap image = new Bitmap(context.Server.MapPath("images/stars_5.png")))
			////{
			////	using (MemoryStream ms = new MemoryStream())
			////	{
			////		image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			////		context.Response.ContentType = "image/png";
			////		ms.WriteTo(context.Response.OutputStream);
			////	}
			////}

			// Or maybe: https://jamessdixon.wordpress.com/2013/10/01/handling-images-in-webapi/
			////public HttpResponseMessage Get(int id)
			////{
			////	var result = new HttpResponseMessage(HttpStatusCode.OK);
			////	String filePath = HostingEnvironment.MapPath("~/Images/HT.jpg");
			////	FileStream fileStream = new FileStream(filePath, FileMode.Open);
			////	Image image = Image.FromStream(fileStream);
			////	MemoryStream memoryStream = new MemoryStream();
			////	image.Save(memoryStream, ImageFormat.Jpeg);
			////	result.Content = new ByteArrayContent(memoryStream.ToArray());
			////	result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

			////	return result;
			////}

			return "value " + vendor;
		}
	}
}