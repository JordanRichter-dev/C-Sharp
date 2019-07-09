using CarInsuranceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress,
            DateTime birthDay, int carYear, string carMake, string carModel, 
            bool dui, int tickets, bool fullCoverage, bool liabilityOnly)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (CarInsuranceEntities1 db = new CarInsuranceEntities1())
                {
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;
                    signup.DOB = birthDay;
                    signup.CarYear = carYear;
                    signup.CarMake = carMake;
                    signup.CarModel = carModel;
                    signup.DUI = dui;
                    signup.Tickets = tickets;
                    signup.FullCoverage = fullCoverage;
                    signup.LiabilityOnly = liabilityOnly;
                    

                    db.SignUps.Add(signup);
                    db.SaveChanges();
                }




                return View("DisplayQuote");
            }
        }
    }
}