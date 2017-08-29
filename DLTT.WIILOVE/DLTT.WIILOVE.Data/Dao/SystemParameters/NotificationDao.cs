using System.Collections.Generic;
using System.Linq;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.SystemParameters
{
    public class NotificationDao
    {
        public void Add(Notification notifi)
        {
            using (var db = new WiiloveEntities())
            {
                db.Notifications.Add(notifi);
                db.SaveChanges();
            }
        }

        public void Remove(long? formId, long? toId)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.Notifications.Where(x => x.FormId == formId && x.ToId == toId);
                db.Notifications.RemoveRange(item);
                db.SaveChanges();
            }
        }

        public bool CheckNotfi(long? formId, long? toId)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.Notifications.FirstOrDefault(x => x.FormId == formId && x.ToId == toId);
                if (item == null)
                    return false;
                return true;
            }
        }
        

        public bool CheckNotifExist(long? formId, long? toId)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.Notifications.FirstOrDefault(x => (x.FormId == formId && x.ToId == toId) || (x.FormId == toId && x.ToId == formId));
                if (item == null)
                    return true;
                return false;
            }
        }
        public List<Notification> GetAllByUserId(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Notifications.Where(x => x.ToId == id).ToList();
            }
        }

        public void isReadById(long? id,bool isRead = true)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.Notifications.FirstOrDefault(x => x.Id == id);
                item.isRead = isRead;
                db.SaveChanges();
            }
        }

        public int? GetCountNotifIsRead(long? id, bool isRead = false)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Notifications.Count(x => x.ToId == id && x.isRead == isRead);
            }
        }

        public void IsReadByUserId(long? id, bool isRead = true)
        {
            using (var db = new WiiloveEntities())
            {
                var items = db.Notifications.Where(x => x.ToId == id).ToList();
                items.ForEach(x =>x.isRead = isRead);
                db.SaveChanges();
            }
        }
    }
}
