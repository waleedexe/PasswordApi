using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi
{
    class Program
    {
        const string baseAddress = "http://localhost:52288/";

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                //TestConnection();
                Console.WriteLine("Listening on ({0}) ... ", baseAddress);

                Console.ReadLine();
            }
        }
    }
}
