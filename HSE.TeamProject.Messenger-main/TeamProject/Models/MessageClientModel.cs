using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class MessageClientModel
    {
        public Profile CurrentProfile { get; set; }
        public ICollection<Letter> Letters { get; set; }
    }
}