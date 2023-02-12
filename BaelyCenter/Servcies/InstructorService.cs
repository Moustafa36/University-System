using BaelyCenter.Data;
using BaelyCenter.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BaelyCenter.Servcies
{
    public class InstructorService : IInstructorService
    {
        BaelyDB Context;
        public InstructorService(BaelyDB _context)
        {
            Context = _context;
        }
        public List<Instructor> GetAll()
        {
            return Context.instructors.Include(D=>D.Dept).ToList();
        }
        public Instructor GetById(int Id)
        {
            return Context.instructors.Include(D=>D.Dept).FirstOrDefault(I=>I.Id == Id);
        }
        public Instructor GetByName(string Name)
        {
            return Context.instructors.FirstOrDefault(I => I.Name.ToLower() == Name.ToLower());
        }
        public int Create(Instructor inst)
        {
            Context.instructors.Add(inst);
            return Context.SaveChanges();
            
        }
        public int Update(Instructor NewInstructor, int id)
        {
            Instructor OrginalInstructor = Context.instructors.FirstOrDefault(I=>I.Id==id);
            OrginalInstructor.Name = NewInstructor.Name;
            OrginalInstructor.Address = NewInstructor.Address;
            OrginalInstructor.Age = NewInstructor.Age;
            OrginalInstructor.image = NewInstructor.image;
            OrginalInstructor.Gender = NewInstructor.Gender;
            OrginalInstructor.Salary = NewInstructor.Salary;
            OrginalInstructor.Dept_Id = NewInstructor.Dept_Id;
            return Context.SaveChanges();

        }
        public int Delete(int id)
        {
             Context.Remove(Context.instructors.FirstOrDefault(I => I.Id == id));
            return Context.SaveChanges();
        }
        // Get instructor in a Certian Department 
        public List<Instructor> GetByDepartmentId(int DeptId)
        {
            return Context.instructors.Where(I => I.Dept_Id == DeptId).ToList();
        }
        public List<Instructor> GetByInstructorName(string Name)
        {
            List<Instructor> instructors = Context.instructors.ToList();
            List<Instructor> instructors1 = new List<Instructor>();
            List<string> InstructorName = new List<string>();
            if (Name == null) { return instructors1; }
            foreach (Instructor s in instructors)
            {
                InstructorName.Add(s.Name);
            }
            foreach (string s in InstructorName)
            {
                if (s.ToLower().Contains(Name.ToLower())) 
                {
                    instructors1.Add(Context.instructors.FirstOrDefault(I => I.Name.ToLower() == s.ToLower())) ;
                }

            }
            return instructors1;


            
        }

        
    }
}
