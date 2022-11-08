// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> Create(CategoryDTO objDTO);

        public Task<CategoryDTO> Update(CategoryDTO objDTO);

        public Task<int> Delete(int id);

        public Task<CategoryDTO> Get(int id);

        public Task<IEnumerable<CategoryDTO>> GetAll();

    }
}

