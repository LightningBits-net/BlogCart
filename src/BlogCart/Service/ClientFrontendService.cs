using System;
using BlogCart.Service.IService;
using Newtonsoft.Json;
using SharedServices.Data;
using SharedServices.Models;

namespace BlogCart.Service
{
    public class ClientFrontendService : IClientFrontendService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration? _configuration;
        private string? BaseServerUrl;

        public ClientFrontendService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ClientFrontendDTO> Get(int clientId)
        {
            var url = $"{BaseServerUrl}/api/client/{clientId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClientFrontendDTO>();
        }

        public async Task<IEnumerable<ClientFrontendDTO>> GetAll()
        {
            var url = $"{BaseServerUrl}/api/client";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ClientFrontendDTO>>(url);

                if (response != null)
                {
                    return response;
                }
                else
                {
                    // Handle the case when no clients are found
                    return Enumerable.Empty<ClientFrontendDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle the case when the API endpoint is not found or an error occurs
                // Log the exception or perform any necessary error handling
                Console.WriteLine($"Error occurred while retrieving clients: {ex.Message}");

                return Enumerable.Empty<ClientFrontendDTO>();
            }
        }

    }
}

