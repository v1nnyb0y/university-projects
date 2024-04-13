﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class SignUpModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}