using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services;
using WebDzivniekuPatversme.Repositories;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;
using AutoMapper;

namespace WebDzivniekuPatversme
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(WebShelterDbContext), new WebShelterDbContext(Configuration
                .GetConnectionString("ShelterConnection"))));

            services.AddScoped<IAnimalsService, AnimalsService>();
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();

            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<IShelterService, ShelterService>();
            services.AddScoped<IShelterRepository, ShelterRepository>();

            services.AddTransient<UserChecker>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("ShelterConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityErrorDescriberLV>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "619227943562-v605it2qhn6n15ggdsb4kt0tnfieii6d.apps.googleusercontent.com";
                    options.ClientSecret = "mIWytuZ8nrJ9wEp1-b2CwMVL";
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "1397549057297167";
                    facebookOptions.AppSecret = "e5364442a8ddf9231232730157d0ac66";
                })
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = "26422fa8-13d1-463a-8633-110c5c1c2e51";
                    microsoftOptions.ClientSecret = "UNWdsN-J96-xIiI_r3dIkvYqr-9t6~3Efm";
                });

            services.AddTransient<IEmailSender, EmailSender>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserChecker seeder)
        {
            seeder.RoleChecker();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Animals}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}