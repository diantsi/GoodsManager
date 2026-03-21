using CommunityToolkit.Mvvm.ComponentModel;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Pages;
using GoodsManager.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodsManager.ViewModels
{
    // ViewModel for the main list of warehouses
    public partial class WarehousesViewModel : ObservableObject
    {
        private readonly IWarehouseService _warehouseService;

        // Collection of warehouses to show in the list
        public ObservableCollection<WarehouseListDTO> Warehouses { get; set; }

        private WarehouseListDTO _selectedWarehouse;

        // Current selected warehouse in the list
        public WarehouseListDTO SelectedWarehouse
        {
            get => _selectedWarehouse;
            set => SetProperty(ref _selectedWarehouse, value);
        }

        // Command to handle clicking on a warehouse
        public Command WarehouseSelectedCommand { get; }

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;

            // Load all warehouses from the service
            Warehouses = new ObservableCollection<WarehouseListDTO>(_warehouseService.GetAllWarehouses());
            WarehouseSelectedCommand = new Command(LoadWarehouse);
        }

        private async void LoadWarehouse()
        {
            if (SelectedWarehouse == null)
                return;

            // Go to the details page and pass the warehouse ID
            await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> {
                { "WarehouseId", SelectedWarehouse.Id }
            });

            // Reset selection so we can click the same warehouse again
            SelectedWarehouse = null;
        }
    }
}