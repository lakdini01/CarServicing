using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TypesOfServiceWeb.Models;

namespace TypesOfServiceWeb.Controllers
{
    public class TypesOfServiceWebController : ApiController
    {
        public IHttpActionResult GetTypeOfServiceList()
        {
            /*IList<ServiceCenter> center = null;*/
            
            using (var carService = new CarServicingManagementEntities())
            {
                var center = carService.TypesOfServices.ToList();

                if (center == null)
                {
                    return NotFound();
                }
                return Ok(center);
            }
        }

        [ValidateAntiForgeryToken]
        public IHttpActionResult PostAddNewServiceType(int id,TypesOfService type) // ---------------Adding a service center---------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                type.CenterId = id;
                carService.TypesOfServices.Add(type);
                carService.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult GetServiceTypeById(int id)          //------Get service center details by id----------------
        {
            using (var carService = new CarServicingManagementEntities())
            {
                var centerData = carService.TypesOfServices.Where(m => m.ServiceTypeId == id).FirstOrDefault();
                if (centerData != null)
                {
                    return Ok(centerData);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [System.Web.Http.HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult PutServiceType(TypesOfService type)     //-------------Edit center details-----------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                var existingCenterType = carService.TypesOfServices.Where(x => x.ServiceTypeId == type.ServiceTypeId).FirstOrDefault();
                if (existingCenterType != null)
                {
                    existingCenterType.ServiceTypeName = type.ServiceTypeName;
                    existingCenterType.Cost = type.Cost;
                    /*carService.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;*/
                    carService.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(type);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteServiceType(int id)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                TypesOfService type = carService.TypesOfServices.Where(x => x.ServiceTypeId == id).FirstOrDefault();
                carService.TypesOfServices.Remove(type);
                carService.SaveChanges();

                return Ok();
            }
        }
    }
}
