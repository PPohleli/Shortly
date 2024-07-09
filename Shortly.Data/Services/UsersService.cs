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

        public User GetUrlById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public List<User> GetUsers()
        {
            var allUsers = _context.Users.Include(n => n.Urls).ToList();
            return allUsers;
        }
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Update(int id, User user)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDb != null)
            {
                userDb.Email = user.Email;
                userDb.FullName = user.FullName;

                _context.Update(user); // can remove this line - just kept for readability.
                _context.SaveChanges();
            }

            return userDb;
        }
        public void Delete(int id)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userDb != null)
            {
                _context.Users.Remove(userDb);
                _context.SaveChanges();
            }
        }

        
    }
}
