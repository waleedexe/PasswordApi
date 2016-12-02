using Interfaces.Data;
using Interfaces.Service;
using PasswordApi.Entities;
using System;

namespace PasswordApi.Service
{
    public class PasswordService : IPasswordService
    {
        private IPasswordRepository _passwordRepository;
        private IPasswordExpiryService _passwordExpiryService;
        private ICryptoService _cryptoService;

        public PasswordService(IPasswordRepository passwordRepository, IPasswordExpiryService passwordExpiryService, ICryptoService cryptoService)
        {
            _passwordRepository = passwordRepository;
            _passwordExpiryService = passwordExpiryService;
            _cryptoService = cryptoService;
        }

        public string GeneratePassword(string userId)
        {
            // Create a random password that is only returned once. use it to create a hash to save in DB.
            var userPassword = GetRandomPassword();
            var hashOk = CreatePasswordHash(userId, userPassword);

            return (hashOk) ? userPassword : string.Empty;
        }

        public bool IsPasswordValid(string userId, string password)
        {
            // Get user entry from storage.
            var userPasswordInfo = _passwordRepository.GetUserInfo(userId);

            // has the password expired?
            if (_passwordExpiryService.HasPasswordExpired(userPasswordInfo.Expiry))
                return false;

            // need a crypto class to handle different types of algorithms.
            var newHash = _cryptoService.HashPassword(password, userPasswordInfo.HashSalt);
            var areEqual = newHash.HashedPassword.Equals(userPasswordInfo.HashedPassword);

            return areEqual;
        }

        private string GetRandomPassword()
        {
            var passwordBytes = _cryptoService.GenerateRandomSalt();
            return Convert.ToBase64String(passwordBytes);
        }

        private bool CreatePasswordHash(string userId, string password)
        {
            // Compute hash
            var passwordHash = _cryptoService.HashPassword(password);

            // Save hash and salt. Only use salt to re-compute hash.
            var userInfo = new UserPasswordInfo
            {
                UserId = userId,
                HashedPassword = passwordHash.HashedPassword,
                HashSalt = passwordHash.HashSalt,
                Expiry = _passwordExpiryService.GenerateExpiryDate(),
            };

            return _passwordRepository.SaveUserInfo(userInfo);
        }

    }
}
