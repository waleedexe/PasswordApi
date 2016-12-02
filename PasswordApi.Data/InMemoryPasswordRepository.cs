using Interfaces.Data;
using PasswordApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi.Data
{
    /// <summary>
    /// This class is a basic storage for our user details.
    /// UserId is assumed to be unique and will be used as the key to each data record.
    /// </summary>
    public class InMemoryPasswordRepository: IPasswordRepository
    {
        static Dictionary<string, UserPasswordInfo> StorageDictionary = new Dictionary<string, UserPasswordInfo>();

        public bool SaveUserInfo(UserPasswordInfo userInfo)
        {
            if (StorageDictionary.ContainsKey(userInfo.UserId))
            {
                // Log: existing user.
                // Question: Do we want to override user if password has expired?

                return false;
            }

            StorageDictionary.Add(userInfo.UserId, userInfo);

            return true;
        }

        public UserPasswordInfo GetUserInfo(string userId)
        {
            var userInfo = StorageDictionary[userId];

            return userInfo;
        }

    }
}
