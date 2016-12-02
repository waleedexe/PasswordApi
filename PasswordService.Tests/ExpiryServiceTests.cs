using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordApi.Entities;
using FluentAssertions;
using PasswordApi.Service;

namespace PasswordService.Tests
{
    [TestClass]
    public class ExpiryServiceTests
    {
        PasswordExpiryService _service;

        [TestInitialize]
        public void Init()
        { 
            _service = new PasswordExpiryService();
        }

        [TestMethod]
        public void PasswordExpiresAfter30Secs()
        {
            // Arrange
            var passwordExpiryTime = DateTime.Now.AddSeconds(-1);

            // Act
            var result = _service.HasPasswordExpired(passwordExpiryTime);

            // Assert
            Assert.AreEqual(true, result, "Password must have expired after 30 secs.");
        }

        [TestMethod]
        public void LivePasswordShouldNotExpiresNow()
        {
            // Arrange
            var passwordExpiryTime = DateTime.Now.AddSeconds(1);

            // Act
            var result = _service.HasPasswordExpired(passwordExpiryTime);

            // Assert
            Assert.AreEqual(false, result, "Password should not have expired yet");
        }
    }
}
