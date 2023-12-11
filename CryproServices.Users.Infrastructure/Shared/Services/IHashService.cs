using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Shared.Services
{
    public interface IHashService
    {
        public string GetHash(string key);
    }
}
