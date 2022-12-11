using ParkerRandalWeb.Models;

namespace ParkerRandalWeb.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task logout();
    }
}