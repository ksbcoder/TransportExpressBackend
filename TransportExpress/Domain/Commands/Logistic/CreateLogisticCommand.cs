namespace TransportExpress.Domain.Commands.Logistic
{
    public class CreateLogisticCommand
    {
        public Guid ProductID { get; set; }
        public Guid UserID { get; set; }
        public Guid StorageID { get; set; }
        public DateTime DeliveredAt { get; set; }
        public decimal QuantityProduct { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? NumberPlate { get; set; }
        public string? FleetNumber { get; set; }
    }
}