using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Models
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public string NewPassword { get; set; }
    }
}
