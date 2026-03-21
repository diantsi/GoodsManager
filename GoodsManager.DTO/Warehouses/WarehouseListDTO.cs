using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Warehouses
{
    public class WarehouseListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public City Location { get; }
        public decimal TotalValue { get; }

        public WarehouseListDTO(Guid id, string name, City location, decimal totalValue)
        {
            Id = id;
            Name = name;
            Location = location;
            TotalValue = totalValue;
        }
    }
}