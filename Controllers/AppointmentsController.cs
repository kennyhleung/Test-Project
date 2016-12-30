using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthpeuticsAPI.Controllers
{
    public class AppointmentsController : Controller
    {
        public ActionResult Render()
        {
            ViewBag.Title = "Healthpeutics - Appointments";

            return View("~/Views/Appointments.cshtml");
        }
    }
}
