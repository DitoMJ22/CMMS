using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMMS.Models;
using System.IO;


namespace CMMS.Controllers
{
    public class MachineController : Controller
    {
        User _user = new User();
        Machine _machine = new Machine();

        // GET: Machine
        public ActionResult MachineView()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View(_machine.getAllData());
        }

        public ActionResult Create()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            MachineModel machineModel = new MachineModel();
            return View(machineModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(MachineModel machineModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Machine";
            if (ModelState.IsValid)
            {
                if (_machine.isUniqueNoAsset(machineModel.no_asset))
                {
                    if (_machine.insert(machineModel))
                    {
                        TempData["SuccessMessage"] = "Data Succesfully Added";

                        foreach (HttpPostedFileBase file in machineModel.photos)
                        {
                            //Checking file is available to save.  
                            if (file != null)
                            {
                                var InputFileName = Path.GetFileName(machineModel.no_asset + "_" + _machine.idphoto() + ".png"); //penamaan file photo
                                var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/Machine/") + InputFileName);
                                //Save file to server folder  
                                file.SaveAs(ServerSavePath);

                                _machine.insert_photo(machineModel.no_asset, InputFileName);
                            }
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "System Error, Contact Administrator";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Employee Number has been registered!";
                }
                return RedirectToAction("MachineView", "Machine");
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }
            return View(machineModel);
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
            MachineModel machineModel = _machine.getData(id);
            return View(machineModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Update(MachineModel machineModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Machine";
            if (ModelState.IsValidField("name") && ModelState.IsValidField("model") && ModelState.IsValidField("merk") && ModelState.IsValidField("year"))
            {
                if (_machine.update(machineModel))
                {
                    TempData["SuccessMessage"] = "Data Succesfully Update";
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
            return View(machineModel);
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

            TempData["Direct"] = "Machine";
            if (id != null && _machine.isData(id))
            {
                TempData["Data"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("MachineView");
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

            TempData["Direct"] = "Machine";
            if (id != null && _machine.isData(id))
            {
                _machine.delete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("MachineView");
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

            MachineModel machineModel = _machine.getData(id);
            return View(machineModel);
        }

        [HttpGet]
        public ActionResult DeletePhoto(string id_photo, string id_machine)
        {
            TempData["Direct"] = "Machine";
            TempData["DataPhoto"] = id_photo;
            TempData["DataMachine"] = id_machine;
            return RedirectToAction("Detail", new { id = id_machine });
        }

        [HttpGet]
        public ActionResult ActionDeletePhoto(string id_photo, string id_machine)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Machine";
            if (id_photo != null && id_machine != null)
            {
                _machine.delete_photoMachine(id_photo);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Detail", new { id = id_machine });
        }

        [HttpPost]
        public ActionResult ActionCreatePhoto(MachineModel machineModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Machine";
            foreach (HttpPostedFileBase file in machineModel.photos)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(machineModel.no_asset + "_" + _machine.idphoto() + ".png"); //penamaan file photo
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/Machine/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);

                    _machine.insert_photo(machineModel.no_asset, InputFileName);

                    TempData["SuccessMessage"] = "Photo Machine has been succesfully Uploaded";

                }
            }
            return RedirectToAction("Detail", new { id = machineModel.no_asset });
        }

        [HttpGet]
        public ActionResult Sparepart(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            MachineModel machineModel = _machine.getData(id);
            return View(machineModel);
        }

        [HttpPost]
        public ActionResult ActionCreateSparepart(string id_machine, string id_sparepart, string quantity)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            TempData["Direct"] = "Machine";

            if (id_machine != "" || id_sparepart != "" || quantity != "")
            {

                TempData["SuccessMessage"] = "Sparepart Machine has been succesfully Added";
            }

            return RedirectToAction("Sparepart", new { id = id_machine });
        }

        [HttpGet]
        public ActionResult History(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            MachineModel machineModel = _machine.getData(id);
            return View(machineModel);
        }
    }
}