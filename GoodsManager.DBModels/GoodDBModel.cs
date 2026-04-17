using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DBModels
{
    public class GoodDBModel
    {
        private int _quantity;
        private decimal _price;

        // Id is generated only once and cannot be changed later
        public Guid Id { get; init; }
        public Guid WarehouseId { get; set; }
        public string Title { get; set; }

        public int Quantity
        {
            get => _quantity;
            set => _quantity = value < 0 ? 0 : value;
        }

        public decimal Price
        {
            get => _price;
            set => _price = value < 0 ? 0 : value;
        }

        public Category ItemCategory { get; set; }
        public string Description { get; set; }

        public GoodDBModel() { }

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
