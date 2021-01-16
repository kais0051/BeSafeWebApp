using BeSafeWebApp.BLL;
using BeSafeWebApp.Common;
using BeSafeWebApp.Contracts.Configurations;
using BeSafeWebApp.Contracts.Interfaces;
using BeSafeWebApp.DLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSafeWebApp
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Also make top level configuration available (for EF configuration and access to connection string)
            services.AddSingleton(Configuration); //IConfigurationRoot
            services.AddSingleton<IConfiguration>(Configuration);

            //Add Support for strongly typed Configuration and map to class
            services.AddOptions();
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            //Set database.
            if (Configuration["AppConfig:UseInMemoryDatabase"] == "true")
            {
                services.AddDbContext<BeSafeContext>(opt => opt.UseInMemoryDatabase("BeSafeMemory"));
            }
            else
            {
                services.AddDbContext<BeSafeContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("SQLServerconnectionString")));
            }

            //Cors policy is added to controllers via [EnableCors("CorsPolicy")]
            //or .UseCors("CorsPolicy") globally
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials() //Core 3.0 removed.
                        );
            });

            //Instance injection
            services.AddScoped(typeof(IAutoMapConverter<,>), typeof(AutoMapConverter<,>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryBusinessLogic, CategoryBusinessLogic>();

            services.AddScoped<IMasterItemRepository, MasterItemRepository>();
            services.AddScoped<IMasterItemBusinessLogic, MasterItemBusinessLogic>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
