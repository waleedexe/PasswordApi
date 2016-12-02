using PasswordApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Service
{
    public interface ICryptoService
    {
        HashInfo HashPassword(string password);
        HashInfo HashPassword(string password, string hashSalt);
        byte[] GenerateRandomSalt();
    }
}
