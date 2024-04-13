using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int Text { get; set; }
        public DateTime DateAndTimeOfSending { get; set; }
        public int? SenderId { get; set; }
        public virtual Profile Sender { get; set; }
    }
}