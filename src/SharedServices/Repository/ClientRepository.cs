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

        public async Task<ClientDTO> Get(int id)
        {
            var obj = await _db.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
            if (obj != null)
            {
                return _mapper.Map<Client, ClientDTO>(obj);
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
                objFromDb.DomainName = objDTO.DomainName;
                objFromDb.DateCreated = objDTO.DateCreated;
                objFromDb.Email = objDTO.Email;
                objFromDb.Counter = objDTO.Counter;
                objFromDb.IsActive = objDTO.IsActive;
                objFromDb.BillingStartDate = objDTO.BillingStartDate;
                objFromDb.BillingEndDate = objDTO.BillingEndDate;
                objFromDb.BillingAmount = objDTO.BillingAmount;
                objFromDb.BillingCycle = objDTO.BillingCycle;
                await _db.SaveChangesAsync();
                return _mapper.Map<Client, ClientDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}

