using Microsoft.EntityFrameworkCore;
using SehirRehberii.API.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SehirRehberii.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

                return user;
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                return null;
            }
            return user;
        }


        public async Task<User> Register(User user, string password)
        {
            

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<bool> UserExists(string username )
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

    
    }
}
