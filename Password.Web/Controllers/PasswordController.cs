using Newtonsoft.Json;
using Password.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Password.Web.Controllers
{
    public class PasswordController : Controller
    {
        const string ApiAddress = "http://localhost:52288";


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            var result = CreatePassword(user.UserId);
            user.Password = result;

            return View(user);
        }

        public ActionResult Validate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(UserModel user)
        {
            var result = ValidatePassword(user.UserId, user.Password);
            user.IsValid = result;

            return View(user);
        }

        private string CreatePassword(string userId)
        {
            var requestUrl = string.Format("{0}/api/password/{1}", ApiAddress, userId);
            // Create HttpCient and make a request to api/values 
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(requestUrl).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        private bool ValidatePassword(string userId, string password)
        {
            var requestUrl = string.Format("{0}/api/password/{1}", ApiAddress, userId);
            var remoteModel = new { userId, password };
            var content = new StringContent(JsonConvert.SerializeObject(remoteModel), Encoding.UTF8, "application/json");
            // Create HttpCient and make a request to api/values 
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(requestUrl, content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(result);
            }
        }
    }
}