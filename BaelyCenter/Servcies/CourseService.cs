using BaelyCenter.Data;
using BaelyCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace BaelyCenter.Servcies
{
    public class CourseService : ICourseService
    {
        readonly BaelyDB Context;
        public CourseService(BaelyDB _context)
        {
            Context = _context;
        }
        public List<Course> GetAll()
        {
            return Context.courses.Include(d=>d.Inst).Include(d => d.Dept).ToList();
        }
        public Course GetById(int Id)
        {
            return Context.courses.Include(d => d.Inst).Include(d => d.Dept).FirstOrDefault(c => c.Id == Id);
        }
        public Course GetbyName(string Name)
        {
            return Context.courses.Include(d => d.Inst).Include(d => d.Dept).FirstOrDefault(c => c.Name.ToLower() == Name.ToLower());
        }
       
        public int Create(Course course)
        {
            Context.courses.Add(course);
            return Context.SaveChanges();
        }
        public int Update(Course NewCourse, int Id)
        {
            Course OrginalCourse = Context.courses.FirstOrDefault(C => C.Id == Id);
            OrginalCourse.Name = NewCourse.Name;
            OrginalCourse.CourseDegree = NewCourse.CourseDegree;
            OrginalCourse.MinDegree = NewCourse.MinDegree;
            OrginalCourse.Dept_Id = NewCourse.Dept_Id;
            OrginalCourse.Inst_Id = NewCourse.Inst_Id;
            //OrginalCourse.StudentCourse = NewCourse.StudentCourse;
            return Context.SaveChanges();

        }
        public int Delete(int Id)
        {
            Context.Remove(Context.courses.FirstOrDefault(C => C.Id == Id));
            return Context.SaveChanges();
        }
        //instructor  
        public List<Course> GetAllInstructorCourses(int InstId)
        {
            return Context.courses.Where(c => c.Inst_Id == InstId).Include(d=>d.Dept).Include(sc=>sc.StudentCourse).ToList();
        }
        public int GetInstsructorId(int CourseId)
        {
            return Context.courses.FirstOrDefault(c => c.Id == CourseId).Inst_Id;

        }

    }
}
