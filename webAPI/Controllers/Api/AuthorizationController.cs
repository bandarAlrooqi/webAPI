using System;
using System.Collections.Generic;
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
       
        [HttpGet]
        public HttpResponseMessage IsValid(string userName, string password)
        {
            using (var entities = new EmplyeePageEntities())
            {
                var credential = entities.credentials.FirstOrDefault(name =>
                    name.user_name.Equals(userName.Trim()) && name.passcode.Equals(password));
                return credential != null ? Request.CreateResponse(HttpStatusCode.OK, credential)
                    : Request.CreateResponse(HttpStatusCode.NotFound,
                        "Incorrect UserName Or Password");
            }
           
        }
        [HttpGet]
        public HttpResponseMessage IsValid()
        {
            using (var entities = new EmplyeePageEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
           
        }
    }
}