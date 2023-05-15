using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Repository.IRepository;
using SharedServices.Models;
using System.Net.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogRepository.GetAll());
        }


        [HttpGet("{blogId}")]
        public async Task<IActionResult> Get(int? blogId)
        {
            if (blogId==null || blogId==0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }

            var blog = await _blogRepository.Get(blogId.Value);
            if (blog==null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status404NotFound
                });
            }
            //return Ok(await _productRepository.GetAll());
            return Ok(blog);
        }

        //[HttpPut("{blogId}")]
        //public async Task<IActionResult> Update(int blogId, [FromBody] BlogDTO updatedBlog)
        //{
        //    var blog = await _blogRepository.Get(blogId);
        //    if (blog == null)
        //    {
        //        return NotFound();
        //    }

        //    blog.Rating = updatedBlog.Rating;
        //    blog.Views = updatedBlog.Views;

        //    var updated = await _blogRepository.Update(blog);
        //    return Ok(updated);
        //}

    }
}

