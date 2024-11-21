using Clients.API.DTOs;

namespace Clients.API.Interfaces
{
    public interface ClienteAPIInterface
    {
        Task<string> RequestCoupon(ClienteAPIDTO clientAPIDTO);
    }
}
