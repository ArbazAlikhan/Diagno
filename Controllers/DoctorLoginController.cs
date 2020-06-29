using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticMedicalCenter.Models;

namespace DiagnosticMedicalCenter.Controllers
{
    public class DoctorLoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection formData)
        {
            if ((formData["UserId"] == null) || (formData["UserId"] == "")|| (formData["UserId"] == " "))
            {
                ViewBag.ValidationMessage = "User Id cannot be blank.";
            }
            else if ((formData["Password"] == null) || (formData["Password"] == "")|| (formData["Password"] == " "))
            {
                ViewBag.ValidationMessage = "Password cannot be blank.";
            }
            else
            {
                int userId = Convert.ToInt32(formData["UserId"]);
                string password = formData["Password"];
                DBContext DoctorLogin = new DBContext();
                foreach (var item in DoctorLogin.Doctors)
                {
                    if ((item.DoctorId == userId) && (item.Password == password) && (item.Status == "Accepted"))
                    {
                        Session["UserId"] = item.DoctorId;
                        Session["Password"] = item.Password;

                        return RedirectToAction("DoctorHome");
                    }
                    else
                    {
                        ViewBag.ValidationMessage = "Invalid User Id (or) Incorrect Password(or) The request is Pending with Admin.";
                        
                    }

                }
            }
            return View("Login");
        }
        public ActionResult DoctorHome()
        {
            if ((Session["UserId"] != null) || (Session["Password"] != null))
            {
                ViewBag.ValidationMessage = "Login Success";
                return View("DoctorHome");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}