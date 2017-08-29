using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Dao.Accounts;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.AccountBiz
{
    public class RelationshipManagement
    {
        public void AddRela(long? formId, long? toId)
        {
            RelationshipDao dao = new RelationshipDao();
            dao.Add(new UserRelationship()
            {
                AccountId = formId,
                TargetId = toId,
                Type = 1  //Là bạn
            });
        }

        public void UnFriend(long? formId, long? toId)
        {
            RelationshipDao dao = new RelationshipDao();
            dao.Remove(formId,toId);
        }

       
    }
}
