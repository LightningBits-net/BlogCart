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
    public class ProductRespository : IProductRespository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public ProductRespository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(objDTO);

            var addedobj = _db.ECommerceProducts.Add(obj);
           await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ECommerceProducts.FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
                _db.ECommerceProducts.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _db.ECommerceProducts.Include(u=>u.Category).Include(u=>u.ECommerceProductPrices).FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
               return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();

        }

      
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            //return Task.FromResult(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.ECommerceProducts.Include(u => u.Category)));
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.ECommerceProducts.Include(u => u.Category).Include(u=>u.Category).Include(u=>u.ECommerceProductPrices));

        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.ECommerceProducts.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name=objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                objFromDb.Color = objDTO.Color;
                objFromDb.ShopFavorites = objDTO.ShopFavorites;
                objFromDb.CustomerFavorites = objDTO.CustomerFavorites;
                _db.ECommerceProducts.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

