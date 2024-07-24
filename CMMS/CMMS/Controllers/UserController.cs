using CMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMMS.Controllers
{
    public class UserController : Controller
    {
        User _user = new User();


        // GET: User
        public ActionResult View()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            return View(_user.getAllData());
        }

        public ActionResult Create()
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }
            UserModel userModel = new UserModel();
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(UserModel userModel)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "User";
            if (ModelState.IsValid)
            {
                if (_user.isUniqueNIK(userModel.nik))
                {
                    if (_user.insert(userModel))
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
                    TempData["ErrorMessage"] = "Employee Number has been registered!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please Complete The Form Data!";
            }
            return View(userModel);
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
            UserModel userModel = _user.getData(id);
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Update(UserModel userModel, string temppassword)
        {
            // Throw session timeout
            if (Session["user"] == null)
            {
                TempData["Message"] = "Session Timeout!";
                return RedirectToAction("user", "login");
            }

            @TempData["Direct"] = "User";
            if (ModelState.IsValidField("upt") && ModelState.IsValidField("role") && ModelState.IsValidField("name") && ModelState.IsValidField("email") && ModelState.IsValidField("phone"))
            {
                if (userModel.password == null) //kalau passwordnya gak diisi
                {
                    userModel.password = temppassword; //passwordnya bakalan diisi dari yang lama
                }

                if (_user.update(userModel))
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
            return View(userModel);
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

            TempData["Direct"] = "User";
            if (id != null && _user.isData(id))
            {
                TempData["Data"] = id;
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("View");
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

            TempData["Direct"] = "User";
            if (id != null && _user.isData(id))
            {
                _user.delete(id);
            }
            else
            {
                TempData["EmptyMessage"] = "Data not found!";
            }
            return RedirectToAction("View");
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

            UserModel userModel = _user.getData(id);
            return View(userModel);
        }
    }
}