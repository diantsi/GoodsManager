using GoodsManager.Common.Enums;
using GoodsManager.DBModel;

namespace GoodsManager.Services
{

    // A fake database with hardcoded data.
    internal static class FakeStorage
        {
            private static readonly List<WarehouseDBModel> _warehouses;
            private static readonly List<GoodDBModel> _goods;

            internal static IEnumerable<WarehouseDBModel> Warehouses
            {
                get
                {
                    return _warehouses.ToList();
                }
            }

            internal static IEnumerable<GoodDBModel> Goods
            {
                get
                {
                    return _goods.ToList();
                }
            }

            static FakeStorage()
        {
            var centralWarehouse = new WarehouseDBModel("Центральний", City.Kyiv);
            var leftBankWarehouse = new WarehouseDBModel("Лівобережний", City.Kyiv);
            var westernWarehouse = new WarehouseDBModel("Западенський", City.Lviv);
            var volynskyWarehouse = new WarehouseDBModel("Склад волинок", City.Lutsk);
            var reserveWarehouse = new WarehouseDBModel("Південний", City.Odesa);


                _warehouses = new List<WarehouseDBModel> { centralWarehouse, westernWarehouse, leftBankWarehouse, volynskyWarehouse, reserveWarehouse };

            _goods = new List<GoodDBModel>
            {
                new GoodDBModel(centralWarehouse.Id, "Ноутбук Apple MacBook Pro", 15, 75000, Category.Electronics, "16-inch, M3 Max"),
                new GoodDBModel(centralWarehouse.Id, "Смартфон Samsung Galaxy S24", 40, 40000, Category.Electronics, "256GB, Black"),
                new GoodDBModel(centralWarehouse.Id, "Монітор Dell UltraSharp", 20, 18000, Category.Electronics, "27-inch, 4K"),
                new GoodDBModel(centralWarehouse.Id, "Клавіатура Keychron K8", 50, 4500, Category.Electronics, "Механічна, RGB"),
                new GoodDBModel(centralWarehouse.Id, "Миша Logitech MX Master 3S", 35, 4200, Category.Electronics, "Бездротова"),
                new GoodDBModel(centralWarehouse.Id, "Футболка Nike", 100, 1200, Category.Clothing, "Бавовна, розмір L"),
                new GoodDBModel(centralWarehouse.Id, "Кросівки Adidas", 45, 5500, Category.Clothing, "Для бігу, 42 розмір"),
                new GoodDBModel(westernWarehouse.Id, "Кава в зернах зі Львова", 200, 600, Category.Food, "1 кг, 100% арабіка"),
                new GoodDBModel(centralWarehouse.Id, "Matcha", 80, 450, Category.Food, "Порошковий чай, Японія"),
                new GoodDBModel(centralWarehouse.Id, "Шоколад Roshen", 150, 150, Category.Food, "Чорний, 85%"),

                new GoodDBModel(westernWarehouse.Id, "Куртка зимова Columbia", 20, 8500, Category.Clothing, "Водонепроникна"),
                new GoodDBModel(westernWarehouse.Id, "Черевички Timberland", 30, 7200, Category.Clothing, "Шкіряні, жовті"),
                new GoodDBModel(volynskyWarehouse.Id, "Волинські солодощі", 100, 200, Category.Food, "З горішками і сухофруктами, 1кг")
            };
            }
        }
}
