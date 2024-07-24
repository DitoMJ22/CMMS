using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class HeadOfUPTController : Controller
    {
        MaintenanceCorrective _corrective = new MaintenanceCorrective();
        Corrective _workorder = new Corrective();

        MaintenancePreventive _preventive = new MaintenancePreventive();
        Preventive _workorder1 = new Preventive();


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
        public ActionResult DetailPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder1.setWorkOrderPreventiveID(id);

            PreventiveModel woPreventiveModel = _workorder1.getDataWOPreventiveUser(id);
            return View(woPreventiveModel);
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

            TempData["Direct"] = "HeadOfUPTCorrective";
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
        public ActionResult WOPAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "HeadOfUPTPreventive";
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
        public ActionResult ActionWOCAccept(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "HeadOfUPTCorrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                _workorder.woc_acc(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
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

            TempData["Direct"] = "HeadOfUPTPreventive";
            if (id != null)
            {
                id = _workorder1.setWorkOrderPreventiveID(id);
                _workorder1.wop_acc(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
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

            TempData["Direct"] = "HeadOfUPTCorrectiveReject";
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
        public ActionResult WOPReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "HeadOfUPTPreventiveReject";
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
        public ActionResult ActionWOCReject(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "HeadOfUPTCorrectiveReject";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                _workorder.woc_reject1(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Corrective");
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

            TempData["Direct"] = "HeadOfUPTPreventiveReject";
            if (id != null)
            {
                id = _workorder1.setWorkOrderPreventiveID(id);
                _workorder1.wop_reject1(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Preventive");
        }


    }
}