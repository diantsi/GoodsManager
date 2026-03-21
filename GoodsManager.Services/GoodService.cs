using GoodsManager.DTOModels.Goods;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;

namespace GoodsManager.Services
{
    // Service for managing product (good) data
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _goodRepository;

        public GoodService(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }

        // Get list of products for a specific warehouse
        public IEnumerable<GoodListDTO> GetGoodsByWarehouse(Guid warehouseId)
        {
            foreach (var good in _goodRepository.GetGoodsByWarehouse(warehouseId))
            {
                // Convert DB model to list DTO
                yield return new GoodListDTO(good.Id, good.Title, good.Price, good.ItemCategory);
            }
        }

        // Get full details for one product by ID
        public GoodDetailsDTO GetGood(Guid goodId)
        {
            var good = _goodRepository.GetGood(goodId);

            // Calculate total price and return DTO
            return good is null ? null : new GoodDetailsDTO(
                good.Id, 
                good.WarehouseId, 
                good.Title, 
                good.Quantity, 
                good.Price, 
                good.ItemCategory, 
                good.Description, 
                good.Price * good.Quantity);
        }
    }
}