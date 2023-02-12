using BaelyCenter.Data;
using BaelyCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace BaelyCenter.Servcies
{
    public class StudentService : IStudentService
    {
        BaelyDB Context;
        public StudentService(BaelyDB _context)
        {
            Context = _context;
        }
        //Read
        public List<Student> GetAll()
        {
            return Context.Students.Include(D => D.Department).ToList();
        }
        public Student GetById(int Id)
        {
            return Context.Students.Include(D => D.Department).FirstOrDefault(s => s.Id == Id);
        }
        public Student GetByName(string Name)
        {
           
            var student= Context.Students.FirstOrDefault(s => s.Name.ToLower() == Name.ToLower());
            return student;
        }
        //create
        public int Create(Student Std)
        {
            Context.Students.Add(Std);
            return Context.SaveChanges(); //it will return number of rows that been effected , in this case will be one row
        }
        //update
        public int Update(Student NewStudent, int Id)
        {
            Student student = Context.Students.FirstOrDefault(s => s.Id == Id);
            student.Name = NewStudent.Name;
            student.Address = NewStudent.Address;
            student.Age = NewStudent.Age;
            student.Gender = NewStudent.Gender;
            student.image = NewStudent.image;
            student.Dept_Id = NewStudent.Dept_Id;
            return Context.SaveChanges();


        }
        //Delete
        public int Delete(int id)
        {
            Student student = Context.Students.FirstOrDefault(s => s.Id == id);
            Context.Students.Remove(student);
            return Context.SaveChanges();
        }
    }
}
