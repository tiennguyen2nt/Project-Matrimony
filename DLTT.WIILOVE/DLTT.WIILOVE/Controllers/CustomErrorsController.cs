using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;

namespace DLTT.WIILOVE.Controllers
{
    public class CustomErrorsController : Controller
    {
        // GET: CustomErrors
      
        public ActionResult Error()
        {
            AccountBiz biz = new AccountBiz();
            ViewBag.view = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            return View();
        }
    }
}