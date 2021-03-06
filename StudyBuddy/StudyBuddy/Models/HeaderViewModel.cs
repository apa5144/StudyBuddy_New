﻿using System.Collections.Generic;

namespace StudyBuddy.Models
{
    public class HeaderViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }

        public HeaderViewModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            ProfilePic = string.Empty;
        }
    }
}