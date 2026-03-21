using GoodsManager.Common.Enums;
using GoodsManager.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodsManager.Storage
{
    /// <summary>
    /// In-memory implementation of the storage context using hardcoded mock data.
    /// </summary>
    internal class InMemoryStorageContext : IStorageContext
    {
 
        private record WarehouseRecord(Guid Id, string Name, City Location);

        /// <summary>
        /// Internal record to represent raw good data structure.
        /// </summary>
        private record GoodRecord(Guid Id, Guid WarehouseId, string Title, int Quantity, decimal Price, Category ItemCategory, string Description);

        private static readonly List<WarehouseRecord> _warehouses = new List<WarehouseRecord>();
        private static readonly List<GoodRecord> _goods = new List<GoodRecord>();

        static InMemoryStorageContext()
        {
            // hardcoded mock data
            #region MockStoragePopulation
            var centralWarehouse = new WarehouseRecord(Guid.NewGuid(), "Центральний", City.Kyiv);
            var leftBankWarehouse = new WarehouseRecord(Guid.NewGuid(), "Лівобережний", City.Kyiv);
            var westernWarehouse = new WarehouseRecord(Guid.NewGuid(), "Западенський", City.Lviv);
            var volynskyWarehouse = new WarehouseRecord(Guid.NewGuid(), "Склад волинок", City.Lutsk);
            var reserveWarehouse = new WarehouseRecord(Guid.NewGuid(), "Південний", City.Odesa);

            _warehouses.Add(centralWarehouse);
            _warehouses.Add(leftBankWarehouse);
            _warehouses.Add(westernWarehouse);
            _warehouses.Add(volynskyWarehouse);
            _warehouses.Add(reserveWarehouse);

            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Ноутбук Apple MacBook Pro", 15, 75000, Category.Electronics, "16-inch, M3 Max"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Смартфон Samsung Galaxy S24", 40, 40000, Category.Electronics, "256GB, Black"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Монітор Dell UltraSharp", 20, 18000, Category.Electronics, "27-inch, 4K"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Клавіатура Keychron K8", 50, 4500, Category.Electronics, "Механічна, RGB"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Миша Logitech MX Master 3S", 35, 4200, Category.Electronics, "Бездротова"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Футболка Nike", 100, 1200, Category.Clothing, "Бавовна, розмір L"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Кросівки Adidas", 45, 5500, Category.Clothing, "Для бігу, 42 розмір"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), westernWarehouse.Id, "Кава в зернах зі Львова", 200, 600, Category.Food, "1 кг, 100% арабіка"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Matcha", 80, 450, Category.Food, "Порошковий чай, Японія"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), centralWarehouse.Id, "Шоколад Roshen", 150, 150, Category.Food, "Чорний, 85%"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), westernWarehouse.Id, "Куртка зимова Columbia", 20, 8500, Category.Clothing, "Водонепроникна"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), westernWarehouse.Id, "Черевички Timberland", 30, 7200, Category.Clothing, "Шкіряні, жовті"));
            _goods.Add(new GoodRecord(Guid.NewGuid(), volynskyWarehouse.Id, "Волинські солодощі", 100, 200, Category.Food, "З горішками і сухофруктами, 1кг"));
            #endregion
        }

       
        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            var result = new List<WarehouseDBModel>();
            foreach (var warehouse in _warehouses)
            {
                result.Add(new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location));
            }
            return result;
        }

     
        public IEnumerable<GoodDBModel> GetGoodsByWarehouse(Guid warehouseId)
        {
            var result = new List<GoodDBModel>();
            foreach (var good in _goods.Where(g => g.WarehouseId == warehouseId))
            {
                result.Add(new GoodDBModel(good.Id, good.WarehouseId, good.Title, good.Quantity, good.Price, good.ItemCategory, good.Description));
            }
            return result;
        }
    }
}
