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
    [Route("api/categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public CategoriasController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> PostCategoriaModel(CategoriaModel categoriaModel)
        {
            try
            {
                _db.Categorias.Add(categoriaModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/categorias");
            }
            catch (DbUpdateException)
            {
                if (CategoriaModelExists(categoriaModel.Id_Categoria))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/categorias (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategoriaModel", new { id = categoriaModel.Id_Categoria }, categoriaModel);
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> GetCategorias()
        {
            // Log
            Log.Information("Endpoint access GET: api/categorias");

            return await _db
                .Categorias
                .Include(c => c.Cupones_Categorias)
                    .ThenInclude(cc => cc.Cupon)
                .ToListAsync();
        }

        // GET: api/categorias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaModel>> GetCategoriaModel(int id)
        {
            var categoriaModel = await _db.Categorias.FindAsync(id);

            if (categoriaModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/categorias/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/categorias/{id}");

            return categoriaModel;
        }

        // PUT: api/categorias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaModel(int id, CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.Id_Categoria)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/categorias/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(categoriaModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/categorias/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/categorias/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/categorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaModel(int id)
        {
            var categoriaModel = await _db.Categorias.FindAsync(id);

            if (categoriaModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/categorias/{id} (not found)");

                return NotFound();
            }

            _db.Categorias.Remove(categoriaModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/categorias/{id}");

            return NoContent();
        }

        private bool CategoriaModelExists(int id)
        {
            return _db.Categorias.Any(e => e.Id_Categoria == id);
        }
    }
}
