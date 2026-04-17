using GoodsManager.DBModels;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Repositories
{
    /// <summary>
    /// Repository responsible for data operations on Good entities.
    /// Follows the repository pattern to decouple business logic from data access.
    /// </summary>
    public class GoodRepository : IGoodRepository
    {
        private readonly IStorageContext _storageContext;

        public GoodRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.GetGoodsByWarehouseAsync(warehouseId);
        }

        public Task<GoodDBModel?> GetGoodAsync(Guid goodId)
        {
            return _storageContext.GetGoodAsync(goodId);
        }

        public Task SaveGoodAsync(GoodDBModel good)
        {
            return _storageContext.SaveGoodAsync(good);
        }

        public Task DeleteGoodAsync(Guid goodId)
        {
            return _storageContext.DeleteGoodAsync(goodId);
        }
    }
}
