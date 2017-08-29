using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Payments
{
    public class ProductDao
    {
        public Product GetById(int? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Products.FirstOrDefault(x => x.Id == id);
            }
        }

        public Product GetByCode(string code)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Products.FirstOrDefault(x => x.Code == code);
            }
        }
        public List<Product> GetAllProducts()
        {
            using (var db = new WiiloveEntities())
            {
                return db.Products.ToList();
            }
        }
    }
}
