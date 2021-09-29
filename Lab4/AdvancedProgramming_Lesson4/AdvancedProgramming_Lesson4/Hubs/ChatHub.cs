using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AdvancedProgramming_Lesson4.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace AdvancedProgramming_Lesson4.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendMessage(string user, string message)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _context.Messages.AddAsync(new Message
            {
                Content = message,
                User = user,
                UserId = Guid.Parse(userId),
                Id = Guid.NewGuid()
            });
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
