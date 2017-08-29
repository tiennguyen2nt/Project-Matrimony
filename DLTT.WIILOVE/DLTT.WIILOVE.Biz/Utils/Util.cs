using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Dao.SystemParameters;
using DLTT.WIILOVE.Data.Dao.UserDetails;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Biz.Utils
{
    public class Util
    {
        public static string URL_BACK = "";
        public enum TypeNotif
        {
            AddFriend = 1,
            Like = 2,
            ConfirmFriend
        }
        public class Feature
        {
            public const int Country = 1000;
            public const int City = 1001;
            public const int Language = 1002;
            public const int Religion = 1003;
            public const int Caste = 1004;

        }
        public class Gender
        {
            public const int Male = 1;
            public const int Female = 0;
            public const int Other = 22;
        }

        public class MessageType
        {
            public const int TextMessage = 1;
            public const int PictureMessage = 2;
            public const int FileMessage = 3;
        }


        public class MemberVip
        {
            public const string NotVip = "NotVip";
            public const string Free = "Free";
            public const string Vip = "Vip";
            public const string Base = "Base";
            public const string Pro = "Pro";
        }

        public  class  Email
        {
            public const string SUCCESSREGISTER = "ĐĂNG KỲ TÀI KHOẢN THÀNH CÔNG";
            public const string LOGIN = "THÔNG BÁO ĐĂNG NHẶP";
            public  class Content
            {
                public const string RegisterSuccessfuly = @"";
            }
            
        }

        public class IsFriend
        {
            public const int IsMe = 0; //Là chính mình
            public const int NotifTo = 1;
            public const int NotifForm = 2;
            public const int IsFriendly = 3;
            public const int Nothing = 22;
        }

        public class DonateCode
        {
            public const string Code30 = "5b20c32e73c6339d627d2ab4f0e54227";
            public const string Code50 = "4dc85f6e5389fe9c22113b65967e667b";
        }
        #region Function

        public static string HasAvatar(string avatar,int? gender = null)
        {
            if (avatar != null)
                return avatar;
            else
            {
                if (gender == Util.Gender.Male)
                    return "avatarMan.jpg";
                else if (gender == Util.Gender.Female)
                    return "avatarWoman.jpg";
                return "avatarDefaul2.png";
            }
            
        }

        public static string GetFullName(UserDetail user)
        {
            return (user.LastName == null ? "" : (user.LastName + ' ')) + ((user.MiddleName == null) ? "" : user.MiddleName + ' ') + user.FirstName;
        }

        public static string GetAvatarById(long? id)
        {
            UserDetailDao userDao = new UserDetailDao();
            var item = userDao.GetByAccountId(id);
            return HasAvatar(item.AvatarURL,item.Gender);
        }


        public static string TimeSend(DateTime? date)
        {
            string re = "";
            if (date != null)
            {
                if (date.Value.AddDays(30) < DateTime.Today)
                {
                    return string.Format("{0:d}", date);
                }
                else
                {
                    var interval = DateTime.Now - date.Value;
                    var nSec = interval.Days * 24 * 60 * 60  +
                                 interval.Hours * 60 * 60  +
                                 interval.Minutes * 60  +
                                 interval.Seconds;
                    if (nSec < 60)
                    {
                        re = nSec + " giây trước";
                    }else if (nSec < 60 * 60)
                    {
                        re = nSec / 60 + " phút trước";
                    }else if (nSec < 60 * 60 * 24)
                    {
                        re = nSec / 60 / 60 + " giờ trước";
                    }
                    else
                    {
                        re = nSec / 60 / 60 /24 + " ngày trước";
                    }
                }


            }
            return re;
        }

        public static List<SystemParameter> GetListSystemParameters(int? featureId)
        {
            SystemParameterDao dao = new SystemParameterDao();
            return dao.GetSystemParametersByFeatureId(featureId).ToList();
        }
        #endregion
    }
}
