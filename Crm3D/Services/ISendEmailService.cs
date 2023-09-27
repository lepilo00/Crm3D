using Crm3D.Models;

namespace Crm3D.Services
{
    public interface ISendEmailService
    {
        Task SendEmailAsync(User user);
    }
}
