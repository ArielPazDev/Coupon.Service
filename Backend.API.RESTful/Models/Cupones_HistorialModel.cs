using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Historial")]
    public class Cupones_HistorialModel
    {
        [Key]
        public int Id_Cupon { get; set; }
        public string NroCupon { get; set; }
        public DateTime FechaUso { get; set; }
        public string CodCliente {  get; set; }
    }
}
