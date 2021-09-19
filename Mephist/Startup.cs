using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Data;
using Mephist.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using System.IO;
using AspNet.Security.OAuth.Vkontakte;
using Mephist.Services.DAL;
using Microsoft.AspNetCore.HttpOverrides;

namespace Mephist
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment{ get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<UniversityContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<UnitOfWork>();
            //services.AddTransient<IUniversityRepository, UniversityRepository>();
            if (Environment.IsDevelopment())
                services.AddDbContext<UniversityContext>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("VPS")));
            else
                services.AddDbContext<UniversityContext>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("MephistDB")));



            services.Configure<IdentityOptions>(option => { });

            services.ConfigureApplicationCookie(options => { });

            services.AddAuthentication()
                .AddVkontakte(options =>
                {
                    options.ClientId = "7779983";
                    options.ClientSecret = "y2eTDQ0zta2XYqDhdGx1";
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<UniversityStaticData>();
            services.AddScoped<EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {

            }
            else
            {
                /*
                app.UseHsts();
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
                */
            }

            //app.UseHttpsRedirection();
            

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

    }
}
