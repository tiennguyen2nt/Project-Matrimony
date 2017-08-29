using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Dao.Accounts;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.AccountBiz
{
    public class UserManagement
    {

        public void AddNew(UserOnline on)
        {
            UserOnlineDao dao = new UserOnlineDao();
            dao.Add(on);
        }
        public void Disconnection(string connctionId)
        {
            UserOnlineDao dao = new UserOnlineDao();
            dao.Remove(connctionId);
        }

        public void UpdateConnection(UserOnline on)
        {
            UserOnlineDao dao = new UserOnlineDao();
            dao.UpdateConnection(on);
        }

        public UserOnline GetUser(long? accountId)
        {
            UserOnlineDao dao = new UserOnlineDao();
            return dao.GetByAccountId(accountId);
        }

        public List<UserOnline> GetOnlines()
        {
            UserOnlineDao dao = new UserOnlineDao();
            return dao.GetAll();
        }
    }
}
