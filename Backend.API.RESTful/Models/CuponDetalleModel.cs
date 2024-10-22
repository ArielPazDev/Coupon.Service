using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Detalle")]
    public class CuponDetalleModel
    {
        [Key]
        public int id_Cupon { get; set; }
        public int id_Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
