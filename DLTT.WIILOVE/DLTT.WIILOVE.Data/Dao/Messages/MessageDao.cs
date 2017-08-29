using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Messages
{
    public class MessageDao
    {
        public List<Message> GetListChatByAccoutId(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Messages.Where(x => x.FromId == id || x.ToId == id).OrderByDescending(x => x.CreateDate).ToList();
            }
        }

        public Message GetChatByAccountId(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Messages.FirstOrDefault(x => x.FromId == id || x.ToId == id);
            }
        }

        public Message GetChatById(long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Messages.FirstOrDefault(x => x.ID == id);
            }
        }

        public void Add(Message m)
        {
            using (var db = new WiiloveEntities())
            {
                db.Messages.Add(m);
                db.SaveChanges();
            }
        }

        public void SetNameChat(Message m)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Entry(m);
                query.Property(x => x.Name).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateTime(Message m)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Messages.Find(m.ID);
                query.CreateDate = m.CreateDate;
                db.SaveChanges();
            }

        }
        public Message GetMessageByAccoutId(long? currentId, long? id)
        {
            using (var db = new WiiloveEntities())
            {
                return db.Messages.FirstOrDefault(x => (x.FromId == currentId && x.ToId == id) || (x.FromId == id && x.ToId == currentId));
            }
        }

    }

}
