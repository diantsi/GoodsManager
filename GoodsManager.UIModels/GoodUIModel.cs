using GoodsManager.Common.Enums;
using GoodsManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.UIModels
{
    public class GoodUIModel
    {
        private GoodDBModel _dbModel;
        private Guid _warehouseId;
        private string _title;
        private int _quantity;
        private decimal _unitPrice;
        private Category _category;
        private string _description;
        private decimal _totalValue; 

        public Guid? Id
        {
            get => _dbModel?.Id;
        }

        public Guid WarehouseId
        {
            get => _warehouseId;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
            }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;
            }
        }

        public Category Category
        {
            get => _category;
            set => _category = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public decimal TotalValue
        {
            get => _totalValue;
        }

        public GoodUIModel(Guid warehouseId)
        {
            _warehouseId = warehouseId;
        }

        public GoodUIModel(GoodDBModel dbModel)
        {
            _dbModel = dbModel;
            _warehouseId = dbModel.WarehouseId;
            _title = dbModel.Title;
            _quantity = dbModel.Quantity;
            _unitPrice = dbModel.Price;
            _category = dbModel.ItemCategory;
            _description = dbModel.Description;
            CalculateTotalValue();
        }

        private void CalculateTotalValue()
        {
            _totalValue = _quantity * _unitPrice;
        }


        /// <summary>
        /// Save changes from the UI model back to the DB model.
        /// </summary>
        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Title = _title;
                _dbModel.Quantity = _quantity;
                _dbModel.Price = _unitPrice;
                _dbModel.ItemCategory = _category;
                _dbModel.Description = _description;
            }
            else
            {
                _dbModel = new GoodDBModel(_warehouseId, _title, _quantity, _unitPrice, _category, _description);
            }
        }

        public override string ToString()
        {
            return $"Товар: {Title} ({Category}), Ціна: {UnitPrice} грн, К-сть: {Quantity}";
        }

        public string FullInfo
        {
            get
            {
                return $"--- Повна інформація про {Title} ---\n" +
                       $"Опис: {Description}\n" +
                       $"Категорія: {Category}\n" +
                       $"Кількість на складі: {Quantity}\n" +
                       $"Ціна: {UnitPrice} UAH\n" +
                       $"Повна ціна: {TotalValue} UAH\n";
            }
        }
    }
}
