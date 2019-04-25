using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gusto.Class;
using GustoLib.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Gusto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment environment)
        {
            var Builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true);
            this.Configuration = Builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Injection de dependance pour les controlleurs de Datas
            services.AddDbContext<GustoDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("GustoConnection"), b => b.MigrationsAssembly("Gusto")));

            //injection de dépendance pour Asp-Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GustoDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "catalogue de recettes",
            //        Description = "Fait en .Net Core",
            //        TermsOfService = "None",
            //        Contact = new Contact
            //        {
            //            Name = "toto",
            //            Email = "toto@ynov.com",
            //            Url = "http://www.google.fr"
            //        },
            //        License = new License
            //        {
            //            Name = "a pas",
            //            Url = "a pas"
            //        }
            //    });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    x.IncludeXmlComments(xmlPath);

            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public /*async*/ void Configure(IApplicationBuilder app, IHostingEnvironment env/*, RoleManager<IdentityRole> roleManager*/)
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

            app.UseAuthentication();
            //app.UseSwagger();
            //app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Recettes V1"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(ConfigureRoute);
            //problème de double utilisation du dbcontext
            //await InitializerRoles.Initial(roleManager);
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {

            routeBuilder.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            //équivalent à app.UseMvcWithDefaultRoute();
            routeBuilder.MapRoute(
                name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}
