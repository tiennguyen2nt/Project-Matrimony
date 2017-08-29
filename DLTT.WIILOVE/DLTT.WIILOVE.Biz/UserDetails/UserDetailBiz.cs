using System;
using System.Collections.Generic;
using System.Linq;
using DLTT.WIILOVE.Biz.Utils;
using DLTT.WIILOVE.Data.Dao.Accounts;
using DLTT.WIILOVE.Data.Dao.Payments;
using DLTT.WIILOVE.Data.Dao.SystemParameters;
using DLTT.WIILOVE.Data.Dao.UserDetails;
using DLTT.WIILOVE.Data.Model;
using DLTT.WIILOVE.Data.Model.Param;

namespace DLTT.WIILOVE.Biz.UserDetails
{
    public class UserDetailBiz
    {
        public List<UserDeatilBase> Get20DetailBase()
        {
            UserDetailDao dao = new UserDetailDao();


            return SetValueUser(dao.ViewDetailBases());
        }

        public UserDetail GetById(long? id, long? currentId)
        {
            UserDetailDao dao = new UserDetailDao();
            var item = dao.GetByAccountId(id);
            SystemParameterDao sysDao = new SystemParameterDao();
            var sys = sysDao.GetList();
            var motherTongue = sys.FirstOrDefault(x => x.Id == item.MotherTongueId);
            if (motherTongue != null)
                item.MotherTongue = motherTongue.Name;
            var currentCity = sys.FirstOrDefault(x => x.Id == item.CurrentCityId);
            if (currentCity != null)
                item.CurrentCity = currentCity.Name;
            var country = sys.FirstOrDefault(x => x.Id == item.CurrentCountryId);
            if (country != null)
                item.Country = country.Name;
            var religion = sys.FirstOrDefault(x => x.Id == item.ReligionId);
            if (religion != null)
                item.Religion = religion.Name;
            item.isFriend = Util.IsFriend.Nothing;
            if (id != currentId)
            {
                NotificationDao notifDao = new NotificationDao();
                if (notifDao.CheckNotfi(id, currentId))
                {
                    item.isFriend = Util.IsFriend.NotifTo;
                }
                if (notifDao.CheckNotfi(currentId, id))
                {
                    item.isFriend = Util.IsFriend.NotifForm;
                }
                RelationshipDao relaDao = new RelationshipDao();
                if (relaDao.CheckExistRela(id, currentId))
                    item.isFriend = Util.IsFriend.IsFriendly;
                UserRankingDao rankingDao = new UserRankingDao();
                var rank = rankingDao.GetByAccountId(currentId);
                if (rank.ExpiryDate > DateTime.Now)
                {
                    item.ExpiryDate = rank.ExpiryDate;
                }
            }
            else
            {
                item.isFriend = Util.IsFriend.IsMe;
                UserRankingDao rankingDao = new UserRankingDao();
                var rank = rankingDao.GetByAccountId(id);
                if (rank == null || rank.ExpiryDate == null || rank.ExpiryDate < DateTime.Now)
                {
                    item.VipType = Util.MemberVip.NotVip;
                }
                else if (rank.ExpiryDate > DateTime.Now)
                {
                    item.ExpiryDate = rank.ExpiryDate;
                }
            }
            
            item.AvatarURL = Util.HasAvatar(item.AvatarURL, item.Gender);
            return item;
        }

        public List<UserDeatilBase> SearchBase(SearchParam param, long? id = null)
        {
            return SetValueUser(Search(param, id));
        }
        private List<UserDetail> Search(SearchParam param, long? id)
        {
            UserDetailDao dao = new UserDetailDao();
            List<UserDetail> resulsList = new List<UserDetail>();
            var list = dao.Search(param);
            RelationshipDao daoRel = new RelationshipDao();
            foreach (var item in daoRel.GetIdIsFriends(id))
            {
                var q = list.FirstOrDefault(x => x.AccountId == item.AccountId || x.AccountId == item.TargetId);
                if (q != null)
                    list.Remove(q);
            }
            foreach (var item in list)
            {
                if (CompareBirth(item.DateOfBirth, param.FormAge, param.ToAge) && (param.TextSearch == null || (item.LastName + ' ' + item.MiddleName + (item.MiddleName == null ? "" : " ") + item.FirstName).Contains(param.TextSearch)))
                {
                    resulsList.Add(item);
                }
            }
            return resulsList.ToList();
        }

        private int CompareBirth(DateTime birth)
        {
            int year = DateTime.Now.Year - birth.Year;
            if (DateTime.Now.Month < birth.Month)
                year -= 1;
            return year;
        }
        private bool CompareBirth(DateTime? birth, int? formAge, int? toAge)
        {
            if (formAge == null && toAge == null) return true;
            if (birth == null) return false;
            int year = DateTime.Now.Year - birth.Value.Year;
            if (DateTime.Now.Month < birth.Value.Month)
                year -= 1;
            if (formAge == null)
            {
                return year <= toAge;
            }
            else if (toAge == null)
                return year >= formAge;
            else
            {
                return year <= toAge && year >= formAge;
            }
        }
        public void UpdateAvatar(UserDetail u)
        {
            UserDetailDao dao = new UserDetailDao();
            dao.EditAvatarUrl(u);
        }

