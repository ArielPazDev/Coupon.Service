using Clientes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Clientes.API.Contexts
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ClienteModel> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteModel>().HasKey(cliente => cliente.CodCliente);

            base.OnModelCreating(modelBuilder);
        }
    }
}
