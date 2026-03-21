using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodsManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IGoodRepository _goodRepository;

        // Injecting multiple repositories allows the service to aggregate data from different domain entities.
        public WarehouseService(IWarehouseRepository warehouseRepository, IGoodRepository goodRepository)
        {
            _warehouseRepository = warehouseRepository;
            _goodRepository = goodRepository;
        }

        public IEnumerable<WarehouseListDTO> GetAllWarehouses()
        {
            foreach (var warehouse in _warehouseRepository.GetWarehouses())
            {
                var goods = _goodRepository.GetGoodsByWarehouse(warehouse.Id);
                decimal totalValue = goods.Sum(g => g.Price * g.Quantity);

                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, warehouse.Location, totalValue);
            }
        }

        public WarehouseDetailsDTO GetWarehouse(Guid warehouseId)
        {
            var warehouse = _warehouseRepository.GetWarehouse(warehouseId);

            return warehouse is null ? null : new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location);
        }
    }
}