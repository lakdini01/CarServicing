
using ServiceStationWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ServiceStationWebService.Controllers
{
    public class ServiceStationWebController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetServiceStationList()
        {
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                
                  /*var center = carService.ServiceCenters.Include("DeliveryBoys").Include("TypesOfServices").ToList();*/
                  var center = carService.ServiceCenters.Include("DeliveryBoys").Include("TypesOfServices").Include("BookingServices").ToList();
                    return Ok(center);  
            }
            /*if (center.Count == 0)
            {
                return NotFound();
            }*/
            
        }

        [ValidateAntiForgeryToken]
        public IHttpActionResult PostAddNewServiceCenter(ServiceCenter center) // ---------------Adding a service center---------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                carService.ServiceCenters.Add(center);
                carService.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult GetCenterById(int id)          //------Get service center details by id----------------
        {
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                var centerData = carService.ServiceCenters.Where(m => m.ServiceCenterId == id).FirstOrDefault();
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
        public IHttpActionResult PutServiceCenter(ServiceCenter center)     //-------------Edit center details-----------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                var existingCenter = carService.ServiceCenters.Where(x => x.ServiceCenterId == center.ServiceCenterId).FirstOrDefault();
                if (existingCenter != null)
                {
                    existingCenter.ServiceCenterName = center.ServiceCenterName;
                    existingCenter.Location = center.Location;
                    existingCenter.Rating = center.Rating;
                    existingCenter.MaxNumberOfServices = center.MaxNumberOfServices;
                    
                    /*carService.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;*/
                    carService.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(center);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteServiceCenter(int id)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                
                ServiceCenter center = carService.ServiceCenters.Where(x => x.ServiceCenterId == id).FirstOrDefault();
                var type = carService.TypesOfServices.Where(x => x.CenterId == id);
                var deliveryboys = carService.DeliveryBoys.Where(x => x.ServiceCenterId == id) ;
                foreach(var s in type) { carService.TypesOfServices.Remove(s); }
                foreach(var s in deliveryboys) { carService.DeliveryBoys.Remove(s); }
                carService.ServiceCenters.Remove(center);
                carService.SaveChanges();

                return Ok();
            }
        }
    }
}
