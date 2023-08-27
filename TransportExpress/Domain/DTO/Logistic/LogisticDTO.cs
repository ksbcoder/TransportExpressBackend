using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;

namespace TransportExpress.Domain.DTO.Logistic
{
    public class LogisticDTO
    {
        #region properties
        // logistic
        public Guid LogisticID { get; set; }
        public Guid ProductID { get; set; }
        public Guid UserID { get; set; }
        public Guid StorageID { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public decimal QuantityProduct { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Discount { get; set; }
        public string? NumberPlate { get; set; }
        public string? FleetNumber { get; set; }
        public string GuideNumber { get; set; }

        // product
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }

        // user
        public string NameUser { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // storage
        public string NameStorage { get; set; }
        public decimal CapacityStorage { get; set; }
        public string Location { get; set; }

        // storage type
        public string DescriptionStorage { get; set; }

        // transport
        public string DescriptionTransport { get; set; }
        public decimal CapacityTransport { get; set; }

        #endregion

        public LogisticDTO() { }

        #region Factory
        public LogisticDTO(Entities.Logistic logistic, Product product, User user, Storage storage, StorageType storageType, Transport transport)
        {
            // logistic
            LogisticID = logistic.LogisticID;
            ProductID = logistic.ProductID;
            UserID = logistic.UserID;
            StorageID = logistic.StorageID;
            RegisteredAt = logistic.RegisteredAt;
            DeliveredAt = logistic.DeliveredAt;
            QuantityProduct = logistic.QuantityProduct;
            ShippingPrice = logistic.ShippingPrice;
            Discount = logistic.Discount;
            NumberPlate = logistic.NumberPlate;
            FleetNumber = logistic.FleetNumber;
            GuideNumber = logistic.GuideNumber;

            // product
            NameProduct = product.NameProduct;
            DescriptionProduct = product.DescriptionProduct;

            // user
            NameUser = user.NameUser;
            Identification = user.Identification;
            Phone = user.Phone;
            Email = user.Email;
            Address = user.Address;

            // storage
            NameStorage = storage.NameStorage;
            CapacityStorage = storage.CapacityStorage;
            Location = storage.Location;

            // storage type
            DescriptionStorage = storageType.DescriptionStorage;

            // transport
            DescriptionTransport = transport.DescriptionTransport;
            CapacityTransport = transport.CapacityTransport;
        }
        #endregion
    }
}