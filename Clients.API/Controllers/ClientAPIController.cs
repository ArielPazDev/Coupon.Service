using Azure.Core;
using Clients.API.DTOs;
using Clients.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Clients.API.Controllers
{
    [Route("coupon/request")]
    [ApiController]
    public class ClientAPIController : ControllerBase
    {
        private readonly ClientAPIInterface _clientAPI;

        public ClientAPIController(ClientAPIInterface clientAPI)
        {
            _clientAPI = clientAPI;
        }

        // POST: coupon/request
        [HttpPost]
        public async Task<IActionResult> SendRequestCoupon([FromBody] ClientAPIDTO clientAPIDTO)
        {
            try
            {
                var response = await _clientAPI.RequestCoupon(clientAPIDTO);

                // Log
                Log.Information("Endpoint access POST: coupon/request");

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: coupon/request ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }
    }
}
