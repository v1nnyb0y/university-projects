using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
        public override string ToString()
        {
            return User.Name + " " + User.LastName;
        }
    }
}