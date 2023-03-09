using BlazorFullstackCrudApp.Shared;

namespace BlazorFullstackCrudApp.Client.Services.EmployeeService
{
	public interface IEmployeeService
	{
		List<Employee> Employees { get; set; }
		List<Company> Companies { get; set; }

		Task GetCompanies();
		Task GetEmployees();

		Task GetSingleEmployee(int id);
	}
}
