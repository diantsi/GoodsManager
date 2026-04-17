using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DBModels
{
    public class WarehouseDBModel
    {
        // Id is generated only once during the creation of the object and cannot be changed later.
        public Guid Id { get; init; }
        public string Name { get; set; }
        public City Location { get; set; }

        public WarehouseDBModel() { }

        public WarehouseDBModel(string name, City city)
            : this(Guid.NewGuid(), name, city)
        {
        }

        public WarehouseDBModel(Guid id, string name, City city)
        {
            Id = id;
            Name = name;
            Location = city;
        }
    }
}
