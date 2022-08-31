// LightningBits
using System;
using SharedServices.Respository.IRespository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;

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

        public CategoryDTO Create(CategoryDTO objDTO)
        {
            var obj = _mapper.Map<CategoryDTO, Category>(objDTO);
            obj.CreateDate = DateTime.Now;

            var addedobj = _db.ECommerceCategories.Add(obj);
            _db.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(addedobj.Entity);
        }

        public int Delete(int id)
        {
            var obj = _db.ECommerceCategories.FirstOrDefault(u => u.Id == id);
            if (obj!=null)
            {
                _db.ECommerceCategories.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;
        }

        public CategoryDTO Get(int id)
        {
            var obj = _db.ECommerceCategories.FirstOrDefault(u => u.Id == id);
            if (obj!=null)
            {
               return _mapper.Map<Category, CategoryDTO>(obj);
            }
            return new CategoryDTO();

        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.ECommerceCategories);
        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            var objFromDb = _db.ECommerceCategories.FirstOrDefault(u => u.Id == objDTO.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name=objDTO.Name;
                _db.ECommerceCategories.Update(objFromDb);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

