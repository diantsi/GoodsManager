using GoodsManager.Common.Enums;
using GoodsManager.DBModel;
using GoodsManager.Services;
using static GoodsManager.UIModels.WarehouseUIModel;

namespace GoodsManager.UIModels
{
    public class WarehouseUIModel
    {
   
            private WarehouseDBModel _dbModel;
            private string _name;
            private City _location;
            private List<GoodUIModel> _goods;
        private IStorageService _storage;

        public Guid? Id
        {
           get => _dbModel?.Id;
        }

            public string Name
            {
            get => _name;
            set => _name = value;
            }

            public City Location
            {
                get => _location;
                set => _location = value;
            }

        /// <summary>
        /// Calculates the total price of all loaded products.
        /// Returns -1 if products haven't been loaded yet.
        /// </summary>
        public decimal TotalPriceWarehouse
        {
            get
            {
                if (_goods == null) return -1;

                decimal total = 0;
                foreach (var good in _goods)
                {
                    total += good.TotalValue; 
                }
                return total;
            }
        }

        /// <summary>
        /// Provides a description of the total price to show for users.
        /// </summary>
        public string TotalPriceDesc
        {
            get
            {
                if (TotalPriceWarehouse == -1) return "Не завантажено товарів";
                return $"{TotalPriceWarehouse} грн";
            }
        }

        public IReadOnlyList<GoodUIModel> Goods
        {
            get => _goods;
        }
        public WarehouseUIModel(IStorageService storage)
        {
            _storage = storage;
            _goods = new List<GoodUIModel>();
        }

     
        public WarehouseUIModel(IStorageService storage, WarehouseDBModel dbModel)
            {
            _storage = storage;
            _dbModel = dbModel;
                _name = dbModel.Name;
                _location = dbModel.Location;
            }

   
        public void LoadGoods()
        {
            if (Id == null || _goods != null) return;

            _goods = new List<GoodUIModel>();
            foreach (var productDB in _storage.GetGoods(Id.Value))
            {
                _goods.Add(new GoodUIModel(productDB));
            }
        }

        /// <summary>
        /// Save changes from the UI model back to the DB model.
        /// </summary>
        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Name = _name;
                _dbModel.Location = _location;
            }
            else
            {
                _dbModel = new WarehouseDBModel(_name, _location);
            }
        }

        public override string ToString()
        {
            return $"Склад: {Name}, Місто знаходження: {Location}, Повна сума усіх товарів: {TotalPriceDesc}";
        }

    }
}
