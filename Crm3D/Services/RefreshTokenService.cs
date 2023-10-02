using Azure;
using Crm3D.Data;
using Crm3D.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Crm3D.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly DataContext _dataContext;

        public RefreshTokenService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        public async Task<bool> AddRefreshToken(Employee user, HttpResponse response)
        {
            var result = await _dataContext.Employees.FindAsync(user.Id);
            if (result == null)
                return false;

            var refreshToken = GenerateRefreshToken();

            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };

            response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            result.RefreshToken = refreshToken.Token;
            result.TokenCreated = refreshToken.Created;
            result.TokenExpires = refreshToken.Expires;

            await _dataContext.SaveChangesAsync();
            return true;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7)
            };

            return refreshToken;
        }
    }
}
