using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Categorias")]
    public class CuponCategoriaModel
    {
        [Key]
        public int Id_Cupones_Categorias { get; set; }
        public int Id_Cupon { get; set; }
        public int Id_Categoria { get; set; }
    }
}
