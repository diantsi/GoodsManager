using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodsManager.Common.Enums;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// ViewModel for creating or editing a Warehouse.
    /// </summary>
    public partial class WarehouseCreateViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private Guid? _warehouseId;

        [ObservableProperty]
        private string _title = "Додати склад";

        [ObservableProperty]
        private string _submitButtonText = "Створити";

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private City _selectedCity = City.Kyiv;

        public List<City> Cities { get; } = Enum.GetValues(typeof(City)).Cast<City>().ToList();

        [ObservableProperty]
        private Dictionary<string, string> _errors = new();

        public WarehouseCreateViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("WarehouseId"))
            {
                _warehouseId = (Guid)query["WarehouseId"];
                Title = "Редагувати склад";
                SubmitButtonText = "Зберегти зміни";
                await LoadWarehouseData();
            }
        }

        private async Task LoadWarehouseData()
        {
            if (!_warehouseId.HasValue) return;

            IsBusy = true;
            try
            {
                var warehouse = await _warehouseService.GetWarehouseAsync(_warehouseId.Value);
                if (warehouse != null)
                {
                    Name = warehouse.Name;
                    SelectedCity = warehouse.Location;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load warehouse: {ex.Message}", "OK");
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

            IsBusy = true;
            try
            {
                if (_warehouseId.HasValue)
                {
                    // Update
                    var dto = new WarehouseDetailsDTO(_warehouseId.Value, Name, SelectedCity, 0);
                    await _warehouseService.UpdateWarehouseAsync(dto);
                }
                else
                {
                    // Create
                    var dto = new WarehouseCreateDTO(Name, SelectedCity);
                    await _warehouseService.CreateWarehouseAsync(dto);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to save warehouse: {ex.Message}", "OK");
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

            if (string.IsNullOrWhiteSpace(Name))
            {
                Errors[nameof(Name)] = "Назва обов'язкова";
            }

            OnPropertyChanged(nameof(Errors));
            return !Errors.Any();
        }
    }
}
