using Clients.API.DTOs;

namespace Clients.API.Interfaces
{
    public interface ClientAPIInterface
    {
        Task<string> RequestCoupon(ClientAPIDTO clientAPIDTO);
    }
}
