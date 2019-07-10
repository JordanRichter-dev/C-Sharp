using CarInsurance2.Models;
using CarInsurance2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var signups = db.SignUps.ToList();
                var signupVms = new List<SignUpVm>();
                foreach (var signup in signups)
                {
                    var signupVm = new SignUpVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signup.Quote = signup.Quote;
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
        }
        public ActionResult Display(int Id)
        {
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var signup = db.SignUps.Find(Id);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}