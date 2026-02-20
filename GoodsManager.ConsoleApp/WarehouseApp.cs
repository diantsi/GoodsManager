using GoodsManager.Services;
using GoodsManager.UIModels;
using System.Linq;
using System.Text;

namespace Warehouse.ConsoleApp
{
    internal class WarehouseApp
    {
        enum AppState { Default, WarehouseDetails, GoodDetails, ErrorCmd, Exit }

        private static AppState _currentState = AppState.Default;
        private static IStorageService _storageService = new StorageService();
        private static List<WarehouseUIModel> _warehouses;

        private static WarehouseUIModel _currentWarehouse;

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

                Console.WriteLine("---Введіть 'кінець', щоб закрити програму---");

                currentCommand = Console.ReadLine();
                UpdateAppState(currentCommand);
            }
        }

        /// <summary>
        /// Logic to change different application states
        /// </summary>
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

            foreach (var warehouse in _warehouses)
            {
                Console.WriteLine(warehouse);
            }
        }

        private static void LoadWarehouses()
        {
            if (_warehouses == null)
            {
                var dbWarehouses = _storageService.GetAllWarehouses();
                _warehouses = new List<WarehouseUIModel>();
                foreach (var db in dbWarehouses)
                {
                    _warehouses.Add(new WarehouseUIModel(_storageService, db));
                }
            }
        }

        /// <summary>
        /// Displays details of the selected warehouse
        /// </summary>
        private static void ShowWarehouseDetails(string? warehouseName)
        {
            bool warehouseExists = false;

            foreach (var warehouse in _warehouses)
            {
                if (string.Equals(warehouse.Name, warehouseName?.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    warehouseExists = true;
                    _currentWarehouse = warehouse; 
                    _currentWarehouse.LoadGoods();

                    Console.Clear();
                    Console.WriteLine($"---Товари на складі '{warehouse.Name}': ---");

                    if (warehouse.Goods.Count == 0)
                    {
                        Console.WriteLine("---На цьому складі порожньо---");
                    }
                    else
                    {
                        foreach (var good in warehouse.Goods)
                        {
                            Console.WriteLine(good); 
                        }
                    }
                    Console.WriteLine($"Загальна сума: {warehouse.TotalPriceDesc}");
                    break;
                }
            }

            if (!warehouseExists)
            {
                Console.WriteLine("Упс( Складу з такою назвою не існує.");
                _currentState = AppState.Default;
            }
        }

        /// <summary>
        /// Shows full information for a specific good
        /// </summary>
        private static void ShowGoodDetails(string? productName)
        {
            if (_currentWarehouse == null) return;

            bool goodExists = false;

            foreach (var good in _currentWarehouse.Goods)
            {
                if (string.Equals(good.Title, productName?.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    goodExists = true;
                    Console.WriteLine(good.FullInfo);
                    break;
                }
            }

            if (!goodExists)
            {
                Console.WriteLine("Такого товару не знайдено на цьому складі.");
            }
        }
    }


}