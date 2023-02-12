using BaelyCenter.Models;
using BaelyCenter.ViewModels;

namespace BaelyCenter.Servcies
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department GetById(int Id);
        Department GetByName(string Name);
        int Create(Department department);
        int Update(Department department, int Id);
        int Delete(int id);
        List<DeparmentwithStudentNumberViewModel> GetAllWithStudentNumber();
    }
}