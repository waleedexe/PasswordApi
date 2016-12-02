using Interfaces.Service;
using PasswordApi.Entities;
using System;
using System.Security.Cryptography;

namespace PasswordApi.Service
{
    public class Rfc2898CryptoService: ICryptoService
    {
        // These can move to configuration.
        private const int Iterations = 1000;
        private const int HashSize = 20;
        private const int SaltSize = 16;

        public HashInfo HashPassword(string password)
        {
            // Random salt.
            var salt = GenerateRandomSalt();

            // Create hash for the password.
            var hashInfo = HashPassword(password, Convert.ToBase64String(salt));

            return hashInfo;
        }

        public HashInfo HashPassword(string password, string hashSalt)
        {
            // Use the existing salt to compute the hash for the password.
            var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(hashSalt), Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            var hashInfo = new HashInfo
            {
                HashedPassword = Convert.ToBase64String(hash),
                HashSalt = hashSalt,
            };

            return hashInfo;
        }

        public byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }
    }
}
