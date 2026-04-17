using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.Common.Enums;
using GoodsManager.DTOModels.Goods;
using GoodsManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// ViewModel for creating or editing a Good.
    /// </summary>
    public partial class GoodCreateViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IGoodService _goodService;
        private Guid _warehouseId;
        private Guid? _goodId;

        [ObservableProperty]
        private string _title = "Додати товар";

        [ObservableProperty]
        private string _submitButtonText = "Створити";

        [ObservableProperty]
        private string _goodTitle = string.Empty;

        [ObservableProperty]
        private string _quantityText = "0";

        [ObservableProperty]
        private string _priceText = "0";

        [ObservableProperty]
        private Category _selectedCategory = Category.Electronics;

        [ObservableProperty]
        private string _description = string.Empty;

        public List<Category> Categories { get; } = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

        [ObservableProperty]
        private Dictionary<string, string> _errors = new();

        public GoodCreateViewModel(IGoodService goodService)
        {
            _goodService = goodService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("WarehouseId"))
            {
                _warehouseId = (Guid)query["WarehouseId"];
            }

            if (query.ContainsKey("GoodId"))
            {
                _goodId = (Guid)query["GoodId"];
                Title = "Редагувати товар";
                SubmitButtonText = "Зберегти зміни";
                await LoadGoodData();
            }
        }

        private async Task LoadGoodData()
        {
            if (!_goodId.HasValue) return;

            IsBusy = true;
            try
            {
                var good = await _goodService.GetGoodAsync(_goodId.Value);
                if (good != null)
                {
                    _warehouseId = good.WarehouseId;
                    GoodTitle = good.Title;
                    QuantityText = good.Quantity.ToString();
                    PriceText = good.Price.ToString();
                    SelectedCategory = good.ItemCategory;
                    Description = good.Description;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load product: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (!Validate()) return;

            int quantity = int.Parse(QuantityText);
            decimal price = decimal.Parse(PriceText);

            IsBusy = true;
            try
            {
                if (_goodId.HasValue)
                {
                    // Update
                    var dto = new GoodDetailsDTO(_goodId.Value, _warehouseId, GoodTitle, quantity, price, SelectedCategory, Description, quantity * price);
                    await _goodService.UpdateGoodAsync(dto);
                }
                else
                {
                    // Create
                    var dto = new GoodCreateDTO(_warehouseId, GoodTitle, quantity, price, SelectedCategory, Description);
                    await _goodService.CreateGoodAsync(dto);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to save product: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private bool Validate()
        {
            Errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(GoodTitle))
            {
                Errors[nameof(GoodTitle)] = "Назва обов'язкова";
            }

            if (!int.TryParse(QuantityText, out int q) || q < 0)
            {
                Errors[nameof(QuantityText)] = "Введіть коректну кількість (>= 0)";
            }

            if (!decimal.TryParse(PriceText, out decimal p) || p < 0)
            {
                Errors[nameof(PriceText)] = "Введіть коректну ціну (>= 0)";
            }

            OnPropertyChanged(nameof(Errors));
            return !Errors.Any();
        }
    }
}
