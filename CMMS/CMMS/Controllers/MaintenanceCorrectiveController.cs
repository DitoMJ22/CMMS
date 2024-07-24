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
    public class MaintenanceCorrectiveController : Controller
    {
        MaintenanceCorrective _corrective = new MaintenanceCorrective();

        Callendar _callendar = new Callendar();

        Sparepart _sparepart = new Sparepart();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: Maintenance
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult MyCorrective()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            UserAPIModel user = (UserAPIModel)Session["user"];
            string id_user = user.npk;

            List<MaintenanceCorrectiveModel> maintenanceCorrectiveModel = _corrective.getMyWOC(id_user);
            return View(_corrective.getMyWOC(id_user));
        }

        [HttpGet]
        public ActionResult MyCorrectiveDetail(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _corrective.setWorkOrderCorrectiveID(id);
            MaintenanceCorrectiveModel maintenanceCorrectiveModel = _corrective.getData(id);
            return View(maintenanceCorrectiveModel);
        }

        [HttpPost]
        public ActionResult MyCorrectiveDetail(MaintenanceCorrectiveModel maintenanceCorrectiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenanceCorrective";
            if (maintenanceCorrectiveModel.desc_maintenance != "")
            {
                MaintenanceCorrectiveModel temp = _corrective.getData(maintenanceCorrectiveModel.id_woc);

                maintenanceCorrectiveModel.maintenance_cost = Convert.ToInt32(maintenanceCorrectiveModel.maintenance_cost);


                if (_corrective.maintenanceupdate(maintenanceCorrectiveModel))
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

            return RedirectToAction("MyCorrectiveDetail", new { id = _corrective.getWorkOrderID(maintenanceCorrectiveModel.id_woc) });
        }


        [HttpGet]
        public ActionResult MyCorrectiveSparepart(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _corrective.setWorkOrderCorrectiveID(id);
            MaintenanceCorrectiveModel maintenanceCorrectiveModel = _corrective.getData(id);
            return View(maintenanceCorrectiveModel);
        }


        [HttpGet]
        public ActionResult MyCorrectiveEvidence(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            id = _corrective.setWorkOrderCorrectiveID(id);
            MaintenanceCorrectiveModel maintenanceCorrectiveModel = _corrective.getData(id);
            return View(maintenanceCorrectiveModel);
        }

        [HttpGet]
        public ActionResult MyCorrectiveFinish(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenanceCorrective";
            if (id != null)
            {
                id = _corrective.setWorkOrderCorrectiveID(id);
                TempData["Finish"] = _corrective.getWorkOrderID(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }

            return RedirectToAction("MyCorrectiveDetail", new { id = _corrective.getWorkOrderID(id) });

        }

        [HttpGet]
        public ActionResult ActionMyCorrectiveFinish(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenancePrenvetif";
            if (id != null)
            {
                id = _corrective.setWorkOrderCorrectiveID(id);
                _corrective.maintenancefinish(id);
                //_callendar.finish(_corrective.getData(id).id_callendar);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }

            return RedirectToAction("MyCorrectiveDetail", new { id = _corrective.getWorkOrderID(id) });
        }


        [HttpPost]
        public ActionResult ActionCreateSparepartWOC(string id_woc, string id_sparepart, string quantity)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            TempData["Direct"] = "MaintenanceCorrective";

            if (id_woc != "" || id_sparepart != "" || quantity != "")
            {
                string labelcost = _sparepart.getData(id_sparepart).price;
                int cost = Convert.ToInt32(labelcost.Substring(0, labelcost.IndexOf(","))) * Convert.ToInt32(quantity);

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM [Sparepart] WHERE id=@id_sparepart", con);
                con.Open();
                cmd1.Parameters.AddWithValue("@id_sparepart", id_sparepart);
                SqlDataReader dr = cmd1.ExecuteReader();
                dr.Read();
                int stok = Convert.ToInt32(dr["Stock"]);
                dr.Close();

                if (stok < Convert.ToInt32(quantity))
                {
                    TempData["Direct"] = "Stock";
                    TempData["Data"] = id_woc;
                    TempData["EmptyMessage"] = "Data not found!";

                }
                else
                {
                    _corrective.insert_sparepartwoc(id_woc, quantity, cost, id_sparepart);
                    TempData["SuccessMessage"] = "Sparepart Machine has been succesfully Added" + quantity.ToString();
                }
            }

            return RedirectToAction("MyCorrectiveSparepart", new { id = _corrective.getWorkOrderID(id_woc) });
        }

        [HttpGet]
        public ActionResult ActionDeleteSparepartWOC(string id_woc, string id_sparepart)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            TempData["Direct"] = "MaintenanceCorrective";

            if (id_woc != "" || id_sparepart != "")
            {
                id_woc = _corrective.setWorkOrderCorrectiveID(id_woc);
                _corrective.delete_sparepartmachinewoc(id_woc, id_sparepart);
                TempData["SuccessMessage"] = "Sparepart Machine has been succesfully Deleted";
            }

            return RedirectToAction("MyCorrectiveSparepart", new { id = _corrective.getWorkOrderID(id_woc) });
        }

        [HttpPost]
        public ActionResult ActionCreatePhotoWOC(MaintenanceCorrectiveModel maintenanceCorrectiveModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenanceCorrective";
            foreach (HttpPostedFileBase file in maintenanceCorrectiveModel.photos)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    string id_woc = _corrective.getWorkOrderID(maintenanceCorrectiveModel.id_woc);
                    var InputFileName = Path.GetFileName(id_woc + "_" + _corrective.idphoto() + ".png"); //penamaan file photo
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/WOC/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    _corrective.insert_photo(id_woc, InputFileName);

                    TempData["SuccessMessage"] = "Evidence photo has been succesfully Added";

                }
            }
            return RedirectToAction("MyCorrectiveEvidence", new { id = _corrective.getWorkOrderID(maintenanceCorrectiveModel.id_woc) });
        }

        [HttpGet]
        public ActionResult ActionDeletePhotoWOC(string id_photo, string id_woc)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "MaintenanceCorrective";
            if (id_woc != null && id_photo != null)
            {
                _corrective.delete_photoCorrective(id_photo);
                TempData["SuccessMessage"] = "Evidence photo has been succesfully Deleted";

            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("MyCorrectiveEvidence", new { id = id_woc });
        }
    }
}