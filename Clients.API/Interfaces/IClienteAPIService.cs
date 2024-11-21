using Clients.API.DTOs;

namespace Clients.API.Interfaces
{
    public interface IClienteAPIService
    {
        Task<string> ReclamarCupon(ReclamarCuponDTO clientAPIDTO);
        Task<string> UsarCupon(UsarCuponDTO usarCuponDTO);
        Task<string> ObtenerCupones(ObtenerCuponesDTO obtenerCuponesDTO);
    }
}
