using CarServicingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarServicingManagement.Controllers
{
    public class ServiceStationController : Controller
    {
        //-------------------------------- Add------------------------

        public ActionResult Display()
        {
            IEnumerable<ServiceCenter> center = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51847/api/");
                var responseTask = client.GetAsync("ServiceStationWeb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ServiceCenter>>();
                    readTask.Wait();

                    center = readTask.Result;

                   
                    var data = from i in center select i;
                    
                    Session["deliveryBoy"] = data.ToList();

                }
                else
                {
                    center = Enumerable.Empty<ServiceCenter>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(center);
        }

        public ActionResult Index()
        {
            IEnumerable<ServiceCenter> center = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51847/api/");
                var responseTask = client.GetAsync("ServiceStationWeb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ServiceCenter>>();
                    readTask.Wait();

                    center = readTask.Result;
                    
                }
                else
                {
                    center = Enumerable.Empty<ServiceCenter>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(center);
        }
        public ActionResult AddServiceCenter()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddServiceCenter(ServiceCenter center)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var postTask = client.PostAsJsonAsync<ServiceCenter>("ServiceStationWeb", center);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service center added successfully !";
                    return RedirectToAction("Index");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(center);

        }
        //-------------------Edit----------------------------

        public ActionResult EditServiceCenter(int id)
        {
            ServiceCenter center = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var postTask = client.GetAsync("ServiceStationWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceCenter>();
                    readTask.Wait();
                    center = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(center);
        }
        [HttpPost]
        public ActionResult EditServiceCenter(ServiceCenter center)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var postTask = client.PutAsJsonAsync<ServiceCenter>("ServiceStationWeb", center);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service center edited successfully !";
                    return RedirectToAction("Index");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(center);
        }
        //------------------------------------Delete--------------------------

        public ActionResult DeleteServiceCenter(int id)
        {
            ServiceCenter center = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var postTask = client.GetAsync("ServiceStationWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceCenter>();
                    readTask.Wait();
                    center = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(center);
        }
        [HttpPost,ActionName("DeleteServiceCenter")]
        public ActionResult DeleteServiceCenterConfirm(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var deleteTask = client.DeleteAsync("ServiceStationWeb/"+id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service center deleted successfully !";
                    return RedirectToAction("Index");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View("DeleteServiceCenter");
        }
        //--------------------------Details-------------------------

        public ActionResult DetailsServiceCenter(int id)
        {
            ServiceCenter center = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51847/api/");

                var responseTask = client.GetAsync("ServiceStationWeb?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceCenter>();
                    readTask.Wait();
                    center = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(center);
        }
    }
}