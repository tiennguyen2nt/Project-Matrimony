using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Data.Model.Param
{
   public class MessageDetailParam
    {
        public List<MessagesDetail> MessagesDetails { get; set; }
        public long? MessageId {get; set; }
        public long? SendId { get; set; }
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public string ProfileURL { get; set; }
    }
}
