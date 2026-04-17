using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.DTOModels.Goods;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Pages;
using GoodsManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// ViewModel for displaying and managing details of a specific warehouse.
    /// Handles loading goods associated with the warehouse and navigation to good details.
    /// </summary>
    public partial class WarehouseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodService _goodService;

        private Guid _warehouseId;

        [ObservableProperty]
        private WarehouseDetailsDTO? _currentWarehouse;

        [ObservableProperty]
        private ObservableCollection<GoodListDTO> _goods = new();

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IGoodService goodService)
        {
            _warehouseService = warehouseService;
            _goodService = goodService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("WarehouseId"))
            {
                _warehouseId = (Guid)query["WarehouseId"];
            }
        }

        /// <summary>
        /// Refreshes warehouse details and the list of goods asynchronously.
        /// </summary>
        [RelayCommand]
        public async Task RefreshDataAsync()
        {
            IsBusy = true;
            try
            {
                CurrentWarehouse = await _warehouseService.GetWarehouseAsync(_warehouseId);
                if (CurrentWarehouse == null)
                {
                    if (Shell.Current != null)
                    {
                        await Shell.Current.DisplayAlert("Error", "Warehouse not found.", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                    return;
                }

                var goodsList = await _goodService.GetGoodsByWarehouseAsync(_warehouseId);
                Goods.Clear();
                foreach (var good in goodsList)
                {
                    Goods.Add(good);
                }
            }
            catch (Exception ex)
            {
                if (Shell.Current != null)
                    await Shell.Current.DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to the details of a specific good.
        /// </summary>
        [RelayCommand]
        private async Task LoadGood(Guid goodId)
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(GoodDetailsPage)}", new Dictionary<string, object> {
                    { "GoodId", goodId }
                });
            }
            catch (Exception ex)
            {
                if (Shell.Current != null)
                    await Shell.Current.DisplayAlert("Error", $"Navigation failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
