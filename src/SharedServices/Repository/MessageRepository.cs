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
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MessageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MessageDTO> Create(MessageDTO objDTO)
        {
            var obj = _mapper.Map<MessageDTO, Message>(objDTO);
            obj.Timestamp = DateTime.UtcNow;

            // Determine if the message is from the user or the AI
            obj.IsUserMessage = objDTO.IsUserMessage;

            var addedObj = await _db.Messages.AddAsync(obj);
            await _db.SaveChangesAsync();

            var clientId = addedObj.Entity.Conversation.ClientId; // Get the client id via navigation properties.

            return _mapper.Map<Message, MessageDTO>(addedObj.Entity);
        }

        //public async Task<MessageDTO> Create(MessageDTO objDTO)
        //{
        //    var obj = _mapper.Map<MessageDTO, Message>(objDTO);
        //    obj.Timestamp = DateTime.UtcNow;

        //    var addedObj = await _db.Messages.AddAsync(obj);
        //    await _db.SaveChangesAsync();

        //    var clientId = addedObj.Entity.Conversation.ClientId; // Get the client id via navigation properties.

        //    return _mapper.Map<Message, MessageDTO>(addedObj.Entity);
        //}

        public async Task<MessageDTO> Get(int id)
        {
            var message = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<Message, MessageDTO>(message);
        }

        public async Task<IEnumerable<MessageDTO>> GetAll()
        {
            var messages = await _db.Messages.ToListAsync();
            return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
        }
        public async Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId)
        {
            var messages = await _db.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp)
                .Take(20) // Get the last 20 messages
                .ToListAsync();

            return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
        }


        public async Task<int> Delete(int id)
        {
            var obj = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (obj != null)
            {
                _db.Messages.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        //public async Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId)
        //{
        //    var messages = await _db.Messages
        //        .Where(m => m.ConversationId == conversationId)
        //        .ToListAsync();

        //    return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
        //}

        public async Task<MessageDTO> Update(MessageDTO objDTO)
        {
            var objFromDb = await _db.Messages.FirstOrDefaultAsync(m => m.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Content = objDTO.Content;
                objFromDb.Timestamp = objDTO.Timestamp;
                objFromDb.IsUserMessage = objDTO.IsUserMessage;
                objFromDb.ConversationId = objDTO.ConversationId;

                _db.Messages.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Message, MessageDTO>(objFromDb);
            }
            return null;
        }
    }
}

