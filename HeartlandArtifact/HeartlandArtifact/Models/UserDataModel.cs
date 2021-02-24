﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HeartlandArtifact.Models
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
