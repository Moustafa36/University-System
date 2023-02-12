using BaelyCenter.Models;
using BaelyCenter.Servcies;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Controllers
{
    //Role for Admin
    public class StudentCourseController : Controller
    {
        IStudentCourseService studentCourseRepo;
        IStudentService studentRepo;
        ICourseService courseRepo;
        public StudentCourseController(IStudentCourseService studentCourseRepo, IStudentService studentRepo, ICourseService courseRepo)
        {
            this.studentCourseRepo = studentCourseRepo;
            this.studentRepo = studentRepo;
            this.courseRepo = courseRepo;
        }

        public IActionResult ShowAll()
        {
            return View(studentCourseRepo.GetAll());
        }
        public IActionResult Create()
        {
            StudentCourse StdCrs = new StudentCourse();
            ViewData["StudentList"] = studentRepo.GetAll();
            ViewData["CourseList"] = courseRepo.GetAll();
            return View(StdCrs);
        }
        [HttpPost]
        public IActionResult Create(StudentCourse stdCrs)
        {
            if (ModelState.IsValid)
            {
                var StudentCourseinDB = studentCourseRepo.GetAll().Any(sc => sc.Course_Id == stdCrs.Course_Id&&sc.Student_Id==stdCrs.Student_Id);
                if (StudentCourseinDB) 
                {
                    ModelState.AddModelError("","this student is already in the same course");
                    ViewData["StudentList"] = studentRepo.GetAll();
                    ViewData["CourseList"] = courseRepo.GetAll();
                    return View(stdCrs);
                }
                studentCourseRepo.Create(stdCrs.Course_Id,stdCrs.Student_Id);
                return RedirectToAction("ShowAll");
            }
            ViewData["StudentList"] = studentRepo.GetAll();
            ViewData["CourseList"] = courseRepo.GetAll();
            return View(stdCrs);
        }
        public IActionResult Drop(int CourseId, int StudentId)
        {
            if (CourseId != 0 && StudentId != 0)
            {

                studentCourseRepo.Drop(CourseId, StudentId);
                return RedirectToAction("ShowAll");
            }
            return NotFound();
        }
































        //public IActionResult Details(int CourseId, int StudentId)
        //{
        //    return View(studentCourseRepo.GetByStudentCourseId(CourseId,StudentId));
        //}
        
    }
}
