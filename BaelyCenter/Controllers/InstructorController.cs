using BaelyCenter.Models;
using BaelyCenter.Servcies;
using BaelyCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorService InstructorRepo;
        IDepartmentService DepartmentRepo;
        IStudentService StudentRepo;
        ICourseService CourseRepo;
        IStudentCourseService StudentCourseRepo;
        public InstructorController(IInstructorService _instructorRepo , IDepartmentService _departmentRepo,
            IStudentService studentRepo, ICourseService courseRepo, IStudentCourseService studentCourseRepo)
        {
            InstructorRepo = _instructorRepo;
            DepartmentRepo = _departmentRepo;
            StudentRepo = studentRepo;
            CourseRepo = courseRepo;
            StudentCourseRepo = studentCourseRepo;
        }
        //Role for Admin
        public IActionResult ShowAll()
        {
            return View(InstructorRepo.GetAll());
        }
        public IActionResult Details([FromRoute]int Id)
        {
            return View(InstructorRepo.GetById(Id));
        }
        public IActionResult Add()
        {
            Instructor instructor = new Instructor();
            List<Department> departments = DepartmentRepo.GetAll();
            ViewData["DepartmentList"] = departments;
           return View(instructor);

        }
        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                InstructorRepo.Create(instructor);
                return RedirectToAction("ShowAll");
            }
            ViewData["DepartmentList"] = DepartmentRepo.GetAll();
            return View(instructor);
        }
        public IActionResult Edit(int Id)
        {
            Instructor instructor = InstructorRepo.GetById(Id);
            ViewData["DepartmentList"] = DepartmentRepo.GetAll();
           return View("Add", instructor);

        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor, int Id)
        {
            if (ModelState.IsValid)
            {
                InstructorRepo.Update(instructor, Id);
                return RedirectToAction("Showall");
            }
            ViewData["DepartmentList"] = DepartmentRepo.GetAll();
            return View("Add", instructor);

        }

        public IActionResult Delete(int Id)
        {
            try {
                InstructorRepo.Delete(Id);
                return RedirectToAction("Showall");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.InnerException.Message);
                return View("Showall");
            }
        }

        public IActionResult MyCourses([FromRoute]int Id)
        {
            List<Course> courses = CourseRepo.GetAllInstructorCourses(Id);
            ViewData["Students"] = StudentRepo.GetAll();
            ViewData["InstructorId"] = Id;
            return View(courses);
        }
        public IActionResult MyStudent(int Id)
        {
            List<StudentCourse> SC = StudentCourseRepo.GetAllStudentByCoursId(Id);
            ViewData["instructorId"] = CourseRepo.GetInstsructorId(Id);
            List<StudentcourseViewModel> SCVM = new List<StudentcourseViewModel>();
            string color;
            foreach (var item in SC)
            {
                color = "red";
                if (item.Degree >= (item.Course.CourseDegree / 2)) { color = "green"; }
                SCVM.Add(new StudentcourseViewModel() { CourseId = Id, Degree = (double)item.Degree, Color = color,
                    StudentId = item.Student_Id, StudentName = StudentRepo.GetById(item.Student_Id).Name }) ;
            }
            return View(SCVM);
           
        }
        public IActionResult SetDegree(int StudentId,int CourseId)
        {
            StudentCourse SC = StudentCourseRepo.GetByStudentCourseId(CourseId,StudentId);
            return View(SC);
        }
        public IActionResult SaveDegree(StudentCourse EditSC)
        {
            if (ModelState.IsValid)
            {
                StudentCourseRepo.Edit(EditSC);
                return RedirectToAction("MyStudent",new {  Id = EditSC.Course_Id} );
            }
            EditSC = StudentCourseRepo.GetByStudentCourseId(EditSC.Course_Id,EditSC.Student_Id); 
            return View("SetDegree",EditSC);
            
        }
        //Actions for Department
        public IActionResult GetInstructors(int DeptId)
        {
            List<Instructor> instructors = InstructorRepo.GetByDepartmentId(DeptId);
            if (DeptId == 0) { return new EmptyResult(); }
            return PartialView("PartialViewForDepartment",instructors);
        }
        public IActionResult GetInstructorByName(string Name)
        {
            List<Instructor> instructors = InstructorRepo.GetByInstructorName(Name);
            if (Name == null) { return new EmptyResult(); }
            return PartialView("PartialViewForDepartment", instructors);
        }

    }
}
