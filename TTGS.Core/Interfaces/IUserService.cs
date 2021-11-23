using System.Threading.Tasks;
using TTGS.Shared.Request;
using TTGS.Shared.Response;

namespace TTGS.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
    }
}
