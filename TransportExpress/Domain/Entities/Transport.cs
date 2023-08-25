using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class Transport
    {
        public Guid TransportID { get; private set; }
        public string DescriptionTransport { get; private set; }
        public decimal CapacityTransport { get; private set; }
        public Enums.StateEntity StateTransport { get; private set; }

        public Transport() { }

        #region Access
        public void SetTransportID(Guid transportID)
        {
            TransportID = transportID;
        }
        public void SetDescription(string descriptionTransport)
        {
            DescriptionTransport = descriptionTransport;
        }
        public void SetCapacityTransport(decimal capacityTransport)
        {
            CapacityTransport = capacityTransport;
        }
        public void SetStateTransport(Enums.StateEntity stateTransport)
        {
            StateTransport = stateTransport;
        }
        #endregion  
    }
}