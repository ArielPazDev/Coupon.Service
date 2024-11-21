using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Detalle")]
    public class Cupones_DetalleModel
    {
        [Key]
        public int Id_Cupon { get; set; }
        [Key]
        public int Id_Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
