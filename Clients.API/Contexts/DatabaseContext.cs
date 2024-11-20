using Clients.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Clients.API.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ClientModel> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientModel>().HasKey(c => c.CodCliente);

            base.OnModelCreating(modelBuilder);
        }
    }
}
