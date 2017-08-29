using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DLTT.WIILOVE.Data.Model.Param;
using DLTT.WIILOVE.Models;
using DLTT.WIILOVE.Biz.AccountBiz;

namespace DLTT.WIILOVE.Controllers
{
    public class AccountController : Controller
    {   
        // GET: Account
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            if (Membership.ValidateUser(login.Username, login.Password) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(login.Username, login.Remember);
                return RedirectToAction("NewFeed", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai!");
                return View("RedirectLogin", login);
            }

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RedirectLogin()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectLogin(Login login)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            if (Membership.ValidateUser(login.Username, login.Password) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(login.Username, login.Remember);
                return RedirectToAction("NewFeed", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai!");
                return View(login);
            }

        }
     
        public ActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            return View(new User());
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            if (ModelState.IsValid && user.Terms)
            {
                try
                {
                    AccountBiz biz = new AccountBiz();
                    biz.Add(user);
                    if(Membership.ValidateUser(user.EmailOrPhone, user.Password))
                        FormsAuthentication.SetAuthCookie(user.EmailOrPhone, false);
                    return RedirectToAction("NewFeed", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            if(!user.Terms)
            ModelState.AddModelError("", "Bạn chưa đồng ý với điều khoản và dịch vụ của chúng tôi.");
            return View(user);
        }
       [HttpPost]
        public bool IsExistEmailorPhone(string EmailOrPhone)
       {

           if (EmailOrPhone.Equals("abc@gmail.com"))
               return true;
           return false;
       }
    }
}