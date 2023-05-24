// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface IMessageRepository
    {
        Task<MessageDTO> Create(MessageDTO messageDTO);
        Task<MessageDTO> Get(int id);
        Task<IEnumerable<MessageDTO>> GetAll();
        Task<MessageDTO> Update(MessageDTO messageDTO);
        Task<int> Delete(int id);
    }
}

