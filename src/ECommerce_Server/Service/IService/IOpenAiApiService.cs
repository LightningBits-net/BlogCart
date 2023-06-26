using System;
namespace ECommerce_Server.Service.IService
{
    public interface IOpenAiApiService
    {
        Task<string> SendMessageAsync(int conversationId, string prompt);
    }

}

