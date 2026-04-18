using GoodsManager.Common.Enums;
using GoodsManager.DBModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsManager.Storage
{
    /// <summary>
    /// In-memory implementation of the storage context using original mock data.
    /// Provides asynchronous operations
    /// </summary>
    public class InMemoryStorageContext : IStorageContext
    {
        private readonly ConcurrentDictionary<Guid, WarehouseDBModel> _warehouses = new();
        private readonly ConcurrentDictionary<Guid, GoodDBModel> _goods = new();

        public InMemoryStorageContext()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            var centralWarehouse = new WarehouseDBModel(Guid.NewGuid(), "Центральний", City.Kyiv);
            var leftBankWarehouse = new WarehouseDBModel(Guid.NewGuid(), "Лівобережний", City.Kyiv);       
            var westernWarehouse = new WarehouseDBModel(Guid.NewGuid(), "Западенський", City.Lviv);        
            var volynskyWarehouse = new WarehouseDBModel(Guid.NewGuid(), "Склад волинок", City.Lutsk);     
            var reserveWarehouse = new WarehouseDBModel(Guid.NewGuid(), "Південний", City.Odesa);

            _warehouses.TryAdd(centralWarehouse.Id, centralWarehouse);
            _warehouses.TryAdd(leftBankWarehouse.Id, leftBankWarehouse);
            _warehouses.TryAdd(westernWarehouse.Id, westernWarehouse);
            _warehouses.TryAdd(volynskyWarehouse.Id, volynskyWarehouse);
            _warehouses.TryAdd(reserveWarehouse.Id, reserveWarehouse);

            var goods = new List<GoodDBModel>
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

            foreach (var good in goods)
            {
                _goods.TryAdd(good.Id, good);
            }
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            await Task.Yield();
            foreach (var warehouse in _warehouses.Values)
            {
                yield return warehouse;
            }
        }

        public Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId)
        {
            _warehouses.TryGetValue(warehouseId, out var warehouse);
            return Task.FromResult(warehouse);
        }

        public Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            _warehouses[warehouse.Id] = warehouse;
            return Task.CompletedTask;
        }

        public Task DeleteWarehouseAsync(Guid warehouseId)
        {
            _warehouses.TryRemove(warehouseId, out _);
            return Task.CompletedTask;
        }

        public Task DeleteGoodsByWarehouseAsync(Guid warehouseId)
        {
            var goodsToDelete = _goods.Values.Where(g => g.WarehouseId == warehouseId).ToList();
            foreach (var good in goodsToDelete)
            {
                _goods.TryRemove(good.Id, out _);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId)
        {
            var result = _goods.Values.Where(g => g.WarehouseId == warehouseId);
            return Task.FromResult(result);
        }

        public Task<GoodDBModel?> GetGoodAsync(Guid goodId)
        {
            _goods.TryGetValue(goodId, out var good);
            return Task.FromResult(good);
        }

        public Task SaveGoodAsync(GoodDBModel good)
        {
            _goods[good.Id] = good;
            return Task.CompletedTask;
        }

        public Task DeleteGoodAsync(Guid goodId)
        {
            _goods.TryRemove(goodId, out _);
            return Task.CompletedTask;
        }
    }
}
