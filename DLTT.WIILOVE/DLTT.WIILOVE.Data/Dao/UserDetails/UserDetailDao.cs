using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Data.Dao.UserDetails
{
    public class UserDetailDao
    {
        public List<UserDetail> ViewDetailBases(int count = 20)
        {
            using (var db = new WiiloveEntities())
            {
                var items = db.UserDetails.OrderBy(x => Guid.NewGuid()).Take(count);
                return items.ToList();
            }
        }

        public List<UserDetail> GetListByNotif(long? id)
        {
            var sql = @"select enUser.* from UserDetails enUser
inner join Notification enNotifTo on  enNotifTo.formId = enUser.AccountId
where enNotifTo.ToId = @p0  Order by enNotifTo.CreatedDate desc";
            using (var db = new WiiloveEntities())
            {
                var items = db.UserDetails.SqlQuery(sql, id);
                return items.ToList();
            }
        }

        public List<UserDetail> GetListFriends(long? id)
        {
            var sql = @"select enDe.* from UserDetails enDe 
                        where 
	                        enDe.AccountId in 
		                        (select enRel.TargetId 
		                        from UserRelationship enRel 
		                        where enRel.Type =1 
			                        and enRel.AccountId = @p0)
	                        or enDe.AccountId in (select enRel.AccountId 
		                        from UserRelationship enRel 
		                        where enRel.Type =1 
			                        and enRel.TargetId = @p0)";
            using (var db = new WiiloveEntities())
            {
                var items = db.UserDetails.SqlQuery(sql, id);
                return items.ToList();
            }
        }

        public UserDetail GetByAccountId(long? Id)
        {
            using (var db = new WiiloveEntities())
            {
                var item = db.UserDetails.FirstOrDefault(x => x.AccountId == Id);
                return item;
            }
        }

        public void Add(UserDetail u)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserDetails.Add(u);
                db.SaveChanges();
            }
        }


        public List<UserDetail> Search(SearchParam param)
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserDetails.Where(x => (x.Gender == param.Gender || param.Gender == null)  && (x.ReligionId == param.Religion || param.Religion == null) && (x.CurrentCityId == param.City || param.City == null) && (param.Country == null || param.Country == x.CurrentCountryId)).ToList();
            }

        }
       


        public List<UserDetail> SearchByName(string text)
        {
            using (var db = new WiiloveEntities())
            {
                return db.UserDetails.Where(x => (x.LastName + ' ' + x.MiddleName + (x.MiddleName == null ? "" : " ") + x.FirstName).Contains(text)).ToList();
            }
        }

        public List<UserDetail> GetAllUser(UserDetail user)
        {
            using (var db = new WiiloveEntities())
            {
                var items = db.UserDetails.Where(x => x.AccountId != user.AccountId);
                return items.ToList();
            }
        }

        public void EditAvatarUrl(UserDetail u)
        {
            using (var db = new WiiloveEntities())
            {
                db.UserDetails.Attach(u);
                var entry = db.Entry(u);
                entry.Property(x => x.AvatarURL).IsModified = true;
                db.SaveChanges();
            }
        }
        public void Edit(UserDetail u)
        {
            using (var db = new WiiloveEntities())
            {
               var item =  db.UserDetails.FirstOrDefault(x => x.AccountId == u.AccountId);
                item.About = u.About;
                item.DateOfBirth = u.DateOfBirth;
                item.Height = u.Height;
                item.Weight = u.Weight;
                item.Gender = u.Gender;
                item.CurrentCityId = u.CurrentCityId;
                item.CurrentCountryId = u.CurrentCountryId;
                item.ReligionId = u.ReligionId;
                item.MotherTongueId = u.MotherTongueId;
                db.SaveChanges();
            }
        }

    }
}
