using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.DTOModels.Goods;
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
    /// ViewModel for displaying and managing details of a specific warehouse.
    /// Handles loading, filtering, sorting goods, and warehouse management (Edit/Delete).
    /// </summary>
    public partial class WarehouseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodService _goodService;
        private Guid _warehouseId;
        private List<GoodListDTO> _allGoods = new();

        [ObservableProperty]
        private WarehouseDetailsDTO? _currentWarehouse;

        [ObservableProperty]
        private ObservableCollection<GoodListDTO> _goods = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedSortOption = "Title";

        public List<string> SortOptions { get; } = new() { "Title", "Price", "Quantity", "Category" };

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
        public async Task RefreshData()
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
                _allGoods = goodsList.ToList();
                ApplyFilterAndSort();
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
        /// Applies search filter and sorting to the goods collection locally.
        /// </summary>
        [RelayCommand]
        public void ApplyFilterAndSort()
        {
            var filtered = _allGoods.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(g => g.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                                              g.ItemCategory.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            filtered = SelectedSortOption switch
            {
                "Price" => filtered.OrderBy(g => g.Price),
                "Quantity" => filtered.OrderByDescending(g => g.Quantity),
                "Category" => filtered.OrderBy(g => g.ItemCategory.ToString()),
                _ => filtered.OrderBy(g => g.Title)
            };

            Goods.Clear();
            foreach (var good in filtered)
            {
                Goods.Add(good);
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
                await Shell.Current.GoToAsync(nameof(GoodDetailsPage), new Dictionary<string, object> {
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

        /// <summary>
        /// Navigates to the page for adding a new good to this warehouse.
        /// </summary>
        [RelayCommand]
        private async Task AddGood()
        {
            await Shell.Current.GoToAsync("GoodCreatePage", new Dictionary<string, object> {
                { "WarehouseId", _warehouseId }
            });
        }

        /// <summary>
        /// Navigates to the page for editing the current warehouse.
        /// </summary>
        [RelayCommand]
        private async Task EditWarehouse()
        {
            await Shell.Current.GoToAsync("WarehouseCreatePage", new Dictionary<string, object> {
                { "WarehouseId", _warehouseId }
            });
        }

        /// <summary>
        /// Deletes the current warehouse after user confirmation.
        /// </summary>
        [RelayCommand]
        private async Task DeleteWarehouse()
        {
            if (CurrentWarehouse == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Підтвердження", $"Ви впевнені, що хочете видалити склад '{CurrentWarehouse.Name}'?", "Так", "Ні");
            if (!confirm) return;

            IsBusy = true;
            try
            {
                await _warehouseService.DeleteWarehouseAsync(_warehouseId);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete warehouse: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
