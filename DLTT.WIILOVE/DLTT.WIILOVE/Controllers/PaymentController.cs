using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.Payments;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Product()
        {
            AccountBiz biz = new AccountBiz();
            ViewBag.view = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ProductBiz pBiz = new ProductBiz();
            return View(pBiz.GetProducts());
        }

        public ActionResult Purchase(int id)
        {
            AccountBiz biz = new AccountBiz();
            ViewParam item = new ViewParam();;
            var product = new Product();
            try
            {
                item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
                PaymentManagement pay = new PaymentManagement();
                ViewBag.Money = pay.GetRanking(item.AccountId);
                ProductBiz pBiz = new ProductBiz();
                product = pBiz.CheckExistProduct(id);
            }
            catch (Exception e)
            {
                item.MessageNotifition = e.Message;
                ViewBag.view = item;
                return RedirectToAction("Product");
            }
            ViewBag.view = item;
            return View(product);
        }
        public ActionResult Buy(int id)
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
           
            PaymentManagement pay = new PaymentManagement();
            try
            {
                pay.BuyVip(item.AccountId,id);
            }
            catch (Exception e)
            {
                item.MessageNotifition = e.Message;
                return View("Error");
            }
            ViewBag.view = item;
            return View("BuySuccessful");
        }

    }
}