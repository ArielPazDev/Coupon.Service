using Backend.API.RESTful.Contexts;
using Backend.API.RESTful.DTOs;
using Backend.API.RESTful.Interfaces;
using Backend.API.RESTful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Backend.API.RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly ICuponesService _cuponesService;
        private readonly IMailerService _mailerService;

        public SolicitudCuponesController(DatabaseContext db, ICuponesService cuponesService, IMailerService mailerService)
        {
            _db = db;
            _cuponesService = cuponesService;
            _mailerService = mailerService;
        }

        [HttpPost("SolicitudCupon")]
        public async Task<IActionResult> SolicitudCupon(ClienteDTO clienteDTO)
        {
            try
            {
                if (clienteDTO.CodCliente.IsNullOrEmpty())
                    throw new Exception("El CodCliente no puede estar vacio");

                string nroCupon = await _cuponesService.GenerarNroCupon();

                Cupon_ClienteModel cupon_ClienteModel = new Cupon_ClienteModel();
                cupon_ClienteModel.Id_Cupon = clienteDTO.Id_Cupon;
                cupon_ClienteModel.NroCupon = nroCupon;
                cupon_ClienteModel.FechaAsignado = DateTime.Now;
                cupon_ClienteModel.CodCliente = clienteDTO.CodCliente;

                _db.Cupones_Clientes.Add(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                if (!clienteDTO.CodCliente.IsNullOrEmpty())
                    await _mailerService.SendEmail(clienteDTO.Email, nroCupon);

                return Ok(new
                {
                    Mensaje = "El cupón se dio de alta",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("QuemadoCupon")]
        public async Task<IActionResult> QuemadoCupon(string nroCupon)
        {
            try
            {
                var cupon_ClienteModel = await _db.Cupones_Clientes.FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cupon_ClienteModel == null)
                {
                    // Log
                    //Log.Information($"Endpoint access GET: api/cupones/clientes/{id} (not found)");

                    return NotFound();
                }

                Cupones_HistorialModel cupones_HistorialModel = new Cupones_HistorialModel();
                cupones_HistorialModel.Id_Cupon = cupon_ClienteModel.Id_Cupon;
                cupones_HistorialModel.NroCupon = nroCupon;
                cupones_HistorialModel.FechaUso = DateTime.Now;
                cupones_HistorialModel.CodCliente = cupon_ClienteModel.CodCliente;

                _db.Cupones_Historial.Add(cupones_HistorialModel);
                await _db.SaveChangesAsync();

                _db.Cupones_Clientes.Remove(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    Mensaje = "El cupón se quemó",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
