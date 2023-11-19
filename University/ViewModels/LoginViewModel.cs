using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your Username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
