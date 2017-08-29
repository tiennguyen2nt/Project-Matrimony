using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Payments
{
    public class UserRankingDao
    {
        public UserRanking GetByAccountId(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserRankings.FirstOrDefault(x => x.AccountId == id);
            }
        }

        public void AddNew(UserRanking ur)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserRankings.Add(ur);
                db.SaveChanges();
            }
        }

        public void EditUserRanking(UserRanking ur)
        {
            using (var db = new WiiloveEntities())
            {
                db.Entry(ur).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
