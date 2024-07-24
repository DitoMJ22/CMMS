using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class CorrectiveController : Controller
    {
        Corrective _workorder = new Corrective();
        Machine _machine = new Machine();
        Lab _lab = new Lab();
        Callendar _callendar = new Callendar();
        //Notification _notification = new Notification();

        public ActionResult ViewCorrective()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user = (UserAPIModel)Session["user"];
            return View(_workorder.getAllWOCorrectiveUser(user.npk));
        }

        public ActionResult CreateCorrective()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            CorrectiveModel woCorrectiveModel = new CorrectiveModel();
            return View(woCorrectiveModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCorrective(CorrectiveModel woCorrectiveModel)
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (ModelState.IsValid)
            {
                UserAPIModel user = (UserAPIModel)Session["user"];
                woCorrectiveModel.requested_by = user.npk;
                woCorrectiveModel.status = "Draft";

                if (_workorder.wocinsert(woCorrectiveModel))
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

            return RedirectToAction("ViewCorrective");
        }

        [HttpGet]
        public ActionResult UpdateCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder.setWorkOrderCorrectiveID(id);

            CorrectiveModel woCorrectiveModel = _workorder.getDataWOCorrectiveUser(id);
            woCorrectiveModel.request_date = Convert.ToDateTime(woCorrectiveModel.request_date).ToString("yyyy-MM-dd");
            woCorrectiveModel.deadline = Convert.ToDateTime(woCorrectiveModel.deadline).ToString("yyyy-MM-dd");
            woCorrectiveModel.finish_date = Convert.ToDateTime(woCorrectiveModel.finish_date).ToString("yyyy-MM-dd");
            return View(woCorrectiveModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateCorrective(CorrectiveModel woCorrectiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (ModelState.IsValid)
            {

                if (_workorder.wocupdate(woCorrectiveModel))
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
            return View(woCorrectiveModel);
        }

        [HttpGet]
        public ActionResult DeleteCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                TempData["Data"] = _workorder.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
        }

        [HttpGet]
        public ActionResult ActionDeleteCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);

                _callendar.delete(_workorder.getDataWOCorrectiveUser(id).id_callendar);
                _workorder.wocdelete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
        }

        [HttpGet]
        public ActionResult SendCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                TempData["SendValue"] = _workorder.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
        }

        [HttpGet]
        public ActionResult ActionSendCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                _workorder.wocsend(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
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
        public ActionResult ReminderCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                TempData["Reminder"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
        }

        [HttpGet]
        public ActionResult ActionReminderCorrective(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Corrective";
            if (id != null)
            {
                id = _workorder.setWorkOrderCorrectiveID(id);
                //_notification.notificationwoc(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("ViewCorrective");
        }
    }
}