using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using AdvancedProgramming_Lesson4.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming_Lesson4.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public string Message { get; private set; } = "PageModel in C#";

        public UsersModel(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGetAsync()
        {
            var isAdmin = _httpContextAccessor.HttpContext.User.Identity.Name == "admin@email.com";

            if (!isAdmin)
            {
                return;
            }

            Users = await _context.Users
                .ToListAsync();
        }
    }
}
