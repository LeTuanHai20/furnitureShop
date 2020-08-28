using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application;
using Infrastructure.Common;
using Infrastructure.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Shop.Application;
using Web.Controllers;
using Web.Models;

namespace Web
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
            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddDbContext<ApplicationDBContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("NpgsqlConection"), b => b.MigrationsAssembly("DatabaseTools"))
            );
            ApplicationDBContext.ConfigureServices(services);

            services.AddDbContext<ShopDBContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("NpgsqlConection"), b => b.MigrationsAssembly("DatabaseTools"))
            );
            ShopDBContext.ConfigureServices(services);

            services.AddMemoryCache();
            services.AddScoped<ICacheBase, CacheBase>();
            services.AddScoped<UserInfoCache>();
            services.AddScoped<ConfigurationCache>();
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddScoped<AppSettings>();
            services.AddScoped<ShoppingCart>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConfigurationCache configurationCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            configurationCache.SetConfiguration();
        }
    }
}
