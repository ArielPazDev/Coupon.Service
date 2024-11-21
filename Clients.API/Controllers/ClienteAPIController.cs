using Azure.Core;
using Clients.API.DTOs;
using Clients.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Clients.API.Controllers
{
    [Route("api/cupones")]
    [ApiController]
    public class ClienteAPIController : ControllerBase
    {
        private readonly IClienteAPIService _clientAPI;

        public ClienteAPIController(IClienteAPIService clientAPI)
        {
            _clientAPI = clientAPI;
        }

        // POST: api/cupones/reclamar
        [HttpPost("reclamar")]
        public async Task<IActionResult> ReclamarCupon([FromBody] ReclamarCuponDTO clientAPIDTO)
        {
            try
            {
                var response = await _clientAPI.ReclamarCupon(clientAPIDTO);

                // Log
                Log.Information("Endpoint access POST: api/cupones/reclamar");

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: api/cupones/reclamar ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }

        // POST: api/cupones/usar
        [HttpPost("usar")]
        public async Task<IActionResult> UsarCupon([FromBody] UsarCuponDTO usarCuponDTO)
        {
            try
            {
                var response = await _clientAPI.UsarCupon(usarCuponDTO);

                // Log
                Log.Information("Endpoint access POST: api/cupones/usar");

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: api/cupones/usar ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }

        // POST: api/cupones/obtener
        [HttpPost("obtener")]
        public async Task<IActionResult> ObtenerCupones([FromBody] ObtenerCuponesDTO obtenerCuponesDTO)
        {
            try
            {
                var response = await _clientAPI.ObtenerCupones(obtenerCuponesDTO);

                // Log
                Log.Information("Endpoint access POST: api/cupones/obtener");

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: api/cupones/obtener ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }
    }
}
