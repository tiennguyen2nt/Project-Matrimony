using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.Payments;
using DLTT.WIILOVE.Biz.Utils;

namespace DLTT.WIILOVE.Controllers
{
    public class DonateController : Controller
    {
        // GET: Donate
        public ActionResult Index(int? id)
        {
            AccountBiz biz = new AccountBiz();
            ViewBag.view = biz.GetBaseDetail(HttpContext.User.Identity.Name);
           
            if (id != null)
                Util.URL_BACK = id.ToString();
            return View();
        }

        public ActionResult Donate(string seriNumber, string code)
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            try
            {
                if (ModelState.IsValid && seriNumber.Equals(code))
                {
                    PaymentManagement pay = new PaymentManagement();
                    pay.Donate(item.AccountId, code.Trim());
                    return RedirectToAction("Purchase", "Payment", new { id = Util.URL_BACK });
                }
            }
            catch (Exception e)
            {
                item.MessageNotifition = e.Message;
            }
            ViewBag.view = item;
            return View("Index");
        }
    }
}