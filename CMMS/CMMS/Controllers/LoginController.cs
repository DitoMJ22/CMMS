using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class LoginController : Controller
    {

        User _user = new User();

        // GET: Login
        public ActionResult User()
        {
            //Notification notification = new Notification();
            //notification.triggernotification();
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(UserAPIModel userModel)
        {
            try
            {
                UserAPIModel model = new UserAPIModel()
                {
                    result      = userModel.result.ToString(),
                    npk         = userModel.npk.ToString(),
                    username    = userModel.username.ToString(),
                    nama        = userModel.nama.ToString(),
                    //email       = userModel.email.ToString(),
                    struktur    = userModel.struktur.ToString(),
                    jabatan     = userModel.jabatan.ToString(),
                    role        = userModel.role.ToString(),
                };
                Session["user"] = model;
                Session.Timeout = 120000;
                if (Session["user"] != null)
                {
                    TempData["SuccessMessage"] = "Welcome " + model.nama.ToString();
                    TempData["Direct"] = null;
                    if (model.jabatan.ToString() == "Kepala Seksi" && model.struktur.ToString() == "Unit Pelayanan Teknis Perawatan")
                    {
                        TempData["Direct"] = "KepalaMaintenance";
                    }
                    else if(model.jabatan.ToString() == "Staff" && model.struktur.ToString() == "Unit Pelayanan Teknis Manufaktur")
                    {
                        TempData["Direct"] = "PICMaintenanceUPT";
                    }
                    else if (model.jabatan.ToString() == "Kepala Seksi" && model.struktur.ToString() == "Unit Pelayanan Teknis Manufaktur")
                    {
                        TempData["Direct"] = "KepalaUPT";
                    }
                    else if (model.jabatan.ToString() == "Staff" && model.struktur.ToString() == "Unit Pelayanan Teknis Perawatan")
                    {
                        TempData["Direct"] = "Maintenance";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Wrong Employees Number or Password !";
                return RedirectToAction("User");
            }
            return RedirectToAction("User");
        }

        public ActionResult Logout()
        {
            TempData["Message"] = "";
            Session.Remove("User");
            //Session.Clear();
            return RedirectToAction("User");
        }
    }
}