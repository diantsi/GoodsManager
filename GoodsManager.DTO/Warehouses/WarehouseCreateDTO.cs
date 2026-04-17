using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Warehouses
{
    /// <summary>
    /// Data Transfer Object used for creating a new Warehouse.
    /// </summary>
    public class WarehouseCreateDTO
    {
        public string Name { get; set; }
        public City Location { get; set; }

        public WarehouseCreateDTO(string name, City location)
        {
            Name = name;
            Location = location;
        }
    }
}
