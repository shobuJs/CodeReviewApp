using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Config
    {
        private double _Fees_GEN = 0.00;
        private double _Fees_SCST = 0.00;
        private string _College_ids = string.Empty;

        private string _suser = string.Empty;

        public string suser
        {
            get { return _suser; }
            set { _suser = value; }
        }

        public double Fees_GEN
        {
            get { return _Fees_GEN; }
            set { _Fees_GEN = value; }
        }

        public double Fees_SCST
        {
            get { return _Fees_SCST; }
            set { _Fees_SCST = value; }
        }

        //Added by swapnil thakare on dated 20-09-2021
        public string College_ids
        {
            get { return _College_ids; }
            set { _College_ids = value; }
        }

        #region Private Member

        private int _configNo = 0;

        // private int _configNo = 0;

        private string _eventName = string.Empty;
        private int _eventNo = 0;
        private string _status = string.Empty;
        private string _collegeCode = string.Empty;
        private int _ConfigID = 0;
        private int _SessionNo = 0;
        private int _degree_no = 0;
        private string _departNos = string.Empty;
        private int _branchNo = 0;
        private int _Intake = 0;
        private int _SC = 0;
        private int _ST = 0;
        private int _GEN = 0;
        private int _OBC = 0;
        private int _Cnfno = 0;
        private string _Details = string.Empty;
        private DateTime _Config_sdate = DateTime.Now;
        private DateTime _Config_stime = DateTime.Now;
        private DateTime _Config_edate = DateTime.Now;
        private DateTime _Config_etime = DateTime.Now;
        private double _Fees = 0;
        private int _Admbatch = 0;
        private string _DegreeNoS = string.Empty;
        private string _BranchNoS = string.Empty;
        private string _FeeS = string.Empty;
        private string _ConfigIDS = string.Empty;
        private string _StartDate = string.Empty;
        private string _EndDate = string.Empty;
        private string _chkWeek = string.Empty;
        private int _UGPG = 0;
        private int _Month = 0;
        private int _College_id = 0;
        private string _CampusNos = string.Empty;
        private string _Intakes = string.Empty;
        private int _Uano = 0;
        private string _ExamDate = string.Empty;
        private string _Mode = string.Empty;
        private string _Medium = string.Empty;

        #endregion Private Member

        #region Private Property Fields

        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public string Medium
        {
            get { return _Medium; }
            set { _Medium = value; }
        }

        public string ExamDate
        {
            get { return _ExamDate; }
            set { _ExamDate = value; }
        }

        public int ConfigNo
        {
            get { return _configNo; }
            set { _configNo = value; }
        }

        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        public int Uano
        {
            get { return _Uano; }
            set { _Uano = value; }
        }

        public int UGPG
        {
            get { return _UGPG; }
            set { _UGPG = value; }
        }

        public int College_id
        {
            get { return _College_id; }
            set { _College_id = value; }
        }

        public int Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        public string CampusNos
        {
            get { return _CampusNos; }
            set { _CampusNos = value; }
        }

        public string Intakes
        {
            get { return _Intakes; }
            set { _Intakes = value; }
        }

        public string chkWeek
        {
            get { return _chkWeek; }
            set { _chkWeek = value; }
        }

        public string ConfigIDS
        {
            get { return _ConfigIDS; }
            set { _ConfigIDS = value; }
        }

        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public string FeeS
        {
            get { return _FeeS; }
            set { _FeeS = value; }
        }

        public string BranchNoS
        {
            get { return _BranchNoS; }
            set { _BranchNoS = value; }
        }

        public string DegreeNoS
        {
            get { return _DegreeNoS; }
            set { _DegreeNoS = value; }
        }

        public int EventNo
        {
            get { return _eventNo; }
            set { _eventNo = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int ConfigID
        {
            get { return _ConfigID; }
            set { _ConfigID = value; }
        }

        public string DepartNos
        {
            get { return _departNos; }
            set { _departNos = value; }
        }

        public int SessionNo
        {
            get { return _SessionNo; }
            set { _SessionNo = value; }
        }

        public int Degree_No
        {
            get { return _degree_no; }
            set { _degree_no = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int Intake
        {
            get { return _Intake; }
            set { _Intake = value; }
        }

        public int SCQuota
        {
            get { return _SC; }
            set { _SC = value; }
        }

        public int STQuota
        {
            get { return _ST; }
            set { _ST = value; }
        }

        public int GENQuota
        {
            get { return _GEN; }
            set { _GEN = value; }
        }

        public int OBCQuota
        {
            get { return _OBC; }
            set { _OBC = value; }
        }

        public int Cnfno
        {
            get { return _Cnfno; }
            set { _Cnfno = value; }
        }

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }

        public DateTime Config_SDate
        {
            get { return _Config_sdate; }
            set { _Config_sdate = value; }
        }

        public DateTime Config_STime
        {
            get { return _Config_stime; }
            set { _Config_stime = value; }
        }

        public DateTime Config_EDate
        {
            get { return _Config_edate; }
            set { _Config_edate = value; }
        }

        public DateTime Config_ETime
        {
            get { return _Config_etime; }
            set { _Config_etime = value; }
        }

        public double Fees
        {
            get { return _Fees; }
            set { _Fees = value; }
        }

        public int Admbatch
        {
            get { return _Admbatch; }
            set { _Admbatch = value; }
        }

        #endregion Private Property Fields
    }
}