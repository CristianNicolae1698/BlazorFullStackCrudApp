using BlazorFullstackCrudApp.Server.Data;
using BlazorFullstackCrudApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullstackCrudApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task <ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.Include( e => e.Company).ToListAsync();
            return Ok(employees);
        }

        [HttpGet("companies")]
        public async Task<ActionResult<Company>> GetCompanies(int id)
        {
            var companies = await _context.Companies.ToListAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            var employee = _context.Employees.Include(e=>e.Id==id).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Sorry, no employee here");
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            employee.Company = null;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(await GetDbEmployees());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee, int id)
        {
            var dbEmployee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (dbEmployee == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.Role = employee.Role;
            dbEmployee.CompanyId = employee.CompanyId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbHero = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
                return NotFound("Sorry, but no hero for you. :/");

            _context.Employees.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        private async Task<List<Employee>> GetDbEmployees()
        {
            return await _context.Employees.Include(e => e.Company).ToListAsync();
        }
    }
}
