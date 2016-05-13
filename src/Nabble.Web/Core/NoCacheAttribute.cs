using Microsoft.AspNet.Mvc.Filters;

namespace Nabble.Web.Core
{
	public class NoCacheAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);

			context.HttpContext.Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
			context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
			context.HttpContext.Response.Headers.Add("Expires", "-1");
			context.HttpContext.Response.Headers.Add("Surrogate-Control", "no-store");
		}
	}
}