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
    public class UsersRepository: IUsersRepository
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
    }
}
