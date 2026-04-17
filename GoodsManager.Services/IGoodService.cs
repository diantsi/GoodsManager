using GoodsManager.DTOModels.Goods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    // Defines the contract for business operations related to Goods.
    public interface IGoodService
    {
        Task<IEnumerable<GoodListDTO>> GetGoodsByWarehouseAsync(Guid warehouseId);
        Task<GoodDetailsDTO?> GetGoodAsync(Guid goodId);
        Task CreateGoodAsync(GoodCreateDTO good);
        Task UpdateGoodAsync(GoodDetailsDTO good);
        Task DeleteGoodAsync(Guid goodId);
    }
}