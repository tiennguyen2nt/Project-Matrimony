using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Workflow.ComponentModel.Compiler;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.UserDetails;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [Authorize]
        // GET: Profile
        public ActionResult Index(long? id)
        {
            if (id == null) RedirectToAction("NewFeed", "Home");
            UserDetailBiz biz = new UserDetailBiz();
            AccountBiz accbiz = new AccountBiz();
            var item = accbiz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            return View(biz.GetById(id,item.AccountId));
        }

        public ActionResult Edit(long? id)
        {
            if (id == null) RedirectToAction("NewFeed", "Home");
            UserDetailBiz biz = new UserDetailBiz();
            AccountBiz accbiz = new AccountBiz();
            var item = accbiz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            return View(biz.GetById(id, item.AccountId));
        }

        [HttpPost]
        public ActionResult Edit(UserDetail ud)
        {
            UserDetailBiz biz = new UserDetailBiz();
            AccountBiz accbiz = new AccountBiz();
            var item = accbiz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            if (ModelState.IsValid)
            {
                biz.UpdateProfile(ud,item.AccountId.Value);
                return RedirectToAction("Index","Profile",new {Id = item.AccountId});
            }
            return View(biz.GetById(item.AccountId, null));

        }
    }
}