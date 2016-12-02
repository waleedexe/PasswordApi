using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordApi.Entities;
using FluentAssertions;
using PasswordApi.Service;

namespace PasswordService.Tests
{
    [TestClass]
    public class CryptoTests
    {
        Rfc2898CryptoService _crypto;

        [TestInitialize]
        public void Init()
        { 
            _crypto = new Rfc2898CryptoService();
        }

        [TestMethod]
        public void GetaPassword()
        {
            // Arrange
            var password = "password";

            // Act
            var result = _crypto.HashPassword(password);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<HashInfo>();
            result.HashSalt.Should().NotBeNullOrEmpty();
            result.HashedPassword.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetRandomPassword()
        {
            // Arrange
            var password = "password";

            // Act
            var result1 = _crypto.HashPassword(password);
            var result2 = _crypto.HashPassword(password);

            // Assert
            result1.Should().NotBeSameAs(result2,
                "Calling the generator with the same data should not create same password.");
        }
    }
}
