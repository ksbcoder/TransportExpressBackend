namespace TransportExpress.Domain.Commands.Transport
{
    public class CreateTransportCommand
    {
        public string DescriptionTransport { get; set; }
        public decimal CapacityTransport { get; set; }
    }
}