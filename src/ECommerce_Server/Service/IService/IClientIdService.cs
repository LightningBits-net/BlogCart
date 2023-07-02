using System;
namespace ECommerce_Server.Service.IService
{
    public interface IClientIdService
    {
        Task<int?> GetServerClientId();
    }

}

