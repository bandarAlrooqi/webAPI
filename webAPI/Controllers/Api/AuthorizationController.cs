using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tabels;

namespace webAPI.Controllers.Api
{
    public class AuthorizationController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ShowDepartment()
        {
            using (var entities = new EmplyeePageEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.departments.Include(e=>e.employees).ToList());
            }
        }
    }
}