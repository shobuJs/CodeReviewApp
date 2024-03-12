using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class CommunicationTrEntity
    {
        public int CommandType { get; set; }
        public int CheckStatus { get; set; }
        public int easID { get; set; }
        public int objEvent { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string scheTime { get; set; }
        public int status { get; set; }
        public int fetchDynaStatus { get; set; }
        public string Mail { get; set; }
        public string Tomail { get; set; }
        public string CCmail { get; set; }
        public string BCCmail { get; set; }
        public int acticityID { get; set; }
        public string activityName { get; set; }
        public int userNo { get; set; }
        public int eyeEmailID { get; set; }
    }
}
