using Clients.API.DTOs;
using Clients.API.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Clients.API.Services
{
    public class ClienteAPIService : IClienteAPIService
    {
        public async Task<string> ReclamarCupon(ReclamarCuponDTO reclamarCuponDTO)
        {
            try
            {
                // HTTP Client
                var httpClient = new HttpClient();

                // HTTP Client (content)
                var json = JsonConvert.SerializeObject(reclamarCuponDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // HTTP Client (request / response)
                var response = await httpClient.PostAsync("https://localhost:7000/api/SolicitudCupones/SolicitudCupon", content);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    return message;
                }
                else
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

        public async Task<string> UsarCupon(UsarCuponDTO usarCuponDTO)
        {
            try
            {
                // HTTP Client
                var httpClient = new HttpClient();

                // HTTP Client (content)
                var json = JsonConvert.SerializeObject(usarCuponDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // HTTP Client (request / response)
                var response = await httpClient.PostAsync("https://localhost:7000/api/SolicitudCupones/QuemadoCupon", content);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    return message;
                }
                else
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

        public async Task<string> ObtenerCuponesActivos(ObtenerCuponesActivosDTO obtenerCuponesActivosDTO)
        {
            try
            {
                // HTTP Client
                var httpClient = new HttpClient();

                // HTTP Client (request / response)
                var response = await httpClient.GetAsync("https://localhost:7000/api/SolicitudCupones/ObtenerCuponesActivos?CodCliente=" + obtenerCuponesActivosDTO.CodCliente);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    return message;
                }
                else
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
