using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SharedServices.Data;
using SharedServices.Models;
using SharedServices.Repository;
using SharedServices.Repository.IRepository;

namespace ECommerce_Server.Service
{
    public class OpenAIApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IMessageRepository _messageRepository;


        public OpenAIApiService(HttpClient httpClient, IConfiguration configuration, IMessageRepository messageRepository) // Modify this line
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _apiKey = configuration["APIKeys:MyChatGPTAPI"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            _messageRepository = messageRepository;

        }

        public async Task<string> SendMessageAsync(int conversationId, string prompt)
        {
            try
            {
                //var messagesForApiRequest = await _messageRepository.GetMessagesForApiRequest(conversationId);

                //var messages = new List<object>(messagesForApiRequest);

                //messages.Add(new
                //{
                //    role = "user",
                //    content = prompt
                //});
                var messagesForApiRequest = await _messageRepository.GetMessagesForApiRequest(conversationId);

                var messages = messagesForApiRequest.Append(new
                {
                    role = "user",
                    content = prompt
                });


                var messagesJson = JsonSerializer.Serialize(messages);

            Console.WriteLine("Messages sent to API: " + messagesJson);
            Console.WriteLine("Prompt sent to API: " + prompt);  //for development

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages,
                temperature = 0.4,
                max_tokens = 2400,
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                    await _messageRepository.PurgeMessages(conversationId);
                    await _messageRepository.DeleteMessagesWithDefaultResponseAndPrompts(conversationId);

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







