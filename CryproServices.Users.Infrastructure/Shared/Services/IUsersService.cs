using CryproServices.Users.Infrastructure.Models;
using CryptoServices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Shared.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> UpdateUser(UpdateUserModel updateUser);
        public Task<User> AddUser(NewUserModel newUser);
        public Task<bool> RemoveUser(Guid id);
        public Task<User?> GetUserByLoginAndPassword(LoginUserModel model);
    }
}
