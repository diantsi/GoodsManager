using GoodsManager.Pages;
using GoodsManager.Services;
using Microsoft.Extensions.Logging;

namespace GoodsManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IStorageService, StorageService>();

            builder.Services.AddTransient<WarehousesPage>();

            builder.Services.AddTransient<WarehouseDetailsPage>();
            builder.Services.AddTransient<GoodDetailsPage>();


            return builder.Build();
        }
    }
}
