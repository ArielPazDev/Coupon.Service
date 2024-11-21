using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones")]
    public class CuponModel
    {
        [Key]
        public int Id_Cupon { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeDto { get; set; }
        public decimal ImportePromo { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int Id_Tipo_Cupon { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Cupon_CategoriaModel>? Cupones_Categorias { get; set; }

        [ForeignKey("Id_Tipo_Cupon")]
        public virtual Tipo_CuponModel? Tipo_Cupon { get; set; }
    }
}
