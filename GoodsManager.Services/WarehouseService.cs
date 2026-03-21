using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodsManager.Services
{
    // Service for managing warehouse data
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IGoodRepository _goodRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IGoodRepository goodRepository)
        {
            _warehouseRepository = warehouseRepository;
            _goodRepository = goodRepository;
        }

        // Get all warehouses and calculate total value for each
        public IEnumerable<WarehouseListDTO> GetAllWarehouses()
        {
            foreach (var warehouse in _warehouseRepository.GetWarehouses())
            {
                // Find goods for this warehouse
                var goods = _goodRepository.GetGoodsByWarehouse(warehouse.Id);
                // Sum (Price * Quantity) for all goods
                decimal totalValue = goods.Sum(g => g.Price * g.Quantity);

                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, warehouse.Location, totalValue);
            }
        }

        // Get details for one warehouse by ID
        public WarehouseDetailsDTO GetWarehouse(Guid warehouseId)
        {
            var warehouse = _warehouseRepository.GetWarehouse(warehouseId);

            if (warehouse is null) return null;

            // Calculate total value of goods for this warehouse
            var goods = _goodRepository.GetGoodsByWarehouse(warehouse.Id);
            decimal totalValue = goods.Sum(g => g.Price * g.Quantity);

            // Return DTO with all info
            return new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, totalValue);
        }
    }
}