using GoodsManager.DTOModels.Goods;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;

namespace GoodsManager.Services
{
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _goodRepository;

        public GoodService(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }

        public IEnumerable<GoodListDTO> GetGoodsByWarehouse(Guid warehouseId)
        {
            foreach (var good in _goodRepository.GetGoodsByWarehouse(warehouseId))
            {
                yield return new GoodListDTO(good.Id, good.Title, good.Price, good.ItemCategory);
            }
        }

        public GoodDetailsDTO GetGood(Guid goodId)
        {
            var good = _goodRepository.GetGood(goodId);

            return good is null ? null : new GoodDetailsDTO(good.Id, good.WarehouseId, good.Title, good.Quantity, good.Price, good.ItemCategory, good.Description);
        }
    }
}