        public List<UserDeatilBase> GetListUser(string find)
        {
            AccountDao accDao = new AccountDao();
            var id = accDao.GetAccount(find).ID;
            return GetListUser(id);
        }

        public List<UserDeatilBase> GetListUser(long? id)
        {
            UserDetailDao dao = new UserDetailDao();
            var user = dao.GetByAccountId(id);
            var items = dao.GetAllUser(user);
            foreach (var item in items)
            {
                if (user.Gender != null)
                {
                    if (item.Gender != user.Gender)
                        item.Point += 500;
                }
                else
                {
                    item.Point -= 70;
                }

                if (user.CurrentCountryId != null)
                    if (item.CurrentCountryId == user.CurrentCountryId)
                        item.Point += 200;
                if (user.CurrentCityId != null)
                    if (item.CurrentCityId == user.CurrentCityId)
                        item.Point += 300;
                if (user.MotherTongueId != null)
                    if (item.MotherTongueId == user.MotherTongueId)
                        item.Point += 100;
                if (user.CasteId != null)
                    if (item.CasteId == user.CasteId)
                        item.Point += 150;
                if (item.AvatarURL == null)
                    item.Point -= 100;
                if (user.DateOfBirth != null)
                {
                    if (item.DateOfBirth != null)
                    {
                        if (item.DateOfBirth.Value.AddYears(6) > user.DateOfBirth &&
                            item.DateOfBirth.Value.AddYears(-6) < user.DateOfBirth)
                            item.Point += 500;
                    }
                    else
                    {
                        item.Point -= 100;
                    }
                }

            }
            var result = items.Where(x => x.Point > 950).OrderByDescending(x => x.Point).Take(50).ToList();
            return SetValueUser(result, id);
        }

        public List<UserDeatilBase> GetListFriend(long? id)
        {
            UserDetailDao dao = new UserDetailDao();
            return SetValueUser(dao.GetListFriends(id));
        }

        private List<UserDeatilBase> SetValueUser(List<UserDetail> items, long? id = null)
        {
            var list = new List<UserDeatilBase>();
            SystemParameterDao sysDao = new SystemParameterDao();
            var notifDao = new NotificationDao();

            var sys = sysDao.GetList();
            foreach (var item in items)
            {
                if (notifDao.CheckNotifExist(item.AccountId, id))
                {
                    UserDeatilBase uBase = new UserDeatilBase();
                    uBase.AccountId = item.AccountId;
                    uBase.FirstName = item.FirstName;
                    uBase.FullName = Util.GetFullName(item);
                    if (item.Gender == 1)
                        uBase.Gender = "Nam";
                    else if (item.Gender == 0)
                        uBase.Gender = "Nữ";
                    else
                    {
                        uBase.Gender = "Không rõ";
                    }
                    if (item.DateOfBirth != null) uBase.Age = CompareBirth(item.DateOfBirth.Value);
                    uBase.AvatarUrl = Util.HasAvatar(item.AvatarURL, item.Gender);
                    var currentCity = sys.FirstOrDefault(x => x.Id == item.CurrentCityId);
                    if (currentCity != null)
                        uBase.City = currentCity.Name;
                    var country = sys.FirstOrDefault(x => x.Id == item.CurrentCountryId);
                    if (country != null)
                        uBase.Country = country.Name;
                    var caste = sys.FirstOrDefault(x => x.Id == item.CasteId);
                    if (caste != null)
                        uBase.Country = caste.Name;
                    var religion = sys.FirstOrDefault(x => x.Id == item.ReligionId);
                    if (religion != null)
                        uBase.Religion = religion.Name;
                    var motherTongue = sys.FirstOrDefault(x => x.Id == item.MotherTongueId);
                    if (motherTongue != null)
                        uBase.MotherTongue = motherTongue.Name;

                    list.Add(uBase);
                }
            }
            return list;
        }

        public void UpdateProfile(UserDetail u,long id)
        {
            UserDetailDao dao = new UserDetailDao();
            u.AccountId = id;
            u.DateOfBirth = new DateTime(u.Year,u.Month,u.Day);
            dao.Edit(u);

        }

        /*
         * Các field thêm vào trong Object UserDetail
         

         #region Other
        public string MotherTongue { get; set; }
        public string CurrentCity { get; set; }
        public string Country { get; set; }
        public string Religion { get; set; }
        public int Point { get; set; }
        public int isFriend { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string VipType { get; set; }
        #endregion

         */
    }
}
