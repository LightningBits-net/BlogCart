// LightningBits
using System;
using SharedServices.Repository.IRepository;
using AutoMapper;
using SharedServices.Data;
using SharedServices;
using Microsoft.EntityFrameworkCore;
using SharedServices.Models;
using System.Reflection.Metadata;

namespace SharedServices.Repository
{
    public class ClientRepository : IClientRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public ClientRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ClientFrontendDTO> GetClientFrontendData(int clientId)
        {
            var client = await _db.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
            if (client != null)
            {
                return _mapper.Map<Client, ClientFrontendDTO>(client);
            }
            return null;
        }

        public async Task<ClientDTO> Create(ClientDTO objDTO)
        {
            var obj = _mapper.Map<ClientDTO, Client>(objDTO);
            obj.DateCreated = DateTime.Now;

            var addedobj = _db.Clients.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Client, ClientDTO>(addedobj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
            if (obj != null)
            {
                _db.Clients.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        //public async Task<ClientDTO> Get(int id)
        //{
        //    var client = await _db.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
        //    if (client != null)
        //    {
        //        client.Counter += 0.5f; // Increment by 0.5
        //        _db.Clients.Update(client);
        //        await _db.SaveChangesAsync();
        //        return _mapper.Map<Client, ClientDTO>(client);
        //    }
        //    return new ClientDTO();
        //}

        public async Task<ClientDTO> Get(int id)
        {
            var client = await _db.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
            if (client != null)
            {
                return _mapper.Map<Client, ClientDTO>(client);
            }
            return new ClientDTO();
        }


        public Task<IEnumerable<ClientDTO>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(_db.Clients));
        }

        public async Task<ClientDTO> Update(ClientDTO objDTO)
        {
            var objFromDb = await _db.Clients.FirstOrDefaultAsync(u => u.ClientId == objDTO.ClientId);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Address = objDTO.Address;
                objFromDb.DomainName = objDTO.DomainName;
                objFromDb.DateCreated = objDTO.DateCreated;
                objFromDb.Description = objDTO.Description;
                objFromDb.Email = objDTO.Email;
                objFromDb.Counter = objDTO.Counter;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.IsActive = objDTO.IsActive;
                objFromDb.BillingCycle = objDTO.BillingCycle;
                objFromDb.BillingAmount = objDTO.BillingAmount;
                objFromDb.BillingStartDate = objDTO.BillingStartDate;
                objFromDb.BillingEndDate = objDTO.BillingEndDate;
                _db.Clients.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Client, ClientDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

