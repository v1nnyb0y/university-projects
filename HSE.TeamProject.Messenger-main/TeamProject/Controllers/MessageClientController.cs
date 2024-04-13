using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class MessageClientController : Controller
    {
        TeamProjectContext Context = new TeamProjectContext();
        // GET: MessageClient
        [HttpGet]
        public ActionResult Index()
        {
            Profile currentProfile = Session["profile"] as Profile;
            List<Letter> letters = Context.Letters.Where(l => l.Receivers.Select(r => r.Id).Contains(currentProfile.Id)).ToList();
            MessageClientModel messageClientModel = new MessageClientModel
            {
                CurrentProfile = currentProfile,
                Letters = letters
            };
            return View("MessageClient", messageClientModel);
        }

        [HttpPost]
        public JsonResult WriteLetter(LetterEditorModel letterEditorModel)
        {
            Profile sender = Context.Profiles.First(p => p.Id == letterEditorModel.SenderId);
            string[] receivers = letterEditorModel.Receivers.Split(' ');
            IEnumerable<Profile> profiles = receivers.Select(r => Context.Profiles.FirstOrDefault(p => p.Email == r));
            profiles = profiles.Where(p => p != null);
            Letter letter = new Letter
            {
                Sender = sender,
                Title = letterEditorModel.Title,
                Text = letterEditorModel.Text,
                DateAndTimeOfSending = DateTime.Now,
                Receivers = profiles.ToList()
            };
            Context.Letters.Add(letter);
            Context.SaveChanges();
            return Json(true);
        }
    }
}