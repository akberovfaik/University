using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Data
{
    [Table("Grades")]
    public class Grades
    {
        [Key]
        public int GradeId { get; set; }
        public string SubjectOfGrade { get; set; }
        public string Grade { get; set; }
        public DateTime GradeDate { get; set; }
        public string StudentName { get; set; }
    }
}
