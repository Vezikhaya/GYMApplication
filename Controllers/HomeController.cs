using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMApplication.Models;
using Microsoft.AspNet.Identity;

namespace GYMApplication.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]

        public ActionResult Index()
        {
            if (User.IsInRole("Receptionist"))
            {
                return RedirectToAction("Receptionist");
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("IndexAdmin");
            }
            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("IndexManager");
            }
            if (User.IsInRole("Driver"))
            {
                return RedirectToAction("IndexDriver");
            }
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("IndexEmployee");
            }
            return View();
        }

        public ActionResult MyMembership()
        {
            var userID = User.Identity.GetUserId();

            return View(db.MemberRegistrations.Where(x => x.MainMemberID == userID).ToList());
        }
        public ActionResult Athorize()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public void SendEmail(string Email)
        {
            var email = new Email();
            email.To = Email;
            email.Body = "Dear Customer<br/>Welcome to our Gym<br/> <br/><br/>Regurds<br/><br/>";
            email.Subject = "Gym Membership";
            email.Sendmail();
        }
            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}