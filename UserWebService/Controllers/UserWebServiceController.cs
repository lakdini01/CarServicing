using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using UserWebService.Models;

namespace UserWebService.Controllers
{
    public class UserWebServiceController : ApiController
    {

       /* public IHttpActionResult GetAllContent()
        {
            using(var carService = new CarServicingManagementEntities())
            {
                *//*carService.Configuration.ProxyCreationEnabled = false;*//*
                var allData = carService.Users.Include("Service");
                return Ok();
            }
        }*/
        // GET api/values
        [ValidateAntiForgeryToken]
       public IHttpActionResult PostAddNewUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using(var carService = new CarServicingManagementEntities())
            {
                
                if (user.DateOfJoin >= DateTime.Today)
                {
                    user.RoleId = 4;

                    carService.Users.Add(user);
                    carService.SaveChanges();
                }
            }
            return Ok();
        }

        //----------------- Login 
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetLogin(string email, string password)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                var check = carService.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (check != null)
                {
                    
                    return Ok(check);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public IHttpActionResult GetUserById(int id)
        {
            using (var carService = new CarServicingManagementEntities())
            {
                /*carService.Configuration.ProxyCreationEnabled = false;*/
                var userData = carService.Users.Where(m => m.Id == id).FirstOrDefault();
                if(userData != null)
                {
                    return Ok(userData);
                }
                else
                {
                   return NotFound();
                }
            }
        }
        [System.Web.Http.HttpPut]
        public IHttpActionResult PutUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            using (var carService = new CarServicingManagementEntities())
            {
               /* carService.Configuration.ProxyCreationEnabled = false;*/
                var existingUser = carService.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                if(existingUser != null)
                {
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Age = user.Age;
                    existingUser.Gender = user.Gender;
                    existingUser.Password = user.Password;
                    existingUser.ContactNumber = user.ContactNumber;
                    existingUser.Email = user.Email;
                    /*carService.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;*/
                    carService.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                
                
                
            }
            return Ok(user);
        }

    }
}
