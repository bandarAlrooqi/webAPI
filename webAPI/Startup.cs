using Microsoft.Owin;
using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using webAPI.Controllers.Api;

[assembly: OwinStartup(typeof(webAPI.Startup))]

namespace webAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);
            var provider =new AuthorizationController.Authorization();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(5000),
                Provider = provider
            };
            
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
        }
    }
}
