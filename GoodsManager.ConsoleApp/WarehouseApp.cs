using GoodsManager.DTOModels.Goods;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Repositories;
using GoodsManager.Services;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.ConsoleApp
{
    internal class WarehouseApp
    {
        enum AppState { Default, WarehouseDetails, GoodDetails, ErrorCmd, Exit }

        private static AppState _currentState = AppState.Default;

        private static readonly IStorageContext _storageContext = new SqliteStorageContext();
        private static readonly IWarehouseRepository _warehouseRepo = new WarehouseRepository(_storageContext);
        private static readonly IGoodRepository _goodRepo = new GoodRepository(_storageContext);

        private static readonly IWarehouseService _warehouseService = new WarehouseService(_warehouseRepo, _goodRepo);
        private static readonly IGoodService _goodService = new GoodService(_goodRepo);

        private static List<WarehouseListDTO> _warehouses;
        private static WarehouseDetailsDTO? _currentWarehouse;
        private static List<GoodListDTO> _currentWarehouseGoods;
        private static decimal _currentWarehouseTotalValue;

        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("---Вітаю! Це менеджер товарів і складів---");

            string? currentCommand = null;

            while (_currentState != AppState.Exit)
            {
                switch (_currentState)
                {
                    case AppState.Default:
                        await ShowAllWarehouses();
                        Console.WriteLine("\n--> Введіть назву складу, щоб відкрити його:");
                        break;

                    case AppState.WarehouseDetails:
                        await ShowWarehouseDetails(currentCommand);
                        if (_currentState == AppState.WarehouseDetails)
                        {
                            Console.WriteLine("\n--> Введіть назву товару для повної інфи або 'назад', щоб повернутись:");
                        }
                        break;

                    case AppState.GoodDetails:
                        await ShowGoodDetails(currentCommand);
                        Console.WriteLine("\n--> Введіть назву товару для повної інфи або 'назад', щоб повернутись:");
                        break;

                    case AppState.ErrorCmd:
                        Console.WriteLine("Упс, не коректна команда( Спробуйте ще раз.");
                        _currentState = AppState.Default;
                        break;
                }

                if (_currentState != AppState.Exit)
                {
                    Console.WriteLine("---Введіть 'кінець', щоб закрити програму---");
                    currentCommand = Console.ReadLine();
                    UpdateAppState(currentCommand);
                }
            }
        }

        private static void UpdateAppState(string? cmd)
        {
            string? command = cmd?.ToLower().Trim();
            if (command == "кінець")
            {
                _currentState = AppState.Exit;
                Console.WriteLine("Дякую, що звільнили мене! Іду пити каву");
            }
            else if (command == "назад")
            {
                _currentState = AppState.Default;
            }
            else if (!string.IsNullOrWhiteSpace(command))
            {
                if (_currentState == AppState.Default)
                {
                    _currentState = AppState.WarehouseDetails;
                }
                else if (_currentState == AppState.WarehouseDetails)
                {
                    _currentState = AppState.GoodDetails;
                }
            }
        }

        private static async Task ShowAllWarehouses()
        {
            Console.Clear();
            Console.WriteLine("--- Ось усі доступні склади: ---");
            await LoadWarehouses();

            foreach (var w in _warehouses)
            {
                Console.WriteLine($"- {w.Name} (Місто: {w.Location}, Загальна вартість: {w.TotalValue} грн)");
            }
        }

        private static async Task LoadWarehouses()
        {
            _warehouses = new List<WarehouseListDTO>();
            await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
            {
                _warehouses.Add(warehouse);
            }
        }

        private static async Task ShowWarehouseDetails(string? warehouseName)
        {
            var listDto = _warehouses?.FirstOrDefault(w => string.Equals(w.Name, warehouseName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (listDto != null)
            {
                _currentWarehouse = await _warehouseService.GetWarehouseAsync(listDto.Id);
                if (_currentWarehouse != null)
                {
                    var goods = await _goodService.GetGoodsByWarehouseAsync(_currentWarehouse.Id);
                    _currentWarehouseGoods = goods.ToList();
                    _currentWarehouseTotalValue = listDto.TotalValue;

                    Console.Clear();
                    Console.WriteLine($"--- Товари на складі '{_currentWarehouse.Name}': ---");

                    if (_currentWarehouseGoods.Count == 0)
                    {
                        Console.WriteLine("--- На цьому складі порожньо ---");
                    }
                    else
                    {
                        foreach (var good in _currentWarehouseGoods)
                        {
                            Console.WriteLine($"- {good.Title} (Категорія: {good.ItemCategory}, Ціна: {good.Price} грн)");
                        }
                    }
                    Console.WriteLine($"Загальна сума: {_currentWarehouseTotalValue} грн");
                }
            }
            else
            {
                Console.WriteLine("Упс( Складу з такою назвою не існує.");
                _currentState = AppState.Default;
            }
        }

        private static async Task ShowGoodDetails(string? productName)
        {
            if (_currentWarehouse == null || _currentWarehouseGoods == null) return;

            var goodListDto = _currentWarehouseGoods.FirstOrDefault(g => string.Equals(g.Title, productName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (goodListDto != null)
            {
                var detailsDto = await _goodService.GetGoodAsync(goodListDto.Id);
                if (detailsDto != null)
                {
                    decimal totalCost = detailsDto.Price * detailsDto.Quantity;

                    Console.WriteLine("\n--- Деталі товару ---");
                    Console.WriteLine($"Назва: {detailsDto.Title}");
                    Console.WriteLine($"Категорія: {detailsDto.ItemCategory}");
                    Console.WriteLine($"Опис: {detailsDto.Description}");
                    Console.WriteLine($"Кількість: {detailsDto.Quantity} шт.");
                    Console.WriteLine($"Ціна за одиницю: {detailsDto.Price} грн");
                    Console.WriteLine($"Загальна вартість товару: {totalCost} грн");
                    Console.WriteLine("----------------------");
                }
            }
            else
            {
                Console.WriteLine("Такого товару не знайдено на цьому складі.");
            }
        }
    }
}
