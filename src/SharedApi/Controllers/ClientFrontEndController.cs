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
            return Ok(await _clientRepository.GetAll());
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> Get(int? clientId)
        {
            if (clientId == null || clientId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var clientData = await _clientRepository.Get(clientId.Value);
            if (clientData == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            clientData.Counter += 1; // Increment by 1
            var updatedClientData = await _clientRepository.Update(clientData);

            return Ok(updatedClientData);
        }

        //[HttpGet("{clientId}")]
        //public async Task<IActionResult> Get(int? clientId)
        //{
        //    if (clientId == null || clientId == 0)
        //    {
        //        return BadRequest(new ErrorModelDTO()
        //        {
        //            ErrorMessage = "Invalid Id",
        //            StatusCode = StatusCodes.Status400BadRequest
        //        });
        //    }

        //    var clientData = await _clientRepository.Get(clientId.Value);
        //    if (clientData == null)
        //    {
        //        return BadRequest(new ErrorModelDTO()
        //        {
        //            ErrorMessage = "Invalid Id",
        //            StatusCode = StatusCodes.Status404NotFound
        //        });
        //    }

        //    return Ok(clientData);
        //}

    }
}
