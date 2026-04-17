using GoodsManager.ViewModels;

namespace GoodsManager.Pages
{
    public partial class WarehouseCreatePage : ContentPage
    {
        public WarehouseCreatePage(WarehouseCreateViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
