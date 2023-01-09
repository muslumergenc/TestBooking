using System.Threading.Tasks;

namespace SanProject.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login();
    }
}