// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface IMessageRepository
    {
        Task<MessageDTO> Create(MessageDTO objDTO);
        Task<MessageDTO> Get(int id);
        Task<IEnumerable<MessageDTO>> GetAll();
        Task<MessageDTO> Update(MessageDTO objDTO);
        Task<int> Delete(int id);

        Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId);
    }
}

