using CryptoServices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Models
{
    public class LoggedUserModel
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
