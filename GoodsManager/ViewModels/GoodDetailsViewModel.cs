using CommunityToolkit.Mvvm.ComponentModel;
using GoodsManager.Common.Enums;
using GoodsManager.DTOModels.Goods;
using GoodsManager.Services;
using System;
using System.Collections.Generic;

namespace GoodsManager.ViewModels
{
    // ViewModel for showing detailed info about a single product
    public partial class GoodDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IGoodService _goodService;

        private GoodDetailsDTO _currentGood;

        // Exposing properties for data binding
        public string Title => _currentGood?.Title;
        public int? Quantity => _currentGood?.Quantity;
        public decimal? Price => _currentGood?.Price;
        public Category? ItemCategory => _currentGood?.ItemCategory;
        public string Description => _currentGood?.Description;

        // Total cost from DTO
        public decimal TotalCost => _currentGood?.TotalValue ?? 0;

        public GoodDetailsViewModel(IGoodService goodService)
        {
            _goodService = goodService;
        }

        // Get product ID from navigation parameters
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Get ID and load the product from service
            var goodId = (Guid)query["GoodId"];
            _currentGood = _goodService.GetGood(goodId);

            // Notify UI that all properties have changed
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(ItemCategory));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(TotalCost));
        }
    }
}