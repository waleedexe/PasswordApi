using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasswordApi.Tests
{
    [TestClass]
    public class PasswordGeneratorTests
    {
        [TestMethod]
        public void GetaPassword()
        {
            // Arrange
            var sampleUserId = "user1";
            var gen = new PasswordGenerator();

            // Act
            var password = gen.GenerateUserPassword(sampleUserId);

            // Assert
            Assert.IsNotNull(password);
        }

        [TestMethod]
        public void GetRandomPassword()
        {
            // Arrange
            var sampleUserId = "user1";
            var gen = new PasswordGenerator();

            // Act
            var password1 = gen.GenerateUserPassword(sampleUserId);
            var password2 = gen.GenerateUserPassword(sampleUserId);

            // Assert
            Assert.AreNotEqual<string>(password1, password2);
        }
    }
}
