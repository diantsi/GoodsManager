using GoodsManager.Common.Enums;
using GoodsManager.DBModel;
using static GoodsManager.UIModels.WarehouseUIModel;

namespace GoodsManager.UIModels
{
    public class WarehouseUIModel
    {
   
            private WarehouseDBModel _dbModel;
            private string _name;
            private City _location;
            private List<GoodUIModel> _products;

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


        public decimal TotalPriceWarehouse
        {
            get
            {
                if (_products == null) return -1;

                decimal total = 0;
                foreach (var product in _products)
                {
                    total += product.TotalValue; 
                }
                return total;
            }
        }

        public string TotalPriceDesc
        {
            get
            {
                if (TotalPriceWarehouse == -1) return "Не завантажено продуктів";
                return $"{TotalPriceWarehouse} грн";
            }
        }

        public IReadOnlyList<GoodUIModel> Products
        {
            get => _products;
        }
        public WarehouseUIModel()
        {
            _products = new List<GoodUIModel>();
        }

     
        public WarehouseUIModel( WarehouseDBModel dbModel)
            {
                _dbModel = dbModel;
                _name = dbModel.Name;
                _location = dbModel.Location;
            }


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

    }
}
