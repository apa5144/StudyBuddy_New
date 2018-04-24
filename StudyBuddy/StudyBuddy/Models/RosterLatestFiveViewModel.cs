using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Models
{
    public class RosterLastestFiveViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RosterLastestFiveViewModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
    }
}