using GoodsManager.Common.Enums;

namespace GoodsManager.DBModel
{

    public class WarehouseDBModel
    {
        // Id is generated only once during the creation of the object and cannot be changed later.
        public Guid Id { get; set; }
        public string Name { get; set; }
        public City Location { get; set; }

        public WarehouseDBModel() { }

        // Constructor for a new warehouse (generates new Guid)
        public WarehouseDBModel(string name, City city)
            : this(Guid.NewGuid(), name, city)
        {
        }

        // Constructor for existing warehouse (e.g., from DB or FakeStorage)
        public WarehouseDBModel(Guid id, string name, City city)
        {
            Id = id;
            Name = name;
            Location = city;
        }
    }

  

}