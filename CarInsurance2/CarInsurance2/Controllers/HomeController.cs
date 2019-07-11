using CarInsurance2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress,
            DateTime dob, int? carYear, string carMake, string carModel,
            int? tickets, bool? dui, bool? fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) 
                || string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel) || tickets.HasValue == false || carYear.HasValue == false 
                || dui.HasValue == false || fullCoverage.HasValue == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            else 
            {
                //code for calculating a quote
                decimal quote = 50;
                int age = CalculateAge(dob);

                if (age <= 25 || age >= 100)
                {
                    quote += 25;
                }
                if (age <= 18)
                {
                    quote += 100;
                }

                if (carYear < 2000 || carYear > 2015)
                {
                    quote += 25;
                }

                if (carMake == "Porsche")
                {
                    quote += 25;
                }
                if (carMake == "Porsche" && carModel == "911 Carrera")
                {
                    quote += 25;
                }

                if (tickets >= 1)
                {
                    quote += (decimal)tickets * 10;
                }

                if (dui == true)
                {
                    Decimal duiMarkup = 1.25m;
                    quote *= duiMarkup;
                }

                if (fullCoverage == true)
                {
                    Decimal fullCovMarkup = 1.5m;
                    quote *= fullCovMarkup;
                }

                

                using (CarInsuranceEntities db = new CarInsuranceEntities())
                {
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;
                    signup.DOB = dob;
                    signup.CarYear = carYear;
                    signup.CarMake = carMake;
                    signup.CarModel = carModel;
                    signup.DUI = dui;
                    signup.Tickets = tickets;
                    signup.FullCoverage = fullCoverage;
                    signup.Quote = quote;


                    db.SignUps.Add(signup);
                    db.SaveChanges();
                }


                quote = Math.Round(quote, 2);
                ViewBag.Quote = quote;
                return View("DisplayQuote");
            }
        }

        


        // For calculating age  

        public int CalculateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age -= 1;
            return age;
        }




    }
}