using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi.Entities
{
    public class HashInfo
    {
        public string HashedPassword { get; set; }
        public string HashSalt { get; set; }
    }
}
