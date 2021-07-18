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
    [Authorize (Roles="admin")]
    public class DepartmentController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage ShowDepartment()
        {
            using (var entities = new EmplyeePageEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.departments.Include(e=>e.employees).ToList());
            }
        }
        [HttpGet]
        public HttpResponseMessage ShowDepartment(int id)
        {
            using (var entities = new EmplyeePageEntities())
            {
                var entity = entities.departments.FirstOrDefault(i => i.id == id);

                return entity != null ? Request.CreateResponse(HttpStatusCode.OK, entity)
                    : Request.CreateResponse(HttpStatusCode.NotFound,
                            $"Department with this ID \"{id}\" could not be found !");
            }
        }

        [HttpPost]
        public HttpResponseMessage AddDepartment( department de)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    entities.departments.Add(de);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, de);
                    message.Headers.Location = new Uri(Request.RequestUri + de.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteDepartment(int id)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    var entity = entities.departments.FirstOrDefault(i => i.id == id);
                    if (entity != null)
                    {
                        entities.departments.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                    return Request.CreateResponse(HttpStatusCode.NotFound,
                        $"Department with this ID \"{id}\" could not be found !");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPut]
        public HttpResponseMessage EditDepartment(int id,department de)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    var entity = entities.departments.FirstOrDefault(i => i.id == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            $"Department with this ID \"{id}\" could not be found !");
                    }

                    entity.name = de.name;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
