using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public User()
        {
            Profiles = new List<Profile>();
        }
    }
}