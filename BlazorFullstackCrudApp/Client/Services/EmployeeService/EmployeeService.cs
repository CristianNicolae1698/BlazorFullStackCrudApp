using BlazorFullstackCrudApp.Client.Pages;
using BlazorFullstackCrudApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Net.Http.Json;
using Employee = BlazorFullstackCrudApp.Shared.Employee;

namespace BlazorFullstackCrudApp.Client.Services.EmployeeService
{
	public class EmployeeService : IEmployeeService
	{
		private readonly NavigationManager _navigationManager;
		private readonly HttpClient _http;

		public EmployeeService(HttpClient http, NavigationManager navigationManager)
		{
			
			_http = http;
            _navigationManager = navigationManager;
        }

		public List<Employee> Employees { get; set; } = new List<Employee>();
		public List<Company> Companies { get; set; } = new List<Company>();

		public async Task GetCompanies()
		{
			var result = await _http.GetFromJsonAsync<List<Company>>("api/employee/companies");
			if (result != null)

				Companies = result;
		}

		public async Task<Employee> GetSingleEmployee(int id)
		{
			var result = await _http.GetFromJsonAsync<Employee>($"api/employee/{id}");
			if (result != null)
				return result;
			throw new Exception("Hero not found!");

		}

		public async Task GetEmployees()
		{
			var result = await _http.GetFromJsonAsync<List<Employee>>("api/employee");
			if (result != null)

				Employees = result;

		}
        public async Task SetEmployees(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Employee>>();
            Employees = response;
            _navigationManager.NavigateTo("employees");
        }

        public async Task CreateEmployee(Employee employee)
		{
			var result = await _http.PostAsJsonAsync("api/employee", employee);
            await SetEmployees(result);

        }

		public async Task UpdateEmployee(Employee employee)
		{
			var result = await _http.PutAsJsonAsync($"api/employee{employee.Id}", employee);
            await SetEmployees(result);
        }

		public async Task DeleteEmployee(int id)
		{
			var result = await _http.DeleteAsync($"api/employee/{id}");
			await SetEmployees(result);
		}

		
	}
}
