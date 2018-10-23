using System.Threading.Tasks;
using ElectronNET.API;
using Entelect.TimesheetsToHollardJira.Domain.Configuration;
using Entelect.TimesheetsToHollardJira.Domain.Configuration.Interface;
using Entelect.TimesheetsToHollardJira.Integrate.Repository;
using Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entelect.TimesheetsToHollardJira.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IAppConfiguration config = Configuration.Get<AppConfiguration>();
            services.AddSingleton(config);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //IAppConfiguration config = Configuration.Get<AppConfiguration>();

            IHttpRepository httpRepo = new HttpRepository();
            IHollardJiraRepository hollardJiraRepository = new HollardJiraRepository(httpRepo, config.HollardUsername, config.HollardPassword, config.HollardRemainingEstimate);
            IEntelectTimesheetRepository entelectTimesheetRepository = new EntelectTimesheetRepository(httpRepo, config.EntelectUsername, config.EntelectPassword);

            services.AddTransient(construct => httpRepo);
            services.AddTransient(construct => hollardJiraRepository);
            services.AddTransient(construct => entelectTimesheetRepository);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Open the Electron-Window here
            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
        }
    }
}
