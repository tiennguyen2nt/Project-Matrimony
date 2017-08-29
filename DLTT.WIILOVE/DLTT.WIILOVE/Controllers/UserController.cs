using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.UserDetails;

namespace DLTT.WIILOVE.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Friends()
        {
            AccountBiz biz = new AccountBiz();
            var baseview = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = baseview;
            UserDetailBiz ubiz = new UserDetailBiz();
            return View(ubiz.GetListFriend(baseview.AccountId));
        }

        public ActionResult UploadAvatar()
        {
            AccountBiz biz = new AccountBiz();
            var baseview = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = baseview;
            return View();
        }
    }
}