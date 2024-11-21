using Backend.API.RESTful.Contexts;
using Backend.API.RESTful.DTOs;
using Backend.API.RESTful.Interfaces;
using Backend.API.RESTful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly ICuponesService _cuponesService;
        private readonly ISendEmailService _sendEmailService;

        public SolicitudCuponesController(DatabaseContext db, ICuponesService cuponesService, ISendEmailService sendEmailService)
        {
            _db = db;
            _cuponesService = cuponesService;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("SolicitarCupon")]
        public async Task<IActionResult> SolicitarCupon(ClienteDTO clienteDTO)
        {
            try
            {
                if (clienteDTO.CodCliente.IsNullOrEmpty())
                    throw new Exception("El CodCliente no puede estar vacio");

                string nroCupon = await _cuponesService.GenerarNroCupon();

                Cupon_ClienteModel cupon_ClienteModel = new Cupon_ClienteModel();
                cupon_ClienteModel.Id_Cupon = clienteDTO.Id_Cupon;
                cupon_ClienteModel.NroCupon = nroCupon;
                cupon_ClienteModel.FechaAsignado = DateOnly.FromDateTime(DateTime.Now);
                cupon_ClienteModel.CodCliente = clienteDTO.CodCliente;

                _db.Cupones_Clientes.Add(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                await _sendEmailService.EnviarEmailCliente(clienteDTO.Email, nroCupon);

                return Ok(new
                {
                    Mensaje = "Se dio de alta el registro",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /*[HttpPost("UsarCupon")]
        public async Task<IActionResult> UsarCupon(string nroCupon, string codCliente, string emailCliente)
        {
        }*/
    }
}
