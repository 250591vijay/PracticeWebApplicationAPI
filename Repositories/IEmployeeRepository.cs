using DataAccessLayer;
using System.Collections;

namespace WebApplicationAPI.Repositories
{
    public interface IEmployeeRepository
    {
        // To get all employee
        Task<IEnumerable<Employee>>GetEmployees();
        // To get only one employee
        Task <Employee> GetEmployee(int id);
        Task <Employee> AddEmployee(Employee employee);
        Task <Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int id);
        Task<Employee> Search(string name);
    }
}
