using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Pages;
using GoodsManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// ViewModel for the main list of warehouses.
    /// Manages loading, filtering, sorting, and navigation.
    /// </summary>
    public partial class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private List<WarehouseListDTO> _allWarehouses = new();

        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses = new();

        [ObservableProperty]
        private WarehouseListDTO? _selectedWarehouse;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedSortOption = "Name";

        public List<string> SortOptions { get; } = new() { "Name", "Location", "Total Value" };

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Refreshes the list of warehouses from the service asynchronously.
        /// </summary>
        [RelayCommand]
        public async Task RefreshData()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                _allWarehouses.Clear();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    _allWarehouses.Add(warehouse);
                }
                ApplyFilterAndSort();
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
        /// Applies search filter and sorting to the displayed collection.
        /// </summary>
        [RelayCommand]
        public void ApplyFilterAndSort()
        {
            var filtered = _allWarehouses.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(w => w.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                                              w.Location.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            filtered = SelectedSortOption switch
            {
                "Location" => filtered.OrderBy(w => w.Location.ToString()),
                "Total Value" => filtered.OrderByDescending(w => w.TotalValue),
                _ => filtered.OrderBy(w => w.Name)
            };

            Warehouses.Clear();
            foreach (var warehouse in filtered)
            {
                Warehouses.Add(warehouse);
            }
        }

        /// <summary>
        /// Navigates to the details of a selected warehouse.
        /// </summary>
        [RelayCommand]
        private async Task LoadWarehouse()
        {
            if (SelectedWarehouse == null)
                return;

            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync(nameof(WarehouseDetailsPage), new Dictionary<string, object> {
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

        /// <summary>
        /// Navigates to the page for creating a new warehouse.
        /// </summary>
        [RelayCommand]
        private async Task AddWarehouse()
        {
            await Shell.Current.GoToAsync("WarehouseCreatePage");
        }
    }
}
