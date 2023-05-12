using System;
using BlogCart.Service.IService;
using Newtonsoft.Json;
using SharedServices.Data;
using SharedServices.Models;

namespace BlogCart.Service
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration? _configuration;
        private string? BaseServerUrl;

        public BlogService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
      
        public async Task<BlogDTO> Get(int blogId)
        {
            var response = await _httpClient.GetAsync($"/Api/blog/{blogId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var blog = JsonConvert.DeserializeObject<BlogDTO>(content);
                blog.ImageUrl = BaseServerUrl + blog.ImageUrl;
                return blog;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        //public Task<IEnumerable<BlogDTO>> GetAll()
        //{
        //    return Task.FromResult(_mapper.Map<IEnumerable<Blog>, IEnumerable<BlogDTO>>(_db.Blogs.Include(u => u.BlogCategory)));
        //}
        public async Task<IEnumerable<BlogDTO>> GetAll()
        {

            var response = await _httpClient.GetAsync("/Api/Blog");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var blogs = JsonConvert.DeserializeObject<IEnumerable<BlogDTO>>(content);
                return blogs;
            }
            return new List<BlogDTO>();
        }

        public async Task<BlogDTO> Update(int blogId, int rating, int views)
        {
            var updatedData = new
            {
                Rating = rating,
                Views = views
            };

            var response = await _httpClient.PutAsJsonAsync($"/Api/blog/{blogId}", updatedData);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var updatedBlog = JsonConvert.DeserializeObject<BlogDTO>(content);
            return updatedBlog;
        }


    }
}

