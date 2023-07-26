using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Internal;
using System.Threading.Tasks;
using Microsoft.DotNet.MSIdentity.Shared;
using SharedServices.Data;
using SharedServices.Models;
using SharedServices.Repository;
using SharedServices.Repository.IRepository;
using ECommerce_Server.Service.IService;

namespace ECommerce_Server.Service
{
    public class OpenAiApiService : IOpenAiApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IConversationRepository _conversationRepository;

        public OpenAiApiService(HttpClient httpClient, IConfiguration configuration, IConversationRepository conversationRepository)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           
            //_apiKey = configuration["APIKeys:MyChatGPTAPI"];
            //_apiKey = configuration["APIKeys:KenChatGPTAPI"];
            _apiKey = configuration["APIKeys:MyChatGPTDEVKEY"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            _conversationRepository = conversationRepository;
        }

        public async Task<string> SendMessageAsync(int conversationId, string prompt)
        {
            try
            {
                var conversation = await _conversationRepository.Get(conversationId);
                if (conversation == null)
                {
                    Console.WriteLine("Conversation not found.");
                    return CreateDefaultResponse();
                }

                // Use conversation.Context as the message context
                var conversationContext = conversation.Context ?? string.Empty;

                // Use the SystemMessage from the conversation or a default message if it's null or empty
                var systemContext = !string.IsNullOrEmpty(conversation.SystemMessage)
                    ? conversation.SystemMessage
                    : "This message is your intial context training, please follow these instructions but do not repeat the content of this message in your reponses, You are an AI language model trained to be my most valued assitant, you will act as an editor in all aspects of languange";

                var messages = new List<object>
                {
                    new
                    {  
                        role = "system",
                        content = systemContext
                    },
                    new
                    {
                        role = "user",
                        content = conversationContext
                    }
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo-16k",
                    messages,
                    temperature = 0.6,
                    max_tokens = 8150,   //max out at 8150 tokens
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    return CreateDefaultResponse();
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var parsedResponse = JsonDocument.Parse(jsonResponse);
                var result = parsedResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                var tokenCount = parsedResponse.RootElement.GetProperty("usage").GetProperty("total_tokens").GetInt32();

                Console.WriteLine($">>>>>>>>>>>>>>>>System Context: {systemContext}<<<<<<<<<<<<<<<<<");
                Console.WriteLine($">>>>>>>>>>>>>>Sent context: {conversationContext} <<<<<<<<<<>>>>>>>>>>with token count: {tokenCount}<<<<<<<<<<<<<<<<<<");
                Console.WriteLine($">>>>>>>>>>>>>>>>Received response: {result}<<<<<<<<<<<<<<<<<");

                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API request failed with error: {ex.Message}");
                return CreateDefaultResponse();
            }
        }

        private string CreateDefaultResponse()
        {
            return "I'm sorry, I couldn't provide a response at the moment, please try again";
        }
    }
}







