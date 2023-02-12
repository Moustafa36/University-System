using BaelyCenter.Data;
using BaelyCenter.Models;
using BaelyCenter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BaelyCenter.Servcies
{
    public class DepartmentService:IDepartmentService
    {
        BaelyDB Context ;
        public DepartmentService(BaelyDB _context)
        {
            Context = _context;
        }

        public List<Department> GetAll ()
        { 
            return Context.departments.Include(I=>I.Manager).ToList();
        }
        public Instructor GetInstructorNotManeger(int Id)
        {

            return (Instructor)Context.instructors.Where(I => I.Id==Id);
        }
        
        public List<DeparmentwithStudentNumberViewModel> GetAllWithStudentNumber()
        {
            List<Department> departmentList = Context.departments.Include(I=>I.Manager).ToList();
            List<Student> studentList = Context.Students.ToList();
            List<DeparmentwithStudentNumberViewModel> Departments = new List<DeparmentwithStudentNumberViewModel>();
            DeparmentwithStudentNumberViewModel DepartmentViewModel = new DeparmentwithStudentNumberViewModel();
            int count ;
            

            foreach (Department Dept in departmentList)
            {
                 count = 0;
                foreach (Student std in studentList)
                {
                    if (Dept.Id == std.Dept_Id)
                    {
                        count++;
                    }
                }
            DepartmentViewModel.Id = Dept.Id;
            DepartmentViewModel.DepartmentName = Dept.Name;
            DepartmentViewModel.InstructorName = Dept.Manager.Name;
            DepartmentViewModel.StudentNumber = count;
            Departments.Add(DepartmentViewModel);
            DepartmentViewModel = new DeparmentwithStudentNumberViewModel();
                
            }
            return Departments;
        }
        
        public Department GetById(int Id)
        {
            return Context.departments.Include(I => I.Manager).FirstOrDefault(D =>D.Id  == Id);
        }
        public Department GetByName(string Name)
        {

            return Context.departments.FirstOrDefault(s => s.Name.ToLower() == Name.ToLower());
            
        }
        //create
        public int Create(Department department)
        {
            Context.departments.Add(department);
            return Context.SaveChanges(); //it will return number of rows that been effected , in this case will be one row
        }
        //update
        public int Update(Department department, int Id)
        {
            Department dept = Context.departments.FirstOrDefault(D => D.Id == Id);
            dept.Name = department.Name;
            dept.Manager_Id = department.Manager_Id;
            return Context.SaveChanges();


        }
        //Delete
        public int Delete(int id)
        {
            Department department = Context.departments.FirstOrDefault(D => D.Id == id);
            Context.departments.Remove(department);
            return Context.SaveChanges();
        }


    }
}
