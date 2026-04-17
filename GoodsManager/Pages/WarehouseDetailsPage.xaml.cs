using GoodsManager.ViewModels;
using Microsoft.Maui.Controls;

namespace GoodsManager.Pages
{
    public partial class WarehouseDetailsPage : ContentPage
    {
        private readonly WarehouseDetailsViewModel _viewModel;

        public WarehouseDetailsPage(WarehouseDetailsViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.RefreshDataCommand.Execute(null);
        }
    }
}
