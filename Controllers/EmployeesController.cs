using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplicationAPI.Repositories;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")] // it means api/Employees
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // we have to iniltialize dependency injection means interface ko intilalize
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()

        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }

        }
        [HttpGet("{id:int}")]
        // to get only one data we need to convert implicitly so we added Task<ActionResult<Employee>>
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();

                }
                return result;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                // AddEmployee (employee) will call which is define in EmployeeRepository
                var CreatedEmployee = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = CreatedEmployee.Id }, CreatedEmployee);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }
        }
        [HttpPut("{id:int}")]

        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var employeeUpdate = await _employeeRepository.GetEmployee(id);
                if (employeeUpdate == null)
                {
                    return NotFound($"Employee Id ={id} Not Found");
                }
                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = await _employeeRepository.GetEmployee(id);
                if (deleteEmployee == null)
                {
                    return NotFound($"Employee Id ={id} Not Found");
                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }
        }

        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name)
        {
            try
            {
               var result = await _employeeRepository.Search(name);

               //if (result.Any())
               if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
            }
        }
    }
}

//public IActionResult Index()
//{
//    return View();
//}