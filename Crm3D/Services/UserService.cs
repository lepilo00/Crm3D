using Crm3D.Data;
using Crm3D.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Crm3D.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<User>> AddUser(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user == null)
                return null;

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }
    }
}
