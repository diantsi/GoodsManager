using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    /// ViewModel for the main list of warehouses.
    /// Manages loading and navigation to individual warehouse details.
    /// </summary>
    public partial class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses = new();

        [ObservableProperty]
        private WarehouseListDTO? _selectedWarehouse;

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Refreshes the list of warehouses from the service asynchronously.
        /// </summary>
        public async Task RefreshDataAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Warehouses.Clear();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    Warehouses.Add(warehouse);
                }
            }
            catch (Exception ex)
            {
                if (Shell.Current != null)
                    await Shell.Current.DisplayAlert("Error", $"Failed to load warehouses: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Handles navigation to the details of a selected warehouse.
        /// </summary>
        [RelayCommand]
        private async Task LoadWarehouse()
        {
            if (SelectedWarehouse == null)
                return;

            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> {
                    { "WarehouseId", SelectedWarehouse.Id }
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
                SelectedWarehouse = null;
            }
        }
    }
}
