using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clients.API.Models
{
    [Table("Clientes")]
    public class ClientModel
    {
        [Key]
        public string CodCliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido_Cliente { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }
}
