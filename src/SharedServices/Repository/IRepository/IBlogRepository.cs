// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
	public interface IBlogRepository
	{
        public Task<BlogDTO> Create(BlogDTO objDTO);

        public Task<BlogDTO> Update(BlogDTO objDTO);

        public Task<int> Delete(int id);

        public Task<BlogDTO> Get(int id);

        public Task<IEnumerable<BlogDTO>> GetAll();
    }
}

