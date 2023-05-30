using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedServices.Data;
using SharedServices.Models;
using SharedServices.Repository.IRepository;

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
            try
            {
                var conversation = await _db.Conversations.FirstOrDefaultAsync(c => c.Id == id);
                if (conversation != null)
                {
                    _db.Conversations.Remove(conversation);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<ConversationDTO> Get(int id)
        {
            try
            {
                var conversation = await _db.Conversations
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.Id == id);

                return _mapper.Map<Conversation, ConversationDTO>(conversation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<ConversationDTO>> GetAll()
        {
            try
            {
                var conversations = await _db.Conversations
                    .Include(c => c.Messages)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<Conversation>, IEnumerable<ConversationDTO>>(conversations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ConversationDTO> Create(ConversationDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<ConversationDTO, Conversation>(objDTO);
                var addedObj = await _db.Conversations.AddAsync(obj);
                await _db.SaveChangesAsync();

                return _mapper.Map<Conversation, ConversationDTO>(addedObj.Entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ConversationDTO> GetByName(string name)
        {
            try
            {
                var conversation = await _db.Conversations
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.Name == name);

                return _mapper.Map<Conversation, ConversationDTO>(conversation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<ConversationDTO>> GetAllByClientId(int clientId)
        {
            try
            {
                var conversations = await _db.Conversations
                    .Include(c => c.Messages)
                    .Where(c => c.ClientId == clientId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<Conversation>, IEnumerable<ConversationDTO>>(conversations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ConversationDTO> Update(ConversationDTO objDTO)
        {
            try
            {
                var objFromDb = await _db.Conversations.Include(c => c.Messages).FirstOrDefaultAsync(u => u.Id == objDTO.Id);
                if (objFromDb != null)
                {
                    objFromDb.Name = objDTO.Name;
                    _db.Conversations.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Conversation, ConversationDTO>(objFromDb);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
