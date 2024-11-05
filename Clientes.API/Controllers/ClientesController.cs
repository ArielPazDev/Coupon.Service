using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.API.Context;
using Clientes.API.Models;
using Serilog;

namespace Clientes.API.Controllers
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
        public async Task<ActionResult<ClienteModel>> PostClienteModel(ClienteModel clienteModel)
        {
            _db.Clientes.Add(clienteModel);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteModelExists(clienteModel.CodCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Log
            Log.Information("Endpoint access POST api/clientes");

            return CreatedAtAction("GetClienteModel", new { id = clienteModel.CodCliente }, clienteModel);
        }


        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> GetClientes()
        {
            // Log
            Log.Information("Endpoint access GET api/clientes");

            return await _db.Clientes.ToListAsync();
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> GetClienteModel(string id)
        {
            var clienteModel = await _db.Clientes.FindAsync(id);

            if (clienteModel == null)
            {
                return NotFound();
            }

            // Log
            Log.Information($"Endpoint access GET api/clientes/{id}");

            return clienteModel;
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteModel(string id, ClienteModel clienteModel)
        {
            if (id != clienteModel.CodCliente)
            {
                return BadRequest();
            }

            _db.Entry(clienteModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                // Log
                Log.Information($"Endpoint access PUT api/clientes/{id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteModelExists(id))
                {
                    // Log
                    Log.Error($"Endpoint access PUT api/clientes/{id} (not found)");

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
        public async Task<IActionResult> DeleteClienteModel(string id)
        {
            var clienteModel = await _db.Clientes.FindAsync(id);
            if (clienteModel == null)
            {
                Log.Error($"Endpoint access DELETE api/clientes/{id} (not found)");

                return NotFound();
            }

            _db.Clientes.Remove(clienteModel);
            await _db.SaveChangesAsync();

            // Log
            Log.Information($"Endpoint access DELETE api/clientes/{id}");

            return NoContent();
        }

        private bool ClienteModelExists(string id)
        {
            return _db.Clientes.Any(e => e.CodCliente == id);
        }
    }
}
