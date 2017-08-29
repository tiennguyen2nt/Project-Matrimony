using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Biz.Email;
using DLTT.WIILOVE.Biz.Payments;
using DLTT.WIILOVE.Data.Model.Param;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Biz.Utils;
using DLTT.WIILOVE.Data.Dao.Accounts;
using DLTT.WIILOVE.Data.Dao.Messages;
using DLTT.WIILOVE.Data.Dao.Payments;
using DLTT.WIILOVE.Data.Dao.SystemParameters;
using DLTT.WIILOVE.Data.Dao.UserDetails;

namespace DLTT.WIILOVE.Biz.AccountBiz
{
    public class AccountBiz
    {
        public void Add(User user)
        {
            AccountDao dao = new AccountDao();
            Account account = new Account();
            account.CreateDate = DateTime.Now;
            account.Deleted = 0;
            if (Validate.IsValidEmail(user.EmailOrPhone))
            {
                if (dao.CheckEmail(user.EmailOrPhone))
                    throw new Exception("Địa chỉ Email đã tồn tại.");
                account.Email = user.EmailOrPhone;
            }
            else if (Validate.IsValidPhoneNumber(user.EmailOrPhone))
            {
                if(dao.CheckEmail(user.EmailOrPhone))
                    throw new Exception("Số điện thoại đã tồn tại.");
                account.PhoneNumber = user.EmailOrPhone;
            }
            else
            {
                throw new Exception("Vui lòng nhập số điện thoại hoặc email!");
            }
            if (string.Compare(user.Password, user.RetypePassword, true) == 0)
                account.Password = user.Password;
            else
                throw new Exception("Hai mật khẩu không giống nhau!");

            UserDetail detail = new UserDetail();
            detail.AccountId = account.ID;
            detail.FirstName = user.FirstName;
            detail.LastName = user.LastName;
            detail.DateOfBirth = new DateTime(user.Year, user.Month, user.Day);
            detail.Gender = user.Gender;
            account.UserDetails.Add(detail);
           
          
            //UserDetailDao detailDao = new UserDetailDao();
            //detailDao.Add(detail);
            if (account.Email != null)
            {
                EmailManagement email = new EmailManagement();
                string str = @"<!DOCTYPE html>
<html>
<head>
 </head>
 <body>
<h2> Xin chào " + user.FirstName + ' ' + user.LastName + @"</h2>
<p> Chào mừng bạn đã đến với Wiilove, </p>
</body>
</html> ";
                try
                {
                    email.SendEmail(account.Email, Util.Email.SUCCESSREGISTER, str);
                }
                catch (Exception e)
                {
                    throw new Exception("Không thể gủi email vui lòng liên hệ Admin để được trợ giúp!");
                }
               
            }

            dao.Add(account);
            PaymentManagement pay = new PaymentManagement();
            pay.FreeVip(account.ID);

        }

        public Account GetIdAccount(string find)
        {
            AccountDao dao = new AccountDao();
            return dao.GetAccount(find);
        }

        public ViewParam GetBaseDetail(string find)
        {
            AccountDao dao = new AccountDao();
            var acc = dao.GetAccount(find);
            UserDetailDao deailDao = new UserDetailDao();
            var detail = deailDao.GetByAccountId(acc.ID);
            ViewParam view = new ViewParam();
            view.AccountId = acc.ID;
            view.Firstname = detail.FirstName;
            view.FullName = Util.GetFullName(detail);
            view.AvatarUrl = Util.HasAvatar(detail.AvatarURL,detail.Gender);
            SystemParameterDao sysDao = new SystemParameterDao();
            view.Citys = sysDao.GetList().Where(x => x.FeatureID == 1001).ToList();
            view.Religions = sysDao.GetList().Where(x => x.FeatureID == 1003).ToList();
            List<UserDeatilBase> bases = new List<UserDeatilBase>();
            var list = deailDao.GetListByNotif(acc.ID);
            foreach (var item in list)
            {
                UserDeatilBase ba = new UserDeatilBase();
                ba.FirstName = item.FirstName;
                ba.FullName = Util.GetFullName(item);
                ba.AccountId = item.AccountId;
                ba.AvatarUrl = Util.HasAvatar(item.AvatarURL,item.Gender);
                ba.Age = DateTime.Now.Year - item.DateOfBirth.Value.Year;
                //ba.Decription = item.
                bases.Add(ba);
            }
            view.UserDeatilBases = bases;
            NotificationDao notifDao = new NotificationDao();
            var num = notifDao.GetCountNotifIsRead(acc.ID);
            if (num > 0 && num != null)
                view.NotifCount = num;
            UserRankingDao rankingDao = new UserRankingDao();
            var rank = rankingDao.GetByAccountId(acc.ID);
            if (rank == null || rank.ExpiryDate == null || rank.ExpiryDate < DateTime.Now)
            {
                view.VipType = Util.MemberVip.NotVip;
                view.MessageNotifition = "Bạn vui lòng mua VIP để sử dụng các dịch vụ tốt nhất của chúng tôi.";
            }
            else if(rank.ExpiryDate > DateTime.Now)
            {
                view.VipType = Util.MemberVip.Vip;
            }
            MessageDetailDao messageDetailDao = new MessageDetailDao();
            var mItems = messageDetailDao.GetListUnRealByAccountId(acc.ID);
            view.MessageCount = mItems.Count;
            List<MessagesDetail> messagesDetails = new List<MessagesDetail>();
            foreach (var item in mItems)
            {
                var q = messagesDetails.FirstOrDefault(x => x.MessagesId == item.MessagesId);
               
                if (q == null)
                {
                    MessageDao mDao = new MessageDao();
                    var m = mDao.GetChatById(item.MessagesId);
                    item.AvatarUrl = Util.GetAvatarById(m.FromId == acc.ID ? m.ToId : m.FromId);
                    item.Name = Util.GetFullName(deailDao.GetByAccountId(m.FromId == acc.ID ? m.ToId : m.FromId));
                    messagesDetails.Add(item);
                }
                else
                {
                    MessageDao mDao = new MessageDao();
                    var m = mDao.GetChatById(item.MessagesId);
                    item.Name = Util.GetFullName(deailDao.GetByAccountId(m.FromId == acc.ID ? m.ToId : m.FromId));
                    item.AvatarUrl = Util.GetAvatarById(m.FromId == acc.ID ? m.ToId : m.FromId);
                    messagesDetails.Remove(q);
                    messagesDetails.Add(item);
                }
            }
            view.NotifiMessages = messagesDetails;
            return view;
        }
    }
}
