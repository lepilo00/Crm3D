using Crm3D.Models;
using Microsoft.EntityFrameworkCore;

namespace Crm3D.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Crm3D-test1;Trusted_Connection=True;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
    }
}
