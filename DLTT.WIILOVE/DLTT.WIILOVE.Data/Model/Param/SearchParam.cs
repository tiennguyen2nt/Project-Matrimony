using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Data.Model.Param
{
    public class SearchParam
    {
        public string TextSearch { get; set; }
        public int? Gender { get; set; }
        public int? FormAge { get; set; }
        public int? ToAge { get; set; }
        public int? City { get; set; }
        public int? Country { get; set; }
        public int? Religion { get; set; }
    }
}
