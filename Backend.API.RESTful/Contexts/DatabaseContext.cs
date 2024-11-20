using Backend.API.RESTful.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.RESTful.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ArticuloModel> Articulos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<Cupon_CategoriaModel> Cupones_Categorias { get; set; }
        public DbSet<Cupon_ClienteModel> Cupones_Clientes { get; set; }
        public DbSet<CuponDetalleModel> Cupones_Detalle { get; set; }
        public DbSet<CuponHistorialModel> Cupones_Historial { get; set; }
        public DbSet<CuponModel> Cupones { get; set; }
        public DbSet<PrecioModel> Precios { get; set; }
        public DbSet<Tipo_CuponModel> Tipo_Cupon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloModel>().HasKey(c => c.Id_Articulo);
            modelBuilder.Entity<CategoriaModel>().HasKey(c => c.Id_Categoria);
            modelBuilder.Entity<Cupon_CategoriaModel>().HasKey(c => c.Id_Cupones_Categorias);
            modelBuilder.Entity<Cupon_ClienteModel>().HasKey(c => c.Id_Cupon);
            modelBuilder.Entity<CuponDetalleModel>().HasKey(c => c.Id_Cupon);
            modelBuilder.Entity<CuponHistorialModel>().HasKey(c => c.Id_Cupon);
            modelBuilder.Entity<CuponModel>().HasKey(c => c.Id_Cupon);
            modelBuilder.Entity<PrecioModel>().HasKey(c => c.Id_Precio);
            modelBuilder.Entity<Tipo_CuponModel>().HasKey(c => c.Id_Tipo_Cupon);

            base.OnModelCreating(modelBuilder);
        }
    }
}
