using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Service
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(string userId);
    }
}
