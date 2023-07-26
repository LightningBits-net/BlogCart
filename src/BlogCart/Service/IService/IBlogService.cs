using System;
using SharedServices.Models;

namespace BlogCart.Service.IService
{
    public interface IBlogService
    {
        public Task<IEnumerable<BlogDTO>> GetAll();
        public Task<BlogDTO> Get(int blogId);
        public Task<BlogDTO> Update(int blogId, int rating, int views);
        Task<IEnumerable<BlogDTO>> GetBlogsByDomain(string domain);
    }
}

