using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.API.RESTful.Contexts;
using Backend.API.RESTful.Models;
using Serilog;

namespace Backend.API.RESTful.Controllers
{
    [Route("api/cupones/clientes")]
    [ApiController]
    public class Cupon_ClienteController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public Cupon_ClienteController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones/clientes
        [HttpPost]
        public async Task<ActionResult<Cupon_ClienteModel>> PostCupon_ClienteModel(Cupon_ClienteModel cupon_ClienteModel)
        {
            try
            {
                cupon_ClienteModel.NroCupon = "000-000-000";
                cupon_ClienteModel.FechaAsignado = DateOnly.FromDateTime(DateTime.Now);

                _db.Cupones_Clientes.Add(cupon_ClienteModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/clientes");
            }
            catch (DbUpdateException)
            {
                if (Cupon_ClienteModelExists(cupon_ClienteModel.NroCupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones/clientes (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupon_ClienteModel", new { id = cupon_ClienteModel.NroCupon }, cupon_ClienteModel);
        }

        // GET: api/cupones/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_ClienteModel>>> GetCupones_Clientes()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/clientes");

            return await _db.Cupones_Clientes.ToListAsync();
        }

        // GET: api/cupones/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon_ClienteModel>> GetCupon_ClienteModel(string id)
        {
            var cupon_ClienteModel = await _db.Cupones_Clientes.FindAsync(id);

            if (cupon_ClienteModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/clientes/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/clientes/{id}");

            return cupon_ClienteModel;
        }

        // PUT: api/cupones/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_ClienteModel(string id, Cupon_ClienteModel cupon_ClienteModel)
        {
            if (id != cupon_ClienteModel.NroCupon)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/clientes/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(cupon_ClienteModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/clientes/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_ClienteModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/clientes/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_ClienteModel(string id)
        {
            var cupon_ClienteModel = await _db.Cupones_Clientes.FindAsync(id);

            if (cupon_ClienteModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/clientes/{id} (not found)");

                return NotFound();
            }

            _db.Cupones_Clientes.Remove(cupon_ClienteModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/clientes/{id}");

            return NoContent();
        }

        private bool Cupon_ClienteModelExists(string id)
        {
            return _db.Cupones_Clientes.Any(e => e.NroCupon == id);
        }
    }
}
