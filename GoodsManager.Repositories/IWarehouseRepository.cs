using GoodsManager.DBModels;
using System;
using System.Collections.Generic;

namespace GoodsManager.Repositories
{
    // Defines the data access contract for Warehouse entities.
    public interface IWarehouseRepository
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
        WarehouseDBModel GetWarehouse(Guid warehouseId);
    }
}