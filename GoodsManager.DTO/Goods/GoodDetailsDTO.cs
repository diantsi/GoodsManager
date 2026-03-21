using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Goods
{
    public class GoodDetailsDTO
    {
        public Guid Id { get; }
        public Guid WarehouseId { get; } 
        public string Title { get; }
        public int Quantity { get; }
        public decimal Price { get; }
        public Category ItemCategory { get; }
        public string Description { get; }
        public decimal TotalValue { get; }

        public GoodDetailsDTO(Guid id, Guid warehouseId, string title, int quantity, decimal price, Category itemCategory, string description, decimal totalValue)
        {
            Id = id;
            WarehouseId = warehouseId;
            Title = title;
            Quantity = quantity;
            Price = price;
            ItemCategory = itemCategory;
            Description = description;
            TotalValue = totalValue;
        }
    }
}