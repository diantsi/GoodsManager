using GoodsManager.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.Services
{
    public interface IStorageService
    {
        public IEnumerable<WarehouseDBModel> GetAllWarehouses();

        public IEnumerable<GoodDBModel> GetGoods(Guid warehouseId);

    }
}
