using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class User
    {
        public Guid UserID { get; private set; }
        public string UidUser { get; private set; }
        public string NameUser { get; private set; }
        public string Identification { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public Enums.TypeUser TypeUser { get; private set; }
        public Enums.StateEntity StateUser { get; private set; }

        public User() { }

        #region Access
        public void SetUserID(Guid userID)
        {
            UserID = userID;
        }
        public void SetUidUser(string uidUser)
        {
            UidUser = uidUser;
        }
        public void SetNameUser(string nameUser)
        {
            NameUser = nameUser;
        }
        public void SetIdentification(string identification)
        {
            Identification = identification;
        }
        public void SetPhone(string phone)
        {
            Phone = phone;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetAddress(string address)
        {
            Address = address;
        }
        public void SetTypeUser(Enums.TypeUser typeUser)
        {
            TypeUser = typeUser;
        }
        public void SetStateUser(Enums.StateEntity stateUser)
        {
            StateUser = stateUser;
        }
        #endregion

        #region Factory
        public static User SetDetailsToUser(User user)
        {
            user.StateUser = Enums.StateEntity.Active;
            return user;
        }
        #endregion
    }
}