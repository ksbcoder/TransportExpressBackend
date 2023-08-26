namespace TransportExpress.Domain.Commands.User
{
    public class AuthenticateUserCommand
    {
        public string UidUSer { get; set; }
        public string Email { get; set; }
    }
}