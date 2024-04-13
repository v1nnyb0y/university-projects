using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TeamProject.Models
{
    public class TeamProjectDbInitializer : DropCreateDatabaseAlways<TeamProjectContext>
    {
        protected override void Seed(TeamProjectContext context)
        {
            User user = new User { Name = "Test", LastName = "Testing" };
            Profile profile = new Profile { Login = "test", Password = "test1", Permission = 7, Email = "test@gmail.com", User = user };
            user.Profiles = new List<Profile> { profile };
            context.Users.Add(user);
            context.Profiles.Add(profile);
            User x;
            Letter letter = new Letter { Title = "Тест", Text = "Всем привет!", DateAndTimeOfSending = DateTime.Now, Sender = profile, Receivers = new List<Profile> { profile } };
            context.Letters.Add(letter);
            base.Seed(context);
        }
    }
}