using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticMedicalCenter.Models;

namespace DiagnosticMedicalCenter.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: PatientLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection formData)
        {
            if ((formData["UserId"] == null) || (formData["UserId"] == ""))
            {
                ViewBag.ValidationMessage = "User Id cannot be blank.";
            }
            else if ((formData["Password"] == null) || (formData["Password"] == ""))
            {
                ViewBag.ValidationMessage = "Password cannot be blank.";
            }
            else
            {
                int userId = Convert.ToInt32(formData["UserId"]);
                string password = formData["Password"];
                DataBaseContext adminLogin = new DataBaseContext();
                foreach (var item in adminLogin.Admins)
                {
                    if ((item.VendorId == userId) && (item.Password == password))
                    {
                        Session["UserId"] = item.VendorId;
                        Session["Password"] = item.Password;

                        return RedirectToAction("AdminHome");
                    }
                    else
                    {
                        ViewBag.ValidationMessage = "Invalid User Id (or) Incorrect Password";
                        return View("Login");
                    }

                }
            }
            return View();
        }
        public ActionResult AdminHome(Admin admin)
        {
            if ((Session["UserId"] != null) || (Session["Password"] != null))
            {
                ViewBag.ValidationMessage = "Login Success";
                return View("AdminHome");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        //from here 

        DataBaseContext viewTestsContext = new DataBaseContext();

        public ActionResult ListPatientIds()
        {
            if (Session["UserId"] != null)
            {
                List<BookAppointment> appointmentsList = viewTestsContext.BookAppointments.Where(m => m.Status.Equals("Approved")).ToList();
                return View(appointmentsList);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        // UPDATE TEST RESULTS
        public ActionResult UpdateTestResults(int patienId)
        {
            if (Session["UserId"] != null)
            {
                var patientDetails = viewTestsContext.BookAppointments.Find(patienId);
                var patientAge = viewTestsContext.Patients.Find(patientDetails.AppointmentId);
                ViewBag.AppointmentId = patientDetails.AppointmentId;
                ViewBag.PatientId = patientDetails.PatientId;
                ViewBag.DoctorId = patientDetails.DoctorId;
                ViewBag.Date = patientDetails.SelectDate;
                ViewBag.MedicareService = patientDetails.MedicareService;
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult SaveTestResults()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult SaveTestResults(TestResult testResults, FormCollection formData)
        {
            if (Session["UserId"] != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ValidationMessage = "Please fill the highlighted mandatory field(s).";
                    return View("UpdateTestResults");
                }
                else
                {
                    foreach (var item in viewTestsContext.TestResults)
                    {
                        if (item.PatientId == Int32.Parse(formData["PatientId"]) && item.MedicareService == formData["MedicareService"])
                        {
                            ViewBag.ValidationMessage = "Test Details already updated.";
                            return View("UpdateTestResults");
                        }
                    }
                    int patientId = Int32.Parse(formData["PatientId"]);
                    string medicareService = formData["MedicareService"];
                    viewTestsContext.TestResults.Add(testResults);
                    viewTestsContext.SaveChanges();
                    ViewBag.ValidationMessage = "Test results updated successfully.";
                    return View("UpdateTestResults");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }

        }

    }
}