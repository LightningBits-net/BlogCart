using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Repository.IRepository;
using SharedServices.Models;


namespace SharedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientFrontendController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientFrontendController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientRepository.GetAll();
            var frontendData = new List<ClientFrontendDTO>();
            foreach (var client in clients)
            {
                frontendData.Add(new ClientFrontendDTO
                {
                    ClientId = client.ClientId,
                    DomainName = client.DomainName,
                    Counter = client.Counter
                });
            }
            return Ok(frontendData);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> Get(int clientId)
        {
            var clientData = await _clientRepository.GetClientFrontendData(clientId);
            if (clientData == null)
            {
                return NotFound();
            }
            return Ok(clientData);
        }

    }
}
