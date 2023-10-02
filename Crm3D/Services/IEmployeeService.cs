using Azure;
using Crm3D.Models;
using Crm3D.Models.DTOs;

namespace Crm3D.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Register(EmployeeDto request);
        Task<Employee> Login(EmployeeDto request, HttpResponse response);

    }
}
