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
    [Route("api/cupones/detalle")]
    [ApiController]
    public class Cupones_DetalleController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public Cupones_DetalleController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones/detalle
        [HttpPost]
        public async Task<ActionResult<Cupones_DetalleModel>> PostCupones_DetalleModel(Cupones_DetalleModel cupones_DetalleModel)
        {
            try
            {
                _db.Cupones_Detalle.Add(cupones_DetalleModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/detalle");
            }
            catch (DbUpdateException)
            {
                if (Cupones_DetalleModelExists(cupones_DetalleModel.Id_Cupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones/detalle (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupones_DetalleModel", new { id = cupones_DetalleModel.Id_Cupon }, cupones_DetalleModel);
        }

        // GET: api/cupones/detalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupones_DetalleModel>>> GetCupones_Detalle()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/detalle");

            return await _db.Cupones_Detalle.ToListAsync();
        }

        // GET: api/cupones/detalle/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cupones_DetalleModel>>> GetCupones_DetalleModel(int id)
        {
            var cupones_DetalleModel = await _db.Cupones_Detalle.Where(cd => cd.Id_Cupon == id).ToListAsync();

            if (cupones_DetalleModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/detalle/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/detalle/{id}");

            return cupones_DetalleModel;
        }

        // PUT: api/cupones/detalle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupones_DetalleModel(int id, Cupones_DetalleModel cupones_DetalleModel)
        {
            if (id != cupones_DetalleModel.Id_Cupon)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/detalle/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(cupones_DetalleModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/detalle/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupones_DetalleModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/detalle/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/detalle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupones_DetalleModel(int id)
        {
            var cupones_DetalleModel = await _db.Cupones_Detalle.FindAsync(id);

            if (cupones_DetalleModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/detalle/{id} (not found)");

                return NotFound();
            }

            _db.Cupones_Detalle.Remove(cupones_DetalleModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/detalle/{id}");

            return NoContent();
        }

        private bool Cupones_DetalleModelExists(int id)
        {
            return _db.Cupones_Detalle.Any(e => e.Id_Cupon == id);
        }
    }
}
