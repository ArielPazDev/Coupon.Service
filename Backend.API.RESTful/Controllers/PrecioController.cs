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
    [Route("api/precio")]
    [ApiController]
    public class PrecioController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public PrecioController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/precio
        [HttpPost]
        public async Task<ActionResult<PrecioModel>> PostPrecioModel(PrecioModel precioModel)
        {
            try
            {
                _db.Precios.Add(precioModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/precio");
            }
            catch (DbUpdateException)
            {
                if (PrecioModelExists(precioModel.Id_Precio))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/precio (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrecioModel", new { id = precioModel.Id_Precio }, precioModel);
        }

        // GET: api/precio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrecioModel>>> GetPrecios()
        {
            // Log
            Log.Information("Endpoint access GET: api/precio");

            return await _db.Precios.ToListAsync();
        }

        // GET: api/precio/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PrecioModel>> GetPrecioModel(int id)
        {
            var precioModel = await _db.Precios.FindAsync(id);

            if (precioModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/precio/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/precio/{id}");

            return precioModel;
        }

        // PUT: api/precio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrecioModel(int id, PrecioModel precioModel)
        {
            if (id != precioModel.Id_Precio)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/precio/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(precioModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/precio/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/precio/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/precio/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrecioModel(int id)
        {
            var precioModel = await _db.Precios.FindAsync(id);

            if (precioModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/precio/{id} (not found)");

                return NotFound();
            }

            precioModel.Precio = 0m;

            _db.Entry(precioModel).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/precio/{id}");

            return NoContent();
        }

        private bool PrecioModelExists(int id)
        {
            return _db.Precios.Any(e => e.Id_Precio == id);
        }
    }
}
