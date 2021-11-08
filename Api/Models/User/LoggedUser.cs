namespace Api.Models.User
{
    public class LoggedUser
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public LoggedUser(string username, string token)
        {
            Username = username;
            Token = token;
        }
    }
}