using GoodsManager.DBModels;
using System;
using System.Collections.Generic;

namespace GoodsManager.Storage
{
    /// <summary>
    /// Defines the contract for data access operations.
    /// </summary>
    public interface IStorageContext
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
        WarehouseDBModel GetWarehouse(Guid warehouseId);
        IEnumerable<GoodDBModel> GetGoodsByWarehouse(Guid warehouseId);
        GoodDBModel GetGood(Guid goodId);
    }
}
