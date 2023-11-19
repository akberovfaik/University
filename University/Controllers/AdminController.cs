using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using University.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using University.ViewModels;
using System.Linq;
using University.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AdminCrud()
        {
            StudentViewModel stuModel = new StudentViewModel();
            stuModel.Students = new List<StudentDetail>();
            var data = _context.Students;
            foreach (var student in data)
            {
                stuModel.Students.Add(new StudentDetail
                {
                    StuId = student.StuId,
                    FullName = student.FullName,
                    Faculty = student.Faculty,
                    DateOfBirth = student.DateOfBirth,
                    Gender = student.Gender,
                    MobileNumber = student.MobileNumber,
                    Address = student.Address
                });
            }
            return View(stuModel);
        }
        [HttpPost]
        public IActionResult AddStudent([Bind("FullName,Faculty,DateOfBirth,Gender,MobileNumber,Address")] StudentDetail newStu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Students students = new Students
                    {
                        FullName = newStu.FullName,
                        Faculty = newStu.Faculty,
                        DateOfBirth = newStu.DateOfBirth,
                        Gender = newStu.Gender,
                        MobileNumber = newStu.MobileNumber,
                        Address = newStu.Address
                    };
                    _context.Students.Add(students);
                    _context.SaveChanges();

                    ViewBag.Message = "Student added successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error occurred while adding the student";
                }
            }
            else
            {
                ViewBag.Message = "Validation failed. Please check your inputs.";
            }
            return RedirectToAction("AdminCrud");
        }
        [HttpPost]
        public IActionResult DeleteStudent(int Id)
        {
            Students studentToDelete = _context.Students.Find(Id);

            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                _context.SaveChanges();
                ViewBag.DeleteMessage = "Student deleted successfully";
            }
            else
            {
                ViewBag.DeleteMessage = "Student not found";
            }

            return RedirectToAction("AdminCrud");
        }
        public IActionResult ConfirmDeletion(int Id)
        {
            Students studentToDelete = _context.Students.Find(Id);

            if (studentToDelete != null)
            {
                return View(studentToDelete);
            }
            else
            {
                ViewBag.DeleteMessage = "Student not found";
                return RedirectToAction("AdminCrud");
            }
        }
        [HttpPost]
        public IActionResult UpdateStudent(int studentId)
        {
            Students studentToUpdate = _context.Students.Find(studentId);

            if (studentToUpdate != null)
            {
                return View(studentToUpdate);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult SaveChanges([Bind("StuId,FullName,Faculty,DateOfBirth,Gender,MobileNumber,Address")] Students updatedStudent)
        {
            if (ModelState.IsValid)
            {
                Students existingStudent = _context.Students.Find(updatedStudent.StuId);

                if (existingStudent != null)
                {
                    existingStudent.FullName = updatedStudent.FullName;
                    existingStudent.Faculty = updatedStudent.Faculty;
                    existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
                    existingStudent.Gender = updatedStudent.Gender;
                    existingStudent.MobileNumber = updatedStudent.MobileNumber;
                    existingStudent.Address = updatedStudent.Address;
                    _context.SaveChanges();
                    ViewBag.Message2 = "Student Is Updated";
                }
                else
                {
                    ViewBag.Message2 = "Student Is Not Updated!";
                }
            }
            return RedirectToAction("AdminCrud");
        }
    }
}
