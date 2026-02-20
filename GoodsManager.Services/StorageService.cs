using GoodsManager.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    public class StorageService : IStorageService
    { 
        private List<WarehouseDBModel> _warehouses;
        private List<GoodDBModel> _goods;

        private void LoadData()
        {
            if (_warehouses != null && _goods != null)
                return;

            _warehouses = FakeStorage.Warehouses.ToList();
            _goods = FakeStorage.Goods.ToList();
        }



        /// <summary>
        /// Get all goods from certain warehouseId
        /// create a copy so no one can mess with original lists
        /// </summary>
        public IEnumerable<GoodDBModel> GetGoods(Guid warehouseId)
        {
            LoadData();
            return _goods.Where(p => p.WarehouseId == warehouseId).ToList();
        }

        public IEnumerable<WarehouseDBModel> GetAllWarehouses()
        {
            LoadData();
            return _warehouses.ToList();
        }
    }
}
