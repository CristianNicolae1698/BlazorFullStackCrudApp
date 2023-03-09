using BlazorFullstackCrudApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullstackCrudApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public static List<Company> companies = new List<Company>
        {
            new Company {Id=1, Name="Facebook"},
            new Company {Id=2, Name="Twitter"}

        };
        public static List<Employee> employees = new List<Employee>
        {
            new Employee{
                Id=1,
                FirstName="Mark",
                LastName="Zuckerberg",
                Role="CTO",
                Company=companies[0]
            },
            new Employee{
                Id=2,
                FirstName="Elon",
                LastName="Musk",
                Role="CEO",
                Company=companies[1]
            }
        };

        [HttpGet]
        public async Task <ActionResult<List<Employee>>> GetEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Sorry, no employee here");
            }
            return Ok(employee);
        }

    }
}
