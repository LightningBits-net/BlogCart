//using ChatGptWizard.Service.IService;
//using ChatGptWizard.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SQLite;

//namespace ChatGptWizard.Service
//{
//    public class PromptService : IPromptService
//    {
//        string _dbPath;
//        private SQLiteAsyncConnection _connection;
//        public PromptService(string dbPath)
//        {
//            _dbPath = dbPath;
//        }

//        private async Task InitAsync()
//        {
//            // Don't Create database if it exists
//            if (_connection != null)
//                return;
//            // Create database and Prompt Table
//            _connection = new SQLiteAsyncConnection(_dbPath);
//            await _connection.CreateTableAsync<Prompt>();
//        }

//        public async Task<List<Prompt>> GetPromptAsync()
//        {
//            await InitAsync();
//            return await _connection.Table<Prompt>().ToListAsync();
//        }

//        public async Task<Prompt> CreatePromptAsync(Prompt paramPrompt)
//        {
//            // Insert
//            await _connection.InsertAsync(paramPrompt);
//            // return the object with the
//            // auto incremented Id populated
//            return paramPrompt;
//        }

//        public async Task<Prompt> UpdatePromptAsync(Prompt paramPrompt)
//        {
//            // Update
//            await _connection.UpdateAsync(paramPrompt);
//            // Return the updated object
//            return paramPrompt;
//        }

//        public async Task<Prompt> DeletePromptAsync(Prompt paramPrompt)
//        {
//            // Delete
//            await _connection.DeleteAsync(paramPrompt);
//            return paramPrompt;
//        }
//    }
//}
