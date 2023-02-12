using BaelyCenter.Models;
using BaelyCenter.Servcies;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Controllers
{
    public class CourseController : Controller
    {
        ICourseService CourseRepo;
        IDepartmentService DepartmentRepo;
        IInstructorService InstructorRepo;
        public CourseController(ICourseService _courseRepo,IDepartmentService _departmentRepo , IInstructorService _instructorRepo)
        {
            CourseRepo = _courseRepo;
            DepartmentRepo = _departmentRepo;
            InstructorRepo = _instructorRepo;
        }

        public IActionResult ShowAll()
        {
            return View(CourseRepo.GetAll());

        }
        public IActionResult Details(int Id)
        {
            return View(CourseRepo.GetById(Id));
        }

        public IActionResult SelectDepartment()
        {
            ViewData["DepartmentList"] = DepartmentRepo.GetAll() ;
            return View();
        }
        public IActionResult Add(int DeptId)
        {
            Course course = new Course();
            
            ViewData["instructorList"] = InstructorRepo.GetByDepartmentId(DeptId);
            ViewData["Department"] = DepartmentRepo.GetById(DeptId);
            ViewData["DeptId"] = DeptId;
            return View(course);
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepo.Create(course);
                return RedirectToAction("Showall");
            }
            ViewData["instructorList"] = InstructorRepo.GetByDepartmentId(course.Dept_Id);
            ViewData["Department"] = DepartmentRepo.GetById(course.Dept_Id);
            return View(course);
        }
        public IActionResult Edit(int Id,int DeptId)
        {
            Course course = CourseRepo.GetById(Id);
            ViewData["instructorList"] = InstructorRepo.GetByDepartmentId(DeptId);
            ViewData["Department"] = DepartmentRepo.GetById(DeptId);
            return View("Add",course);

        }
        [HttpPost]
        public IActionResult Edit(Course newcourse, int Id, int Dept_Id)
        {
            if (ModelState.IsValid)
            {
                CourseRepo.Update(newcourse, Id);
                return RedirectToAction("Showall");
            }
            ViewData["instructorList"] = InstructorRepo.GetByDepartmentId(Dept_Id);
            ViewData["Department"] = DepartmentRepo.GetById(Dept_Id);
            return View("Add", newcourse);

        }
        public IActionResult Delete(int Id)
        {
            try
            {
                CourseRepo.Delete(Id);
                return RedirectToAction("ShowAll");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.InnerException.Message);
                return View("ShowAll");
            }
        }
    }
}
