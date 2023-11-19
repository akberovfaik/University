using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using University.Data;
using University.ViewModels;

namespace University.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _dbContext;
        private List<GradeDetail> grade = new List<GradeDetail>();
        private List<StudentDetail> students = new List<StudentDetail>();
        public StudentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult StuGrades()
        {
            string currentUserName = User.Identity.Name;

            Student_GradeViewModel vm = new Student_GradeViewModel();
            vm.Grade = new List<GradeDetail>();
            var data = _dbContext.Grades
                .Where(grade => grade.StudentName == currentUserName)
                .Select(grade => new GradeDetail
                {
                    StudentName = grade.StudentName,
                    SubjectOfGrade = grade.SubjectOfGrade,
                    Grade = grade.Grade,
                    GradeDate = grade.GradeDate,
                })
                .ToList();
            vm.Grade.AddRange(data);
            return View(vm);
        }
    }
}
