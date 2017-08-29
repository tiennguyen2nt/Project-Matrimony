using System.Web.Mvc;
using DLTT.WIILOVE.Biz.AccountBiz;
using DLTT.WIILOVE.Biz.UserDetails;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Controllers
{
    
    public class SearchController : Controller
    {
        [Authorize]
        [HttpPost]
       public ActionResult Search(SearchParam param)
        {
            AccountBiz biz = new AccountBiz();
            var item = biz.GetBaseDetail(HttpContext.User.Identity.Name);
            ViewBag.view = item;
            UserDetailBiz bizDetail = new UserDetailBiz();
            return View(bizDetail.SearchBase(param,item.AccountId));
        }

        [HttpPost]
        public ActionResult Index(SearchParam param)
        {
            UserDetailBiz bizDetail = new UserDetailBiz();
            return View(bizDetail.SearchBase(param));
        }
    }
}