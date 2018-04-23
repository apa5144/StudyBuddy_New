using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudyBuddy.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        [MinLength(1, ErrorMessage = "First Name must be at least 1 character long.")]
        [MaxLength(50, ErrorMessage = "First Name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        [MinLength(1, ErrorMessage = "Last Name must be at least 1 character long.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [DisplayName("# Number")]
        public long PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("^[A-Za-z0-9._%+-]+@psu.edu$", ErrorMessage = "This system is limited to Penn State students.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(50, ErrorMessage = "Password cannot be more than 50 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords don't match. Please fix and try again.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Status")]
        public bool Availability { get; set; }

        public string ProfilePic { get; set;}

        public int SecurityLevel { get; set; }

        public string Guid { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        //[Required]
        //[MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        //[MaxLength(50, ErrorMessage = "Password cannot be more than 50 characters long.")]
        //[DataType(DataType.Password)]
        //public string NewPassword { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[RegularExpression("^[A-Za-z0-9._%+-]+@psu.edu$", ErrorMessage = "This system is limited to Penn State students.")]
        //public string NewEmail { get; set; }

        public Student()
        {
            Id = default(int);
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = default(long);
            Email = string.Empty;
            Password = string.Empty;
            Availability = default(bool);
            ProfilePic = string.Empty;
            SecurityLevel = default(int);
            Guid = string.Empty;
            //NewPassword = string.Empty;
            //NewEmail = string.Empty;
        }
    }
}