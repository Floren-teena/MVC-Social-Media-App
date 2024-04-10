using System.ComponentModel.DataAnnotations;

namespace dotnetcoremorningclass.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password does not match!!!")]
        public string ConfirmPassword { get; set; }
    }
}
