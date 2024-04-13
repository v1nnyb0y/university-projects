using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class Letter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateAndTimeOfSending { get; set; }
        public int? SenderId { get; set; }
        public virtual Profile Sender { get; set; }
        public virtual ICollection<Profile> Receivers { get; set; }
        public Letter()
        {
            Receivers = new List<Profile>();
        }
    }
}