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
    [Route("api/cupones")]
    [ApiController]
    public class CuponesController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public CuponesController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones
        [HttpPost]
        public async Task<ActionResult<CuponModel>> PostCuponModel(CuponModel cuponModel)
        {
            try
            {
                _db.Cupones.Add(cuponModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones");
            }
            catch (DbUpdateException)
            {
                if (CuponModelExists(cuponModel.Id_Cupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCuponModel", new { id = cuponModel.Id_Cupon }, cuponModel);
        }


        // GET: api/cupones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponModel>>> GetCupones()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones");

            return await _db
                .Cupones
                .Include(c => c.Cupones_Categorias)
                    .ThenInclude(cc => cc.Categoria)
                .Include(c => c.Tipo_Cupon)
                .ToListAsync();
        }

        // GET: api/cupones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CuponModel>> GetCuponModel(int id)
        {
            var cuponModel = await _db.Cupones.FindAsync(id);

            if (cuponModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/{id}");

            return cuponModel;
        }

        // PUT: api/cupones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuponModel(int id, CuponModel cuponModel)
        {
            if (id != cuponModel.Id_Cupon)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(cuponModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuponModel(int id)
        {
            var cuponModel = await _db.Cupones.FindAsync(id);

            if (cuponModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/{id} (not found)");

                return NotFound();
            }

            cuponModel.Activo = false;

            _db.Entry(cuponModel).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/{id}");

            return NoContent();
        }

        private bool CuponModelExists(int id)
        {
            return _db.Cupones.Any(e => e.Id_Cupon == id);
        }
    }
}
