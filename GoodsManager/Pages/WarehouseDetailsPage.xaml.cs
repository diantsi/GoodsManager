using GoodsManager.UIModels;
using GoodsManager.Services;

namespace GoodsManager.Pages;

// Enables receiving the selected warehouse object from the navigation parameters.
[QueryProperty(nameof(CurrentWarehouse), nameof(CurrentWarehouse))]
public partial class WarehouseDetailsPage : ContentPage
{
    private readonly IStorageService _storage;
    private WarehouseUIModel _currentWarehouse;

    public WarehouseUIModel CurrentWarehouse
    {
        get => _currentWarehouse;
        set
        {
            _currentWarehouse = value;
            // Fetching goods only when the user navigates to this specific warehouse.
            _currentWarehouse.LoadGoods();
            BindingContext = CurrentWarehouse;
        }
    }

    public WarehouseDetailsPage(IStorageService storage)
    {
        InitializeComponent();
        _storage = storage;
    }

    private async void GoodSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        var good = (GoodUIModel)e.CurrentSelection[0];
        // Passing the selected product to the next level of the navigation hierarchy.       
        await Shell.Current.GoToAsync($"{nameof(GoodDetailsPage)}",
            new Dictionary<string, object> { { "CurrentGood", good } });
    }
}