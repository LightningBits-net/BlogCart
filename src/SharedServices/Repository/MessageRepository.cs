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

        //public async Task<MessageDTO> Create(MessageDTO objDTO)
        //{
        //    try
        //    {
        //        var obj = _mapper.Map<MessageDTO, Message>(objDTO);
        //        obj.Timestamp = DateTime.UtcNow;
        //        obj.IsUserMessage = objDTO.IsUserMessage;

        //        var addedObj = await _db.Messages.AddAsync(obj);
        //        await _db.SaveChangesAsync();

        //        var clientId = addedObj.Entity.Conversation.ClientId;

        //        return _mapper.Map<Message, MessageDTO>(addedObj.Entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception in MessageRepository.Create: {ex.Message}");
        //        return null;
        //    }
        //}

        public async Task<MessageDTO> Create(MessageDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<MessageDTO, Message>(objDTO);
                obj.Timestamp = DateTime.UtcNow;
                obj.IsUserMessage = objDTO.IsUserMessage;

                var addedObj = await _db.Messages.AddAsync(obj);
                await _db.SaveChangesAsync();

                var conversation = await _db.Conversations
                    .Include(c => c.Client)
                    .FirstOrDefaultAsync(c => c.Id == obj.ConversationId);

                var clientId = conversation?.ClientId ?? 0;

                return _mapper.Map<Message, MessageDTO>(addedObj.Entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.Create: {ex.Message}");
                return null;
            }
        }


        public async Task<MessageDTO> Get(int id)
        {
            try
            {
                var message = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
                return _mapper.Map<Message, MessageDTO>(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.Get: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetAll()
        {
            try
            {
                var messages = await _db.Messages.ToListAsync();
                return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.GetAll: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId)
        {
            try
            {
                var messages = await _db.Messages
                    .Where(m => m.ConversationId == conversationId)
                    .OrderByDescending(m => m.Timestamp)
                    .Take(20)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.GetAllByConversationId: {ex.Message}");
                return null;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var obj = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
                if (obj != null)
                {
                    _db.Messages.Remove(obj);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.Delete: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> ToggleFavorite(int id)
        {
            try
            {
                var objFromDb = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
                if (objFromDb != null)
                {
                    objFromDb.IsFav = !objFromDb.IsFav; // Correct field name
                    _db.Messages.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.ToggleFavorite: {ex.Message}");
                return false;
            }
        }


        public async Task<MessageDTO> Update(MessageDTO objDTO)
        {
            try
            {
                var objFromDb = await _db.Messages.FirstOrDefaultAsync(m => m.Id == objDTO.Id);
                if (objFromDb != null)
                {
                    objFromDb.Content = objDTO.Content;
                    objFromDb.Timestamp = objDTO.Timestamp;
                    objFromDb.IsUserMessage = objDTO.IsUserMessage;
                    objFromDb.ConversationId = objDTO.ConversationId;
                    objFromDb.IsFav = objDTO.IsFav; // Add this line

                    _db.Messages.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Message, MessageDTO>(objFromDb);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.Update: {ex.Message}");
                return null;
            }
        }

        //public async Task<MessageDTO> Update(MessageDTO objDTO)
        //{
        //    try
        //    {
        //        var objFromDb = await _db.Messages.FirstOrDefaultAsync(m => m.Id == objDTO.Id);
        //        if (objFromDb != null)
        //        {
        //            objFromDb.Content = objDTO.Content;
        //            objFromDb.Timestamp = objDTO.Timestamp;
        //            objFromDb.IsUserMessage = objDTO.IsUserMessage;
        //            objFromDb.ConversationId = objDTO.ConversationId;

        //            _db.Messages.Update(objFromDb);
        //            await _db.SaveChangesAsync();
        //            return _mapper.Map<Message, MessageDTO>(objFromDb);
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception in MessageRepository.Update: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}

