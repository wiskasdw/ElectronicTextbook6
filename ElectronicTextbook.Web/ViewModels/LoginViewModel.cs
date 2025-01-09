using System.ComponentModel.DataAnnotations;

namespace ElectronicTextbook.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }
    }
}