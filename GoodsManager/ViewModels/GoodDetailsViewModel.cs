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
        public async Task RefreshDataAsync()
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
    }
}
