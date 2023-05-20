using BlogCart.Service.IService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedServices.Models;

namespace BlogCart.Service
{
    public class ClientFrontendService : IClientFrontendService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;

        public ClientFrontendService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ClientFrontendDTO> Get(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Api/ClientFrontend/{clientId}");
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ClientFrontendDTO>(content);
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the client: {ex.Message}");
                return new ClientFrontendDTO();
            }
        }

        public async Task<IEnumerable<ClientFrontendDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Api/ClientFrontend");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClientFrontendDTO>>(content);
                }
                else
                {
                    return Enumerable.Empty<ClientFrontendDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting all clients: {ex.Message}");
                return Enumerable.Empty<ClientFrontendDTO>();
            }
        }
        //public async Task<int> GetClientIdFromDomain(string domain)
        //{
        //    var clients = await GetAll();
        //    var client = clients.FirstOrDefault(c => c.DomainName == domain);
        //    return client?.ClientId ?? 0; // return 0 or some default value if client not found
        //}
        public async Task<int> GetClientIdFromDomain(string domain)
        {
            var clients = await GetAll();
            var client = clients.FirstOrDefault(c => c.DomainName == domain);
            if (client != null)
            {
                var response = await _httpClient.GetAsync($"/api/ClientFrontend/{client.ClientId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    client = JsonConvert.DeserializeObject<ClientFrontendDTO>(content);
                }
            }
            return client?.ClientId ?? 0; // Return client ID or 0 if client not found
        }

    }
}


