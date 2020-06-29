using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticMedicalCenter.Models;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace DiagnosticMedicalCenter.Controllers
{
    public class BookAppointmentController : Controller
    {
        // GET: BookAppointment
        public ActionResult AppointmentForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AppointmentForm(BookAppointment bookAppointment, FormCollection formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidationMessage = "Please update the highlighted mandatory field(s)";
                return View();
            }
            else
            {
                DataBaseContext bookappointmentContext = new DataBaseContext();
                DateTime dateofAppointment = DateTime.ParseExact(formData["SelectDate"], "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                if(dateofAppointment >= DateTime.Now.Date)
                {
                    bookappointmentContext.BookAppointments.Add(bookAppointment);
                    bookappointmentContext.SaveChanges();
                    ViewBag.ValidationMessage = "Your details are submitted succesfully.";
                    return View();
                }
                else
                {
                    ViewBag.ValidationMessage = "Please select Upcoming Dates.";
                    return View();
                }
                
            }
        }
    }
}