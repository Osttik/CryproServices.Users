using CryptoServices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Shared.Repositories
{
    public interface IUsersRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> UpdateUser(User user);
        public Task<User> AddUser(User user);
        public Task<bool> RemoveUser(Guid id);
        public User? GetUserById(Guid id);
        public Task<User?> GetUserByLoginAndPassword(string login, string password);
    }
}
