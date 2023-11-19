using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Configuration.Internal;
using University.Data;
using University.ViewModels;

namespace University.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        private List<StudentDetail> student = new List<StudentDetail>();
        private List<GradeDetail> grade = new List<GradeDetail>();
        public IActionResult GiveGrades()
        {
            Student_GradeViewModel vm = new Student_GradeViewModel();
            vm.StudentFaculties = GetStudentFaculty();
            vm.SubjectsByFaculty = GetSubjectsForFaculties();
            vm.Student = GetStudentData();
            vm.Grade = grade;
            vm.Grade = new List<GradeDetail>();
            var data = _context.Grades;
            foreach (var item in data)
            {
                vm.Grade.Add(new GradeDetail
                {
                    SubjectOfGrade = item.SubjectOfGrade,
                    Grade = item.Grade,
                    GradeDate = item.GradeDate,
                    StudentName = item.StudentName
                });
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult GiveGrades([Bind("SubjectOfGrade,Grade,GradeDate,StudentName")] Student_GradeViewModel model, GradeDetail grade)
        {
            if (!ModelState.IsValid)
            {
                Grades grades = new Grades
                {
                    SubjectOfGrade = grade.SubjectOfGrade,
                    Grade = grade.Grade,
                    GradeDate = grade.GradeDate,
                    StudentName = grade.StudentName
                };
                _context.Grades.Add(grades);
                _context.SaveChanges();
                model.Student = GetStudentData();
            }
            return RedirectToAction("GiveGrades");
        }
        private List<StudentDetail> GetStudentData()
        {
            return _context.Students.Select(s => new StudentDetail { FullName = s.FullName }).ToList();
        }
        private Dictionary<int, string> GetStudentFaculty()
        {
            return _context.Students.ToDictionary(s => s.StuId, s => s.Faculty);
        }
        private Dictionary<string, List<string>> GetSubjectsForFaculties()
        {
            var subjectsByFaculty = new Dictionary<string, List<string>>();

            subjectsByFaculty["Economy"] = new List<string> { "Micro Economy", "Macro Economy", "Statistics" };
            subjectsByFaculty["Business"] = new List<string> { "Entrepreneurship", "Operations Management", "Principles of Marketing" };
            subjectsByFaculty["Engineering"] = new List<string> { "Engineering Mathematics", "Machine Design", "Thermodynamics" };
            subjectsByFaculty["Computer Science"] = new List<string> { "Data Structures and Algorithms", "Programming in a High-Level Language", "Cybersecurity" };
            subjectsByFaculty["International Relations"] = new List<string> { "Introduction to International Relations", "Diplomacy and Negotiation", "Comparative Foreign Policy" };
            subjectsByFaculty["Architect"] = new List<string> { "Structural Design", "Architectural Drawing and Drafting", "Urban Design and Planning" };

            return subjectsByFaculty;
        }
    }
}
