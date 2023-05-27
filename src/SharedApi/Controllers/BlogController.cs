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
            try
            {
                var blogs = await _blogRepository.GetAll();
                if (blogs == null || !blogs.Any())
                {
                    return NotFound(new ErrorModelDTO()
                    {
                        ErrorMessage = "No blogs found.",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                var publishedBlogs = blogs.Where(blog => blog.Status == "Published");
                return Ok(publishedBlogs);
            }
            catch (Exception ex)
            {
                // Log the exception or perform any necessary error handling
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModelDTO()
                {
                    ErrorMessage = "An error occurred while retrieving the blogs.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }


        [HttpGet("{blogId}")]
        public async Task<IActionResult> Get(int? blogId)
        {
            if (blogId == null || blogId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var blog = await _blogRepository.Get(blogId.Value);
                if (blog == null)
                {
                    return NotFound(new ErrorModelDTO()
                    {
                        ErrorMessage = "Blog not found.",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                if (blog.Status != "Published")
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorModelDTO()
                    {
                        ErrorMessage = "Blog is not published.",
                        StatusCode = StatusCodes.Status403Forbidden
                    });
                }

                return Ok(blog);
            }
            catch (Exception ex)
            {
                // Log the exception or perform any necessary error handling
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModelDTO()
                {
                    ErrorMessage = "An error occurred while retrieving the blog.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }
    }
}

