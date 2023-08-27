namespace TransportExpress.Domain.Commands.Storage
{
    public class CreateStorageCommand
    {
        public Guid StorageTypeID { get; set; }
        public string NameStorage { get; set; }
        public decimal CapacityStorage { get; set; }
        public string Location { get; set; }
    }
}