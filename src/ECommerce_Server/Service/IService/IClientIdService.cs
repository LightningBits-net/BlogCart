using System;



namespace ECommerce_Server.Service.IService
{
    public interface IClientService
    {
        Task<int> GetClientIdAsync();
    }
}

