using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Service
{
    public interface IPasswordValidator
    {
        bool IsPasswordValid(string userId, string password);
    }
}
