using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SharedServices.Models;

namespace ECommerce_Server.Service
{
    public class OpenAIApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _apiKey = configuration["APIKeys:MyChatGPTAPI"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> SendMessageAsync(IEnumerable<MessageDTO> conversationHistory, string prompt)
        {
            var messages = new List<object>();

            // Add previous 20 messages as context
            var contextMessages = conversationHistory.TakeLast(10);
            messages.AddRange(contextMessages.Select(message => new
            {
                role = message.IsUserMessage ? "user" : "assistant",
                content = message.Content
            }));

            messages.Add(new { role = "user", content = prompt });

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages,
                temperature = 0.7,
                max_tokens = 300,
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse); // Log the full API response to the console

            var result = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return result;
        }

    }
}







