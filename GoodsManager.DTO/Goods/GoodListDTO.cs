using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Goods
{
    /// <summary>
    /// Data Transfer Object for representing a Good in lists.
    /// </summary>
    public class GoodListDTO
    {
        public Guid Id { get; }
        public string Title { get; }
        public int Quantity { get; }
        public decimal Price { get; }
        public Category ItemCategory { get; }
        public decimal TotalValue { get; }

        public GoodListDTO(Guid id, string title, int quantity, decimal price, Category itemCategory, decimal totalValue)
        {
            Id = id;
            Title = title;
            Quantity = quantity;
            Price = price;
            ItemCategory = itemCategory;
            TotalValue = totalValue;
        }
    }
}
