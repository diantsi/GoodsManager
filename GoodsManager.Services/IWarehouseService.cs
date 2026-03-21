using GoodsManager.DTOModels.Warehouses;
using System;
using System.Collections.Generic;

namespace GoodsManager.Services
{
    // Acts as a bridge between the UI and the data access layer.
    // Exposes  UI-safe DTOs rather than raw database models.
    public interface IWarehouseService
    {
        IEnumerable<WarehouseListDTO> GetAllWarehouses();
        WarehouseDetailsDTO GetWarehouse(Guid warehouseId);
    }
}