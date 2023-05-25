//using ChatGptWizard.Data;
//using ChatGptWizard.Service.IService;
//using SQLite;

//namespace ChatGptWizard.Service
//{
//    public class MessageService : IMessageService
//    {
//        string _dbPath;
//        private SQLiteAsyncConnection _connection;
//        public MessageService(string dbPath)
//        {
//            _dbPath = dbPath;
//        }
//        private async Task InitAsync()
//        {
//            // Don't Create database if it exists
//            if (_connection != null)
//                return;
//            // Create database and Message Table
//            _connection = new SQLiteAsyncConnection(_dbPath);
//            await _connection.CreateTableAsync<Message>();
//        }
//        public async Task<List<Message>> GetMessageAsync()
//        {
//            await InitAsync();
//            return await _connection.Table<Message>().ToListAsync();
//        }
//        public async Task<Message> CreateMessageAsync(Message paramMessage)
//        {
//            // Insert
//            await _connection.InsertAsync(paramMessage);
//            // return the object with the
//            // auto incremented Id populated
//            return paramMessage;
//        }
//        public async Task<Message> UpdateMessageAsync(Message paramMessage)
//        {
//            // Update
//            await _connection.UpdateAsync(paramMessage);
//            // Return the updated object
//            return paramMessage;
//        }
//        public async Task<Message> DeleteMessageAsync(Message paramMessage)
//        {
//            // Delete
//            await _connection.DeleteAsync(paramMessage);
//            return paramMessage;
//        }
//    }
//}
