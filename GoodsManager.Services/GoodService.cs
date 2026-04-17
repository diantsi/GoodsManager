using GoodsManager.DBModels;
using GoodsManager.DTOModels.Goods;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    /// <summary>
    /// Service for managing Good-related business logic.
    /// Handles mapping and domain-specific rules.
    /// </summary>
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _goodRepository;

        public GoodService(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }

        public async Task<IEnumerable<GoodListDTO>> GetGoodsByWarehouseAsync(Guid warehouseId)
        {
            var goods = await _goodRepository.GetGoodsByWarehouseAsync(warehouseId);
            return goods.Select(g => new GoodListDTO(g.Id, g.Title, g.Quantity, g.Price, g.ItemCategory, g.Price * g.Quantity));
        }

        public async Task<GoodDetailsDTO?> GetGoodAsync(Guid goodId)
        {
            var good = await _goodRepository.GetGoodAsync(goodId);
            if (good == null) return null;

            return new GoodDetailsDTO(good.Id, good.WarehouseId, good.Title, good.Quantity, good.Price, good.ItemCategory, good.Description, good.Price * good.Quantity);
        }

        public async Task CreateGoodAsync(GoodCreateDTO good)
        {
            var model = new GoodDBModel(Guid.NewGuid(), good.WarehouseId, good.Title, good.Quantity, good.Price, good.ItemCategory, good.Description);
            await _goodRepository.SaveGoodAsync(model);
        }

        public async Task UpdateGoodAsync(GoodDetailsDTO good)
        {
            var model = new GoodDBModel(good.Id, good.WarehouseId, good.Title, good.Quantity, good.Price, good.ItemCategory, good.Description);
            await _goodRepository.SaveGoodAsync(model);
        }

        public async Task DeleteGoodAsync(Guid goodId)
        {
            await _goodRepository.DeleteGoodAsync(goodId);
        }
    }
}
