using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MediaRequest.Application;
using MediaRequest.Data;
using MediatR;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Commands;
using MediaRequest.Domain.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediaRequest.WebUI.Models.IdentityModels;
//using MediaRequest.WebUI.Models.Configuration;

namespace MediaRequest
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddUserSecrets<Startup>();

            //builder.AddUserSecrets("16420ac2-2938-40e6-ade0-e700111f68a3");

            //if (env.IsDevelopment())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            Configuration = builder.Build();

            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("conn");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<IHttpHelper, HttpHelper>();
            services.AddSession(opt => opt.Cookie.IsEssential = true);

            services.Configure<ApiKeys>(Configuration.GetSection("ApiKeys"));
            services.Configure<ServicePath>(Configuration.GetSection("Path"));

            services.AddMediatR(
                typeof(GetSingleMovieHandler).Assembly,
                typeof(AddRequestHandler).Assembly);

            //services.AddDbContext<IMediaDbContext, MediaDbContext>(opt => opt.UseSqlServer(conn));
            //services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(conn));

            services.AddDbContext<IMediaDbContext, MediaDbContext>(opt => opt.UseSqlite(conn));
            services.AddDbContext<IdentityContext>(opt => opt.UseSqlite(conn));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                new DataInitializer(Configuration).SeedData(userManager, roleManager);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Search", "{action}/{term?}", new { controller = "Home" });

                //routes.MapRoute("Movies", "{action}", new { controller = "Movie" });
                routes.MapRoute("ShowMovie", "{action}/{slug}", new { controller = "Movie" });

                routes.MapRoute(
                    name: "Short",
                    template: "{action}",
                    defaults: new { controller = "Home" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
                    //template: "{controller=Movie}/{action=ShowMovie}/{id?}");
            });
        }
    }
}
