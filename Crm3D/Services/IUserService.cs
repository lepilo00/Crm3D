using Crm3D.Models;

namespace Crm3D.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
