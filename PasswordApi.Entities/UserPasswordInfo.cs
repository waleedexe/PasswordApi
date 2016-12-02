using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi.Entities
{
    public class UserPasswordInfo: HashInfo
    {
        public string UserId { get; set; }
        public DateTime Expiry { get; set; }
    }
}
