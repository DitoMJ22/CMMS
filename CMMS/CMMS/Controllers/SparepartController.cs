using CMMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class SparepartController : Controller
    {
        // GET: Sparepart
        User _user = new User();
        Sparepart _sparepart = new Sparepart();

        public ActionResult SparepartView()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View(_sparepart.getAllData());
        }

        // GET: Sparepart/Details/5
        [HttpGet]
        public ActionResult Detail(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            SparepartModel sparepartModel = _sparepart.getData(id);
            return View(sparepartModel);
        }

        // GET: Sparepart/Create
        public ActionResult Create()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            SparepartModel sparepartModel = new SparepartModel();
            return View(sparepartModel);
        }

        // POST: Sparepart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SparepartModel sparepartModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Sparepart";
            if (ModelState.IsValid)
            {
                string id_sparepart = _sparepart.idsparepart();
                if (_sparepart.insert(sparepartModel))
                {
                    TempData["SuccessMessage"] = "Data Succesfully Added";

                    foreach (HttpPostedFileBase file in sparepartModel.photos)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(id_sparepart + "_" + _sparepart.idphoto() + ".png"); //penamaan file photo
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/Sparepart/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);

                            _sparepart.insert_photo(id_sparepart, InputFileName);
                        }
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "System Error, Contact Administrator";
                }
                return RedirectToAction("SparepartView", "Sparepart");
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }
            return View(sparepartModel);
        }

        // GET: Sparepart/Edit/5
        public ActionResult Update(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            SparepartModel sparepartModel = _sparepart.getData2(id);
            return View(sparepartModel);
        }

        // POST: Sparepart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SparepartModel sparepartModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "Sparepart";
            if (ModelState.IsValidField("name") && ModelState.IsValidField("function") && ModelState.IsValidField("Stock") && ModelState.IsValidField("unit") && ModelState.IsValidField("price"))
            {
                if (_sparepart.update(sparepartModel))
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
            return RedirectToAction("SparepartView", "Sparepart");
        }

        // GET: Sparepart/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Sparepart";
            if (id != null && _sparepart.isData(id))
            {
                TempData["Data"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("SparepartView");
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

            TempData["Direct"] = "Sparepart";
            if (id != null && _sparepart.isData(id))
            {
                _sparepart.delete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("SparepartView");
        }

        // POST: Sparepart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeletePhoto(string id_photo, string id_sparepart)
        {
            TempData["Direct"] = "Sparepart";
            TempData["DataPhoto"] = id_photo;
            TempData["DataSparepart"] = id_sparepart;
            return RedirectToAction("Detail", new { id = id_sparepart });
        }

        [HttpGet]
        public ActionResult ActionDeletePhoto(string id_photo, string id_sparepart)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Sparepart";
            if (id_photo != null && id_sparepart != null)
            {
                _sparepart.delete_photoPart(id_photo);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("Detail", new { id = id_sparepart });
        }

        [HttpPost]
        public ActionResult ActionCreatePhoto(SparepartModel sparepartModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            TempData["Direct"] = "Sparepart";
            foreach (HttpPostedFileBase file in sparepartModel.photos)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(sparepartModel.id + "_" + _sparepart.idphoto() + ".png"); //penamaan file photo
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/Sparepart/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);

                    _sparepart.insert_photo(sparepartModel.id, InputFileName);

                    TempData["SuccessMessage"] = "Photo Sparepart has been succesfully Uploaded";

                }
            }
            string id_sparepart = sparepartModel.id;
            return RedirectToAction("Detail", new { id = id_sparepart });
        }
    }
}
