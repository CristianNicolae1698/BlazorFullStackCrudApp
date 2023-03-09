using BlazorFullstackCrudApp.Shared;
using System.Net.Http.Json;

namespace BlazorFullstackCrudApp.Client.Services.EmployeeService
{
	public class EmployeeService : IEmployeeService
	{
		private readonly HttpClient _http;

		public EmployeeService(HttpClient http)
		{
			_http = http;
		}

		public List<Employee> Employees { get; set ; }=new List<Employee>();
		public List<Company> Companies { get; set; } = new List<Company>();

		public Task GetCompanies()
		{
			throw new NotImplementedException();
		}

		public Task GetSingleEmployee(int id)
		{
			throw new NotImplementedException();
		}

		public async Task GetEmployees()
		{
			var result =await _http.GetFromJsonAsync<List<Employee>>("api/employee");
			if (result != null)
			
				Employees = result;
			
		}
	}
}
