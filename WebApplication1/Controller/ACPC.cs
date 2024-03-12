using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ACPC
    {
        #region
        private int _sessionNo = 0;
        private string _IdNo = string.Empty;
        private int _semesterN0 = 0;
        private int _degreeNo = 0;
        private string _recieptCode = string.Empty;
        private string _demandNo = string.Empty;
        private bool _isCancelled = true;
        private bool _isReconciled = false;
        private string _Branch = string.Empty;
        private string _remark = string.Empty;
        private DateTime challanDate = DateTime.MinValue;
        private DateTime printDate = DateTime.MinValue;
        private string _amount = string.Empty;
        private int _uano = 0;
        #endregion

        #region

        public int Uano
        {
            get { return _uano; }
            set { _uano = value; }
        }

        public int SessionN0
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string IdNo
        {
            get { return _IdNo; }
            set { _IdNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterN0; }
            set { _semesterN0 = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public string RecieptCode
        {
            get { return _recieptCode; }
            set { _recieptCode = value; }
        }

        public string DemandNo
        {
            get { return _demandNo; }
            set { _demandNo = value; }
        }

        public bool IsCanelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        public bool IsReconciled
        {
            get { return _isReconciled; }
            set { _isReconciled = value; }
        }

        public DateTime ChallanDate
        {
            get { return challanDate; }
            set { challanDate = value; }
        }

        public DateTime PrintDate
        {
            get { return printDate; }
            set { printDate = value; }
        }

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion
    }
}