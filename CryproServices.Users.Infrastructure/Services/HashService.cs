using CryproServices.Users.Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryproServices.Users.Infrastructure.Services
{
    public class HashService: IHashService
    {
        public string GetHash(string key)
        {
            byte[]? hash;
            using (MD5 md5 = MD5.Create())
            {
                md5.Initialize();
                md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash = md5.Hash;
            }
            return Encoding.UTF8.GetString(hash!);
        }
    }
}
