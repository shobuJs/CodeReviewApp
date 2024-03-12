using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class NCCActivity
    {
        #region Private Members

        private int _Activity_Type_No = 0;
        private int _Activity_Type = 0;
        private int _UANO = 0;
        private string _IP_Address;
        private int _Activity_id = 0;
        private int _NCC_ration_type = 0;
        private int _IDNO = 0;
        private string _IDNOS;
        private int _BranchNo = 0;
        private int _SemesterNo = 0;
        private int Collegecode = 0;
        private System.Nullable<System.DateTime> _Entery_date;
        private System.Nullable<System.DateTime> _Remove_date;
        private int _campNo;
        private int _nccTypeNo;
        private int _nccRationNo;
        private string _campName;
        private string _Location;
        private DateTime _campFromDate;
        private DateTime _campToDate;
        private decimal _duration;
        private string _nccFilepath;
        private string _nccFilename;
        private int _nccDocsNo;
        private string camp_details;
        private DateTime _remove;
        private DateTime _date;

        #endregion Private Members

        #region Public Property Fields

        public DateTime AddDate
        {
            get { return _date; }
            set { _date = value; }
        }

        public int ActivityNo
        {
            get { return _Activity_Type_No; }
            set { _Activity_Type_No = value; }
        }

        public int ActivityType
        {
            get { return _Activity_Type; }
            set { _Activity_Type = value; }
        }

        public int UANO
        {
            get { return _UANO; }
            set { _UANO = value; }
        }

        public string IP_Address
        {
            get { return _IP_Address; }
            set { _IP_Address = value; }
        }

        public int ActivityID
        {
            get { return _Activity_id; }
            set { _Activity_id = value; }
        }

        public int Ration_Type
        {
            get { return _NCC_ration_type; }
            set { _NCC_ration_type = value; }
        }

        public int Idno
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public string IDNOS
        {
            get { return _IDNOS; }
            set { _IDNOS = value; }
        }

        public int BRANCHNO
        {
            get { return _BranchNo; }
            set { _BranchNo = value; }
        }

        public int SEMNO
        {
            get { return _SemesterNo; }
            set { _SemesterNo = value; }
        }

        public int CCODE
        {
            get { return Collegecode; }
            set { Collegecode = value; }
        }

        public System.Nullable<System.DateTime> ENTRYDATE
        {
            get { return _Entery_date; }
            set { _Entery_date = value; }
        }

        public System.Nullable<System.DateTime> REMOVEDATE
        {
            get { return _Remove_date; }
            set { _Remove_date = value; }
        }

        public int Campno
        {
            get { return _campNo; }
            set { _campNo = value; }
        }

        public int nccTypeNo
        {
            get { return _nccTypeNo; }
            set { _nccTypeNo = value; }
        }

        public int nccRationNo
        {
            get { return _nccRationNo; }
            set { _nccRationNo = value; }
        }

        public DateTime campFromDate
        {
            get { return _campFromDate; }
            set { _campFromDate = value; }
        }

        public DateTime campToDate
        {
            get { return _campToDate; }
            set { _campToDate = value; }
        }

        public string campName
        {
            get { return _campName; }
            set { _campName = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public decimal duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public string nccFilepath
        {
            get { return _nccFilepath; }
            set { _nccFilepath = value; }
        }

        public string nccFilename
        {
            get { return _nccFilename; }
            set { _nccFilename = value; }
        }

        public int nccDocsNo
        {
            get { return _nccDocsNo; }
            set { _nccDocsNo = value; }
        }

        public string Camp_Detail
        {
            get { return camp_details; }
            set { camp_details = value; }
        }

        public DateTime RemoveDate
        {
            get { return _remove; }
            set { _remove = value; }
        }

        #endregion Public Property Fields
    }
}