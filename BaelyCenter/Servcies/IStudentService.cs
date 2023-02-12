using BaelyCenter.Models;

namespace BaelyCenter.Servcies
{
    public interface IStudentService
    {
        int Create(Student Std);
        int Delete(int id);
        List<Student> GetAll();
        Student GetById(int Id);
        Student GetByName(string Name);
        int Update(Student NewStudent, int Id);
    }
}