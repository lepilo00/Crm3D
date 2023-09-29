using Crm3D.Models;
using Crm3D.Models.DTOs;

namespace Crm3D.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(Employee employee);
    }
}
