using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Commands.Logistic
{
    public class UpdateLogisticCommand
    {
        public Guid LogisticID { get; set; }
        public Guid ProductID { get; set; }
        public Guid ClientID { get; set; }
        public Guid StorageID { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public decimal QuantityProduct { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Discount { get; set; }
        public string? NumberPlate { get; set; }
        public string? FleetNumber { get; set; }
        public string GuideNumber { get; set; }
        public Enums.StateEntity StateLogistic { get; set; }
    }
}