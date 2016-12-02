using Interfaces.Service;
using System;

namespace PasswordApi.Service
{
    public class PasswordExpiryService: IPasswordExpiryService
    {
        private const int ExpirySeconds = 30;   // Should be configurable?

        public DateTime GenerateExpiryDate()
        {
            return DateTime.Now.AddSeconds(ExpirySeconds);
        }

        public bool HasPasswordExpired(DateTime passwordExpiry)
        {
            return (DateTime.Now > passwordExpiry);
        }
    }
}
