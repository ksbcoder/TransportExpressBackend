using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Commands.Logistic
{
    public class UpdateLogisticCommand
    {
        public Guid LogisticID { get; private set; }
        public Guid ProductID { get; private set; }
        public Guid ClientID { get; private set; }
        public Guid StorageID { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public DateTime DeliveredAt { get; private set; }
        public decimal QuantityProduct { get; private set; }
        public decimal ShippingPrice { get; private set; }
        public decimal Discount { get; private set; }
        public string? NumberPlate { get; private set; }
        public string? FleetNumber { get; private set; }
        public string GuideNumber { get; private set; }
        public Enums.StateEntity StateLogistic { get; private set; }
    }
}