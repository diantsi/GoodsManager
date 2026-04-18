using GoodsManager.Pages;
using GoodsManager.Repositories;
using GoodsManager.Services;
using GoodsManager.Storage;
using GoodsManager.ViewModels; 
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace GoodsManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IStorageContext, SqliteStorageContext>();

            builder.Services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddSingleton<IGoodRepository, GoodRepository>();

            builder.Services.AddSingleton<IWarehouseService, WarehouseService>();
            builder.Services.AddSingleton<IGoodService, GoodService>();

            builder.Services.AddSingleton<WarehousesPage>();
            builder.Services.AddTransient<WarehouseDetailsPage>();
            builder.Services.AddTransient<GoodDetailsPage>();
            builder.Services.AddTransient<WarehouseCreatePage>();
            builder.Services.AddTransient<GoodCreatePage>();

            builder.Services.AddSingleton<WarehousesViewModel>();
            builder.Services.AddTransient<WarehouseDetailsViewModel>();
            builder.Services.AddTransient<GoodDetailsViewModel>();
            builder.Services.AddTransient<WarehouseCreateViewModel>();
            builder.Services.AddTransient<GoodCreateViewModel>();

            return builder.Build();
        }
    }
}
