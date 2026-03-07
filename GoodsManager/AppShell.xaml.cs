using GoodsManager.Pages;

namespace GoodsManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}", typeof(WarehouseDetailsPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}/{nameof(GoodDetailsPage)}", typeof(GoodDetailsPage));
        }
    }
}
