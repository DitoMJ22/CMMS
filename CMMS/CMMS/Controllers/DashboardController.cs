using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View();
        }

        public ActionResult KepalaMaintenance()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user1 = (UserAPIModel)Session["user"];
            if (user1.jabatan.ToString() != "Kepala Seksi" && user1.struktur.ToString() != "Unit Pelayanan Teknis Perawatan")
            {
                return RedirectToAction("User", "Login");
            }

            return View();
        }

        public ActionResult PICMaintenanceUPT()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user1 = (UserAPIModel)Session["user"];
            if (user1.jabatan.ToString() != "Staff" && user1.struktur.ToString() != "Unit Pelayanan Teknis Manufaktur")
            {
                return RedirectToAction("User", "Login");
            }
            return View();
        }

        public ActionResult Maintenance()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user1 = (UserAPIModel)Session["user"];
            if (user1.jabatan.ToString() != "Staff" && user1.struktur.ToString() != "Unit Pelayanan Teknis Perawatan")
            {
                return RedirectToAction("User", "Login");
            }
            return View();
        }
        public ActionResult KepalaUPT()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user1 = (UserAPIModel)Session["user"];
            if (user1.jabatan.ToString() != "Kepala Seksi" && user1.struktur.ToString() != "Unit Pelayanan Teknis Manufaktur")
            {
                return RedirectToAction("User", "Login");
            }
            return View();
        }

    }
}