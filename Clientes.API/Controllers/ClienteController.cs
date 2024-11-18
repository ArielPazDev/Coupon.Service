using Clientes.API.DTOs;
using Clientes.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Clientes.API.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // POST: api/cliente
        [HttpPost]
        public async Task<IActionResult> EnviarSolicitudCupons([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var respuesta = await _clienteService.SolicitarCupon(clienteDTO);

                // Log
                Log.Information("Endpoint access POST: api/cliente");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error($"Endpoint access POST: api/cliente ({ex.Message})");

                return BadRequest(ex.Message);
            }
        }
    }
}
