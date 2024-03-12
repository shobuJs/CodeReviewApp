using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Installment
    {
        private string _StudentName = string.Empty;
        private int _IDNO;
        private int _ReciptNo;
        private byte _Install_Status;
        private int _NoOfInstallment;
        private string _Install_Status_YN = string.Empty;
        private string _CollegeCode = string.Empty;
        private string _ReciptType = string.Empty;
        private int _RegNo;
        private int _UANO;
        private DateTime _TransDate;
        private string _IP_Address;
        private int _SessionNo;

        public int SessionNo
        {
            get { return _SessionNo; }
            set { _SessionNo = value; }
        }

        public string IP_Address
        {
            get { return _IP_Address; }
            set { _IP_Address = value; }
        }

        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }

        public int UANO
        {
            get { return _UANO; }
            set { _UANO = value; }
        }

        public int RegNo
        {
            get { return _RegNo; }
            set { _RegNo = value; }
        }

        public string ReciptType
        {
            get { return _ReciptType; }
            set { _ReciptType = value; }
        }

        public string CollegeCode
        {
            get { return _CollegeCode; }
            set { _CollegeCode = value; }
        }

        public string Install_Status_YN
        {
            get { return _Install_Status_YN; }
            set { _Install_Status_YN = value; }
        }

        public int NoOfInstallment
        {
            get { return _NoOfInstallment; }
            set { _NoOfInstallment = value; }
        }

        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        public int IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public int ReciptNo
        {
            get { return _ReciptNo; }
            set { _ReciptNo = value; }
        }

        public byte Install_Status
        {
            get { return _Install_Status; }
            set { _Install_Status = value; }
        }
    }
}