using BookingWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BookingWebService.Controllers
{
    public class BookingServiceWebController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllBookingServiceList()
        {
            /*IList<ServiceCenter> center = null;*/

            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;

                var bookingService = carService.BookingServices.ToList();

                if (bookingService == null)
                {
                    return NotFound();
                }
                return Ok(bookingService);
            }
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetBookingServiceListByUserId(int userId)
        {
            /*IList<ServiceCenter> center = null;*/

            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;

                var bookingService = carService.BookingServices.Where(x=>x.UserId == userId).ToList();

                if (bookingService == null)
                {
                    return NotFound();
                }
                return Ok(bookingService);
            }
        }

        [ValidateAntiForgeryToken]
        public IHttpActionResult PostAddNewBookingService(int id,int userId,BookingService book) // ---------------Adding a service center---------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                var findTotCost = carService.TypesOfServices.Where(x => x.CenterId == id);
                double totCost = 0;
                foreach(var cost in findTotCost) { totCost += Convert.ToDouble(cost.Cost); } //  to find the total cost
                if (book.ServiceStartDate >= DateTime.Today && book.ServiceEndDate >= book.ServiceStartDate)
                {
                    book.ServiceCenterId = id;
                    book.UserId = userId;
                    book.BookedDate = DateTime.Today.Date;
                    
                    book.TotalCost = totCost;
                    carService.BookingServices.Add(book);
                    carService.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            
        }
        public IHttpActionResult GetBookingServiceById(int id)          //------Get service center details by id----------------
        {
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                var bookingData = carService.BookingServices.Where(m => m.BookingId == id).FirstOrDefault();
                if (bookingData != null)
                {
                    return Ok(bookingData);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [System.Web.Http.HttpPut]
        
        public IHttpActionResult PutBookingService(int deliveryBoyId,BookingService booking)     //-------------Edit center details-----------
        {
            
            
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                var existingBooking = carService.BookingServices.Where(x => x.BookingId == booking.BookingId).FirstOrDefault();
                if (existingBooking != null)
                {
                    
                    /*existingBooking.DeliveryBoyId = deliveryBoyId;*/
                    existingBooking.Status = "Approved";
                    existingBooking.DeliveryBoyId = deliveryBoyId;
                    /*carService.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;*/
                    carService.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(booking);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteBookingService(int id)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                carService.Configuration.ProxyCreationEnabled = false;
                BookingService booking = carService.BookingServices.Where(x => x.BookingId == id).FirstOrDefault();
                carService.BookingServices.Remove(booking);
                carService.SaveChanges();

                return Ok();
            }
        }
    }
}
