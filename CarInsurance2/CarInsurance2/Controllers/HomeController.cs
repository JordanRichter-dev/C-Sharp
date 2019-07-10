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
            DateTime dob, int carYear, string carMake, string carModel,
            bool dui, int tickets, bool fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) 
                || string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(tickets.ToString())
                || string.IsNullOrEmpty(dob.ToString()) || string.IsNullOrEmpty(carYear.ToString())
                || string.IsNullOrEmpty(fullCoverage.ToString()) || string.IsNullOrEmpty(dui.ToString()))
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
                    quote += (10 * tickets);
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
                age = age - 1;
            return age;
        }




    }
}