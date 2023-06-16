using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        //public async Task<ConversationDTO> GetAndUpdateContext(int id)
        //{
        //    try
        //    {
        //        var conversation = await _db.Conversations
        //            .Include(c => c.Messages)
        //            .FirstOrDefaultAsync(c => c.Id == id);

        //        if (conversation != null)
        //        {
        //            var recentNonFavMessages = conversation.Messages
        //                .Where(message => !message.IsFav)
        //                .OrderByDescending(message => message.Timestamp)
        //                .Take(4)
        //                .Select(message => new
        //                {
        //                    role = message.IsUserMessage ? "user" : "assistant",
        //                    content = message.Content
        //                })
        //                .ToList();

        //            var favMessages = conversation.Messages
        //                .Where(message => message.IsFav)
        //                .OrderByDescending(message => message.Timestamp)
        //                .Take(6)
        //                .Select(message => new
        //                {
        //                    role = message.IsUserMessage ? "user" : "assistant",
        //                    content = message.Content
        //                })
        //                .ToList();

        //            var recentMessages = conversation.Messages
        //                .Where(message => !message.IsFav)
        //                .OrderByDescending(message => message.Timestamp)
        //                .Take(10)
        //                .Select(message => new
        //                {
        //                    role = message.IsUserMessage ? "user" : "assistant",
        //                    content = message.Content
        //                })
        //                .ToList();

        //            recentNonFavMessages.AddRange(favMessages);
        //            recentNonFavMessages.AddRange(recentMessages);

        //            recentNonFavMessages.Reverse(); // Reverses the order so the most recent message is at the end

        //            var updatedContextList = recentNonFavMessages.ToList();
        //            var updatedContextString = string.Empty;

        //            while (updatedContextList.Count > 0)
        //            {
        //                updatedContextString = JsonSerializer.Serialize(updatedContextList);

        //                if (updatedContextString.Length <= 6000)
        //                {
        //                    break;
        //                }

        //                updatedContextList.RemoveAt(0);
        //            }

        //            conversation.Context = updatedContextString;

        //            _db.Conversations.Update(conversation);
        //            await _db.SaveChangesAsync();

        //            return _mapper.Map<Conversation, ConversationDTO>(conversation);
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}

        public async Task<ConversationDTO> GetAndUpdateContext(int id)
        {
            try
            {
                var conversation = await _db.Conversations
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (conversation != null)
                {
                    var favMessages = conversation.Messages
                        .Where(message => message.IsFav)
                        .OrderByDescending(message => message.Timestamp)
                         .Take(6)
                        .Select(message => new
                        {
                            role = message.IsUserMessage ? "user" : "assistant",
                            content = message.Content
                        });

                    var recentNonFavMessages = conversation.Messages
                        .Where(message => !message.IsFav)
                        .OrderByDescending(message => message.Timestamp)
                         .Take(6)
                        .Select(message => new
                        {
                            role = message.IsUserMessage ? "user" : "assistant",
                            content = message.Content
                        });

                    var updatedContext = recentNonFavMessages.Concat(favMessages);

                    // Convert the updatedContext to string
                    var updatedContextString = JsonSerializer.Serialize(updatedContext);

                    // Check if the string length exceeds 6000 characters
                    if (updatedContextString.Length > 18000)
                    {
                        // Split the string into individual data sets
                        var datasets = updatedContext.Select(item => JsonSerializer.Serialize(item));

                        // Calculate the total character count
                        var totalCharacterCount = updatedContext.Sum(item => JsonSerializer.Serialize(item).Length);

                        // Initialize a variable to keep track of the current character count
                        var currentCharacterCount = 0;

                        // Iterate over the datasets and remove the first ones until the character count is below 6000
                        foreach (var dataset in datasets)
                        {
                            currentCharacterCount += dataset.Length;

                            if (currentCharacterCount > 18000)
                            {
                                break;
                            }

                            // Remove the dataset from the updatedContextString
                            updatedContextString = updatedContextString.Replace(dataset, "");
                        }
                    }

                    // Assign the updatedContextString to conversation context
                    conversation.Context = updatedContextString;

                    _db.Conversations.Update(conversation);
                    await _db.SaveChangesAsync();

                    return _mapper.Map<Conversation, ConversationDTO>(conversation);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
                    .Include(c => c.Messages.OrderByDescending(m => m.IsFav).ThenByDescending(m => m.Timestamp))
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
                    objFromDb.Context = objDTO.Context;
                    objFromDb.SystemMessage = objDTO.SystemMessage;
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
