using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace University.Data
{
    [Table("Students")]
    public class Students
    {
        [Key]
        public int StuId { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
