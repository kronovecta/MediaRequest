using MediaRequest.Application;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Queries;
using MediaRequest.Data;
using MediaRequest.Domain.Configuration;
using MediaRequest.WebUI.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MediaRequest
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile($"settings.json", false, true)
                .AddYamlFile("settings.yaml", false, true)
                .AddEnvironmentVariables(prefix: "Apollo_")
                .AddUserSecrets<Startup>();

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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();

            #region Service Classes
            services.AddScoped<IHttpHelper, HttpHelper>();
            services.AddScoped<DataInitializer>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            #endregion

            #region Data Protection
            services.AddDataProtection();
            #endregion

            #region Configuration classes
            services.Configure<ApiKeys>(Configuration.GetSection("ApiKeys"));
            services.Configure<ServicePath>(Configuration.GetSection("Path"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            #endregion

            #region HTTP Clients
            services.AddHttpClient<RadarrClient>(client => client.BaseAddress = new Uri(Configuration.GetSection("Path:Radarr").Value));
            services.AddHttpClient<TMDBClient>(client => client.BaseAddress = new Uri(Configuration.GetSection("Path:TMDB").Value));
            
            services.AddHttpClient<SonarrClient>(client => {

                client.BaseAddress = new Uri(Configuration.GetSection("Path:Sonarr").Value);
                client.DefaultRequestHeaders.Add("X-Api-Key", Configuration.GetSection("ApiKeys:Sonarr").Value);
            });
            #endregion

            #region MediatR
            services.AddMediatR(
                typeof(GetSingleMovieHandler).Assembly,
                typeof(AddRequestHandler).Assembly);
            #endregion

            #region Cache
            services.AddMemoryCache();
            services.AddResponseCaching();
            #endregion

            #region Contexts
            services.AddDbContext<IMediaDbContext, MediaDbContext>(opt => opt.UseSqlite(conn, x => x.MigrationsAssembly("MediaRequest.WebUI")));
            services.AddDbContext<IdentityContext>(opt => opt.UseSqlite(conn, x => x.MigrationsAssembly("MediaRequest.WebUI")));
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(opt => 
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;

                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityContext>();
            #endregion

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMediaDbContext context, IdentityContext identityContext, DataInitializer initializer)
        {
            var cx = context as MediaDbContext;
            if (!cx.Database.CanConnect())
            {
                await cx.Database.MigrateAsync();
                await identityContext.Database.MigrateAsync();
                await initializer.SeedData();
            }

            await initializer.SetConfigValues();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 2
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseResponseCaching();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("ShowMovie", "{{action}}/{{slug}}", new { controller = "Home" });
                endpoints.MapControllerRoute("ShowSeries", "{{action}}/{{slug}}", new { controller = "Television" });

                endpoints.MapControllerRoute("Search", "{{action}}/{term?}", new { controller = "Home" });
                endpoints.MapControllerRoute("default", "{{action}}", new { controller = "Home" });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
