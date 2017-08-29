using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.UserDetails;
using DLTT.WIILOVE.Data.Model.Param;
using DLTT.WIILOVE.Models;

namespace DLTT.WIILOVE.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewFeed", "Home");
            }
            UserDetailBiz biz = new UserDetailBiz();
            UserParam userParam = new UserParam();
            userParam.UserDetails = biz.Get20DetailBase();
            userParam.User = new User();
            return View(userParam);
            
        }


        [Authorize]
        public ActionResult NewFeed()
        {
            AccountBiz biz = new AccountBiz();
            ViewBag.view = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            return View();
        }
    }
}