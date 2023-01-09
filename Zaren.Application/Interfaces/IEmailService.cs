using SanProject.Application.Models;
using System.Threading.Tasks;

namespace SanProject.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}