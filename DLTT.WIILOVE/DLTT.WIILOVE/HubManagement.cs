using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.MessageBiz;
using DLTT.WIILOVE.Biz.UserDetails;
using DLTT.WIILOVE.Biz.Utils;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;
using Microsoft.AspNet.SignalR;

namespace DLTT.WIILOVE
{
    [Authorize]
    public class HubManagement : Hub
    {
        #region Notifition and AddFriend


        public void Connect(string AccountId)
        {
            var id = Context.ConnectionId;
            UserManagement management = new UserManagement();
            UserDetailBiz detailBiz = new UserDetailBiz();
            var list = detailBiz.GetListUser(long.Parse(AccountId));

            var ons = management.GetOnlines();
            foreach (var item in list)
            {
                var on = ons.FirstOrDefault(x => x.AccountId == item.AccountId);
                if (on != null)
                    item.ConnectionId = on.ConnectionId;
            }
            var con = management.GetUser(long.Parse(AccountId));
            if (con == null)
            {
                UserOnline on = new UserOnline();
                on.AccountId = long.Parse(AccountId);
                on.ConnectionId = id;

                management.AddNew(on);

                Clients.Caller.onConnected(id, AccountId, list.Where(x => x.AccountId != long.Parse(AccountId)));

            }
            else
            {
                management.UpdateConnection(new UserOnline() { AccountId = long.Parse(AccountId), ConnectionId = id });
                Clients.Caller.onConnected(id, AccountId, list.Where(x => x.AccountId != long.Parse(AccountId)));

            }
            Clients.AllExcept(id).onNewUserConnected(id, list.FirstOrDefault(x => x.AccountId == long.Parse(AccountId)));

        }

        public void UnConfirm(string id, string currentId)
        {
            UserManagement management = new UserManagement();
            var item = management.GetUser(long.Parse(id));
            string connectionId = null;
            if (item != null)
                connectionId = item.ConnectionId;
            NotificationManagement notif = new NotificationManagement();
            if (!notif.CheckExistNotif(long.Parse(currentId), long.Parse(id)))
            {
                notif.UnNotif(long.Parse(currentId), long.Parse(id));
                Clients.Client(connectionId).unSendNotifi(currentId, id);
            }
        }
        public void UnSendNotif(string toConnectionId, string fromId, string toId)
        {
            NotificationManagement notif = new NotificationManagement();
            notif.UnNotif(long.Parse(fromId), long.Parse(toId));
            Clients.Client(toConnectionId).unSendNotifi(fromId, toId);
        }

        public void AddFriend(string id, string currentId)
        {
            UserManagement management = new UserManagement();
            var item = management.GetUser(long.Parse(id));
            string connectionId = null;
            if (item != null)
                connectionId = item.ConnectionId;
            Notif(connectionId, long.Parse(currentId), long.Parse(id), (int)Util.TypeNotif.AddFriend);

        }

        private void Notif(string connectionId, long? formId, long? toId, int type,
            string text = "Đã gửi yêu cầu kết bạn đến bạn")
        {
            NotificationManagement notif = new NotificationManagement();
            if (connectionId != null)
            {
                UserDetailBiz biz = new UserDetailBiz();
                var item = biz.GetById(formId, null);
                var fullname = (item.LastName == null ? "" : (item.LastName + ' ')) + ((item.MiddleName == null) ? "" : item.MiddleName + ' ') + item.FirstName;
                item.Account = new Account();
                if (notif.CheckExistNotif(formId, toId))
                {
                    Clients.Client(connectionId).sendPrivateNotifi(formId, item, fullname, text);
                    notif.AddNew(formId, toId, type, text);
                }
            }
        }
        public void ConfirmFriend(string id, string currentId)
        {
            NotificationManagement notif = new NotificationManagement();
            RelationshipManagement rela = new RelationshipManagement();
            UserManagement management = new UserManagement();
            var item = management.GetUser(long.Parse(id));
            string connectionId = null;
            if (item != null)
                connectionId = item.ConnectionId;
            rela.AddRela(long.Parse(currentId), long.Parse(id));
            notif.UnNotif(long.Parse(id), long.Parse(currentId));

            Notif(connectionId, long.Parse(currentId), long.Parse(id), (int)Util.TypeNotif.ConfirmFriend, "Đã chấp nhận yêu cầu kết bạn");
        }
        public void SendNotif(string toConnectionId, string formId, string toId)
        {
            Notif(toConnectionId, long.Parse(formId), long.Parse(toId), (int)Util.TypeNotif.AddFriend);
        }

        public void IsReadNotif(string currentId)
        {
            NotificationManagement notif = new NotificationManagement();
            notif.IsReads(long.Parse(currentId));
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            UserManagement management = new UserManagement();
            management.Disconnection(id);
            return base.OnDisconnected(stopCalled);
        }


        #endregion

        #region Messeger

        public void SendMesseger(string messageId, string id, string sendId, string textMessage)
        {
            MessageManagement message = new MessageManagement();
            var mess = message.SendMessage(long.Parse(messageId), long.Parse(id), textMessage);
            UserManagement management = new UserManagement();
            var itemId = management.GetUser(long.Parse(sendId));
            string connectionId = null;
            if (itemId != null)
            {
                connectionId = itemId.ConnectionId;
                Clients.Client(connectionId).GetMessage(mess);
            }
            Clients.Caller.SendMessage(mess);
        }

        public void IsReadMessage(string messageId, string currentId)
        {
            MessageManagement message = new MessageManagement();
            message.IsRead(long.Parse(messageId),long.Parse(currentId));
        }
        #endregion
    }
}