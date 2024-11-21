using Azure.Core;
using Clients.API.DTOs;
using Clients.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Clients.API.Controllers
{
    [Route("api/cupones/solicitar")]
    [ApiController]
    public class ClienteAPIController : ControllerBase
    {
        private readonly ClienteAPIInterface _clientAPI;

        public ClienteAPIController(ClienteAPIInterface clientAPI)
        {
            _clientAPI = clientAPI;
        }

        // POST: api/cupones/solicitar
        [HttpPost]
        public async Task<IActionResult> SendRequestCoupon([FromBody] ClienteAPIDTO clientAPIDTO)
        {
            try
            {
                var response = await _clientAPI.RequestCoupon(clientAPIDTO);

                // Log
                Log.Information("Endpoint access POST: api/cupones/solicitar");

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: api/cupones/solicitar ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }
    }
}
