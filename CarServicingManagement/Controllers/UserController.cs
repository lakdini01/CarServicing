using CarServicingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarServicingManagement.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Register(User user, string repassword)
        {
            using(var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri("http://localhost:1234/api/");

                var postTask = client.PostAsJsonAsync<User>("UserWebService",user); 
                postTask.Wait();

                var result = postTask.Result;
                if (user.DateOfJoin < DateTime.Today)
                {
                    TempData["dateCheck"] = "Date should be greater than previous date!.";
                    
                }
                else if(result.IsSuccessStatusCode)
                {
                    
                    if (user.Password != repassword)
                    {

                        TempData["msg"] = "Password and retyped password is not matching !";
                        
                    }
                    else
                    {
                        TempData["successMessage"] = "User registration successfull !";
                        return RedirectToAction("Login");
                    }
                    TempData["successMessage"] = null;
                }
                return View(user);
                
                
                
                
                
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            

        }
        // Edit user details
        public ActionResult EditUser(int id)
        {
            User user = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:1234/api/");

                var postTask = client.GetAsync("UserWebService?id="+id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    user = readTask.Result;
                }  
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(User user,string repassword)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:1234/api/");

                var postTask = client.PutAsJsonAsync<User>("UserWebService", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Display");

                }
                else if (user.Password != repassword)
                {

                    TempData["msg"] = "Password and retyped password is not matching !";
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(user);
        }

        //---------------- Login Page----------------
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var client = new HttpClient())
            {
                Session.Clear();
                client.BaseAddress = new Uri("http://localhost:1234/api/");

                var postTask = client.GetAsync("UserWebService?email=" + user.Email + "&password=" + user.Password);
                postTask.Wait();

                var result = postTask.Result;
                
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<User>();
                    Session["userName"] = data.Result.FirstName+" "+data.Result.LastName;
                    TempData["roleid"] = data.Result.RoleId;
                    Session["userId"] = data.Result.Id;
                    if (TempData["roleid"].Equals(2))
                    {
                        Session["admin"] = "admin";
                        Session["userName"] += " - ADMIN";
                        return RedirectToAction("Display","ServiceStation");
                    }else if (TempData["roleid"].Equals(4))
                    {
                        Session["admin"] = null;
                        return RedirectToAction("Display","ServiceStation");
                    }

                }
                else
                {
                    Session["admin"] = null;
                    Session["userName"] = null;
                    TempData["roleid"] = null;
                    
                    ViewBag.validation = "Please check your email Id and password again !";
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View();
        }

        public ActionResult Display()
        {
            User user = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:1234/api/");

                var postTask = client.GetAsync("UserWebService");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    user = readTask.Result;
                }
            }
            /*ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/
            return View(user);
        }
    }
}
