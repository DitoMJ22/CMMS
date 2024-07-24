using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class LabController : Controller
    {
        Lab _lab = new Lab();

        // GET: User
        public ActionResult LabView()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View(_lab.getAllData());
        }

        public ActionResult Create()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            LabModel labModel = new LabModel();
            return View(labModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(LabModel labModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Lab";
            if (ModelState.IsValid)
            {
                if (_lab.insert(labModel))
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
            return View(labModel);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            LabModel labModel = _lab.getData(id);
            return View(labModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Update(LabModel labModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Lab";
            if (ModelState.IsValidField("upt") && ModelState.IsValidField("pic") && ModelState.IsValidField("name"))
            {

                if (_lab.update(labModel))
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
            return View(labModel);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Lab";
            if (id != null && _lab.isData(id))
            {
                TempData["Data"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("LabView");
        }

        [HttpGet]
        public ActionResult ActionDelete(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Lab";
            if (id != null && _lab.isData(id))
            {
                _lab.delete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("LabView");
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            LabModel labModel = _lab.getData(id);
            return View(labModel);
        }
    }
}