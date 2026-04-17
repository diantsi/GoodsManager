using GoodsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Repositories
{
    // Defines the data access contract for Warehouse entities.
    public interface IWarehouseRepository
    {
        IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync();
        Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId);
        Task SaveWarehouseAsync(WarehouseDBModel warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);
    }
}