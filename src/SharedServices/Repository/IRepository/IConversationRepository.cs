// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
        public interface IConversationRepository
        {
        public interface IConversationRepository
        {
            Task<ConversationDTO> Create(ConversationDTO conversationDTO);
            Task<ConversationDTO> Get(int id);
            Task<ConversationDTO> GetByName(string name);
            Task<IEnumerable<ConversationDTO>> GetAllByClientId(int clientId);
            Task<IEnumerable<ConversationDTO>> GetAll();
            Task<ConversationDTO> Update(ConversationDTO conversationDTO);
            Task<int> Delete(int id);
        }

    }
}

