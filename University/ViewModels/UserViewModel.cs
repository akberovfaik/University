using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class UserViewModel 
    {
        public List<UserDetail> Users { get; set; }
    }
    public class UserDetail 
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string UserPassWord { get; set; }
        [Compare("UserPassWord", ErrorMessage = "Passwords doesn't match! Please Try Again!")]
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }
    }
}
