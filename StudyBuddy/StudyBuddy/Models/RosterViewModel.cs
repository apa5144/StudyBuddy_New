﻿using System.Collections.Generic;

namespace StudyBuddy.Models
{
    public class RosterViewModel
    {
        public int SectionId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }
        public bool SectionAvailability { get; set; }
        public string SectionColor { get; set; }
        public List<RosterLastestFiveViewModel> LatestFive { get; set; }

        public RosterViewModel()
        {
            SectionId = default(int);
            StudentId = default(int);
            Title = string.Empty;
            Subject = string.Empty;
            Section = string.Empty;
            SectionAvailability = default(bool);
            SectionColor = string.Empty;
            LatestFive = default(List<RosterLastestFiveViewModel>);
        }
    }
}