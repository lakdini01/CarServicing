using CarServicingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarServicingManagement.Controllers
{
    public class TypesOfServiceController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<TypesOfService> type = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50281/api/");
                var responseTask = client.GetAsync("TypesOfServiceWeb");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TypesOfService>>();
                    readTask.Wait();

                    type = readTask.Result;
                }
                else
                {
                    type = Enumerable.Empty<TypesOfService>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
            }
            return View(type);
        }
        public ActionResult AddServiceType()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddServiceType(int id,TypesOfService type)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var postTask = client.PostAsJsonAsync<TypesOfService>("TypesOfServiceWeb/"+id.ToString(), type);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service type added successfully !";
                    return RedirectToAction("Index", "ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(type);

        }
        //-------------------Edit----------------------------

        public ActionResult EditServiceType(int id)
        {
            TypesOfService type = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var postTask = client.GetAsync("TypesOfServiceWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TypesOfService>();
                    readTask.Wait();
                    type = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(type);
        }
        [HttpPost]
        public ActionResult EditServiceType(TypesOfService type)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var postTask = client.PutAsJsonAsync<TypesOfService>("TypesOfServiceWeb", type);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service type edited successfully !";
                    return RedirectToAction("Index", "ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(type);
        }
        //------------------------------------Delete--------------------------

        public ActionResult DeleteServiceType(int id)
        {
            TypesOfService type = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var postTask = client.GetAsync("TypesOfServiceWeb?id=" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TypesOfService>();
                    readTask.Wait();
                    type = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(type);
        }
        [HttpPost, ActionName("DeleteServiceType")]
        public ActionResult DeleteServiceTypeConfirm(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var deleteTask = client.DeleteAsync("TypesOfServiceWeb/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Service type deleted successfully !";
                    return RedirectToAction("Index", "ServiceStation");

                }
                TempData["successMessage"] = null;
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View("DeleteServiceCenter");
        }
        //--------------------------Details-------------------------

        public ActionResult DetailsServiceType(int id)
        {
            TypesOfService type = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50281/api/");

                var responseTask = client.GetAsync("TypesOfServiceWeb?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TypesOfService>();
                    readTask.Wait();
                    type = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(type);
        }
        
    }
}