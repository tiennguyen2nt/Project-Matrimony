using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.Accounts
{
    public class AccountDao
    {
        public bool Login(string username, string password)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Accounts.FirstOrDefault(x => (x.UserName.Equals(username) || x.Email.Equals(username) || x.PhoneNumber.Equals(username)) && x.Password.Equals(password));
                if (query != null)
                    return true;
                return false;
            }
        }

        public void Add(Account account)
        {
            using (var db = new WiiloveEntities())
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }

        public Account GetAccount(string find)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Accounts.FirstOrDefault(
                    x => x.UserName.Equals(find) || x.Email.Equals(find) || x.PhoneNumber.Equals(find));
                return query;
            }
        }

        public bool CheckEmail(string email)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Accounts.Count(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                return query > 0;
            }
        }

        public bool CheckPhoneNumber(string phone)
        {
            using (var db = new WiiloveEntities())
            {
                var query = db.Accounts.Count(x => x.PhoneNumber.Equals(phone));
                return query > 0;
            }
        }
    }
}
