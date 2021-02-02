using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDzivniekuPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebDzivniekuPatversme.Models.ViewModels.NewsViewModel> NewsViewModel { get; set; }
    }
}