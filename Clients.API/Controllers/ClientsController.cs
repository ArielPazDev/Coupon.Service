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
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public ClientsController(DatabaseContext db)
        {
            _db = db;
        }

        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult<ClientModel>> PostClientModel(ClientModel clientModel)
        {
            try
            {
                _db.Clients.Add(clientModel);
                await _db.SaveChangesAsync();

                // Log
                Log.Information("Endpoint access POST: api/clients");
            }
            catch (DbUpdateException)
            {
                if (ClientModelExists(clientModel.CodCliente))
                {
                    // Log
                    Log.Error("Endpoint access POST: api/clients (conflict)");

                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientModel", new { id = clientModel.CodCliente }, clientModel);
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientModel>>> GetClients()
        {
            // Log
            Log.Information("Endpoint access GET: api/clients");

            return await _db.Clients.ToListAsync();
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> GetClientModel(string id)
        {
            var clientModel = await _db.Clients.FindAsync(id);

            if (clientModel == null)
            {
                // Log
                Log.Information($"Endpoint access GET: api/clients/{id} (not found)");

                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET: api/clients/{id}");

            return clientModel;
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientModel(string id, ClientModel clientModel)
        {
            if (id != clientModel.CodCliente)
            {
                // Log
                Log.Information($"Endpoint access PUT: api/clients/{id} (bad request)");

                return BadRequest();
            }

            _db.Entry(clientModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT: api/clients/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(id))
                {
                    // Log
                    Log.Information($"Endpoint access PUT: api/clients/{id} (not found)");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientModel(string id)
        {
            var clientModel = await _db.Clients.FindAsync(id);

            if (clientModel == null)
            {
                // Log
                Log.Information($"Endpoint access DELETE: api/clients/{id} (not found)");

                return NotFound();
            }

            _db.Clients.Remove(clientModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE: api/clients/{id}");

            return NoContent();
        }

        private bool ClientModelExists(string id)
        {
            return _db.Clients.Any(c => c.CodCliente == id);
        }
    }
}