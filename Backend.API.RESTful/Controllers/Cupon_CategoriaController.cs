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
    [Route("api/cupones/categorias")]
    [ApiController]
    public class Cupon_CategoriaController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public Cupon_CategoriaController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/cupones/categorias
        [HttpPost]
        public async Task<ActionResult<Cupon_CategoriaModel>> PostCupon_CategoriaModel(Cupon_CategoriaModel cupon_CategoriaModel)
        {
            try
            {
                _db.Cupones_Categorias.Add(cupon_CategoriaModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/cupones/categorias");
            }
            catch (DbUpdateException)
            {
                if (Cupon_CategoriaModelExists(cupon_CategoriaModel.Id_Cupon))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/cupones/categorias (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupon_CategoriaModel", new { id = cupon_CategoriaModel.Id_Cupones_Categorias }, cupon_CategoriaModel);
        }

        // GET: api/cupones/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_CategoriaModel>>> GetCupones_Categorias()
        {
            // Log
            Log.Information("Endpoint access GET: api/cupones/categorias");

            return await _db.Cupones_Categorias.ToListAsync();
        }

        // GET: api/cupones/categorias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon_CategoriaModel>> GetCupon_CategoriaModel(int id)
        {
            var cupon_CategoriaModel = await _db.Cupones_Categorias.FindAsync(id);

            if (cupon_CategoriaModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/cupones/categorias/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/cupones/categorias/{id}");

            return cupon_CategoriaModel;
        }

        // PUT: api/cupones/categorias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_CategoriaModel(int id, Cupon_CategoriaModel cupon_CategoriaModel)
        {
            if (id != cupon_CategoriaModel.Id_Cupones_Categorias)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/cupones/categorias/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(cupon_CategoriaModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/cupones/categorias/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_CategoriaModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/cupones/categorias/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cupones/categorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_CategoriaModel(int id)
        {
            var cupon_CategoriaModel = await _db.Cupones_Categorias.FindAsync(id);

            if (cupon_CategoriaModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/cupones/categorias/{id} (not found)");

                return NotFound();
            }

            _db.Cupones_Categorias.Remove(cupon_CategoriaModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/cupones/categorias/{id}");

            return NoContent();
        }

        private bool Cupon_CategoriaModelExists(int id)
        {
            return _db.Cupones_Categorias.Any(e => e.Id_Cupones_Categorias == id);
        }
    }
}
