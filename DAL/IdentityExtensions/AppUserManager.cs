using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Model.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IdentityExtensions
{
    public class AppUserManager : UserManager<AuthUser>
    {
        public AppUserManager(IDataProtectionProvider protectionProvider, UserStore<AuthUser> userStore) : base(userStore)
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            UserTokenProvider = new DataProtectorTokenProvider<AuthUser>(protectionProvider.Create("ASP.NET Identity"));

            UserValidator = new UserValidator<AuthUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        public static void InitializeUserManager(AppUserManager manager, IAppBuilder app)
        {
            manager.UserValidator = new UserValidator<AuthUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = app.GetDataProtectionProvider();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AuthUser>(
                    dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //TokenLifespan = TimeSpan.FromHours(3)
                };
            }
        }
    }
}
