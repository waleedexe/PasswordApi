using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordApi.Entities;
using FluentAssertions;
using PasswordApi.Service;

namespace PasswordService.Tests
{
    [TestClass]
    public class PasswordServiceTests
    {
        Mock<Interfaces.Data.IPasswordRepository> _passwordRepoMock;
        Mock<Interfaces.Service.IPasswordExpiryService> _passwordExpiryMock;
        Mock<Interfaces.Service.ICryptoService> _cryptoMock;
        PasswordApi.Service.PasswordService _service;

        [TestInitialize]
        public void Init()
        {
            _passwordRepoMock = new Mock<Interfaces.Data.IPasswordRepository>();
            _passwordExpiryMock = new Mock<Interfaces.Service.IPasswordExpiryService>();
            _cryptoMock = new Mock<Interfaces.Service.ICryptoService>();
            //_service = new PasswordApi.Service.PasswordService(_passwordRepoMock.Object, _passwordExpiryMock.Object, _cryptoMock.Object);
        }

        [TestMethod]
        public void GeneratePassword()
        {
            // Arrange
            _passwordRepoMock.Setup(m => m.SaveUserInfo(It.IsAny<UserPasswordInfo>()))
                .Returns(true);
            _service = new PasswordApi.Service.PasswordService(_passwordRepoMock.Object, _passwordExpiryMock.Object, new Rfc2898CryptoService());
            var sampleUserId = "user1";

            // Act
            var password = _service.GeneratePassword(sampleUserId);

            // Assert
            password.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetRandomPassword()
        {
            // Arrange
            _passwordRepoMock.Setup(m => m.SaveUserInfo(It.IsAny<UserPasswordInfo>()))
               .Returns(true);
            _service = new PasswordApi.Service.PasswordService(_passwordRepoMock.Object, _passwordExpiryMock.Object, new Rfc2898CryptoService());
            var sampleUserId = "user1";

            // Act
            var password1 = _service.GeneratePassword(sampleUserId);
            var password2 = _service.GeneratePassword(sampleUserId);

            // Assert
            Assert.AreNotEqual<string>(password1, password2,
                "Calling the generator with the same data should not create same password.");
        }

        [TestMethod]
        public void ValidateCorrectUserIdAndPassword()
        {
            // Arrange
            _passwordRepoMock.Setup(m => m.SaveUserInfo(It.IsAny<UserPasswordInfo>()))
               .Returns(true);
            _service = new PasswordApi.Service.PasswordService(_passwordRepoMock.Object, _passwordExpiryMock.Object, new Rfc2898CryptoService());
            var sampleUserId = "user1";
            var password = _service.GeneratePassword(sampleUserId);
            var hash = new Rfc2898CryptoService().HashPassword(password);
            _passwordRepoMock.Setup(m => m.GetUserInfo(sampleUserId))
               .Returns(new UserPasswordInfo {
                   HashedPassword = hash.HashedPassword,
                   HashSalt = hash.HashSalt,
                   Expiry = DateTime.Now.AddSeconds(-1), //valid
               }); 
            

            // Act
            var result = _service.IsPasswordValid(sampleUserId, password);

            // Assert
            Assert.IsTrue(result, "userid and password must be valid");
        }

        [TestMethod]
        public void ValidateCorrectUserIdAndWrongPassword()
        {
            // Arrange
            _passwordRepoMock.Setup(m => m.SaveUserInfo(It.IsAny<UserPasswordInfo>()))
               .Returns(true);
            _service = new PasswordApi.Service.PasswordService(_passwordRepoMock.Object, _passwordExpiryMock.Object, new Rfc2898CryptoService());
            var sampleUserId = "user1";
            var password = _service.GeneratePassword(sampleUserId);
            var wrongPassword = _service.GeneratePassword("user2");
            var hash = new Rfc2898CryptoService().HashPassword(password);
            _passwordRepoMock.Setup(m => m.GetUserInfo(sampleUserId))
               .Returns(new UserPasswordInfo
               {
                   HashedPassword = hash.HashedPassword,
                   HashSalt = hash.HashSalt,
                   Expiry = DateTime.Now.AddSeconds(-1), //valid
               });

            // Act
            var result = _service.IsPasswordValid(sampleUserId, wrongPassword);

            // Assert
            Assert.IsFalse(result, "userid and password must not be valid");
        }
    }
}
