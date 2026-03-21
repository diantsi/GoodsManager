using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages;

public partial class GoodDetailsPage : ContentPage
{
    public GoodDetailsPage(GoodDetailsViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}