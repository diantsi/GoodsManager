using GoodsManager.DBModel;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;

namespace GoodsManager.Repositories
{
    // responsible for data retrieval
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;

        // Constructor injection of the storage context ensures the repository isn't tightly coupled to a specific database instance.
        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            return _storageContext.GetWarehouses();
        }

        public WarehouseDBModel GetWarehouse(Guid warehouseId)
        {
            return _storageContext.GetWarehouse(warehouseId);
        }
    }
}