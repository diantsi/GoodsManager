using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages
{
    public partial class WarehouseDetailsPage : ContentPage
    {
        public WarehouseDetailsPage(WarehouseDetailsViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}