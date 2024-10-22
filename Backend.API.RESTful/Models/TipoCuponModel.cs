using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Tipo_Cupon")]
    public class TipoCuponModel
    {
        [Key]
        public int Id_Tipo_Cupon { get; set; }
        public string Nombre {  get; set; }
    }
}
