using CarServicingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarServicingManagement.Controllers
{
    public class DeliveryBoysController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<DeliveryBoy> boy = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62325/api/");
                var responseTask = client.GetAsync("DeliveryBoysWeb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DeliveryBoy>>();
                    readTask.Wait();

                    boy = readTask.Result;
                    /*Session["del"] = new SelectList(boy.ToList(), "DeliveryBoyId", "Name");*/
                }
                else
                {
                    boy = Enumerable.Empty<DeliveryBoy>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(boy);
        }
       /* public ActionResult DisplayByCenterId(int centerId)
        {
            IEnumerable<BookingService> book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62325/api/");
                var responseTask = client.GetAsync("BookingServiceWeb?centerId=" + centerId.ToString());
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
        }*/
        public ActionResult AddDeliveryBoy()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddDeliveryBoy(int id,DeliveryBoy boy)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var postTask = client.PostAsJsonAsync<DeliveryBoy>("DeliveryBoysWeb/"+id.ToString(), boy);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Delivery boy added successfully !";
                    return RedirectToAction("Index","ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(boy);

        }
        //-------------------Edit----------------------------

        public ActionResult EditDeliveryBoy(int id)
        {
            DeliveryBoy boy = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var postTask = client.GetAsync("DeliveryBoysWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DeliveryBoy>();
                    readTask.Wait();
                    boy = readTask.Result;
                    
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(boy);
        }
        [HttpPost]
        public ActionResult EditDeliveryBoy(DeliveryBoy boy)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var postTask = client.PutAsJsonAsync<DeliveryBoy>("DeliveryBoysWeb", boy);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Delivery boy edited successfully !";
                    return RedirectToAction("Index", "ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(boy);
        }
        //------------------------------------Delete--------------------------

        public ActionResult DeleteDeliveryBoy(int id)
        {
            DeliveryBoy boy = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var postTask = client.GetAsync("DeliveryBoysWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DeliveryBoy>();
                    readTask.Wait();
                    boy = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(boy);
        }
        [HttpPost, ActionName("DeleteDeliveryBoy")]
        public ActionResult DeleteDeliveryBoyConfirm(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var deleteTask = client.DeleteAsync("DeliveryBoysWeb/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Delivery boy deleted successfully !";
                    return RedirectToAction("Index", "ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View("DeleteDeliveryBoy");
        }
        //--------------------------Details-------------------------

        public ActionResult DetailsDeliveryBoy(int id)
        {
            DeliveryBoy boy = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:62325/api/");

                var responseTask = client.GetAsync("DeliveryBoysWeb?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DeliveryBoy>();
                    readTask.Wait();
                    boy = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(boy);
        }


        
    }
}