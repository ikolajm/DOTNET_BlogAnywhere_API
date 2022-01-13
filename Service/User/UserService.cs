using System.Security.Claims;
using BlogAnywhereNET.Models;
using BlogAnywhereNET.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace BlogAnywhereNET.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DbSetup _context;
        public UserService(DbSetup context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            } else {
                return false;
            }
        }

        public async Task<User> EditUser(UserForEdit user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.UserId);
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.Bio = user.Bio;
            _context.SaveChanges();

            return existingUser;
        }
    }
}