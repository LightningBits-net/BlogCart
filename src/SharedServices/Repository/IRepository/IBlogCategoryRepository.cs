// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
	public interface IBlogCategoryRepository
	{
        public Task<BlogCategoryDTO> Create(BlogCategoryDTO objDTO);

        public Task<BlogCategoryDTO> Update(BlogCategoryDTO objDTO);

        public Task<int> Delete(int id);

        public Task<BlogCategoryDTO> Get(int id);

        public Task<IEnumerable<BlogCategoryDTO>> GetAll();
    }
}

