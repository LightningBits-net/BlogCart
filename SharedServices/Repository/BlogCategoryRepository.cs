// LightningBits
using System;
using SharedServices.Repository.IRepository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;
using Microsoft.EntityFrameworkCore;
using SharedServices.Models;

namespace SharedServices.Repository
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public BlogCategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BlogCategoryDTO> Create(BlogCategoryDTO objDTO)
        {
            var obj = _mapper.Map<BlogCategoryDTO, BlogCategory>(objDTO);
            obj.CreateDate = DateTime.Now;

            var addedobj = _db.BlogCategories.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<BlogCategory, BlogCategoryDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.BlogCategories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.BlogCategories.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<BlogCategoryDTO> Get(int id)
        {
            var obj = await _db.BlogCategories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<BlogCategory, BlogCategoryDTO>(obj);
            }
            return new BlogCategoryDTO();

        }

        public Task<IEnumerable<BlogCategoryDTO>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<BlogCategory>, IEnumerable<BlogCategoryDTO>>(_db.BlogCategories));
        }

        public async Task<BlogCategoryDTO> Update(BlogCategoryDTO objDTO)
        {
            var objFromDb = await _db.BlogCategories.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                _db.BlogCategories.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<BlogCategory, BlogCategoryDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

