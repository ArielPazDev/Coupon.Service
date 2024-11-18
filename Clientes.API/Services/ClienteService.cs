using Clientes.API.DTOs;
using Clientes.API.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Clientes.API.Services
{
    public class ClienteService : IClienteService
    {
        public async Task<string> SolicitarCupon(ClienteDTO clienteDTO)
        {
			try
			{
				// HTTP Client
				var httpClient = new HttpClient();

				// HTTP Client (content)
				var json = JsonConvert.SerializeObject(clienteDTO);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				// HTTP Client (request / response)
				var response = await httpClient.PostAsync("https://localhost:7000/api/Cupon_Cliente", content);

				if (response.IsSuccessStatusCode)
				{
					var msg = await response.Content.ReadAsStringAsync();
					return msg;
				} else
				{
					var error = await response.Content.ReadAsStringAsync();
					throw new Exception($"Error: {error}");
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
        }
    }
}
