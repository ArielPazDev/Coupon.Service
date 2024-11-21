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
    [Route("api/cupones/historial")]
    [ApiController]
    public class Cupones_HistorialController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public Cupones_HistorialController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones/historial
        [HttpPost]
        public async Task<ActionResult<Cupones_HistorialModel>> PostCupones_HistorialModel(Cupones_HistorialModel cupones_HistorialModel)
        {
            try
            {
                _db.Cupones_Historial.Add(cupones_HistorialModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/historial");
            }
            catch (DbUpdateException)
            {
                if (Cupones_HistorialModelExists(cupones_HistorialModel.Id_Cupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones/historial (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupones_HistorialModel", new { id = cupones_HistorialModel.Id_Cupon }, cupones_HistorialModel);
        }

        // GET: api/cupones/historial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupones_HistorialModel>>> GetCupones_Historial()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/historial");

            return await _db.Cupones_Historial.ToListAsync();
        }

        // GET: api/cupones/historial/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupones_HistorialModel>> GetCupones_HistorialModel(int id)
        {
            var cupones_HistorialModel = await _db.Cupones_Historial.FindAsync(id);

            if (cupones_HistorialModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/historial/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/historial/{id}");

            return cupones_HistorialModel;
        }

        // PUT: api/cupones/historial/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupones_HistorialModel(int id, Cupones_HistorialModel cupones_HistorialModel)
        {
            if (id != cupones_HistorialModel.Id_Cupon)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/historial/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(cupones_HistorialModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/historial/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupones_HistorialModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/historial/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/historial/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupones_HistorialModel(int id)
        {
            var cupones_HistorialModel = await _db.Cupones_Historial.FindAsync(id);

            if (cupones_HistorialModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/historial/{id} (not found)");

                return NotFound();
            }

            _db.Cupones_Historial.Remove(cupones_HistorialModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/historial/{id}");

            return NoContent();
        }

        private bool Cupones_HistorialModelExists(int id)
        {
            return _db.Cupones_Historial.Any(e => e.Id_Cupon == id);
        }
    }
}
