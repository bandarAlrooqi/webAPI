using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace webAPI.Controllers
{
    public class Authorization :OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role,"admin"));
                identity.AddClaim(new Claim("userName","admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name,"Bandar"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid grant","userName or password is incorrect");
            }
        }
    }
}