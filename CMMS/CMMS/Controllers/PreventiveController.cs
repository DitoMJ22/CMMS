using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMMS.Models;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class PreventiveController : Controller
    {
        Preventive _workorder = new Preventive();
        Machine _machine = new Machine();
        Lab _lab = new Lab();
        Callendar _callendar = new Callendar();
        //Notification _notification = new Notification();

        public ActionResult ViewPreventive()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user = (UserAPIModel)Session["user"];
            return View(_workorder.getAllWOPreventiveUser(user.npk));
        }

        public ActionResult CreatePreventive()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            PreventiveModel woPreventiveModel = new PreventiveModel();
            return View(woPreventiveModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePreventive(PreventiveModel woPreventiveModel)
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (ModelState.IsValid)
            {
                UserAPIModel user = (UserAPIModel)Session["user"];
                woPreventiveModel.requested_by = user.npk;
                woPreventiveModel.status = "Draft";

                if (_workorder.wopinsert(woPreventiveModel))
                {

                    TempData["SuccessMessage"] = "Data Succesfully Added";

                }
                else
                {
                    TempData["ErrorMessage"] = "System Error, Contact Administrator";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }

            return RedirectToAction("ViewPreventive");
        }

        [HttpGet]
        public ActionResult UpdatePreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder.setWorkOrderPreventiveID(id);

            PreventiveModel woPreventiveModel = _workorder.getDataWOPreventiveUser(id);
            woPreventiveModel.schedule_date = Convert.ToDateTime(woPreventiveModel.schedule_date).ToString("yyyy-MM-dd");
            woPreventiveModel.start_date = Convert.ToDateTime(woPreventiveModel.start_date).ToString("yyyy-MM-dd");
            woPreventiveModel.finish_date = Convert.ToDateTime(woPreventiveModel.finish_date).ToString("yyyy-MM-dd");
            return View(woPreventiveModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdatePreventive(PreventiveModel woPreventiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (ModelState.IsValid)
            {

                if (_workorder.wopupdate(woPreventiveModel))
                {

                    TempData["SuccessMessage"] = "Data Succesfully Updated";

                }
                else
                {
                    TempData["ErrorMessage"] = "System Error, Contact Administrator";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }
            return View(woPreventiveModel);
        }

        [HttpGet]
        public ActionResult DeletePreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                TempData["Data"] = _workorder.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
        }

        [HttpGet]
        public ActionResult ActionDeletePreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);

                _callendar.delete(_workorder.getDataWOPreventiveUser(id).id_callendar);
                _workorder.wopdelete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
        }

        [HttpGet]
        public ActionResult SendPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                TempData["SendValue"] = _workorder.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
        }

        [HttpGet]
        public ActionResult ActionSendPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                _workorder.wopsend(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
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
        public ActionResult ReminderPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                TempData["Reminder"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
        }

        [HttpGet]
        public ActionResult ActionReminderPreventive(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Preventive";
            if (id != null)
            {
                id = _workorder.setWorkOrderPreventiveID(id);
                //_notification.notificationwop(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewPreventive");
        }
    }
}