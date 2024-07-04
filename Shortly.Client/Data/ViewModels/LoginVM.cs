using Shortly.Client.Helpers.Validators;
using System.ComponentModel.DataAnnotations;

namespace Shortly.Client.Data.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email address is required!")]
        [CustomEmailValidator(ErrorMessage = "Email address is not valid - custom")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
