// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> Create(ProductDTO objDTO);

        public Task<ProductDTO> Update(ProductDTO objDTO);

        public Task<int> Delete(int id);

        public Task<ProductDTO> Get(int id);

        public Task<IEnumerable<ProductDTO>> GetAll();

    }
}

