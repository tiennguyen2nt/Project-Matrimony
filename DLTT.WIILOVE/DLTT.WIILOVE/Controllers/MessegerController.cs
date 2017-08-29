using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.MessageBiz;
using DLTT.WIILOVE.Biz.Utils;

namespace DLTT.WIILOVE.Controllers
{
    [Authorize]
    public class MessegerController : Controller
    {
        // GET: Messeger
        public ActionResult Index()
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            if (item.VipType != Util.MemberVip.Vip)
                return RedirectToAction("NewFeed", "Home");
            MessageManagement management = new MessageManagement();
            var chatList = management.GetChatList(item.AccountId);
            var lastMess = chatList.FirstOrDefault();
            if (lastMess == null) return View(chatList);
            var lastMessages = management.GetMessages(lastMess,item.AccountId);
            ViewBag.Messages = lastMessages;
            return View(chatList);
        }

        public ActionResult n(long? id)
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            if (item.VipType != Util.MemberVip.Vip)
                return RedirectToAction("NewFeed", "Home");
            MessageManagement management = new MessageManagement();
            var chatList = management.GetChatList(item.AccountId);
            var message = chatList.LastOrDefault();
            if (id != null)
            {
                var idMess = management.CheckExistMessage(item.AccountId, id);
                if (idMess == null)
                {
                    message = management.AddNewChat(item.AccountId, id);
                    chatList.Add(message);
                }
                else
                {
                    message = idMess;
                }
            }
            var lastMessages = management.GetMessages(message,item.AccountId);
            ViewBag.Messages = lastMessages;
            return View("Index", chatList);
        }
        public ActionResult t(long? id)
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            if (item.VipType != Util.MemberVip.Vip)
                return RedirectToAction("NewFeed", "Home");
            MessageManagement management = new MessageManagement();
            var chatList = management.GetChatList(item.AccountId);
            var message = chatList.LastOrDefault();
            if (id != null)
            {
                var m = management.CheckExistMessageById(id);
                if (m != null)
                    message = m;
            }
            var lastMessages = management.GetMessages(message, item.AccountId);
            ViewBag.Messages = lastMessages;
            return View("Index", chatList);
        }
    }
}