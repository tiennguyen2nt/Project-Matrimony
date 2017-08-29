using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Biz.Utils;
using DLTT.WIILOVE.Data.Dao.Payments;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.Payments
{
    public class PaymentManagement
    {
        public void FreeVip(long? accountId)
        {
            ProductDao productDao = new ProductDao();
            var product = productDao.GetByCode(Util.MemberVip.Free);
            UserRanking rank = new UserRanking();
            rank.AccountId = accountId;
            rank.BuyDate = DateTime.Now;
            rank.ExpiryDate = DateTime.Now.AddDays(product.ExpicyDate ?? 7);
            rank.Rank = 1;
            rank.ProductId = product.Id;
            rank.DonatedAmount = 0;
            UserRankingDao dao = new UserRankingDao();
            dao.AddNew(rank);
        }

        public UserRanking GetRanking(long? id)
        {
            UserRankingDao dao = new UserRankingDao();
            return dao.GetByAccountId(id)??new UserRanking();
        }
        public void Donate(long? id, string code)
        {
            UserRankingDao dao = new UserRankingDao();
            var item = dao.GetByAccountId(id);
            if (item == null)
            {
                item = new UserRanking();
                item.DonatedAmount = 0;
                if (code.Equals(Util.DonateCode.Code30))
                {
                    item.DonatedAmount += 30;
                }
                else if (code.Equals(Util.DonateCode.Code50))
                {
                    item.DonatedAmount += 50;
                }
                else
                {
                    throw new Exception("Mã code không tồn tại hoặc đã được sử dụng!");
                }
                item.AccountId = id;
                dao.AddNew(item);
            }
            else
            {
                if (item.DonatedAmount == null) item.DonatedAmount = 0;
                if (code.Equals(Util.DonateCode.Code30))
                {
                    item.DonatedAmount += 30;
                }
                else if (code.Equals(Util.DonateCode.Code50))
                {
                    item.DonatedAmount += 50;
                }
                else
                {
                    throw new Exception("Mã code không tồn tại hoặc đã được sử dụng!");
                }
                dao.EditUserRanking(item);
            }
        }

        public void BuyVip(long? id, int package)
        {
            UserRankingDao dao = new UserRankingDao();
            var item = dao.GetByAccountId(id);
            ProductDao productDao = new ProductDao();
            var products = productDao.GetAllProducts();
            var product = products.FirstOrDefault(x => x.Id == package);
            if (product == null) throw new Exception("Gói Vip không khả dụng, vui lòng chọn gói khác hoặc liên hệ vói quản trị viên để được trợ giúp!");
            if (item.DonatedAmount >= product.Price)
            {
                item.DonatedAmount -= product.Price;
                item.ProductId = product.Id;
                item.BuyDate = DateTime.Now;
                item.ExpiryDate = DateTime.Now.AddDays(product.ExpicyDate??0);
            }
            else
            {
                throw new Exception("Số tiền trong tài khoản của bạn không đủ để mua gói VIP này!");
            }
            dao.EditUserRanking(item);

        }
    }
}
