using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthpeuticsAPI.Controllers
{
    public class VitalsController : Controller
    {
        public ActionResult Render()
        {
            ViewBag.Title = "Healthpeutics - Vitals";

            return View("~/Views/Vitals.cshtml");
        }
    }
}
