namespace Nabble.Web
{
	using System.IO;
	using Microsoft.AspNet.Builder;
	using Microsoft.AspNet.Hosting;
	using Microsoft.AspNet.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Serilog;
	using Serilog.Sinks.RollingFile;

	public class Startup
	{
		private IHostingEnvironment Environment;

		public Startup(IHostingEnvironment env)
		{
			this.Environment = env;

			// Set up configuration sources.
			IConfigurationBuilder builder =
				new ConfigurationBuilder().AddJsonFile("appsettings.json")
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();

			// Maybe useful in Production
			////Configuration = builder.Build().ReloadOnChanged("appsettings.json");
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			loggerFactory.AddSerilog(
				new LoggerConfiguration().MinimumLevel.Debug()
					.WriteTo.RollingFile(Path.Combine(env.WebRootPath, "..\\logs\\log-{Date}.txt"))
					.CreateLogger());

			app.UseStaticFiles();

			app.UseIISPlatformHandler();

			app.UseMvc(
				r =>
				{
					r.MapRoute(
						name: "default",
						template: "{controller}/{action}/{id?}",
						defaults: new { controller = "Home", action = "Index" });
				});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			if (!this.Environment.IsDevelopment())
			{
				services.AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()); });
			}
		}
	}
}