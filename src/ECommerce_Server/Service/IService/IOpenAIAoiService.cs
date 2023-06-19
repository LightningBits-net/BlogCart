using System;
namespace ECommerce_Server.Service.IService
{
    public interface IOpenAIApiService
    {
        Task<string> SendMessageAsync(int conversationId, string prompt);
    }

}

