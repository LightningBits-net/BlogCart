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

        public async Task<IEnumerable<MessageDTO>> GetAllByConversationId(int conversationId)
        {
            try
            {
                var favMessages = await _db.Messages
                    .Where(m => m.ConversationId == conversationId && m.IsFav)
                    .OrderBy(m => m.Timestamp)
                    .ToListAsync();

                var nonFavMessages = await _db.Messages
                    .Where(m => m.ConversationId == conversationId && !m.IsFav)
                    .OrderBy(m => m.Timestamp)
                    .Take(20 - favMessages.Count)
                    .ToListAsync();

                var messages = favMessages.Concat(nonFavMessages).OrderBy(m => m.Timestamp);

                return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.GetAllByConversationId: {ex.Message}");
                return null;
            }
        }

        public async Task<MessageDTO> Create(MessageDTO objDTO)
        {
            try
            {
                // Delete messages with the default response and their prompts
                await DeleteMessagesWithDefaultResponseAndPrompts(objDTO.ConversationId);

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
                var message = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
                if (message != null)
                {
                    message.IsFav = !message.IsFav;
                    _db.Messages.Update(message);

                    var followingMessage = await _db.Messages
                        .FirstOrDefaultAsync(m => m.ConversationId == message.ConversationId && m.Id == id + 1);

                    if (followingMessage != null)
                    {
                        followingMessage.IsFav = !followingMessage.IsFav;
                        _db.Messages.Update(followingMessage);
                    }

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
                    objFromDb.IsFav = objDTO.IsFav;

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

        public async Task PurgeMessages(int conversationId)
        {
            const int maxMessageCount = 10;

            try
            {
                var conversation = await _db.Conversations
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.Id == conversationId);

                if (conversation != null)
                {
                    var messages = conversation.Messages;

                    if (messages.Count > maxMessageCount)
                    {
                        var messagesToDelete = messages
                            .OrderByDescending(m => m.Timestamp)
                            .Skip(maxMessageCount)
                            .ToList();

                        if (messagesToDelete.Count > 0)
                        {
                            var lastPromptMessage = messagesToDelete.LastOrDefault(m => m.IsUserMessage);
                            var responseMessage = messagesToDelete.LastOrDefault(m => !m.IsUserMessage);

                            if (lastPromptMessage != null && responseMessage != null)
                            {
                                // Delete the last prompt message and its response
                                messagesToDelete.Remove(lastPromptMessage);
                                messagesToDelete.Remove(responseMessage);
                            }

                            _db.Messages.RemoveRange(messagesToDelete);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MessageRepository.PurgeMessages: {ex.Message}");
                // Handle the exception or log an error message as needed
            }
        }

        public async Task DeleteMessagesWithDefaultResponseAndPrompts(int conversationId)
        {
            var defaultResponse = "I'm sorry, I couldn't provide a response at the moment.";
            var errorMessage = "Error: The message is too long and exceeds the token limit.";

            var messagesWithSpecificContent = await _db.Messages
                .Where(m => m.ConversationId == conversationId && (m.Content == defaultResponse || m.Content == errorMessage))
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            foreach (var message in messagesWithSpecificContent)
            {
                var precedingMessageId = message.Id - 1;

                var messagesToDelete = await _db.Messages
                    .Where(m => m.ConversationId == conversationId && (m.Id == message.Id || m.Id == precedingMessageId))
                    .ToListAsync();

                _db.Messages.RemoveRange(messagesToDelete);
                //await _db.SaveChangesAsync();
            }
        }

        //public async Task<MessageDTO> Get(int id)
        //{
        //    try
        //    {
        //        var message = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
        //        return _mapper.Map<Message, MessageDTO>(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception in MessageRepository.Get: {ex.Message}");
        //        return null;
        //    }
        //}

        //public async Task<IEnumerable<MessageDTO>> GetAll()
        //{
        //    try
        //    {
        //        var messages = await _db.Messages.ToListAsync();
        //        return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(messages);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception in MessageRepository.GetAll: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}

