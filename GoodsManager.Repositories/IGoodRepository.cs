using GoodsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Repositories
{
    // Defines the data access contract for Good entities.
    public interface IGoodRepository
    {
        Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId);
        Task<GoodDBModel?> GetGoodAsync(Guid goodId);
        Task SaveGoodAsync(GoodDBModel good);
        Task DeleteGoodAsync(Guid goodId);
    }
}