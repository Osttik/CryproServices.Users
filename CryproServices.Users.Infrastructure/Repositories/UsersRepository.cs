using CryproServices.Users.Infrastructure.Shared.Repositories;
using CryptoServices.Data;
using CryptoServices.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        protected readonly DBContext _context;

        public UsersRepository(DBContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User?> GetUserByLoginAndPassword(string login, string password)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.LoginHash == login && u.PasswordHash == password);
        }

        public async Task<User?> UpdateUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        } 

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> RemoveUser(Guid id)
        {
            var user = _context.Users.First(u => u.Id == id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
