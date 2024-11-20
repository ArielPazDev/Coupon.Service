using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Categorias")]
    public class CategoriaModel
    {
        [Key]
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cupon_CategoriaModel>? Cupones_Categorias { get; set; }
    }
}
