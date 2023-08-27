using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Commands.User
{
    public class CreateUserCommand
    {
        public string UidUser { get; set; }
        public string NameUser { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Enums.TypeUser TypeUser { get; set; }
    }
}