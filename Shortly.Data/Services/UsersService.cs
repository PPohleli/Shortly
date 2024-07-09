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
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            var allUsers = _context.Users.Include(n => n.Urls).ToList();
            return allUsers;
        }
        public User Add(User user)
        {
            throw new NotImplementedException();
        }
        public User Update(int id, User user)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
