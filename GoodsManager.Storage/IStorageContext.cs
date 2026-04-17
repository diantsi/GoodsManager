using GoodsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Storage
{
    /// <summary>
    /// Defines the contract for asynchronous data access operations.
    /// </summary>
    public interface IStorageContext
    {
        IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync();
        Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId);
        Task SaveWarehouseAsync(WarehouseDBModel warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);

        Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId);
        Task<GoodDBModel?> GetGoodAsync(Guid goodId);
        Task SaveGoodAsync(GoodDBModel good);
        Task DeleteGoodAsync(Guid goodId);
    }
}
