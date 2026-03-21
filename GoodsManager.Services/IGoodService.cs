using GoodsManager.DTOModels.Goods;
using System;
using System.Collections.Generic;

namespace GoodsManager.Services
{
    public interface IGoodService
    {
        IEnumerable<GoodListDTO> GetGoodsByWarehouse(Guid warehouseId);
        GoodDetailsDTO GetGood(Guid goodId);
    }
}