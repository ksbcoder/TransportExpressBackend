using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class StorageType
    {
        public Guid StorageTypeID { get; private set; }
        public string DescriptionStorage { get; private set; }
        public Enums.StateEntity StateStorageType { get; private set; }

        public StorageType() { }

        #region Access
        public void SetStorageTypeID(Guid storageTypeID)
        {
            StorageTypeID = storageTypeID;
        }
        public void SetDescriptionStorage(string descriptionStorage)
        {
            DescriptionStorage = descriptionStorage;
        }
        public void SetStateStorageType(Enums.StateEntity stateStorageType)
        {
            StateStorageType = stateStorageType;
        }
        #endregion
    }
}