using GoodsManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.DBModel
{


    public class GoodDB
    {
        public Guid Id { get; }
        public Guid WarehouseId { get; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category ItemCategory { get; set; }
        public string Description { get; set; }

        private GoodDB() { }

        public GoodDB(Guid warehouseId, string title, int quantity, decimal price, Category category, string description = "")
        {
            Id = Guid.NewGuid();
            WarehouseId = warehouseId;
            Title = title;
            Quantity = quantity;
            Price = price;
            ItemCategory = category;
            Description = description;
        }

    }
}
