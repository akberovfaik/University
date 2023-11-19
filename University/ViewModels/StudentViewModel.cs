using System.ComponentModel.DataAnnotations;
using University.Data;

namespace University.ViewModels
{
    public class StudentViewModel
    {
        public List<StudentDetail> Students { get; set; }
    }
    public class StudentDetail
    {
        public int StuId { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
