using Crm3D.Models;

namespace Crm3D.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> AddUser(User user);
        Task<List<User>> DeleteUser(int id);
    }
}
