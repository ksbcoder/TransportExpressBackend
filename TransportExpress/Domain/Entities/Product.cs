using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class Product
    {
        public Guid ProductID { get; private set; }
        public Guid TransportID { get; private set; }
        public string NameProduct { get; private set; }
        public string DescriptionProduct { get; private set; }
        public Enums.StateEntity StateProduct { get; private set; }

        public Product() { }

        #region Access
        public void SetProductID(Guid productID)
        {
            ProductID = productID;
        }
        public void SetTransportID(Guid transportID)
        {
            TransportID = transportID;
        }
        public void SetNameProduct(string nameProduct)
        {
            NameProduct = nameProduct;
        }
        public void SetDescription(string descriptionProduct)
        {
            DescriptionProduct = descriptionProduct;
        }
        public void SetStateProduct(Enums.StateEntity stateProduct)
        {
            StateProduct = stateProduct;
        }
        #endregion

        #region Factory
        public static Product SetDetailsProduct(Product product)
        {
            product.SetStateProduct(Enums.StateEntity.Active);
            return product;
        }
        #endregion
    }
}