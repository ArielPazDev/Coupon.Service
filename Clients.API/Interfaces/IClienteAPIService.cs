using Clients.API.DTOs;

namespace Clients.API.Interfaces
{
    public interface IClienteAPIService
    {
        Task<string> RequestCoupon(ClienteAPIDTO clientAPIDTO);
    }
}
