using Clientes.API.DTOs;

namespace Clientes.API.Interfaces
{
    public interface IClienteService
    {
        Task<string> SolicitarCupon(ClienteDTO clienteDTO);
    }
}
