using GoodsManager.DBModel;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;

namespace GoodsManager.Repositories
{
    public class GoodRepository : IGoodRepository
    {
        private readonly IStorageContext _storageContext;

        public GoodRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<GoodDBModel> GetGoodsByWarehouse(Guid warehouseId)
        {
            return _storageContext.GetGoodsByWarehouse(warehouseId);
        }

        public GoodDBModel GetGood(Guid goodId)
        {
            return _storageContext.GetGood(goodId);
        }
    }
}