using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Data
{
    [Table("UserInfo")]
    public class Users : IdentityUser 
    {
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }
    }
}
