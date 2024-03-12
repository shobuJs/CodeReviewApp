using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class CertificateMaster
    {
        //general info

        #region Private Member

        private int _certNo = 0;
        private int _idNo = 0;
        private int _issueStatus = 0;
        private int _can = 0;

        private DateTime _issueDate = DateTime.Today;
        private string _ipAddress = string.Empty;
        private string _attendance = string.Empty;
        private string _conduct = string.Empty;
        private DateTime _leavingDate = DateTime.MinValue;

        private int _uaNO = 0;
        private string _collegeCode = string.Empty;
        private int _sessionNo = 0;

        private string _reason = string.Empty;
        private string _remark = string.Empty;
        private int _migrationStatus = 0;
        private int _regStatus = 0;
        private DateTime _migrationDate = DateTime.Today;
        private string _cardNo = string.Empty;
        private int _Migration_org_duplct;

        #endregion Private Member

        #region Public Property Fields

        //public string Rollno { get; set; }
        //public string StudentName { get; set; }
        //public int Degreeno { get; set; }
        //public int Branchno { get; set; }
        //public int Admbatch { get; set; }
        //public string Certificate_No { get; set; }
        //public decimal Cert_Amt { get; set; }

        //Added by Sneha Dobl on 07/04/2021

        public string Rollno { get; set; }
        public string StudentName { get; set; }
        public int Degreeno { get; set; }
        public int Branchno { get; set; }
        public int Admbatch { get; set; }
        public string Certificate_No { get; set; }
        public decimal Cert_Amt { get; set; }
        public int App_Type { get; set; }
        public int App_ID { get; set; }

        public int Migration_org_duplct
        {
            get { return _Migration_org_duplct; }
            set { _Migration_org_duplct = value; }
        }

        public int CertNo
        {
            get { return _certNo; }
            set { _certNo = value; }
        }

        public int IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }

        public int IssueStatus
        {
            get { return _issueStatus; }
            set { _issueStatus = value; }
        }

        public int Can
        {
            get { return _can; }
            set { _can = value; }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        public string Attendance
        {
            get { return _attendance; }
            set { _attendance = value; }
        }

        public string Conduct
        {
            get { return _conduct; }
            set { _conduct = value; }
        }

        public DateTime LeavingDate
        {
            get { return _leavingDate; }
            set { _leavingDate = value; }
        }

        public int SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }

        private int _semesterNo = 0;

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public int UaNO
        {
            get { return _uaNO; }
            set { _uaNO = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public int MigrationStatus
        {
            get { return _migrationStatus; }
            set { _migrationStatus = value; }
        }

        public int RegStatus
        {
            get { return _regStatus; }
            set { _regStatus = value; }
        }

        public DateTime MigrationDate
        {
            get { return _migrationDate; }
            set { _migrationDate = value; }
        }

        public string CardNo
        {
            get { return _cardNo; }
            set { _cardNo = value; }
        }

        #endregion Public Property Fields
    }
}