using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clients.API.Contexts;
using Clients.API.Models;
using Serilog;

namespace Clients.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public ClientesController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<ClienteModel>> PostClientModel(ClienteModel clientModel)
        {
            try
            {
                _db.Clients.Add(clientModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/clientes");
            }
            catch (DbUpdateException)
            {
                if (ClientModelExists(clientModel.CodCliente))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/clientes (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientModel", new { id = clientModel.CodCliente }, clientModel);
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> GetClients()
        {
            // Log
            Log.Information("Endpoint access GET: api/clientes");

            return await _db.Clients.ToListAsync();
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> GetClientModel(string id)
        {
            var clientModel = await _db.Clients.FindAsync(id);

            if (clientModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/clientes/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/clientes/{id}");

            return clientModel;
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientModel(string id, ClienteModel clientModel)
        {
            if (id != clientModel.CodCliente)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/clientes/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(clientModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/clientes/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/clientes/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientModel(string id)
        {
            var clientModel = await _db.Clients.FindAsync(id);

            if (clientModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/clientes/{id} (not found)");

                return NotFound();
            }

            _db.Clients.Remove(clientModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/clientes/{id}");

            return NoContent();
        }

        private bool ClientModelExists(string id)
        {
            return _db.Clients.Any(c => c.CodCliente == id);
        }
    }
}