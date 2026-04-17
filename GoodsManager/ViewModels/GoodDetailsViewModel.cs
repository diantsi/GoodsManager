using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.DTOModels.Goods;
using GoodsManager.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// ViewModel for displaying detailed information about a single product (Good).
    /// Handles good management (Edit/Delete).
    /// </summary>
    public partial class GoodDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IGoodService _goodService;
        private Guid _goodId;

        [ObservableProperty]
        private GoodDetailsDTO? _currentGood;

        public GoodDetailsViewModel(IGoodService goodService)
        {
            _goodService = goodService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("GoodId"))
            {
                _goodId = (Guid)query["GoodId"];
            }
        }

        /// <summary>
        /// Loads product details asynchronously.
        /// </summary>
        [RelayCommand]
        public async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentGood = await _goodService.GetGoodAsync(_goodId);
                if (CurrentGood == null)
                {
                    if (Shell.Current != null)
                    {
                        await Shell.Current.DisplayAlert("Error", "Product not found.", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Shell.Current != null)
                    await Shell.Current.DisplayAlert("Error", $"Failed to load product details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to the page for editing the current good.
        /// </summary>
        [RelayCommand]
        private async Task EditGood()
        {
            await Shell.Current.GoToAsync("GoodCreatePage", new Dictionary<string, object> {
                { "GoodId", _goodId }
            });
        }

        /// <summary>
        /// Deletes the current good after user confirmation.
        /// </summary>
        [RelayCommand]
        private async Task DeleteGood()
        {
            if (CurrentGood == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Підтвердження", $"Ви впевнені, що хочете видалити товар '{CurrentGood.Title}'?", "Так", "Ні");
            if (!confirm) return;

            IsBusy = true;
            try
            {
                await _goodService.DeleteGoodAsync(_goodId);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete product: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
