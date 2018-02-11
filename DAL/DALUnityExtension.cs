using DAL.IdentityExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;
using Unity.Injection;
using Unity.Lifetime;

namespace DAL
{
    public class DALUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterInstance(new Context(), new PerResolveLifetimeManager());

            var accountInjectionConstructor = new InjectionConstructor(new ResolvedParameter<Context>());
            Container.RegisterType<IUserStore<AuthUser>, UserStore<AuthUser>>(accountInjectionConstructor);
            Container.RegisterType<AppUserManager>();
        }
    }
}
