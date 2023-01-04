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
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public ToDoItemRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ToDoItemDTO> Create(ToDoItemDTO objDTO)
        {
            var obj = _mapper.Map<ToDoItemDTO, ToDoItem>(objDTO);
            obj.DateCreated = DateTime.Now;

            var addedobj = _db.ToDoItems.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<ToDoItem, ToDoItemDTO>(addedobj.Entity);
        }
    

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ToDoItems.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.ToDoItems.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

      

        public async Task<ToDoItemDTO> Get(int id)
        {
            var obj = await _db.ToDoItems.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<ToDoItem, ToDoItemDTO>(obj);
            }
            return new ToDoItemDTO();

        }

    

        public Task<IEnumerable<ToDoItemDTO>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<ToDoItem>, IEnumerable<ToDoItemDTO>>(_db.ToDoItems));
        }



        public async Task<ToDoItemDTO> Update(ToDoItemDTO objDTO)
        {
            var objFromDb = await _db.ToDoItems.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.ToDo = objDTO.ToDo;
                objFromDb.DateCreated = objDTO.DateCreated;
                objFromDb.Completed = objDTO.Completed;
                objFromDb.Comment = objDTO.Comment;
                _db.ToDoItems.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<ToDoItem, ToDoItemDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

