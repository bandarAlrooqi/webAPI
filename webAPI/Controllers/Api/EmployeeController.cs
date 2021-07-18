using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tabels;
using System.Data.Entity;
using webAPI.Models;

namespace webAPI.Controllers.WebAPI
{
    [Authorize (Roles="admin")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage  ShowEmployees(string gender = "all")
        {
            using (var entities = new EmplyeePageEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Include(c=>c.credentials).Include(d=>d.department1).ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e=>e.sex.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e => e.sex.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "The Value For Gender Must Be All, Male, or Female");
                }
                
            }
        }
        [HttpGet]
        public HttpResponseMessage ShowEmployees(int id)
        {
            using (var entities = new EmplyeePageEntities())
            {
                var entity =  entities.employees.FirstOrDefault(i=>i.id == id);

                return entity != null ? Request.CreateResponse(HttpStatusCode.OK, entity)
                    : Request.CreateResponse(HttpStatusCode.NotFound,
                            $"Employee with this ID \"{id}\" could not be found !");
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmployees(EmployeeModel em)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    employee employee = new employee
                    {
                        id = em.id,
                        name = em.name,
                        sex = em.sex,
                        date_of_hiring = em.date_of_hiring,
                        department = em.department,
                        department1 = entities.departments.FirstOrDefault(i => i.id == em.department)
                    };

                    entities.employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, em);
                    message.Headers.Location = new Uri(Request.RequestUri + em.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    var entity = entities.employees.FirstOrDefault(i => i.id == id);
                    
                    if (entity != null)
                    {
                        var credential = entities.credentials.FirstOrDefault(e => e.id == id);
                        if(credential != null)
                            entities.credentials.Remove(credential);
                        
                        entities.employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                    return Request.CreateResponse(HttpStatusCode.NotFound,
                        $"Employee with this ID \"{id}\" could not be found !");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPut]
        public HttpResponseMessage EditEmployee(int id, EmployeeModel em)
        {
            try
            {
                using (var entities = new EmplyeePageEntities())
                {
                    var entity = entities.employees.FirstOrDefault(i => i.id == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            $"Employee with this ID \"{id}\" could not be found !");
                    }
                    entity.name = em.name.Trim();
                    entity.date_of_hiring = em.date_of_hiring;
                    entity.sex = em.sex.Trim();
                    var dep = entities.departments.FirstOrDefault(i => i.name.ToLower().Trim().Equals(em.departmentName.ToLower()));
                    entity.department = (dep.id);
                    entity.department1 = dep;
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
