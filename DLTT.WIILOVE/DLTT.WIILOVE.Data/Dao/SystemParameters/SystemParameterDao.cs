using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLTT.WIILOVE.Data.Model;

namespace DLTT.WIILOVE.Data.Dao.SystemParameters
{
    public class SystemParameterDao
    {
        public IList<SystemParameter> GetList()
        {
            using (var db = new WiiloveEntities())
            {
                return db.SystemParameters.ToList();
            }
        }

        public IList<SystemParameter> GetSystemParametersByFeatureId(int? featureId)
        {
            using (var db = new WiiloveEntities())
            {
                return db.SystemParameters.Where(x => x.FeatureID == featureId).ToList();
            }
        }
    }
}
