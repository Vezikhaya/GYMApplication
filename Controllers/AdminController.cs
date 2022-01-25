using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult AdminClasses()
        {
            return View();
        }

        public ActionResult AdminLayout()
        {

            return View();
        }

    }
}