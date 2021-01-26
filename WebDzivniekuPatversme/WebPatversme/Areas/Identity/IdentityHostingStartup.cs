using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebDzivniekuPatversme.Areas.Identity.IdentityHostingStartup))]
namespace WebDzivniekuPatversme.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}