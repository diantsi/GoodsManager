using GoodsManager.Common.Enums;

namespace GoodsManager.DBModel
{

    public class WarehouseDBModel
    {

        public Guid Id { get; }
        public string Name { get; set; }
        public City Location { get; set; }


        public WarehouseDBModel() { }


        public WarehouseDBModel(string name, City city)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = city;
        }


    }

  

}