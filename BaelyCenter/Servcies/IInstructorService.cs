using BaelyCenter.Models;

namespace BaelyCenter.Servcies
{
    public interface IInstructorService
    {
        List<Instructor> GetAll();
        Instructor GetById(int Id);
        Instructor GetByName(string Name);
        public int Create(Instructor inst);
        int Update(Instructor NewInstructor, int id);
        int Delete(int id);
        List<Instructor> GetByDepartmentId(int DeptId);
        public List<Instructor> GetByInstructorName(string Name);

    }
}