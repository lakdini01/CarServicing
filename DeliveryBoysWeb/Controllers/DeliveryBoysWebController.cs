using DeliveryBoysWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace DeliveryBoysWeb.Controllers
{
    public class DeliveryBoysWebController : ApiController
    {
        public IHttpActionResult GetDeliveryBoysList()
        {
            /*IList<ServiceCenter> center = null;*/

            using (var carService = new CarServicingManagementEntities())
            {
                var deliveryBoys = carService.DeliveryBoys.ToList();

                if (deliveryBoys == null)
                {
                    return NotFound();
                }
                return Ok(deliveryBoys);
            }
        }
       /* public IHttpActionResult GetDeliveryBoyListByUserId(int centerId)
        {
            *//*IList<DeliveryBoy> center = null;*//*

            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;

                var deliveryBoysList = carService.DeliveryBoys.Where(x => x.ServiceCenterId == centerId).ToList();

                if (deliveryBoysList == null)
                {
                    return NotFound();
                }
                return Ok(deliveryBoysList);
            }
        }*/
        [ValidateAntiForgeryToken]
        public IHttpActionResult PostAddNewDeliveryBoy(int id,DeliveryBoy boy) // ---------------Adding a service center---------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                boy.ServiceCenterId = id;

                carService.DeliveryBoys.Add(boy);
                carService.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult GetDeliveryBoyById(int id)          //------Get service center details by id----------------
        {
            using (var carService = new CarServicingManagementEntities())
            {
                var deliveryBoyData = carService.DeliveryBoys.Where(m => m.DeliveryBoyId == id).FirstOrDefault();
                if (deliveryBoyData != null)
                {
                    return Ok(deliveryBoyData);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [System.Web.Http.HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult PutDeliveryBoy(DeliveryBoy boy)     //-------------Edit center details-----------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                var existingDeliveryBoy = carService.DeliveryBoys.Where(x => x.DeliveryBoyId == boy.DeliveryBoyId).FirstOrDefault();
                if (existingDeliveryBoy != null)
                {
                    existingDeliveryBoy.Name = boy.Name;
                    existingDeliveryBoy.Experience = boy.Experience;
                    existingDeliveryBoy.Age = boy.Age;
                    existingDeliveryBoy.Address = boy.Address;
                    existingDeliveryBoy.Mobile = boy.Mobile;
                    /*carService.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;*/
                    carService.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(boy);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteDeliveryBoy(int id)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                DeliveryBoy boy = carService.DeliveryBoys.Where(x => x.DeliveryBoyId == id).FirstOrDefault();
                carService.DeliveryBoys.Remove(boy);
                carService.SaveChanges();

                return Ok();
            }
        }
    }
}
