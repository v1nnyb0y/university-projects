using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Provider;
using SmartHouse_Control_Web.Models;
using SmartHouse_Control_Web.Repositories;
using SmartHouse_Control_Web.Repositories.Partial_Repositories;

namespace SmartHouse_Control_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MasterPagePartial()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Неудачная попытка входа.");
                return View("MasterPagePartial", model);
            }

            IPersonality rep = new WebSiteRepository();
            object account = await Task.Run(() => rep.Authentication
                                                                 (
                                                                  model.Login,
                                                                  model.Password
                                                                 )
                                                            );
            if (account == null) {
                ModelState.AddModelError("", "Неудачная попытка входа.");
                return View("MasterPagePartial", model);
            }

            Session["CurrUsr"] = account;
            return RedirectToAction
                (
                 "Index",
                 "Account"
                );
        }

        
        public ActionResult SignOut() {

            if (!ModelState.IsValid) {
                Redirect
                    (
                     "/Home"
                    );
            }

            Session["CurrUsr"] = null;
            Redirect
                (
                 "/Home"
                );
            return View("MasterPagePartial");
        }
    }
}