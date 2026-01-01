using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(CredentialsViewModel request);
        Task<ClaimsIdentity> GetClaimsIdentity(CredentialsViewModel credentials);
        Task<ClaimsIdentity> CheckCredentials(User userToVerify, CredentialsViewModel credentials);
    }
}
