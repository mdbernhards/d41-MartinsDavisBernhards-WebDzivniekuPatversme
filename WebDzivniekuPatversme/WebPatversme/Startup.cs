using WebPatversme.Data;
using WebDzivniekuPatversme.Services;
using WebDzivniekuPatversme.Repository;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebPatversme
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
            services.Add(new ServiceDescriptor(typeof(WebShelterDbContext), new WebShelterDbContext(Configuration.GetConnectionString("ShelterConnection"))));

            services.AddScoped<IAnimalsServices, AnimalsServices>();
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();

            services.AddScoped<IContactsServices, ContactsServices>();
            services.AddScoped<IContactsRepository, ContactsRepository>();

            services.AddScoped<INewsServices, NewsServices>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<IShelterServices, ShelterServices>();
            services.AddScoped<IShelterRepository, ShelterRepository>();

            services.AddScoped<IHomeServices, HomeServices>();
            services.AddScoped<IHomeRepository, HomeRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("ShelterConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}