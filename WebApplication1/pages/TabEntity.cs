using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class TabEntity
    {
        public int tabNo { get; set; }
        public string tabName { get; set; }
        public int status { get; set; }
        public int commandType { get; set; }
        public int CheckStatus { get; set; }
        public int userNo { get; set; }
        public string userName { get; set; }
        public string createdDate { get; set; }
        public int tempValue { get; set; }
    }
}
