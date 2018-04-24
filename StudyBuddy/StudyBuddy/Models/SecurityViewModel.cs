using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace StudyBuddy.Models
{
    public class SecurityViewModel
    {
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("^[A-Za-z0-9._%+-]+@psu.edu$", ErrorMessage = "This system is limited to Penn State students.")]
        [DisplayName("Old Email")]
        public string OldEmail { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("^[A-Za-z0-9._%+-]+@psu.edu$", ErrorMessage = "This system is limited to Penn State students.")]
        [DisplayName("New Email")]
        public string NewEmail { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("^[A-Za-z0-9._%+-]+@psu.edu$", ErrorMessage = "This system is limited to Penn State students.")]
        [Compare("NewEmail", ErrorMessage = "Emails don't match. Please fix and try again.")]
        [DisplayName("Confirm Email")]
        public string ConfirmEmail { get; set; }

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Current Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match. Please fix and try again.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Change Email?")]
        public bool ChangeEmail { get; set; }

        [DisplayName("Change Password?")]
        public bool ChangePassword { get; set; }

        public SecurityViewModel()
        {
            Email = string.Empty;
            OldEmail = string.Empty;
            NewEmail = string.Empty;
            ConfirmEmail = string.Empty;
            Password = string.Empty;
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
            ChangeEmail = default(bool);
            ChangePassword = default(bool);
        }
    }
}