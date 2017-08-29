using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Data.Model.Param
{
    public class ViewParam
    {
        public long? AccountId { get; set; }
        public string Firstname { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public List<SystemParameter> Citys { get; set; }
        public  List<SystemParameter> Religions { get; set; }
        public List<UserDeatilBase> UserDeatilBases { get; set; }
        public List<MessagesDetail> NotifiMessages { get; set; }
        public int? NotifCount { get; set; }
        public int? MessageCount { get; set; }
        public string VipType { get; set; }
        public string MessageNotifition { get; set; }
    }
}
