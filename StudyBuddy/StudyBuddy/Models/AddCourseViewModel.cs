using BLToolkit.Validation;

namespace StudyBuddy.Models
{
    public class AddCourseViewModel
    {
        [Required]
        public string Courses { get; set; }

        [Required]
        public string Sections { get; set; }

    }
}