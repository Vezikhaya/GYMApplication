using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using GYMApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GYMApplication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }


        public void CreateRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Receptionist"))
            {
                role = new IdentityRole();
                role.Name = "Receptionist";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Manager"))
            {
                role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Employee"))
            {
                role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Driver"))
            {
                role = new IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }
        }

        public void CreateUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            //  user.Email = "Admin@esolutions.com";
            // user.UserName = user.Email;
            user.UserName = "Admin@VeeGym.com";
            user.Email = "Admin@VeeGym.com";

            var check = userManager.Create(user, "Password@12");

            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");

            }
            var userManager2 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user2 = new ApplicationUser();
            //  user.Email = "Admin@esolutions.com";
            // user.UserName = user.Email;
            user2.UserName = "Receptionist@GYM.com";
            user2.Email = "Receptionist@GYM.com";
            var check2 = userManager.Create(user2, "Password@12");
            if (check2.Succeeded)
            {
                userManager.AddToRole(user2.Id, "Receptionist");
            }

            var userManager3 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user3 = new ApplicationUser();
            user3.UserName = "Manager@GYM.com";
            user3.Email = "Manager@GYM.com";
            var check3 = userManager.Create(user3, "Password@12");
            if (check3.Succeeded)
            {
                userManager.AddToRole(user3.Id, "Manager");
            }

            var userManager4 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user4 = new ApplicationUser();
            user4.UserName = "Driver@GYM.com";
            user4.Email = "Driver@GYM.com";
            var check4 = userManager.Create(user4, "Password@12");
            if (check4.Succeeded)
            {
                userManager.AddToRole(user4.Id, "Driver");
            }

        }
    }
}