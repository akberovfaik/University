using University.Data;

namespace University.ViewModels
{
    public class Student_GradeViewModel
    {
        public List<StudentDetail> Student { get; set; }
        public List<GradeDetail> Grade { get; set; }
        public Dictionary<int, string> StudentFaculties { get; internal set; }
        public Dictionary<string, List<string>> SubjectsByFaculty { get; internal set; }

        public Student_GradeViewModel()
        {
            Student = new List<StudentDetail>();
            Grade = new List<GradeDetail>();
        }
    }
}
