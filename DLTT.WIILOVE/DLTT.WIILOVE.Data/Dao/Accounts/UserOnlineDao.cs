using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Accounts
{
    public class UserOnlineDao
    {

        public void Add(UserOnline u)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserOnlines.Add(u);
                db.SaveChanges();
            }
        }
        
        public List<UserOnline> GetAll()
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserOnlines.ToList();
            }
        }

        public UserOnline GetByAccountId(long? accountId)
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserOnlines.FirstOrDefault(x => x.AccountId == accountId);
            }
        }

        public void Remove(string connectionId)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.UserOnlines.FirstOrDefault(x => x.ConnectionId.Equals(connectionId));
                if (item != null)
                {
                    db.UserOnlines.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateConnection(UserOnline on)
        {
            using (var db = new WiiloveEntities())
            {
               var item = db.UserOnlines.FirstOrDefault(x => x.AccountId == on.AccountId);
                item.ConnectionId = on.ConnectionId;
               
                db.SaveChanges();
            }
        }
    }
}
