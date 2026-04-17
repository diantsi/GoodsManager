using GoodsManager.DTOModels.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    // Acts as a bridge between the UI and the data access layer.
    // Exposes UI-safe DTOs rather than raw database models.
    public interface IWarehouseService
    {
        IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync();
        Task<WarehouseDetailsDTO?> GetWarehouseAsync(Guid warehouseId);
        Task CreateWarehouseAsync(WarehouseCreateDTO warehouse);
        Task UpdateWarehouseAsync(WarehouseDetailsDTO warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);
    }
}