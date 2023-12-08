using CryproServices.Users.Infrastructure.Shared.Repositories;
using CryproServices.Users.Infrastructure.Shared.Services;
using CryptoServices.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Services
{
    public class UsersService: IUsersService
    {
        protected readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }


        public Task<List<User>> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }
    }
}
