using Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PasswordApi.Controllers
{
    public class PasswordController: ApiController
    {
        private IPasswordService _service;

        public PasswordController(IPasswordService service)
        {
            _service = service;
        }

        public string Get(string userid)
        {
            var result = _service.GeneratePassword(userid);
            return result;
        }

        [HttpPost]
        public bool Validate([FromBody] Models.UserPasswordModel data)
        {
            return _service.IsPasswordValid(data.userId, data.password);
        }
    }
}
