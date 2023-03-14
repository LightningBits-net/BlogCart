// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface IToDoItemRepository
    {
        public Task<ToDoItemDTO> Create(ToDoItemDTO objDTO);

        public Task<ToDoItemDTO> Update(ToDoItemDTO objDTO);

        public Task<int> Delete(int id);

        public Task<ToDoItemDTO> Get(int id);

        public Task<IEnumerable<ToDoItemDTO>> GetAll();
    }
}

