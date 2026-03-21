using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.DTOModels.Goods;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Pages;
using GoodsManager.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodsManager.ViewModels
{
    // ViewModel for showing detailed info about a single warehouse
    public partial class WarehouseDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodService _goodService;

        // Current warehouse object
        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;

        // Collection of goods in this warehouse
        [ObservableProperty]
        private ObservableCollection<GoodListDTO> _goods;

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IGoodService goodService)
        {
            _warehouseService = warehouseService;
            _goodService = goodService;
        }

        // Method to get parameters from navigation
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Get ID from query
            var warehouseId = (Guid)query["WarehouseId"];

            // Load data for this warehouse and its goods
            CurrentWarehouse = _warehouseService.GetWarehouse(warehouseId);
            Goods = new ObservableCollection<GoodListDTO>(_goodService.GetGoodsByWarehouse(warehouseId));

            // Tell the UI that we have new goods list
            OnPropertyChanged(nameof(Goods));
        }

        // Command to load details for a specific good
        [RelayCommand]
        private void LoadGood(Guid goodId)
        {
            // Navigate to good details page and pass its ID
            Shell.Current.GoToAsync($"{nameof(GoodDetailsPage)}", new Dictionary<string, object> {
                { "GoodId", goodId }
            });
        }
    }
}