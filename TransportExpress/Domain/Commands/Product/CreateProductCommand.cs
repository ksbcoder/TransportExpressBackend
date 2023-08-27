namespace TransportExpress.Domain.Commands.Product
{
    public class CreateProductCommand
    {
        public Guid TransportID { get; set; }
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }
    }
}