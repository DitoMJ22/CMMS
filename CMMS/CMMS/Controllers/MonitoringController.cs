using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMMS.Models;

namespace CMMS.Controllers
{
    public class MonitoringController : Controller
    {
        Monitoring _monitoring = new Monitoring();

        public ActionResult ViewMonitoring()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            //UserAPIModel user = (UserAPIModel)Session["user"];
            return View(_monitoring.getAll());
        }
    }
}