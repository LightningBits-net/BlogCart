// LightningBits
using System;
using SharedServices.Respository.IRespository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;
using Microsoft.EntityFrameworkCore;
using SharedServices.Models;

namespace SharedServices.Respository
{
    public class ProductPriceRespository : IProductPriceRespository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductPriceRespository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var obj = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);

            var addedobj = _db.ECommerceProductPrices.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ECommerceProductPrices.FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
                _db.ECommerceProductPrices.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(int id)
        {
            var obj = await _db.ECommerceProductPrices.FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
               return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id=null)
        {
            if(id!=null && id>0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>
                (_db.ECommerceProductPrices.Where(u=>u.ProductId==id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ECommerceProductPrices);
            }

        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var objFromDb = await _db.ECommerceProductPrices.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if(objFromDb!=null)
            {
                objFromDb.Price = objDTO.Price;
                objFromDb.MyProperty = objDTO.MyProperty;
                objFromDb.ProductId = objDTO.ProductId;
                objFromDb.Size = objDTO.Size;
                _db.ECommerceProductPrices.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

