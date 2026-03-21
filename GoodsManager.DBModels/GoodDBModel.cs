using GoodsManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.DBModel
{


    public class GoodDBModel
    {
        // Id is generated only once and cannot be changed later
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category ItemCategory { get; set; }
        public string Description { get; set; }

        public GoodDBModel() { }

        // Constructor for a new good 
        public GoodDBModel(Guid warehouseId, string title, int quantity, decimal price, Category category, string description = "")
            : this(Guid.NewGuid(), warehouseId, title, quantity, price, category, description)
        {
        }

        public GoodDBModel(Guid id, Guid warehouseId, string title, int quantity, decimal price, Category category, string description = "")
        {
            Id = id;
            WarehouseId = warehouseId;
            Title = title;
            Quantity = quantity;
            Price = price;
            ItemCategory = category;
            Description = description;
        }
    }
}
