using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.DataContext;

namespace WebApplicationAPI.Repositories
{
    // Interface ko implement karne k liye hum class create karte hai
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;

        public EmployeeRepository(ApplicationDbContext Context)
        {
            this._Context = Context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _Context.Employees.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _Context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _Context.Employees.FirstOrDefaultAsync(b => b.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _Context.SaveChangesAsync();
                return result;

            }
            return null;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await _Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _Context.Employees.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Employee> Search(string name)
        {
            // IQuerable to use query with _context to get all employee
            IQueryable<Employee> query = _Context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            // IEnumerable type hai is liye hume list tyoe m send karna hoga is k liye
            return (Employee)query;
                // return await query.ToListAsync();
        }
    }
}
