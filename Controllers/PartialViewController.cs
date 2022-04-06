using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Partial1Controller : Controller
    {
        // GET: Partial
        private DbContextRMS db = new DbContextRMS();
        public ActionResult Index()
        {
            List<Registration> registrations = db.Registrations.OrderByDescending(x => x.Id).ToList<Registration>();
            return View(registrations);
        }

        [HttpGet]
        public PartialViewResult Create()   //Insert PartialView  
        {
            return PartialView(); //new WebApplication1.Models.Registration()
        }
        [HttpPost]
        public JsonResult Create(Registration reg) // Record Insert  
        {
            DbContextRMS db = new DbContextRMS();
            db.Registrations.Add(reg);
            db.SaveChanges();
            return Json(reg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult Edit(Int32 Id)  // Update PartialView  
        {
            DbContextRMS db = new DbContextRMS();
            Registration reg = db.Registrations.Where(x => x.Id == Id).FirstOrDefault();
            Registration registration = new Registration();

            registration.EmailId = reg.Id.ToString();
            registration.Firstname = reg.Firstname;
            registration.Lastname = reg.Lastname;
            registration.ContactNo = reg.ContactNo;
            registration.EmailId = reg.EmailId;

            return PartialView(registration);
        }

        [HttpPost]
        public JsonResult Edit(Registration registration)  // Record Update 
        {

            DbContextRMS db = new DbContextRMS(); ;
            Registration reg = db.Registrations.Where(x => x.Id == registration.Id).FirstOrDefault();


            reg.Firstname = registration.Firstname;
            reg.Lastname = registration.Lastname;
            reg.ContactNo = registration.ContactNo;
            reg.EmailId = registration.EmailId;
            db.SaveChanges();

            return Json(reg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(Int32 Id)
        {
            Registration reg = db.Registrations.Where(x => x.Id == Id).FirstOrDefault();
            db.Registrations.Remove(reg);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}