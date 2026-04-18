using GoodsManager.Common.Enums;
using System;
using SQLite;

namespace GoodsManager.DBModels
{
    [Table("Warehouses")]
    public class WarehouseDBModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }
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
