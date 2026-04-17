using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly WarehousesViewModel _viewModel;

    public WarehousesPage(WarehousesViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshDataAsync();
    }
}
