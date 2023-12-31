﻿using TransportExpress.Domain.Common;
using TransportExpress.Domain.Common.Handlers;

namespace TransportExpress.Domain.Entities
{
    public class Logistic
    {
        public Guid LogisticID { get; private set; }
        public Guid ProductID { get; private set; }
        public Guid UserID { get; private set; }
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

        public Logistic() { }

        #region Access
        public void SetLogisticID(Guid logisticID)
        {
            LogisticID = logisticID;
        }
        public void SetProductID(Guid productID)
        {
            ProductID = productID;
        }
        public void SetUserID(Guid userID)
        {
            UserID = userID;
        }
        public void SetStorageID(Guid storageID)
        {
            StorageID = storageID;
        }
        public void SetRegisteredAt(DateTime registeredAt)
        {
            RegisteredAt = registeredAt;
        }
        public void SetDeliveredAt(DateTime deliveredAt)
        {
            DeliveredAt = deliveredAt;
        }
        public void SetQuantityProduct(decimal quantityProduct)
        {
            QuantityProduct = quantityProduct;
        }
        public void SetShippingPrice(decimal shippingPrice)
        {
            ShippingPrice = shippingPrice;
        }
        public void SetDiscount(decimal discount)
        {
            Discount = discount;
        }
        public void SetNumberPlate(string? numberPlate)
        {
            NumberPlate = numberPlate;
        }
        public void SetFleetNumber(string? fleetNumber)
        {
            FleetNumber = fleetNumber;
        }
        public void SetGuideNumber(string guideNumber)
        {
            GuideNumber = guideNumber;
        }
        public void SetStateLogistic(Enums.StateEntity stateLogistic)
        {
            StateLogistic = stateLogistic;
        }
        #endregion

        #region Factory
        public static Logistic SetDetailsLogistic(Logistic logistic)
        {
            logistic.SetRegisteredAt(DateTime.Now);
            logistic.SetGuideNumber(LogisticHandler.GenerateGuideNumber(10));
            logistic.SetStateLogistic(Enums.StateEntity.Active);
            return logistic;
        }
        #endregion
    }
}