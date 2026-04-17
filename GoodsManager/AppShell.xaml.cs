using GoodsManager.Pages;

namespace GoodsManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WarehouseDetailsPage), typeof(WarehouseDetailsPage));
            Routing.RegisterRoute(nameof(GoodDetailsPage), typeof(GoodDetailsPage));
            Routing.RegisterRoute(nameof(WarehouseCreatePage), typeof(WarehouseCreatePage));
            Routing.RegisterRoute(nameof(GoodCreatePage), typeof(GoodCreatePage));
        }
    }
}
