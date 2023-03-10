using BlazorFullstackCrudApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullstackCrudApp.Server.Data
{
	public class DataContext :DbContext
	{
		public DataContext(DbContextOptions<DataContext> options):base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Facebook" },
				new Company { Id = 2, Name = "Twitter" }
			);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Mark",
                    LastName = "Zuckerberg",
                    Role = "CTO",  
                    CompanyId = 1,
                },
                new Employee
                {
                Id = 2,
                FirstName = "Elon",
                LastName = "Musk",
                Role = "CEO",
                CompanyId = 2,
                }
            );

        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
 
    }
}
