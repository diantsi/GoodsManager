using GoodsManager.DBModels;
using System;
using System.Collections.Generic;

namespace GoodsManager.Repositories
{
    // Defines the data access contract for Good entities.
    public interface IGoodRepository
    {
        IEnumerable<GoodDBModel> GetGoodsByWarehouse(Guid warehouseId);

        GoodDBModel GetGood(Guid goodId);
    }
}