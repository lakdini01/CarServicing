using CarServicingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarServicingManagement.Controllers
{
    public class BookingServiceController : Controller
    {
        // GET: BookingService
        public ActionResult DisplayAllBookings()
        {
            IEnumerable<BookingService> book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62400/api/");
                var responseTask = client.GetAsync("BookingServiceWeb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookingService>>();
                    readTask.Wait();

                    book = readTask.Result;
                    
                }
                else
                {
                    book = Enumerable.Empty<BookingService>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(book);
        }
        public ActionResult Index(int userId) // display booking details for the relevant User
        {
            IEnumerable<BookingService> book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62400/api/");
                var responseTask = client.GetAsync("BookingServiceWeb?userId="+userId.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookingService>>();
                    readTask.Wait();

                    book = readTask.Result;
                    
                }
                else
                {
                    book = Enumerable.Empty<BookingService>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(book);
        }
        public ActionResult AddBooking()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddBooking(int id,int userId,BookingService book)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var postTask = client.PostAsJsonAsync<BookingService>("BookingServiceWeb?id=" + id.ToString()+"&userId="+userId.ToString(), book);
                postTask.Wait();

                var result = postTask.Result;
                
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Your request is submitted";
                    
                    return RedirectToAction("Display", "ServiceStation");

                }
                else if(book.ServiceStartDate < DateTime.Today)
                {
                    TempData["dateStart"] = "Date shouldn't be previous date !";
                }
                else if(book.ServiceEndDate < book.ServiceStartDate)
                {
                    TempData["dateEnd"] = "Date shouldn't be previous date !";
                }
               

            }
          
            return View(book);

        }
        //-------------------Edit----------------------------

        public ActionResult EditBooking(int id)
        {
            BookingService book = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var postTask = client.GetAsync("BookingServiceWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookingService>();
                    readTask.Wait();
                    book = readTask.Result;
                    
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBooking(int deliveryBoyId,BookingService book)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var postTask = client.PutAsJsonAsync<BookingService>("BookingServiceWeb?deliveryBoyId="+deliveryBoyId.ToString(), book);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["remove"] = "remove";
                    return RedirectToAction("DisplayAllBookings","BookingService");

                }
                TempData["remove"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(book);
        }
        //------------------------------------Delete--------------------------

        public ActionResult DeleteBooking(int id)
        {
            BookingService book = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var postTask = client.GetAsync("BookingServiceWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookingService>();
                    readTask.Wait();
                    book = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(book);
        }
        [HttpPost, ActionName("DeleteBooking")]
        public ActionResult DeleteBookingConfirm(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var deleteTask = client.DeleteAsync("BookingServiceWeb/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DisplayAllBookings","BookingService");

                }

            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View("DeleteBookingService");
        }
        //--------------------------Details-------------------------

        public ActionResult DetailsBooking(int id)
        {
            BookingService book = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62400/api/");

                var responseTask = client.GetAsync("BookingServiceWeb?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookingService>();
                    readTask.Wait();
                    book = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(book);
        }
    }
}