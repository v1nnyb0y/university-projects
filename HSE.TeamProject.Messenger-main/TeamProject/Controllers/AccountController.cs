using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using System.Data.Entity;
using TeamProject.Enums;

namespace TeamProject.Controllers
{
    public class AccountController : Controller
    {
        TeamProjectContext Context = new TeamProjectContext();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            Profile currentProfile = Context.Profiles.FirstOrDefault(p => p.Login == loginModel.Login && p.Password == loginModel.Password);
            if (currentProfile == null)
            {
                return new HttpUnauthorizedResult();
            }
            Session["profile"] = currentProfile;
            IEnumerable<User> users = Context.Users;
            ViewBag.Users = users;
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel signUpModel)
        {
            User user = new User
            {
                Name = signUpModel.Name,
                LastName = signUpModel.LastName
            };
            int permission = 0;
            foreach (string role in signUpModel.Roles)
            {
                permission += RoleEnumHelper.GetRoleValue(role);
            }
            Profile profile = new Profile
            {
                Login = signUpModel.Login,
                Password = signUpModel.Password,
                User = user,
                Email = signUpModel.Email,
                Permission = permission
            };
            Context.Users.Add(user);
            Context.Profiles.Add(profile);
            Context.SaveChanges();
            return View("Login");
        }
    }
}