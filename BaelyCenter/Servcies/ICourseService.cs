using BaelyCenter.Models;

namespace BaelyCenter.Servcies
{
    public interface ICourseService
    {
        int Create(Course course);
        int Delete(int Id);
        List<Course> GetAll();
        Course GetById(int Id);
        Course GetbyName(string Name);
        int Update(Course NewCourse, int Id);
        public List<Course> GetAllInstructorCourses(int InstId);
        int GetInstsructorId(int CourseId);
    }
}