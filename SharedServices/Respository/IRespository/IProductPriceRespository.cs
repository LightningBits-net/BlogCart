// LightningBits
using System;
using SharedServices.Data;
using SharedServices.Models;

namespace SharedServices.Respository.IRespository
{
    public interface IProductPriceRespository
    {
        public Task<ProductPriceDTO> Create(ProductPriceDTO objDTO);

        public Task<ProductPriceDTO> Update(ProductPriceDTO objDTO);

        public Task<int> Delete(int id);

        public Task<ProductPriceDTO> Get(int id);

        public Task<IEnumerable<ProductPriceDTO>> GetAll(int? id=null);
    }
}

