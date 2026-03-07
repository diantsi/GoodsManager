using GoodsManager.UIModels;
using GoodsManager.Services;
using System.Collections.ObjectModel;

namespace GoodsManager.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly IStorageService _storage;
    public ObservableCollection<WarehouseUIModel> Warehouses { get; set; }

    public WarehousesPage(IStorageService storageService)
    {
        InitializeComponent();

        // Dependency Injection: Injecting the shared storage service instance
        _storage = storageService;
        Warehouses = new ObservableCollection<WarehouseUIModel>();

        // Populating the UI collection by wrapping each database entity into a UI model
        foreach (var warehouse in _storage.GetAllWarehouses())
        {
            Warehouses.Add(new WarehouseUIModel(_storage, warehouse));
        }

        BindingContext = this;
    }

    /// <summary>
    /// Event handler for warehouse selection. Navigates to the details page 
    /// while passing the selected WarehouseUIModel as a parameter.
    /// </summary>
    private async void WarehouseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        var warehouse = (WarehouseUIModel)e.CurrentSelection[0];

        // Passing the complex object to the details page via Shell dictionary
        await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}",
            new Dictionary<string, object> { { nameof(WarehouseDetailsPage.CurrentWarehouse), warehouse } });

        // Deselect item to allow re-selection later
        ((CollectionView)sender).SelectedItem = null;
    }
}