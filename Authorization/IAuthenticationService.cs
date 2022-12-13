using ParkerRandalWeb.Models;

namespace ParkerRandalWeb.Authorization
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task logout();
    }
}
