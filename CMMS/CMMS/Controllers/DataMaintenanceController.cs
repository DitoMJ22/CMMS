using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class DataMaintenanceController : Controller
    {
        MaintenanceCorrective _corrective = new MaintenanceCorrective();
        Corrective _workorder = new Corrective();

        Callendar _callendar = new Callendar();

        Sparepart _sparepart = new Sparepart();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: KAUPT
        public ActionResult CheckCorrectiveAdmin()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            return View(_corrective.getAllWOCFinished());
        }


        [HttpGet]
        public ActionResult DetailCheckCorrectiveAdmin(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder.setWorkOrderCorrectiveID(id);

            CorrectiveModel woCorrectiveModel = _workorder.getDataWOCorrectiveUser(id);
            return View(woCorrectiveModel);
        }
    }
}