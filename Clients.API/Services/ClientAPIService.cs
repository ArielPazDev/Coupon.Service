using Clients.API.DTOs;
using Clients.API.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Clients.API.Services
{
    public class ClientAPIService : ClientAPIInterface
    {
        public async Task<string> RequestCoupon(ClientAPIDTO clientAPIDTO)
        {
			try
			{
				// HTTP Client
				var httpClient = new HttpClient();

				// HTTP Client (content)
				var json = JsonConvert.SerializeObject(clientAPIDTO);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				// HTTP Client (request / response)
				var response = await httpClient.PostAsync("https://localhost:7000/api/Cupon_Cliente", content);

				if (response.IsSuccessStatusCode)
				{
					var message = await response.Content.ReadAsStringAsync();

					return message;
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
