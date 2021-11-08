using Api.Models.User;

namespace Api.Services.Authentication
{
    public interface IAuthenticationService
    {
        public LoggedUser Signup(Credentials credentials);
        public LoggedUser Signin(Credentials credentials);
    }
}
