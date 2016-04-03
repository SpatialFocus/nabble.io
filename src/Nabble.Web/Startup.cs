namespace Nabble.Web
{
	using Microsoft.AspNet.Builder;
	using Microsoft.AspNet.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.
			IConfigurationBuilder builder =
				new ConfigurationBuilder().AddJsonFile("appsettings.json")
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseIISPlatformHandler();

			app.UseMvc(
				r =>
				{
					r.MapRoute(name: "default", template: "{controller}/{action}/{id?}",
						defaults: new { controller = "Home", action = "Index" });
				});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}
	}
}