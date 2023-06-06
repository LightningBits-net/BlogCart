// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface IMessageRepository
    {
        Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId);
        Task<MessageDTO> Create(MessageDTO objDTO);
        Task<MessageDTO> Update(MessageDTO objDTO);
        Task<int> Delete(int id);
        Task<bool> ToggleFavorite(int id);
        Task PurgeMessages(int conversationId);
        Task DeleteMessagesWithDefaultResponseAndPrompts(int conversationId);
        //Task<MessageDTO> Get(int id);
        //Task<IEnumerable<MessageDTO>> GetAll();
    }
}

