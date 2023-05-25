//using ChatGptWizard.Data;
//using ChatGptWizard.Service.IService;
//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChatGptWizard.Service
//{
//    public class TopicService : ITopicService
//    {
//        string _dbPath;
//        private SQLiteAsyncConnection _connection;
//        public TopicService(string dbPath)
//        {
//            _dbPath = dbPath;
//        }

//        private async Task InitAsync()
//        {
//            // Don't Create database if it exists
//            if (_connection != null)
//                return;
//            // Create database and Topic Table
//            _connection = new SQLiteAsyncConnection(_dbPath);
//            await _connection.CreateTableAsync<Topic>();
//        }

//        public async Task<List<Topic>> GetTopicAsync()
//        {
//            await InitAsync();
//            return await _connection.Table<Topic>().ToListAsync();
//        }

//        public async Task<Topic> CreateTopicAsync(Topic paramTopic)
//        {
//            // Insert
//            await _connection.InsertAsync(paramTopic);
//            // return the object with the
//            // auto incremented Id populated
//            return paramTopic;
//        }

//        public async Task<Topic> UpdateTopicAsync(Topic paramTopic)
//        {
//            // Update
//            await _connection.UpdateAsync(paramTopic);
//            // Return the updated object
//            return paramTopic;
//        }

//        public async Task<Topic> DeleteTopicAsync(Topic paramTopic)
//        {
//            // Delete
//            await _connection.DeleteAsync(paramTopic);
//            return paramTopic;
//        }
//    }
//}
