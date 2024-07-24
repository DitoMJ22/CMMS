using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMMS.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Controllers
{
    public class AdminPreventiveController : Controller
    {
        // GET: AdminPreventive
        AdminPreventive _preventive = new AdminPreventive();
        Preventive _workorder = new Preventive();
        Callendar _callendar = new Callendar();
        Sparepart _sparepart = new Sparepart();
        Machine _machine = new Machine();
        Lab _lab = new Lab();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: AdminPreventive
        public ActionResult Preventive()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View(_preventive.getAllApproved());
        }


        public ActionResult CreatePreventive(string id)
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            //PreventiveModel woPreventiveModel = _workorder.getDataWOPreventiveUser(id);
            //return View(woPreventiveModel);

            id = _workorder.setWorkOrderPreventiveID(id);

            PreventiveModel adminPreventiveModel = _workorder.getDataWOPreventiveUser(id);
            //PreventiveModel adminPreventiveModel = _workorder.getDataWOPreventiveUser(id, maintenanceby);
            return View(adminPreventiveModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePreventive(PreventiveModel adminPreventiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            var id = _preventive.getWorkOrderID(adminPreventiveModel.id_wop);

            id = _preventive.setWorkOrderPreventiveID(id);
            TempData["Message"] = id;
            if (adminPreventiveModel.id_wop != null)
            {
                _preventive.wop_plot(id, adminPreventiveModel.maintenance_by, adminPreventiveModel.start_date);
                CallendarModel callendarModel = new CallendarModel();
                callendarModel.title = "PM " + adminPreventiveModel.id_machine + "-" + _machine.getData(adminPreventiveModel.id_machine).name;
                callendarModel.description = "Location : " + _lab.getData(_machine.getData2(adminPreventiveModel.id_machine).lab).uptname;
                callendarModel.start = DateTime.Now.ToString();
                callendarModel.end = adminPreventiveModel.start_date;

                if (_callendar.insert(callendarModel))
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
                TempData["EmptyMessage"] = "Data not found!";
            }

            return RedirectToAction("Preventive");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePreventive(AdminPreventiveModel adminPreventiveModel)
        //{
        //    @TempData["Direct"] = "AdminPloting";
        //    if (ModelState.IsValidField("maintenance_by") && ModelState.IsValidField("start_date"))
        //    {
        //        AdminPreventiveModel temp = _Preventive.getData(adminPreventiveModel.id_wop);

        //        adminPreventiveModel.maintenance_by = adminPreventiveModel.maintenance_by;
        //        if (_Preventive.update(adminPreventiveModel))
        //        {
        //            TempData["SuccessMessage"] = "Data Succesfully Updated";
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "System Error, Contact Administrator";
        //        }
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Please Complete The Form Data!";
        //    }
        //    return View(adminPreventiveModel);

        //}


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

            PreventiveModel adminPreventiveModel = _workorder.getDataWOPreventiveUser(id);
            return View(adminPreventiveModel);
        }
    }
}