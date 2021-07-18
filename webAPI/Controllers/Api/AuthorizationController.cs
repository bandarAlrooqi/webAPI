using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services;
using Tabels;

namespace webAPI.Controllers.Api
{
    public class AuthorizationController : ApiController
    {
        private static bool _isAuthorized;

        [HttpGet]
        public HttpResponseMessage IsValid(string userName, string password)
        {
            using (var entities = new EmplyeePageEntities())
            {
                var users = entities.credentials.Include(e => e.employee).Where
                (u => u.user_name == userName && password == u.passcode).ToList();
                //Database is case insensitive, need to filter extra to do the case sensitive search
                var credential = users.FirstOrDefault(name =>
                    name.user_name==userName.Trim() && name.passcode==password);
                if (credential != null)
                {
                    _isAuthorized = true;
                    return Request.CreateResponse(HttpStatusCode.OK, credential);
                }
                _isAuthorized = false;
                    return Request.CreateResponse(HttpStatusCode.NotFound,
                                            "Incorrect UserName Or Password");
            }

        }

       public  class Authorization : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                if (_isAuthorized)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    identity.AddClaim(new Claim("userName","admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Name,"Bandar"));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid grant", "userName or password is incorrect");
                }
            }
        }
    }
}