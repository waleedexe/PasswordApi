using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi
{
    public class PasswordGenerator
    {
        public string GenerateUserPassword(string userId)
        {
            // Create a random password that is only returned once. use it to create a hash to save in DB.
            var userPassword = GetRandomPassword();

            return userPassword;
        }

        private string GetRandomPassword()
        {
            var passwordBytes = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(passwordBytes);

            return Convert.ToBase64String(passwordBytes);
        }
    }
}
