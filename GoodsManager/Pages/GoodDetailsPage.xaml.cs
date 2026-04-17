using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages;

public partial class GoodDetailsPage : ContentPage
{
    private readonly GoodDetailsViewModel _viewModel;

    public GoodDetailsPage(GoodDetailsViewModel vm)
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
