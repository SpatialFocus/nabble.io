namespace Nabble.Web
{
	using Microsoft.AspNet.Builder;
	using Microsoft.AspNet.Hosting;
	using Microsoft.Extensions.DependencyInjection;

	public class Startup
	{
		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			app.UseStaticFiles();

			app.UseMvc(
				r =>
				{
					r.MapRoute(name: "default", template: "{controller}/{action}/{id?}",
						defaults: new { controller = "Home", action = "Index" });
				});

			////app.UseIISPlatformHandler();

			////app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}
	}
}