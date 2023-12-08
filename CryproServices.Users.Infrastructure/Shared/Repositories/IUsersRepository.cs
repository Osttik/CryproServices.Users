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
    }
}
