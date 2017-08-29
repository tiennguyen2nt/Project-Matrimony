using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Dao.Payments;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.Payments
{
    public class ProductBiz
    {
        public List<Product> GetProducts()
        {
            ProductDao dao =  new ProductDao();
            return dao.GetAllProducts();
        }

        public Product CheckExistProduct(int? id)
        {
            ProductDao dao = new ProductDao();
            var item = dao.GetById(id);
            if (item == null)
            {
                throw new Exception("Gói Vip không khả dụng, vui lòng chọn gói khác hoặc liên hệ vói quản trị viên để được trợ giúp!");
            }
            return item;
        }
    }
}
