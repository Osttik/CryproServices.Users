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
    }
}
