using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Biz.Utils;
using DLTT.WIILOVE.Data.Dao;
using DLTT.WIILOVE.Data.Dao.Messages;
using DLTT.WIILOVE.Data.Dao.UserDetails;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Biz.MessageBiz
{
    public class MessageManagement
    {
        public List<Message> GetChatList(long? id) 
        {
            MessageDao dao = new MessageDao();
            
            var chatList = dao.GetListChatByAccoutId(id);
            foreach (var item in chatList)
            {
                SetValueMessage(item, id);
                MessageDetailDao detailDao = new MessageDetailDao();
                item.LastMessage = detailDao.GetLastMessage(item.ID);
                item.CountUnRead = detailDao.CountUnRead(item.ID,id);
            }
            return chatList;
        }

        public void SetValueMessage(Message item,long? id)
        {
            UserDetailDao userDao = new UserDetailDao();
            if (item.Name == null)
            {
                if (item.FromId == id)
                {
                    var detail = userDao.GetByAccountId(item.ToId);
                    item.Name = Util.GetFullName(detail);
                    if (item.AvatarURL == null)
                        item.AvatarURL = Util.HasAvatar(detail.AvatarURL, detail.Gender);
                }
                else
                {
                    var detail = userDao.GetByAccountId(item.FromId);
                    item.Name = Util.GetFullName(detail);
                    if (item.AvatarURL == null)
                        item.AvatarURL = Util.HasAvatar(detail.AvatarURL, detail.Gender);
                }
            }
            else
            {
                if (item.FromId == id)
                {
                    var detail = userDao.GetByAccountId(item.ToId);
                    if (item.AvatarURL == null)
                        item.AvatarURL = Util.HasAvatar(detail.AvatarURL, detail.Gender);
                }
                else
                {
                    var detail = userDao.GetByAccountId(item.FromId);
                    if (item.AvatarURL == null)
                        item.AvatarURL = Util.HasAvatar(detail.AvatarURL, detail.Gender);
                }
            }
        }
        public MessageDetailParam GetMessages(Message m,long? currentId)
        {
            MessageDetailDao dao = new MessageDetailDao();
            MessageDetailParam param = new MessageDetailParam();;
            param.MessagesDetails = dao.GetMessagesDetailsByMessId(m.ID);
            SetValueMessage(m, currentId);
            param.AvatarUrl = m.AvatarURL;
            param.Name = m.Name;
            param.MessageId = m.ID;
            param.SendId = currentId == m.FromId ? m.ToId : m.FromId;
            return param;
        }

        public Message CheckExistMessage(long? currentId, long? id)
        {
            MessageDao dao = new MessageDao();
            var item = dao.GetMessageByAccoutId(currentId, id);
            return item;
        }

        public Message CheckExistMessageById(long? id)
        {
            MessageDao dao = new MessageDao();
            var item = dao.GetChatById(id);
            return item;
        }

        public Message AddNewChat(long? currentId, long? id)
        {
            MessageDao dao = new MessageDao();
            Message m = new Message();
            m.FromId = currentId;
            m.ToId = id;
            m.CreateDate = DateTime.Now;;
            m.Status = 1;
            dao.Add(m);
            return m;
        }

        public MessagesDetail SendMessage(long? messageId, long? accountId, string textMessage)
        {
            MessageDetailDao dao = new MessageDetailDao();
            MessagesDetail detail = new MessagesDetail();
            detail.MessagesId = messageId;
            detail.SenderId = accountId;
            detail.TextMessage = textMessage;
            detail.SendTime = DateTime.Now;
            detail.IsRead = false;
            detail.Type = Util.MessageType.TextMessage;
            dao.Add(detail);
            MessageDao mDao = new MessageDao();
            mDao.UpdateTime(new Message() {ID = messageId.Value ,CreateDate = DateTime.Now});
            detail.AvatarUrl = Util.GetAvatarById(accountId);
            detail.TimeSend = Util.TimeSend(detail.SendTime);
            return detail;
        }

        public void IsRead(long? messageId, long? currentId)
        {
            MessageDetailDao dao = new MessageDetailDao();
            dao.IsRead(messageId,currentId);
        }
        /*
         * 
         * Trường thêm vào Entity Message
      
          public string LastMessage { get; set; }
        public int CountUnRead { get; set; }

        *
        *  Trường thêm vào Entity MessageDetail
        *  
         #region Other

       public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string TimeSend { get; set; }

        #endregion

        *
        * 
         */
    }
}
