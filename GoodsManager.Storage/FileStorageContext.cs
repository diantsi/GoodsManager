using GoodsManager.DBModels;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsManager.Storage
{
    /// <summary>
    /// File-based implementation of the storage context.
    /// Stores data in JSON format within the application's data directory.
    /// Follows pattern of grouping related entities into folders.
    /// </summary>
    public class FileStorageContext : IStorageContext
    {
        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "GoodsManagerData");
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private bool _initialized = false;

        private async Task Init()
        {
            if (_initialized) return;

            await _semaphore.WaitAsync();
            try
            {
                if (_initialized) return;

                // Check if directory exists and if it contains any files
                bool directoryExists = Directory.Exists(DatabasePath);
                bool hasFiles = directoryExists && Directory.EnumerateFileSystemEntries(DatabasePath).Any();

                if (!directoryExists || !hasFiles)
                {
                    if (!directoryExists)
                    {
                        Directory.CreateDirectory(DatabasePath);
                    }
                    else
                    {
                        // Clean up partial/empty directory if it was somehow corrupted
                        foreach (var file in Directory.GetFiles(DatabasePath)) File.Delete(file);
                        foreach (var dir in Directory.GetDirectories(DatabasePath)) Directory.Delete(dir, true);
                    }
                    
                    await CreateMockStorageInternal();
                }
                _initialized = true;
            }
            catch (Exception)
            {
                // In a real app, we would log this. 
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task CreateMockStorageInternal()
        {
            // Use InMemoryStorageContext to get seed data
            var inMemoryStorage = new InMemoryStorageContext();

            await foreach (var warehouse in inMemoryStorage.GetWarehousesAsync())
            {
                // Save warehouse directly to file to avoid recursive Init() calls
                var warehousePath = GetWarehouseFilePath(warehouse.Id);
                var warehouseJson = JsonSerializer.Serialize(warehouse);
                await File.WriteAllTextAsync(warehousePath, warehouseJson);

                // Save associated goods
                var goods = await inMemoryStorage.GetGoodsByWarehouseAsync(warehouse.Id);
                foreach (var good in goods)
                {
                    var goodPath = GetGoodFilePath(good.WarehouseId, good.Id);
                    var goodJson = JsonSerializer.Serialize(good);
                    await File.WriteAllTextAsync(goodPath, goodJson);
                }
            }
        }

        private string GetWarehouseFilePath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, $"{warehouseId}.json");
        }

        private string GetWarehouseDirectoryPath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, warehouseId.ToString());
        }

        private string GetGoodFilePath(Guid warehouseId, Guid goodId)
        {
            var dir = GetWarehouseDirectoryPath(warehouseId);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return Path.Combine(dir, $"{goodId}.json");
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            await Init();
            var files = Directory.GetFiles(DatabasePath, "*.json");
            foreach (var file in files)
            {
                WarehouseDBModel? warehouse = null;
                try
                {
                    var json = await File.ReadAllTextAsync(file);
                    warehouse = JsonSerializer.Deserialize<WarehouseDBModel>(json);
                }
                catch (JsonException)
                {
                    // Skip corrupted files
                    continue;
                }

                if (warehouse != null)
                {
                    yield return warehouse;
                }
            }
        }

        public async Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var filePath = GetWarehouseFilePath(warehouseId);
            if (!File.Exists(filePath)) return null;

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<WarehouseDBModel>(json);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            await Init();
            var filePath = GetWarehouseFilePath(warehouse.Id);
            var json = JsonSerializer.Serialize(warehouse);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var filePath = GetWarehouseFilePath(warehouseId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var dirPath = GetWarehouseDirectoryPath(warehouseId);
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
        }

        public async Task<IEnumerable<GoodDBModel>> GetGoodsByWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var goods = new List<GoodDBModel>();
            var dirPath = GetWarehouseDirectoryPath(warehouseId);
            if (!Directory.Exists(dirPath)) return goods;

            foreach (var file in Directory.GetFiles(dirPath, "*.json"))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file);
                    var good = JsonSerializer.Deserialize<GoodDBModel>(json);
                    if (good != null)
                    {
                        goods.Add(good);
                    }
                }
                catch (JsonException)
                {
                    continue;
                }
            }
            return goods;
        }

        public async Task<GoodDBModel?> GetGoodAsync(Guid goodId)
        {
            await Init();
            // Search in all warehouse directories
            foreach (var dir in Directory.GetDirectories(DatabasePath))
            {
                var filePath = Path.Combine(dir, $"{goodId}.json");
                if (File.Exists(filePath))
                {
                    try
                    {
                        var json = await File.ReadAllTextAsync(filePath);
                        return JsonSerializer.Deserialize<GoodDBModel>(json);
                    }
                    catch (JsonException)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public async Task SaveGoodAsync(GoodDBModel good)
        {
            await Init();
            var filePath = GetGoodFilePath(good.WarehouseId, good.Id);
            var json = JsonSerializer.Serialize(good);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task DeleteGoodAsync(Guid goodId)
        {
            await Init();
            foreach (var dir in Directory.GetDirectories(DatabasePath))
            {
                var filePath = Path.Combine(dir, $"{goodId}.json");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return;
                }
            }
        }
    }
}
