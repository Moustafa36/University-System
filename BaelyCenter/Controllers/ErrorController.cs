using BaelyCenter.Models;
using BaelyCenter.Servcies;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Controllers
{
    public class ErrorController : Controller
    {
        IStudentService studentRepo;
        IDepartmentService departmentRepo;
        IInstructorService instructorRepo;
        public ErrorController(IStudentService _studentRepo , IDepartmentService _departmentRepo, IInstructorService instructorRepo)
        {
            this.studentRepo = _studentRepo;
            this.departmentRepo = _departmentRepo;
            this.instructorRepo = instructorRepo;
        }

        public IActionResult NameExist(string Name,int Id) //for student
        {
            Student student = studentRepo.GetByName(Name);
            if (Id == 0)
            {
                if (student == null) { return Json(true); }
                else return Json(false);
            }
            else 
            {
                if (student == null) { return Json(true); }
                else if (Id == student.Id)
                {
                    return Json(true);
                }
                else {
                    return Json(false);
                }
            }

            
        }
        public IActionResult DepartmentExist(string Name, int Id)
        {
            Department Dept = departmentRepo.GetByName(Name);
            if (Id == 0)
            {
                if (Dept == null)
                    return Json(true);
                else return Json(false);
            }
            else 
            {
                if (Dept == null) return Json(true);
                else if (Dept.Id == Id) { return Json(true); }
                else { return Json(false); }
            }
        }
        public IActionResult InstructorExist(string Name, int Id)
        {
            Instructor instructor = instructorRepo.GetByName(Name);
            if (Id == 0)
            {
                if (instructor == null)
                    return Json(true);
                else return Json(false);
            }
            else
            {
                if (instructor == null) return Json(true);
                else if (instructor.Id == Id) { return Json(true); }
                else { return Json(false); }
            }
        }

    }
}
