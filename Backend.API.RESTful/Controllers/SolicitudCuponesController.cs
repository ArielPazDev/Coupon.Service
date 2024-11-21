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
    [Route("api/cupones/solicitud")]
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

        // POST: api/cupones/solicitud/reclamar
        [HttpPost("reclamar")]
        public async Task<IActionResult> SolicitudCupon(ReclamarCuponDTO reclamarCuponDTO)
        {
            try
            {
                if (reclamarCuponDTO.CodCliente.IsNullOrEmpty())
                {
                    // Log
                    Log.Information("Endpoint access POST: api/cupones/solicitud/reclamar (CodCliente null)");

                    throw new Exception("El CodCliente no puede estar vacio");
                }

                string nroCupon = await _cuponesService.GenerarNroCupon();

                Cupon_ClienteModel cupon_ClienteModel = new Cupon_ClienteModel();
                cupon_ClienteModel.Id_Cupon = reclamarCuponDTO.Id_Cupon;
                cupon_ClienteModel.NroCupon = nroCupon;
                cupon_ClienteModel.FechaAsignado = DateTime.Now;
                cupon_ClienteModel.CodCliente = reclamarCuponDTO.CodCliente;

                _db.Cupones_Clientes.Add(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                if (!reclamarCuponDTO.CodCliente.IsNullOrEmpty())
                    await _mailerService.SendEmail(reclamarCuponDTO.Email, nroCupon);

                // Log
                Log.Information($"Endpoint access POST: api/cupones/solicitud/reclamar (NroCupon {nroCupon})");

                return Ok(new
                {
                    Mensaje = "Alta de cupón",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // POST: api/cupones/solicitud/usar
        [HttpPost("usar")]
        public async Task<IActionResult> QuemadoCupon(UsarCuponDTO usarCuponDTO)
        {
            try
            {
                var cupon_ClienteModel = await _db.Cupones_Clientes.FirstOrDefaultAsync(c => c.NroCupon == usarCuponDTO.NroCupon);

                if (cupon_ClienteModel == null)
                {
                    // Log
                    Log.Information("Endpoint access POST: api/cupones/solicitud/usar (not found)");

                    return NotFound();
                }

                Cupones_HistorialModel cupones_HistorialModel = new Cupones_HistorialModel();
                cupones_HistorialModel.Id_Cupon = cupon_ClienteModel.Id_Cupon;
                cupones_HistorialModel.NroCupon = usarCuponDTO.NroCupon;
                cupones_HistorialModel.FechaUso = DateTime.Now;
                cupones_HistorialModel.CodCliente = cupon_ClienteModel.CodCliente;

                _db.Cupones_Historial.Add(cupones_HistorialModel);
                await _db.SaveChangesAsync();

                _db.Cupones_Clientes.Remove(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/solicitud/usar");

                return Ok(new
                {
                    Mensaje = "El cupón se quemó",
                    NroCupon = usarCuponDTO.NroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // GET: api/cupones/solicitud/obtener
        [HttpGet("obtener/{codCliente}")]
        public async Task<ActionResult<IEnumerable<Cupon_ClienteModel>>> ObtenerClientes(string codCliente)
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/solicitud/obtener");

            return await _db.Cupones_Clientes.Where(c => c.CodCliente == codCliente).ToListAsync();
        }
    }
}
