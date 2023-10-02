using Crm3D.Models;

namespace Crm3D.Services
{
    public interface IRefreshTokenService
    {
        Task<bool> AddRefreshToken(Employee user, HttpResponse response);
    }
}
