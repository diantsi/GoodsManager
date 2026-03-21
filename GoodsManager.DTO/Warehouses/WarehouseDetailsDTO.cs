using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Warehouses
{
    public class WarehouseDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public City Location { get; }

        public WarehouseDetailsDTO(Guid id, string name, City location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}