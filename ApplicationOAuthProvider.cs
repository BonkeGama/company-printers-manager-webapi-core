using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CompanyPrinters.Models;

namespace CompanyPrinters
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var db = new CompanyPrinters.Models.Users();

            var user = db.Users.FirstOrDefault(x => x.UserName == context.UserName && x.Password == context.Password);
            //var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //var manager = new UserManager<ApplicationUser>(userStore);
            //var user = await manager.FindAsync(context.UserName, context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
                identity.AddClaim(new Claim("Username", user.UserName));
                identity.AddClaim(new Claim("FirstName", user.FirstName));
                identity.AddClaim(new Claim("LastName", user.LastName));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("Password", user.Password));



                context.Validated(identity);
            }
            else
                return;
        }
    }
}