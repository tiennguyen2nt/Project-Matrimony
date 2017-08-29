using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Data.Model.Param
{
    public class UserDeatilBase
    {
        public long? AccountId { get; set; }
        public string ConnectionId { get; set; }
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string MotherTongue { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string Decription { get; set; }
        public string Message { get; set; }
    }
}
