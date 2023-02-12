using BaelyCenter.Models;

namespace BaelyCenter.Servcies
{
    public interface IStudentCourseService
    {
        int Create(int CourseId, int StudentId);
        int Drop(int CourseId, int StudentId);
        int Edit(StudentCourse studentCourse);
        
        List<StudentCourse> GetAll();
        StudentCourse GetByStudentCourseId(int CourseId, int StudentId);
        List<StudentCourse> GetAllStudentByCoursId(int Id);
    }
}