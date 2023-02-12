using BaelyCenter.Data;
using BaelyCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace BaelyCenter.Servcies
{
    public class StudentCourseService : IStudentCourseService
    {
        BaelyDB Context;
        IStudentService StudentService;
        ICourseService courseService;
        public StudentCourseService(BaelyDB _context, IStudentService studentService, ICourseService courseService)
        {
            Context = _context;
            StudentService = studentService;
            this.courseService = courseService;
        }
        public List<StudentCourse> GetAll()
        {
            return Context.StudentsCourses.Include(c => c.Course).Include(s => s.student).ToList();
        }
        public List<StudentCourse> GetAllStudentByCoursId(int Id)
        {
            return Context.StudentsCourses.Where(sc => sc.Course_Id == Id).ToList();
        }
        public StudentCourse GetByStudentCourseId(int CourseId, int StudentId)
        {
            return Context.StudentsCourses.Include(s=>s.student).Include(c=>c.Course)
                .FirstOrDefault(sc => sc.Course_Id == CourseId && sc.Student_Id == StudentId);
        }
        public int Create(int CourseId, int StudentId)
        {
            Course course = courseService.GetById(CourseId);
            Student student = StudentService.GetById(StudentId);
            StudentCourse studentCourse = new StudentCourse() { Course_Id = CourseId, Student_Id = StudentId };

            course.StudentCourse.Add(studentCourse);
            student.StudentCourse.Add(studentCourse);
            return Context.SaveChanges();
        }

        public int Edit(StudentCourse StdCrs)
        {
            StudentCourse Orginal = Context.StudentsCourses.FirstOrDefault(s=>s.Student_Id==StdCrs.Student_Id&&s.Course_Id==StdCrs.Course_Id);
            Orginal.Degree = StdCrs.Degree;
            return Context.SaveChanges();
        }
        public int Drop(int CourseId, int StudentId)
        {
            Course course = courseService.GetById(CourseId);
            Student student = StudentService.GetById(StudentId);
            StudentCourse studentCourse = Context.StudentsCourses.FirstOrDefault(sc=>sc.Student_Id==StudentId&&sc.Course_Id==CourseId);
             course.StudentCourse.Remove(studentCourse);
            student.StudentCourse.Remove(studentCourse);
            return Context.SaveChanges(); 
        }
        
    }
}
