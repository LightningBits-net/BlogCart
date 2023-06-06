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
            try
            {
                var favMessages = conversationHistory
              .Where(message => message.IsFav)
              .OrderByDescending(message => message.Timestamp)
              .Take(10)
              .Select(message => new
              {
                  role = message.IsUserMessage ? "user" : "assistant",
                  content = message.Content
              });

            var recentNonFavMessages = conversationHistory
                .Where(message => !message.IsFav)
                .OrderByDescending(message => message.Timestamp)
                .Take(6)
                .Select(message => new
                {
                    role = message.IsUserMessage ? "user" : "assistant",
                    content = message.Content
                });



            var messages = new List<object>(favMessages);
            messages.AddRange(recentNonFavMessages);
            // Prepend the instruction to the user's prompt
            messages.Add(new { role = "user", content = "Do not exceed 100 words in total: " + prompt });

            var messagesJson = JsonSerializer.Serialize(messages);

            Console.WriteLine("Messages sent to API: " + messagesJson);
            Console.WriteLine("Prompt sent to API: " + prompt);  //for development

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages,
                temperature = 0.5,
                max_tokens = 2400,
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                // Create and return a default response
                return CreateDefaultResponse();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse); // Log the full API response to the console

            var result = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API request failed with error: {ex.Message}");
                // Handle the error and return a default response
                return CreateDefaultResponse();
            }
        }

        private string CreateDefaultResponse()
        {
            // Create a default response message
            return "I'm sorry, I couldn't provide a response at the moment, if the problem persist Please try again with a shorter message";
        }

    }
}







