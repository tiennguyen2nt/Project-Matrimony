using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Models
{
    public class UserParam
    {
        public List<UserDeatilBase> UserDetails { get; set; }
        public User User { get; set; }

    }
}