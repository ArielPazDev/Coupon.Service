using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Historial")]
    public class CuponHistorialModel
    {
        [Key]
        public int id_Cupon { get; set; }
        public string NroCupon { get; set; }
        public DateOnly FechaUso { get; set; }
        public string CodCliente {  get; set; }
    }
}
