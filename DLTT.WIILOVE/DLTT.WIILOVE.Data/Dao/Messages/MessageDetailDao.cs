using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Messages
{
    public class MessageDetailDao
    {
        public void Add(MessagesDetail md)
        {
            using (var db = new WiiloveEntities())
            {
                db.MessagesDetails.Add(md);
                db.SaveChanges();
            }
        }

        public List<MessagesDetail> GetMessagesDetailsByMessId(long? id)
        {
            using (var db = new WiiloveEntities())
            {
               return db.MessagesDetails.Where(x => x.MessagesId == id).OrderBy(x =>x.SendTime).ToList();
            }
        }

        public string GetLastMessage(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                var lastOrDefault = db.MessagesDetails.Where(x => x.MessagesId == id).OrderByDescending(x => x.SendTime)
                    .FirstOrDefault();
                if (lastOrDefault != null)
                    return lastOrDefault.TextMessage;
                return null;
            }
        }

        public int CountUnRead(long? id,long? accountId)
        {
            using (var db = new WiiloveEntities())
            {
                return db.MessagesDetails.Count(x => x.MessagesId == id && x.IsRead == false && x.SenderId != accountId);
            }
        }

        public void IsRead(long? messId, long? currentId, bool isRead = true)
        {
            using (var db = new WiiloveEntities())
            {
                var items = db.MessagesDetails.Where(x => x.MessagesId == messId && x.IsRead == false && x.SenderId != currentId);
                foreach (var item in items)
                {
                    item.IsRead = isRead;
                }
                db.SaveChanges();
            }
        }

        public List<MessagesDetail> GetListUnRealByAccountId(long? id)
        {
            var sql = @"select enDetail.* from MessagesDetail enDetail
INNER JOIN Messages enMess on enMess.ID = enDetail.MessagesId
Where (enMess.FromId = @p0 or enMess.ToId = @p0) and enDetail.IsRead = 0 and enDetail.SenderId != @p0 ";
            using (var db = new WiiloveEntities())
            {
                var items = db.MessagesDetails.SqlQuery(sql, id);
                return items.ToList();
            }
        }
    }
}
