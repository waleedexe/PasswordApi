using Interfaces.Data;
using Interfaces.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApi.Unity
{
    public class UnityConfiguration
    {
        public UnityResolver GetResolver()
        {
            return new UnityResolver(ConfigureContainer());
        }

        public UnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IPasswordService, Service.PasswordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPasswordRepository, Data.InMemoryPasswordRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPasswordExpiryService, Service.PasswordExpiryService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICryptoService, Service.Rfc2898CryptoService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
