using BaelyCenter.Models;
using BaelyCenter.Servcies;
using BaelyCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentService DepartmentRepo;
        IStudentService StudentRepo;
        IInstructorService InstructorRepo;
        public DepartmentController(IDepartmentService _departmentRepo, IStudentService _studentRepo,IInstructorService _instructorRepo)
        {
            DepartmentRepo = _departmentRepo;
            StudentRepo = _studentRepo;
            InstructorRepo = _instructorRepo;
        }
        //Read 
        public IActionResult ShowAll()
        {
            List<Department> departmentList = DepartmentRepo.GetAll();
            return View(departmentList);

        }
        public IActionResult ShowAllWithStudentNumber()
        {
            return View(DepartmentRepo.GetAllWithStudentNumber());

        }
        public IActionResult Details(int Id)
        {

            return View(DepartmentRepo.GetById(Id));
        }
        public IActionResult ADD()
        {
            Department Dept = new Department();           
             ViewData["instructorsList"]= InstructorRepo.GetAll();
            return View(Dept);
        }
        [HttpPost]
        public IActionResult ADD(Department Dept)
        {
            if (ModelState.IsValid)
            {

                bool ManagerExist = DepartmentRepo.GetAll().Any(D => D.Manager_Id == Dept.Manager_Id);
                if (ManagerExist)
                {
                    ModelState.AddModelError("", "This Instructor is already Manager to other Department");
                    
                }
                else
                {
                    DepartmentRepo.Create(Dept);

                    return RedirectToAction("ShowAll");
                }
            }
                ViewData["instructorsList"] = InstructorRepo.GetAll();
                return View("ADD",Dept);
        }
        public IActionResult Edit([FromRoute]int Id)
        {
            ViewData["instructorsList"] = InstructorRepo.GetAll();
            return View("ADD", DepartmentRepo.GetById(Id));
        }
        [HttpPost]
        public IActionResult Edit(Department Dept, int Id)
        {
            if (ModelState.IsValid)
            {
                bool ManagerExist = DepartmentRepo.GetAll().Any(d => d.Manager_Id == Id);
                Department department = DepartmentRepo.GetById(Id);
                if (ManagerExist && department.Manager_Id != Dept.Manager_Id)
                {
                    ModelState.AddModelError("", "this instructor is already Manager ");
                }
                else
                {
                    DepartmentRepo.Update(Dept, Id);
                    return RedirectToAction("ShowAll");
                }
            }
            ViewData["instructorsList"] = InstructorRepo.GetAll();
            return View("Add",Dept);
        }
        public IActionResult Delete( [FromRoute]int Id)
        {
            try
            {
                DepartmentRepo.Delete(Id);
                return RedirectToAction("ShowAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.InnerException.Message);
                return View("Showall");
            }
        }
        

    }
}
