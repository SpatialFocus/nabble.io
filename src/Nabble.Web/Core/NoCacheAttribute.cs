using Microsoft.AspNet.Mvc.Filters;

namespace Nabble.Web.Core
{
	public class NoCacheAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);

			context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
			context.HttpContext.Response.Headers.Add("Expires", "-1");
		}
	}
}