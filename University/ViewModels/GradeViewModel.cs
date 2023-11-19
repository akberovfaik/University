namespace University.ViewModels
{
    public class GradeViewModel
    {
        public List<GradeDetail> Grades { get; set; }
    }
    public class GradeDetail
    {
        public int GradeId { get; set; }
        public string SubjectOfGrade { get; set; }
        public string Grade { get; set; }
        public DateTime GradeDate { get; set; }
        public string StudentName { get; set; }
    }
}
