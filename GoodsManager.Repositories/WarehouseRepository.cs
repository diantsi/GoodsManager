using GoodsManager.DBModels;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Repositories
{
    /// <summary>
    /// Repository responsible for data operations on Warehouse entities.
    /// Provides an abstraction layer between the domain and the storage context.
    /// </summary>
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;

        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            return _storageContext.GetWarehousesAsync();
        }

        public Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.GetWarehouseAsync(warehouseId);
        }

        public Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            return _storageContext.SaveWarehouseAsync(warehouse);
        }

        public Task DeleteWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.DeleteWarehouseAsync(warehouseId);
        }
    }
}
