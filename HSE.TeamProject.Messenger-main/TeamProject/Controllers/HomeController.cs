using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        TeamProjectContext Context = new TeamProjectContext();
        public ActionResult Index()
        {
            IEnumerable<User> users = Context.Users;
            ViewBag.Users = users;
            return View();
        }

        public ActionResult EnterMessageClient()
        {

            return View();
        }

        public ActionResult EnterChat()
        {
            return View();
        }
    }
}