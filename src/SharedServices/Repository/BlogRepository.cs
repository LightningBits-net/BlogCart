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
	public class BlogRepository : IBlogRepository
	{
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public BlogRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //public async Task<BlogDTO> Create(BlogDTO objDTO)
        //{
        //    var obj = _mapper.Map<BlogDTO, Blog>(objDTO);

        //    var addedobj = _db.Blogs.Add(obj);
        //    await _db.SaveChangesAsync();

        //    return _mapper.Map<Blog, BlogDTO>(addedobj.Entity);
        //}
        public async Task<BlogDTO> Create(BlogDTO objDTO)
        {
            var obj = _mapper.Map<BlogDTO, Blog>(objDTO);
            obj.DateCreated = DateTime.Now;
            obj.LastUpdated = DateTime.Now;

            var addedobj = _db.Blogs.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Blog, BlogDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Blogs.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Blogs.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<BlogDTO> Get(int id)
        {
            var blog = await _db.Blogs.Include(u => u.BlogCategory).FirstOrDefaultAsync(u => u.Id == id);
            if (blog != null)
            {
                blog.Views += 0.5f; // Increment by 0.5
                _db.Blogs.Update(blog);
                await _db.SaveChangesAsync();
                return _mapper.Map<Blog, BlogDTO>(blog);
            }
            return new BlogDTO();
        }

        public Task<IEnumerable<BlogDTO>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<Blog>, IEnumerable<BlogDTO>>(_db.Blogs.Include(u => u.BlogCategory)));
        }

        public async Task<BlogDTO> Update(BlogDTO objDTO)
        {
            var objFromDb = await _db.Blogs.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.LastUpdated = DateTime.Now;
                objFromDb.Description = objDTO.Description;
                objFromDb.Body = objDTO.Body;
                objFromDb.Author = objDTO.Author;
                objFromDb.Featured = objDTO.Featured;
                objFromDb.BlogFavorite = objDTO.BlogFavorite;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.BlogCategoryId = objDTO.BlogCategoryId;
                objFromDb.Views = objDTO.Views;
                objFromDb.DateCreated = objDTO.DateCreated;
                objFromDb.Rating = objDTO.Rating;
                objFromDb.Status = objDTO.Status;
                objFromDb.ClientId = objDTO.ClientId;
                //objFromDb.LastUpdated = objDTO.LastUpdated;

                _db.Blogs.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Blog, BlogDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

