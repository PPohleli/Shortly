using Microsoft.EntityFrameworkCore;
using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Services
{
    public class UsersService : IUsersService
    {
        private AppDbContext _context;
        public UsersService(AppDbContext context) 
        {
            this._context = context;
        }

        public async Task<AppUser> GetUrlByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            var allUsers = await _context.Users.Include(n => n.Urls).ToListAsync();
            return allUsers;
        }
        public async Task<AppUser> AddAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<AppUser> UpdateAsync(int id, AppUser user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDb != null)
            {
                userDb.Email = user.Email;
                userDb.FullName = user.FullName;

                //await _context.Update(user); // can remove this line - just kept for readability.
                await _context.SaveChangesAsync();
            }

            return userDb;
        }
        public async Task DeleteAsync(int id)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userDb != null)
            {
                _context.Users.Remove(userDb);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
