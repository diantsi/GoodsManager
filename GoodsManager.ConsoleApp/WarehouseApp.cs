using GoodsManager.DTOModels.Goods;
using GoodsManager.DTOModels.Warehouses;
using GoodsManager.Repositories;
using GoodsManager.Services;
using GoodsManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.ConsoleApp
{
    internal class WarehouseApp
    {
        enum AppState { Default, WarehouseDetails, GoodDetails, ErrorCmd, Exit }

        private static AppState _currentState = AppState.Default;

        private static readonly IStorageContext _storageContext = new InMemoryStorageContext();
        private static readonly IWarehouseRepository _warehouseRepo = new WarehouseRepository(_storageContext);
        private static readonly IGoodRepository _goodRepo = new GoodRepository(_storageContext);

        private static readonly IWarehouseService _warehouseService = new WarehouseService(_warehouseRepo, _goodRepo);
        private static readonly IGoodService _goodService = new GoodService(_goodRepo);

        private static List<WarehouseListDTO> _warehouses;
        private static WarehouseDetailsDTO _currentWarehouse;
        private static List<GoodListDTO> _currentWarehouseGoods;
        private static decimal _currentWarehouseTotalValue;

        static void Main(string[] args)
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
                        ShowAllWarehouses();
                        Console.WriteLine("\n--> Введіть назву складу, щоб відкрити його:");
                        break;

                    case AppState.WarehouseDetails:
                        ShowWarehouseDetails(currentCommand);
                        Console.WriteLine("\n--> Введіть назву товару для повної інфи або 'назад', щоб повернутись:");
                        break;

                    case AppState.GoodDetails:
                        ShowGoodDetails(currentCommand);
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
            switch (cmd?.ToLower().Trim())
            {
                case "кінець":
                    _currentState = AppState.Exit;
                    Console.WriteLine("Дякую, що звільнили мене! Іду пити каву");
                    break;

                case "назад":
                    _currentState = AppState.Default;
                    break;

                default:
                    if (_currentState == AppState.Default)
                    {
                        _currentState = AppState.WarehouseDetails;
                    }
                    else if (_currentState == AppState.WarehouseDetails)
                    {
                        _currentState = AppState.GoodDetails;
                    }
                    break;
            }
        }

        private static void ShowAllWarehouses()
        {
            Console.Clear();
            Console.WriteLine("--- Ось усі доступні склади: ---");
            LoadWarehouses();

            foreach (var w in _warehouses)
            {
                Console.WriteLine($"- {w.Name} (Місто: {w.Location}, Загальна вартість: {w.TotalValue} грн)");
            }
        }

        private static void LoadWarehouses()
        {
            if (_warehouses == null)
            {
                _warehouses = _warehouseService.GetWarehouses().ToList();
            }
        }

        private static void ShowWarehouseDetails(string? warehouseName)
        {
            var listDto = _warehouses?.FirstOrDefault(w => string.Equals(w.Name, warehouseName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (listDto != null)
            {
                _currentWarehouse = _warehouseService.GetWarehouseDetails(listDto.Id);
                _currentWarehouseGoods = _goodService.GetGoodsByWarehouse(_currentWarehouse.Id).ToList();
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
            else
            {
                Console.WriteLine("Упс( Складу з такою назвою не існує.");
                _currentState = AppState.Default;
            }
        }

        private static void ShowGoodDetails(string? productName)
        {
            if (_currentWarehouse == null || _currentWarehouseGoods == null) return;

            var goodListDto = _currentWarehouseGoods.FirstOrDefault(g => string.Equals(g.Title, productName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (goodListDto != null)
            {
                var detailsDto = _goodService.GetGoodDetails(goodListDto.Id);
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
            else
            {
                Console.WriteLine("Такого товару не знайдено на цьому складі.");
            }
        }
    }
}