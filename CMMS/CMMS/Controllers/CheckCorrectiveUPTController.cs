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
    public class CheckCorrectiveUPTController : Controller
    {
        MaintenanceCorrective _corrective = new MaintenanceCorrective();
        Corrective _workorder = new Corrective();

        Callendar _callendar = new Callendar();

        Sparepart _sparepart = new Sparepart();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: KAUPT
        public ActionResult Corrective()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            return View(_corrective.getAllWOC());
        }


        [HttpGet]
        public ActionResult DetailCorrective(string id)
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

        [HttpGet]
        public ActionResult WOCAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckCorrectiveUPT";
            if (id != null)
            {
                id = _corrective.setWorkOrderCorrectiveID(id);
                TempData["Data"] = _corrective.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
        }

        [HttpGet]
        public ActionResult ActionWOCAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckCorrectiveUPT";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                _workorder.woc_acc2(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
        }

        [HttpGet]
        public ActionResult WOCReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckCorrectiveUPT2";
            if (id != null)
            {
                id = _corrective.setWorkOrderCorrectiveID(id);
                TempData["Data"] = _corrective.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
        }

        [HttpGet]
        public ActionResult ActionWOCReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckCorrectiveUPT2";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                _workorder.woc_reject2(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
        }
    }
}