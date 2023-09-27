using Crm3D.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Crm3D.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new List<User>()
        {
            new User() { Id = 1}
        };


        public async Task<List<User>> GetAllUsers()
        {
            return _users;
        }
    }
}
