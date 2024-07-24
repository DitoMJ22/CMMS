using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMMS.Models;
using Newtonsoft.Json;

namespace CMMS.Controllers
{
    public class AdminCorrectiveController : Controller
    {
        // GET: AdminCorrective
        AdminCorrective _corrective = new AdminCorrective();
        Corrective _workorder = new Corrective();
        Callendar _callendar = new Callendar();
        Sparepart _sparepart = new Sparepart();
        Machine _machine = new Machine();
        Lab _lab = new Lab();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: AdminCorrective
        public ActionResult Corrective()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            else if (Session["user"] != null)
            {
                UserAPIModel user = (UserAPIModel)Session["user"];
                var jabatan = user.jabatan;
                var struktur = user.struktur;

            }

            UserAPIModel user1 = (UserAPIModel)Session["user"];
            if (user1.jabatan.ToString() != "Kepala Seksi" && user1.struktur.ToString() != "Unit Pelayanan Teknis Perawatan")
            {
                return RedirectToAction("User", "Login");
            }
            return View(_corrective.getAllApproved());
        }


        public ActionResult CreateCorrective(string id)
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            id = _workorder.setWorkOrderCorrectiveID(id);

            CorrectiveModel adminCorrectiveModel = _workorder.getDataWOCorrectiveUser(id);
            //CorrectiveModel adminCorrectiveModel = _workorder.getDataWOCorrectiveUser(id, maintenanceby);
            return View(adminCorrectiveModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCorrective(CorrectiveModel adminCorrectiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            var id = _corrective.getWorkOrderID(adminCorrectiveModel.id_woc);

            id = _corrective.setWorkOrderCorrectiveID(id);
            TempData["Message"] = id;
            if (adminCorrectiveModel.id_woc != null)
            {
                _corrective.woc_plot(id, adminCorrectiveModel.maintenance_by, adminCorrectiveModel.deadline);
                CallendarModel callendarModel = new CallendarModel();
                callendarModel.title = "CM " + adminCorrectiveModel.id_machine + "-" + _machine.getData(adminCorrectiveModel.id_machine).name;
                callendarModel.description = "Location : " + _lab.getData(_machine.getData2(adminCorrectiveModel.id_machine).lab).uptname;
                callendarModel.start = DateTime.Now.ToString();
                callendarModel.end = adminCorrectiveModel.deadline;

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

            return RedirectToAction("Corrective");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateCorrective(AdminCorrectiveModel adminCorrectiveModel)
        //{
        //    @TempData["Direct"] = "AdminPloting";
        //    if (ModelState.IsValidField("maintenance_by") && ModelState.IsValidField("start_date"))
        //    {
        //        AdminCorrectiveModel temp = _corrective.getData(adminCorrectiveModel.id_woc);

        //        adminCorrectiveModel.maintenance_by = adminCorrectiveModel.maintenance_by;
        //        if (_corrective.update(adminCorrectiveModel))
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
        //    return View(adminCorrectiveModel);

        //}


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

            CorrectiveModel adminCorrectiveModel = _workorder.getDataWOCorrectiveUser(id);
            return View(adminCorrectiveModel);
        }

    }
}