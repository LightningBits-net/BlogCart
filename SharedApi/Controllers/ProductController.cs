using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Respository.IRespository;
using SharedServices.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharedApi.Controllers
{
    [Route("api/[controller])")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRespository _productRespository;

        public ProductController(IProductRespository productRespository)
        {
            _productRespository = productRespository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRespository.GetAll());
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            if (productId==null || productId==0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }

            var product = _productRespository.Get(productId.Value);
            if (product==null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status404NotFound
                });
            }
            //return Ok(await _productRespository.GetAll());
            return Ok(product);
        }
    }
}

