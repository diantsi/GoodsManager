using GoodsManager.Common.Enums;
using System;

namespace GoodsManager.DTOModels.Goods
{
    public class GoodListDTO
    {
        public Guid Id { get; }
        public string Title { get; }
        public decimal Price { get; }
        public Category ItemCategory { get; }

        public GoodListDTO(Guid id, string title, decimal price, Category itemCategory)
        {
            Id = id;
            Title = title;
            Price = price;
            ItemCategory = itemCategory;
        }
    }
}