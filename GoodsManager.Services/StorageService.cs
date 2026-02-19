using GoodsManager.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    public class StorageService
    { 
        private List<WarehouseDBModel> _warehouses;
        private List<GoodDBModel> _products;

        private void LoadData()
        {
            if (_warehouses != null && _products != null)
                return;

            _warehouses = FakeStorage.Warehouses.ToList();
            _products = FakeStorage.Products.ToList();
        }



        
        public IEnumerable<GoodDBModel> GetProducts(Guid warehouseId)
        {
            LoadData();
            return _products.Where(p => p.WarehouseId == warehouseId).ToList();
        }

        public IEnumerable<WarehouseDBModel> GetAllWarehouses()
        {
            LoadData();
            return _warehouses.ToList();
        }
    }
}
