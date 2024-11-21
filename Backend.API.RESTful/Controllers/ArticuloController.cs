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
    [Route("api/articulos")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public ArticuloController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/articulos
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel)
        {
            try
            {
                _db.Articulos.Add(articuloModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/articulos");
            }
            catch (DbUpdateException)
            {
                if (ArticuloModelExists(articuloModel.Id_Articulo))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/articulos (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArticuloModel", new { id = articuloModel.Id_Articulo }, articuloModel);
        }

        // GET: api/articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloModel>>> GetArticulos()
        {
            // Log
            Log.Information("Endpoint access GET: api/articulos");

            return await _db.Articulos.Include(a => a.Precio).ToListAsync();
        }

        // GET: api/articulos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id)
        {
            var articuloModel = await _db.Articulos.FindAsync(id);

            if (articuloModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/articulos/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/articulos/{id}");

            return articuloModel;
        }

        // PUT: api/articulos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticuloModel(int id, ArticuloModel articuloModel)
        {
            if (id != articuloModel.Id_Articulo)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/articulos/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(articuloModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/articulos/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/articulos/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/articulos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticuloModel(int id)
        {
            var articuloModel = await _db.Articulos.FindAsync(id);

            if (articuloModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/articulos/{id} (not found)");

                return NotFound();
            }

            _db.Articulos.Remove(articuloModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/articulos/{id}");

            return NoContent();
        }

        private bool ArticuloModelExists(int id)
        {
            return _db.Articulos.Any(e => e.Id_Articulo == id);
        }
    }
}
