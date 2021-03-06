﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Models
{
    public class ViewRosterViewModel
    {
        public int SectionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string ProfilePic { get; set; }

        public ViewRosterViewModel()
        {
            SectionId = default(int);
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = default(long);
            ProfilePic = string.Empty;
        }
    }
}