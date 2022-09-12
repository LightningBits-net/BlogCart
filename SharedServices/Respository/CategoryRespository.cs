// LightningBits
using System;
using SharedServices.Respository.IRespository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;
using Microsoft.EntityFrameworkCore;

namespace SharedServices.Respository
{
    public class CategoryRespository : ICategoryRespository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public CategoryRespository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            var obj = _mapper.Map<CategoryDTO, Category>(objDTO);
            obj.CreateDate = DateTime.Now;

            var addedobj = _db.ECommerceCategories.Add(obj);
           await _db.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ECommerceCategories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
                _db.ECommerceCategories.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var obj = await _db.ECommerceCategories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj!=null)
            {
               return _mapper.Map<Category, CategoryDTO>(obj);
            }
            return new CategoryDTO();

        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.ECommerceCategories);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var objFromDb = await _db.ECommerceCategories.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name=objDTO.Name;
                _db.ECommerceCategories.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

