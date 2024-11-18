namespace Clientes.API.DTOs
{
    public class ClienteDTO
    {
        public int Id_Cupon { get; set; }
        public string NroCupon { get; set; }
        public DateTime FechaAsignado { get; set; }
        public string CodCliente { get; set; }
    }
}
