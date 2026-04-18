using GoodsManager.DBModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace GoodsManager.Storage
{
    /// <summary>
    /// SQLite-based implementation of the storage context.
    /// Provides persistent relational storage using SQLite.
    /// </summary>
    public class SqliteStorageContext : IStorageContext
    {
        private const string DatabaseFileName = "goods_manager.db3";
        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
        private SQLiteAsyncConnection? _database;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private async Task Init()
        {
            if (_database is not null)
                return;

            await _semaphore.WaitAsync();
            try
            {
                if (_database is not null)
                    return;

                bool isFirstLaunch = !File.Exists(DatabasePath);
                _database = new SQLiteAsyncConnection(DatabasePath);

                if (isFirstLaunch)
                {
                    await CreateMockStorage();
                }
                else
                {
                    // Ensure tables exist even if file was empty or corrupted
                    await _database.CreateTableAsync<WarehouseDBModel>();
                    await _database.CreateTableAsync<GoodDBModel>();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task CreateMockStorage()
        {
            if (_database == null) return;

            await _database.CreateTableAsync<WarehouseDBModel>();
            await _database.CreateTableAsync<GoodDBModel>();

            var inMemoryStorage = new InMemoryStorageContext();

            await foreach (var warehouse in inMemoryStorage.GetWarehousesAsync())
            {
                await _database.InsertAsync(warehouse);
                var goods = await inMemoryStorage.GetGoodsByWarehouseAsync(warehouse.Id);
                await _database.InsertAllAsync(goods);
            }
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            await Init();
            var warehouses = await _database!.Table<WarehouseDBModel>().ToListAsync();
            foreach (var warehouse in warehouses)
            {
                yield return warehouse;
            }
        }

        public async Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId)
        {
            await Init();
            return await _database!.Table<WarehouseDBModel>().FirstOrDefaultAsync(w => w.Id == warehouseId);
        }

        public async Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            await Init();
            var existing = await GetWarehouseAsync(warehouse.Id);
            if (existing == null)
                await _database!.InsertAsync(warehouse);
            else
                await _database!.UpdateAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await Init();
            await _database!.DeleteAsync<WarehouseDBModel>(warehouseId);
        }

        public async Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId)
        {
            await Init();
            return await _database!.Table<GoodDBModel>().Where(g => g.WarehouseId == warehouseId).ToListAsync();
        }

        public async Task<GoodDBModel?> GetGoodAsync(Guid goodId)
        {
            await Init();
            return await _database!.Table<GoodDBModel>().FirstOrDefaultAsync(g => g.Id == goodId);
        }

        public async Task SaveGoodAsync(GoodDBModel good)
        {
            await Init();
            var existing = await GetGoodAsync(good.Id);
            if (existing == null)
                await _database!.InsertAsync(good);
            else
                await _database!.UpdateAsync(good);
        }

        public async Task DeleteGoodAsync(Guid goodId)
        {
            await Init();
            await _database!.DeleteAsync<GoodDBModel>(goodId);
        }

        public async Task DeleteGoodsByWarehouseAsync(Guid warehouseId)
        {
            await Init();
            // we use a custom query or delete by primary key if needed.
            await _database!.ExecuteAsync("DELETE FROM Goods WHERE WarehouseId = ?", warehouseId);
        }
    }
}
