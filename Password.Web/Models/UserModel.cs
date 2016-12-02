using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Password.Web.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool? IsValid { get; set; }
    }
}