using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMMS.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class CheckPreventiveUPTController : Controller
    {
        MaintenancePreventive _preventive = new MaintenancePreventive();
        Preventive _workorder = new Preventive();

        Callendar _callendar = new Callendar();

        Sparepart _sparepart = new Sparepart();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: KAUPT
        public ActionResult Preventive()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            return View(_preventive.getAllWOP());
        }


        [HttpGet]
        public ActionResult DetailPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder.setWorkOrderPreventiveID(id);

            PreventiveModel woPreventiveModel = _workorder.getDataWOPreventiveUser(id);
            return View(woPreventiveModel);
        }

        [HttpGet]
        public ActionResult WOPAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckPreventiveUPT";
            if (id != null)
            {
                id = _preventive.setWorkOrderPreventiveID(id);
                TempData["Data"] = _preventive.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
        }

        [HttpGet]
        public ActionResult ActionWOPAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckPreventiveUPT";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                _workorder.wop_acc2(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
        }

        [HttpGet]
        public ActionResult WOPReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckPreventiveUPT2";
            if (id != null)
            {
                id = _preventive.setWorkOrderPreventiveID(id);
                TempData["Data"] = _preventive.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
        }

        [HttpGet]
        public ActionResult ActionWOPReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "CheckPreventiveUPT2";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                _workorder.wop_reject2(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
        }
    }
}