using BaelyCenter.Data;
using BaelyCenter.Models;
using BaelyCenter.Servcies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaelyCenter.Controllers
{
    public class StudentController : Controller
    {
        public IStudentService Student_repo;
        public IDepartmentService Department_repo;
            public StudentController(IStudentService _StudentRepo,IDepartmentService _departmentrepo)
        {
            Student_repo = _StudentRepo;
            Department_repo = _departmentrepo;
        }
        public IActionResult ShowAll()
        {
            
            List<Student> students = Student_repo.GetAll();
            return View(students);
        }
        public IActionResult Details(int id)
        {

            Student student = Student_repo.GetById(id);
            return View(student);
        }
        public IActionResult Add()
        {
            Student std = new Student();
            List<Department> Depts = Department_repo.GetAll();
            ViewData["Departments"] = Depts;

            return View(std);
        }
        [HttpPost]
        public IActionResult ADD(Student std)
        {
            if (ModelState.IsValid == false)//Model state is not valid
            {
                ViewData["Departments"] = Department_repo.GetAll();

                return View("Add",std);
            }
            Student_repo.Create(std);

            return RedirectToAction("ShowAll");
        }
        public IActionResult Edit([FromRoute]int id)
        {
            Student Std = Student_repo.GetById(id);
            ViewData["Departments"] = Department_repo.GetAll();
            return View("Add",Std);
        }

        [HttpPost]
        public IActionResult Edit(Student New_std ,int id)
        {
            if (ModelState.IsValid == false)
            {
                ViewData["Departments"] = Department_repo.GetAll();
                return View("Add", New_std);

            }
            Student_repo.Update(New_std,id);
            return RedirectToAction("Showall"); 
        }

        public IActionResult Delete([FromRoute]int Id)
        {
            try
            {
                Student_repo.Delete(Id);
                return RedirectToAction("Showall");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception",ex.InnerException.Message);
                return View("Showall");
            }
        }

    }
}
