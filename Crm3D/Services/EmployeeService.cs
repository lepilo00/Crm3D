using Crm3D.Data;
using Crm3D.Models;
using Crm3D.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Crm3D.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _dataContext;
        public static Employee Employee = new Employee();

        public EmployeeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Employee> Register(EmployeeDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Employee.Username = request.Username;
            Employee.PasswordHash = passwordHash;

            await _dataContext.Employees.AddAsync(Employee);
            await _dataContext.SaveChangesAsync();
            return Employee;
        }

        public async Task<Employee> Login(EmployeeDto request)
        {
            Employee user;
            var allEmployees = await _dataContext.Employees.ToListAsync();

            if (!allEmployees.Where(x => x.Username == request.Username).Any())
            {
                return null;
            }
            else
            {
                user = allEmployees.Where(x => x.Username == request.Username).First();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null; // dont send that password is wrong
            }

            return user;
        }
    }
}
