using GoodsManager.DBModel;
using System;
using System.Collections.Generic;

namespace GoodsManager.Storage
{
    /// <summary>
    /// Defines the contract for data access operations.
    /// </summary>
    internal interface IStorageContext
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();

        IEnumerable<GoodDBModel> GetGoodsByWarehouse(Guid warehouseId);
    }
}
