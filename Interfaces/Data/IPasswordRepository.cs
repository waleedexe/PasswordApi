using PasswordApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Data
{
    public interface IPasswordRepository
    {
        bool SaveUserInfo(UserPasswordInfo userInfo);
        UserPasswordInfo GetUserInfo(string userId);
    }
}
