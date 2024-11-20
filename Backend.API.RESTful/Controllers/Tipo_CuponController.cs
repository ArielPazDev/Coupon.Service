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
    [Route("api/cupones/tipo")]
    [ApiController]
    public class Tipo_CuponController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public Tipo_CuponController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones/tipo
        [HttpPost]
        public async Task<ActionResult<Tipo_CuponModel>> PostTipo_CuponModel(Tipo_CuponModel tipo_CuponModel)
        {
            try
            {
                _db.Tipo_Cupon.Add(tipo_CuponModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/tipo");
            }
            catch (DbUpdateException)
            {
                if (Tipo_CuponModelExists(tipo_CuponModel.Id_Tipo_Cupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones/tipo (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipo_CuponModel", new { id = tipo_CuponModel.Id_Tipo_Cupon }, tipo_CuponModel);
        }

        // GET: api/cupones/tipo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_CuponModel>>> GetTipo_Cupon()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/tipo");

            return await _db.Tipo_Cupon.ToListAsync();
        }

        // GET: api/cupones/tipo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_CuponModel>> GetTipo_CuponModel(int id)
        {
            var tipo_CuponModel = await _db.Tipo_Cupon.FindAsync(id);

            if (tipo_CuponModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/tipo/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/tipo/{id}");

            return tipo_CuponModel;
        }

        // PUT: api/cupones/tipo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_CuponModel(int id, Tipo_CuponModel tipo_CuponModel)
        {
            if (id != tipo_CuponModel.Id_Tipo_Cupon)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/tipo/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(tipo_CuponModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/tipo/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_CuponModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/tipo/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/tipo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_CuponModel(int id)
        {
            var tipo_CuponModel = await _db.Tipo_Cupon.FindAsync(id);

            if (tipo_CuponModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/tipo/{id} (not found)");

                return NotFound();
            }

            _db.Tipo_Cupon.Remove(tipo_CuponModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/tipo/{id}");

            return NoContent();
        }

        private bool Tipo_CuponModelExists(int id)
        {
            return _db.Tipo_Cupon.Any(e => e.Id_Tipo_Cupon == id);
        }
    }
}
