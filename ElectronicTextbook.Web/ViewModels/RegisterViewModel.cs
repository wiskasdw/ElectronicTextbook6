using System.ComponentModel.DataAnnotations;
namespace ElectronicTextbook.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not matching")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}