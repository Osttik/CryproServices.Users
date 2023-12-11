using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Shared.Services
{
    public interface IJWTService
    {
        public string GenerateJwtToken(string userName, string secretKey);
    }
}
