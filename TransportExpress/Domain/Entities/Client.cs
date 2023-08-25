using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class Client
    {
        public Guid ClientID { get; private set; }
        public string NameClient { get; private set; }
        public string Identification { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public Enums.StateEntity StateClient { get; private set; }

        public Client() { }

        #region Access
        public void SetClientID(Guid clientID)
        {
            ClientID = clientID;
        }
        public void SetNameClient(string nameClient)
        {
            NameClient = nameClient;
        }
        public void SetIdentification(string identification)
        {
            Identification = identification;
        }
        public void SetPhone(string phone)
        {
            Phone = phone;
        }
        public void SetAddress(string address)
        {
            Address = address;
        }
        public void SetStateClient(Enums.StateEntity stateClient)
        {
            StateClient = stateClient;
        }
        #endregion
    }
}