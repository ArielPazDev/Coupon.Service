using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones")]
    public class CuponModel
    {
        [Key]
        public int id_Cupon { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float PorcentajeDto { get; set; }
        public float ImportePromo { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int Id_Tipo_Cupon { get; set; }
        public bool Activo { get; set; }
    }
}
