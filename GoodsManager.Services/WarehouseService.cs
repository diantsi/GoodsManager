using GoodsManager.DBModels;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    /// <summary>
    /// Service for managing Warehouse-related business logic.
    /// Maps between DB models and DTOs, ensuring the UI layer only works with DTOs.
    /// </summary>
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IGoodRepository _goodRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IGoodRepository goodRepository)
        {
            _warehouseRepository = warehouseRepository;
            _goodRepository = goodRepository;
        }

        public async IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync()
        {
            await foreach (var warehouse in _warehouseRepository.GetWarehousesAsync())
            {
                var goods = await _goodRepository.GetGoodsByWarehouseAsync(warehouse.Id);
                var totalValue = goods.Sum(g => g.Price * g.Quantity);
                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, warehouse.Location, totalValue);
            }
        }

        public async Task<WarehouseDetailsDTO?> GetWarehouseAsync(Guid warehouseId)
        {
            var warehouse = await _warehouseRepository.GetWarehouseAsync(warehouseId);
            if (warehouse == null) return null;

            var goods = await _goodRepository.GetGoodsByWarehouseAsync(warehouseId);
            var totalValue = goods.Sum(g => g.Price * g.Quantity);

            return new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, totalValue);
        }

        public async Task CreateWarehouseAsync(WarehouseCreateDTO warehouse)
        {
            var model = new WarehouseDBModel(Guid.NewGuid(), warehouse.Name, warehouse.Location);
            await _warehouseRepository.SaveWarehouseAsync(model);
        }

        public async Task UpdateWarehouseAsync(WarehouseDetailsDTO warehouse)
        {
            var model = new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location);
            await _warehouseRepository.SaveWarehouseAsync(model);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await _warehouseRepository.DeleteWarehouseAsync(warehouseId);
        }
    }
}
