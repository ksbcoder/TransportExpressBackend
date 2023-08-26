using TransportExpress.Domain.Common;

namespace TransportExpress.Domain.Entities
{
    public class Storage
    {
        public Guid StorageID { get; private set; }
        public Guid StorageTypeID { get; private set; }
        public string NameStorage { get; private set; }
        public decimal CapacityStorage { get; private set; }
        public string Location { get; private set; }
        public Enums.StateEntity StateStorage { get; private set; }

        public Storage() { }

        #region Access
        public void SetStorageID(Guid storageID)
        {
            StorageID = storageID;
        }
        public void SetStorageTypeID(Guid storageTypeID)
        {
            StorageTypeID = storageTypeID;
        }
        public void SetNameStorage(string nameStorage)
        {
            NameStorage = nameStorage;
        }
        public void SetCapacityStorage(decimal capacityStorage)
        {
            CapacityStorage = capacityStorage;
        }
        public void SetLocation(string location)
        {
            Location = location;
        }
        public void SetStateStorage(Enums.StateEntity stateStorage)
        {
            StateStorage = stateStorage;
        }
        #endregion
    }
}