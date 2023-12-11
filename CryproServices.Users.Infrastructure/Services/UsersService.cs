using CryproServices.Users.Infrastructure.Models;
using CryproServices.Users.Infrastructure.Shared.Repositories;
using CryproServices.Users.Infrastructure.Shared.Services;
using CryptoServices.Data;
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
        protected readonly IHashService _hashService;

        public UsersService(IUsersRepository repository, IHashService hashService)
        {
            _repository = repository;
            _hashService = hashService;
        }

        public Task<List<User>> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public Task<User?> UpdateUser(UpdateUserModel updateUser)
        {
            var user = _repository.GetUserById(updateUser.Id);
            if (user is null) return null;

            if (updateUser.NewName != string.Empty) user.Name = updateUser.NewName;
            if (updateUser.NewPassword != string.Empty) user.PasswordHash = _hashService.GetHash(updateUser.NewPassword);

            return _repository.UpdateUser(user);
        }

        public Task<User> AddUser(NewUserModel newUser)
        {
            var userEntity = new User()
            {
                PasswordHash = _hashService.GetHash(newUser.Password),
                LoginHash = _hashService.GetHash(newUser.Login),
                Name = newUser.Name
            };

            return _repository.AddUser(userEntity);
        }

        public Task<bool> RemoveUser(Guid id)
        {
            return _repository.RemoveUser(id);
        }

        public Task<User?> GetUserByLoginAndPassword(LoginUserModel model)
        {
            return _repository.GetUserByLoginAndPassword(_hashService.GetHash(model.Login), _hashService.GetHash(model.Password));
        }
    }
}
