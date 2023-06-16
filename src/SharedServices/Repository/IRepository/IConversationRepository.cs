// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
        public interface IConversationRepository
        {
            Task<ConversationDTO> Get(int id);
            Task<IEnumerable<ConversationDTO>> GetAll();
            Task<int> Delete(int id);
            Task<ConversationDTO> Create(ConversationDTO objDTO);
            Task<ConversationDTO> Update(ConversationDTO objDTO);

            Task<ConversationDTO> GetByName(string name);
            Task<IEnumerable<ConversationDTO>> GetAllByClientId(int clientId);
            Task<ConversationDTO> GetAndUpdateContext(int id);

    }
}

