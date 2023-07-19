using System;
using ECommerce_Server.Service.IService;
using System.Security.Claims;
using SharedServices.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Server.Service
{
    public class ClientService : IClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public ClientService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<int> GetClientIdAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                var client = await _context.Clients.SingleOrDefaultAsync(c => c.UserId == userIdClaim.Value);
                if (client != null)
                {
                    return client.ClientId;
                }
            }

            return 0;
        }
    }
}

