using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class SpecialRequestConfiguration
    {
        public class SpecialRequestConfig
        {
            public int SPCL_REQUEST_CONFIGID { get; set; }
            public int ACADEMIC_SESSION { get; set; }
            public string ACADEMIC_NAME { get; set; }
            public string SEMESTERNAME { get; set; }
            public int SEMESTERNO { get; set; }
            public int MIN_NO_OF_STUDENTS { get; set; }
            public string STARTDATE { get; set; }
            public string ENDDATE { get; set; }
            public string IPADDRESS { get; set; }
            public int USERNO { get; set; }
        }
        public class BindDropDown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}