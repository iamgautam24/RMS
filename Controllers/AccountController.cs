using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public class AccountController : Controller
    {
        private DbContextRMS db = new DbContextRMS();

        // GET: Account

        public ActionResult Index()
        {
            // var Displaylist = RoleRegistrationViewModel.
            //var RoleList = List < List < Roles >>;
            return View(db.Registrations.ToList());

            //return View(RoleRegistrationViewModel);
        }

        // GET: Account/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            Registration registration = new Registration();
            List<Models.Roles> role = new List<Models.Roles> {
                    new Models.Roles { ID=1,RoleName="Administrator",Checked=false},
                    new Models.Roles { ID = 2, RoleName = "Recruiter",Checked=false},
                    new Models.Roles {ID= 3, RoleName = "Interviewer",Checked=false}
                };
            registration.AvailableRoles = role;
            return View(registration);
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                //registration.RoleName = "Administrator";

                var isEmailAlreadyExists = db.Registrations.Any(x => x.EmailId == registration.EmailId);
                var isPhoneAlreadyExists = db.Registrations.Any(x => x.ContactNo == registration.ContactNo);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(registration);
                }
                if (isPhoneAlreadyExists)
                {
                    ModelState.AddModelError("Phone Number", "User with this phone number already exists");
                    return View(registration);
                }
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registration);
        }

        // GET: Account/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);

        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Phone,Email,Password")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration);
        }

        // GET: Account/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }
        //login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Registration log)
        {
            using (DbContextRMS db = new DbContextRMS())
            {
                var user = db.Registrations.SingleOrDefault(u => u.EmailId == log.EmailId && u.Password == log.Password);
                if (user != null)
                {
                    Session["Email"] = user.Id.ToString();
                    Session["Firstname"] = user.Firstname.ToString();
                    return RedirectToAction("LoggedIn");

                }

                else
                {
                    // ModelState.AddModelError("", "Email or Password is incorrect");
                    return RedirectToAction(nameof(LoginError));
                }
                //return View();
            }


        }
        public ActionResult LoginError()
        {
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["Email"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}