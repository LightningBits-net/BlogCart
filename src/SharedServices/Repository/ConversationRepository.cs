// LightningBits
using System;
using SharedServices.Repository.IRepository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;
using Microsoft.EntityFrameworkCore;
using SharedServices.Models;

namespace SharedServices.Repository
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ConversationRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> Delete(int id)
        {
            var conversation = await _db.Conversations
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation == null)
            {
                return 0;
            }

            _db.Conversations.Remove(conversation);
            return await _db.SaveChangesAsync();
        }

        public async Task<ConversationDTO> Get(int id)
        {
            var conversation = await _db.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<Conversation, ConversationDTO>(conversation);
        }

        public async Task<IEnumerable<ConversationDTO>> GetAll()
        {
            var conversations = await _db.Conversations
                .Include(c => c.Messages)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Conversation>, IEnumerable<ConversationDTO>>(conversations);
        }

        public async Task<ConversationDTO> Create(ConversationDTO objDTO)
        {
            var obj = _mapper.Map<ConversationDTO, Conversation>(objDTO);

            var addedobj = await _db.Conversations.AddAsync(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Conversation, ConversationDTO>(addedobj.Entity);
        }

        public async Task<ConversationDTO> GetByName(string name)
        {
            var conversation = await _db.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Name == name);

            return _mapper.Map<Conversation, ConversationDTO>(conversation);
        }

        public async Task<IEnumerable<ConversationDTO>> GetAllByClientId(int clientId)
        {
            var conversations = await _db.Conversations
                .Include(c => c.Messages)
                .Where(c => c.ClientId == clientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Conversation>, IEnumerable<ConversationDTO>>(conversations);
        }

        public async Task<ConversationDTO> Update(ConversationDTO objDTO)
        {
            var objFromDb = await _db.Conversations.Include(c => c.Messages).FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.ClientId = objDTO.ClientId;
                // update other properties if needed

                _db.Conversations.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Conversation, ConversationDTO>(objFromDb);
            }
            return null;
        }

    }
}
