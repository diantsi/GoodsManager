using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Goods
{
    /// <summary>
    /// Data Transfer Object used for creating a new Good.
    /// </summary>
    public class GoodCreateDTO
    {
        public Guid WarehouseId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category ItemCategory { get; set; }
        public string Description { get; set; }

        public GoodCreateDTO(Guid warehouseId, string title, int quantity, decimal price, Category itemCategory, string description = "")
        {
            WarehouseId = warehouseId;
            Title = title;
            Quantity = quantity;
            Price = price;
            ItemCategory = itemCategory;
            Description = description;
        }
    }
}
