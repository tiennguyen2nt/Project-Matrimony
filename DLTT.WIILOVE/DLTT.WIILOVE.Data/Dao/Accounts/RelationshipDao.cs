using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Accounts
{
    public class RelationshipDao
    {
        public void Add(UserRelationship rela)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserRelationships.Add(rela);
                db.SaveChanges();
            }
        }

        public void Remove(UserRelationship rela)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserRelationships.Remove(rela);
                db.SaveChanges();
            }
        }
        public void Remove(long? id1, long? id2)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.UserRelationships.Where(x => (x.TargetId == id1 && x.AccountId == id2) ||
                                                          (x.TargetId == id2 && x.AccountId == id1));
                db.UserRelationships.RemoveRange(item);
                db.SaveChanges();
            }
        }

        public bool CheckExistRela(long? id1, long? id2)
        {
            using (var db = new WiiloveEntities())
            {
                var num = db.UserRelationships.Count(x => ((x.TargetId == id1 && x.AccountId == id2) ||
                                                         (x.TargetId == id2 && x.AccountId == id1)) && x.Type == 1) ;
                return num > 0;
            }
        }

        public IList<UserRelationship> GetIdIsFriends(long? currentId)
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserRelationships
                    .Where(x => x.Type == 1 && (x.AccountId == currentId || x.TargetId == currentId)).ToList();
            }
        }
    }
}
