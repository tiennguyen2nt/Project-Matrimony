using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DLTT.WIILOVE.Biz.UserDetails;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        // GET: Upload
       

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                var id = System.Web.HttpContext.Current.Request["Id"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    if (_ext.Equals(".jpg") || _ext.Equals(".png"))
                    {
                        _imgname = Guid.NewGuid().ToString();
                        var _comPath = Server.MapPath("/Images/Uploads/TN-") + _imgname + _ext;
                        _imgname = "TN-" + _imgname + _ext;
                        
                        // Saving Image in Original Mode
                        pic.SaveAs(_comPath);

                        // resizing image
                        MemoryStream ms = new MemoryStream();
                        WebImage img = new WebImage(_comPath);

                        if (img.Width > 200)
                            img.Resize(200, 200);
                        img.Save(_comPath);
                        // end resize
                        UserDetail u = new UserDetail();
                        u.Id = Convert.ToInt64(id);
                        u.AvatarURL = _imgname;
                        UserDetailBiz biz = new UserDetailBiz();
                        biz.UpdateAvatar(u);
                    }
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
    }
}