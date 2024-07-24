using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMMS.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class MaintenancePreventiveController : Controller
    {
        MaintenancePreventive _preventive = new MaintenancePreventive();

        Callendar _callendar = new Callendar();

        Sparepart _sparepart = new Sparepart();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: Maintenance
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult MyPreventive()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user = (UserAPIModel)Session["user"];
            string id_user = user.npk;

            List<MaintenancePreventiveModel> maintenancePreventiveModel = _preventive.getMyWOP(id_user);
            return View(_preventive.getMyWOP(id_user));
        }

        [HttpGet]
        public ActionResult MyPreventiveDetail(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _preventive.setWorkOrderPreventiveID(id);
            MaintenancePreventiveModel maintenancePreventiveModel = _preventive.getData(id);
            return View(maintenancePreventiveModel);
        }

        [HttpPost]
        public ActionResult MyPreventiveDetail(MaintenancePreventiveModel maintenancePreventiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenancePreventive";
            if (maintenancePreventiveModel.desc_maintenance != "")
            {
                MaintenancePreventiveModel temp = _preventive.getData(maintenancePreventiveModel.id_wop);

                maintenancePreventiveModel.maintenance_cost = Convert.ToInt32(maintenancePreventiveModel.maintenance_cost);


                if (_preventive.maintenanceupdate(maintenancePreventiveModel))
                {
                    TempData["SuccessMessage"] = "Data Succesfully Updated";
                }
                else
                {
                    TempData["ErrorMessage"] = "Please Update the Yellow Tags";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }

            return RedirectToAction("MyPreventiveDetail", new { id = _preventive.getWorkOrderID(maintenancePreventiveModel.id_wop) });
        }


        [HttpGet]
        public ActionResult MyPreventiveSparepart(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _preventive.setWorkOrderPreventiveID(id);
            MaintenancePreventiveModel maintenancePreventiveModel = _preventive.getData(id);
            return View(maintenancePreventiveModel);
        }


        [HttpGet]
        public ActionResult MyPreventiveEvidence(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _preventive.setWorkOrderPreventiveID(id);
            MaintenancePreventiveModel maintenancePreventiveModel = _preventive.getData(id);
            return View(maintenancePreventiveModel);
        }

        [HttpGet]
        public ActionResult MyPreventiveFinish(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenancePreventive";
            if (id != null)
            {
                id = _preventive.setWorkOrderPreventiveID(id);
                TempData["Finish"] = _preventive.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }

            return RedirectToAction("MyPreventiveDetail", new { id = _preventive.getWorkOrderID(id) });

        }

        [HttpGet]
        public ActionResult ActionMyPreventiveFinish(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenancePrenvetive";
            if (id != null)
            {
                id = _preventive.setWorkOrderPreventiveID(id);
                _preventive.maintenancefinish(id);
                //_callendar.finish(_preventive.getData(id).id_callendar);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }

            return RedirectToAction("MyPreventiveDetail", new { id = _preventive.getWorkOrderID(id) });
        }


        [HttpPost]
        public ActionResult ActionCreateSparepartWOP(string id_wop, string id_sparepart, string quantity)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            TempData["Direct"] = "MaintenancePreventive";

            if (id_wop != "" || id_sparepart != "" || quantity != "")
            {
                string labelcost = _sparepart.getData(id_sparepart).price;
                int cost = Convert.ToInt32(labelcost.Substring(0, labelcost.IndexOf(","))) * Convert.ToInt32(quantity);
                _preventive.insert_sparepartwop(id_wop, quantity, cost, id_sparepart);
                TempData["SuccessMessage"] = "Sparepart Machine has been succesfully Added";
            }

            return RedirectToAction("MyPreventiveSparepart", new { id = _preventive.getWorkOrderID(id_wop) });
        }

        [HttpGet]
        public ActionResult ActionDeleteSparepartWOP(string id_wop, string id_sparepart)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            TempData["Direct"] = "MaintenancePreventive";

            if (id_wop != "" || id_sparepart != "")
            {
                id_wop = _preventive.setWorkOrderPreventiveID(id_wop);
                _preventive.delete_sparepartmachinewop(id_wop, id_sparepart);
                TempData["SuccessMessage"] = "Sparepart Machine has been succesfully Deleted";
            }

            return RedirectToAction("MyPreventiveSparepart", new { id = _preventive.getWorkOrderID(id_wop) });
        }
    }
}