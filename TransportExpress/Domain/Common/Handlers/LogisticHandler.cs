using System.Text;
using TransportExpress.Domain.Entities;

namespace TransportExpress.Domain.Common.Handlers
{
    public class LogisticHandler
    {
        private static readonly Random random = new();
        public static string GenerateGuideNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        public static decimal CalculateDiscount(Logistic logistic, Transport transport)
        {
            decimal discount = 0;

            if (logistic.QuantityProduct > 10)
            {
                switch (transport.DescriptionTransport)
                {
                    case "Terrestre":
                        discount = logistic.ShippingPrice * 0.05m;
                        break;
                    case "Maritimo":
                        discount = logistic.ShippingPrice * 0.03m;
                        break;
                }
            }
            return discount;
        }

        // for update logistic entity
        public static Logistic SetNewValuesLogistic(Logistic oldLogistic, Logistic newLogistic)
        {
            oldLogistic.SetProductID(newLogistic.ProductID);
            oldLogistic.SetUserID(newLogistic.UserID);
            oldLogistic.SetStorageID(newLogistic.StorageID);
            oldLogistic.SetRegisteredAt(newLogistic.RegisteredAt);
            oldLogistic.SetDeliveredAt(newLogistic.DeliveredAt);
            oldLogistic.SetQuantityProduct(newLogistic.QuantityProduct);
            oldLogistic.SetShippingPrice(newLogistic.ShippingPrice);
            oldLogistic.SetDiscount(newLogistic.Discount);
            oldLogistic.SetNumberPlate(newLogistic.NumberPlate);
            oldLogistic.SetFleetNumber(newLogistic.FleetNumber);
            oldLogistic.SetGuideNumber(newLogistic.GuideNumber);
            oldLogistic.SetStateLogistic(newLogistic.StateLogistic);
            return oldLogistic;
        }
    }
}