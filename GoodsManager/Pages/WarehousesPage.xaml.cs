using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages;

public partial class WarehousesPage : ContentPage
{
    public WarehousesPage(WarehousesViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}