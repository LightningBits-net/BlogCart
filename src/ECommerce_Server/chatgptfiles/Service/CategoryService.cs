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
//    public class CategoryService : ICategoryService
//    {
//        string _dbPath;
//        private SQLiteAsyncConnection _connection;
//        public CategoryService(string dbPath)
//        {
//            _dbPath = dbPath;
//        }

//        private async Task InitAsync()
//        {
//            // Don't Create database if it exists
//            if (_connection != null)
//                return;
//            // Create database and Category Table
//            _connection = new SQLiteAsyncConnection(_dbPath);
//            await _connection.CreateTableAsync<Category>();
//        }

//        public async Task<List<Category>> GetCategoryAsync()
//        {
//            await InitAsync();
//            return await _connection.Table<Category>().ToListAsync();
//        }

//        public async Task<Category> CreateCategoryAsync(Category paramCategory)
//        {
//            // Insert
//            await _connection.InsertAsync(paramCategory);
//            // return the object with the
//            // auto incremented Id populated
//            return paramCategory;
//        }

//        public async Task<Category> UpdateCategoryAsync(Category paramCategory)
//        {
//            // Update
//            await _connection.UpdateAsync(paramCategory);
//            // Return the updated object
//            return paramCategory;
//        }

//        public async Task<Category> DeleteCategoryAsync(Category paramCategory)
//        {
//            // Delete
//            await _connection.DeleteAsync(paramCategory);
//            return paramCategory;
//        }
//    }
//}
