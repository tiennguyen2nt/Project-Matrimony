using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Dao.SystemParameters;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.AccountBiz
{
    public class NotificationManagement
    {
        public void AddNew(long? formId, long? toId, int type,string text)
        {
            Notification notifi = new Notification();
            notifi.FormId = formId;
            notifi.ToId = toId;
            notifi.Type = type;
            notifi.isRead = false;
            notifi.Desccription = text;

            NotificationDao dao = new NotificationDao();
            dao.Add(notifi);
        }

        public void UnNotif(long? formId, long? toId)
        {
            NotificationDao dao = new NotificationDao();
            dao.Remove(formId,toId);
        }

        public bool CheckExistNotif(long? formId, long? toId)
        {
            NotificationDao dao = new NotificationDao();
            return dao.CheckNotifExist(formId, toId);
        }
        public List<Notification> GetAllNotificationsOfMe(long? id)
        {
            NotificationDao dao = new NotificationDao();
            return dao.GetAllByUserId(id); 
        }

        public void IsRead(long? id)
        {
            NotificationDao dao = new NotificationDao();
            dao.isReadById(id);
        }

        public void UnRead(long? id)
        {
            NotificationDao dao = new NotificationDao();
            dao.isReadById(id,false);
        }

        public void IsReads(long? id)
        {
            NotificationDao dao = new NotificationDao();
            dao.IsReadByUserId(id);
        }
    }
}
