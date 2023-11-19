using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="This Filed Can't be Empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This Filed Can't be Empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 

        [Required(ErrorMessage = "This Filed Can't be Empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        [Required(ErrorMessage = "This Filed Can't be Empty")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords doesn't match! Please Try Again!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This Filed Can't be Empty")]
        public string Role { get; set;}
    }
}
