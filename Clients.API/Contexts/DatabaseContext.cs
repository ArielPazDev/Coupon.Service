using Clients.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Clients.API.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ClienteModel> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteModel>().HasKey(c => c.CodCliente);

            base.OnModelCreating(modelBuilder);
        }
    }
}
