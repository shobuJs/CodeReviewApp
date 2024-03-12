using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class PStaff
    {
        #region Private Member

        private int _PStaffNo = 0;
        private string _title = string.Empty;
        private string _PStaffCode = string.Empty;
        private string _PStaffName = string.Empty;
        private string _PStaffAddress = string.Empty;
        private string _contactno = string.Empty;
        private string _emailid = string.Empty;
        private int _desigNo = 0;
        private int _deptNo = 0;
        private int _instituteNo = 0;
        private string _internal_external = "I";
        private DateTime _dob = DateTime.MinValue;
        private int _staffno = 0;   //added by reena on 21/10/16
        private int _supervisorno = 0;
        private string _supervisorname = string.Empty;
        private int _type = 0;
        private string _typename = string.Empty;
        private int _sessionno = 0;
        private int _drcno = 0;
        private string _drcname = string.Empty;
        public int _uno = 0;
        private string _Remuname = string.Empty;
        public int _Remuno = 0;
        private string _Idnos = string.Empty;
        private string _InstitutionName = string.Empty;

        public string InstitutionName
        {
            get { return _InstitutionName; }
            set { _InstitutionName = value; }
        }

        public string Idnos
        {
            get { return _Idnos; }
            set { _Idnos = value; }
        }

        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        private string _Specialization = string.Empty;  // add specialization  on  572019

        public string Specialization
        {
            get { return _Specialization; }
            set { _Specialization = value; }
        }

        private string _qualification = string.Empty;

        public string Qualification
        {
            get { return _qualification; }
            set { _qualification = value; }
        }

        private string _indl_exp = string.Empty;

        public string Indl_exp
        {
            get { return _indl_exp; }
            set { _indl_exp = value; }
        }

        private string _teach_exp = string.Empty;

        public string Teach_exp
        {
            get { return _teach_exp; }
            set { _teach_exp = value; }
        }

        private string _collegeCode = string.Empty;

        #endregion Private Member

        #region Public Property Fields

        public int PStaffNo
        {
            get { return _PStaffNo; }
            set { _PStaffNo = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string PStaffCode
        {
            get { return _PStaffCode; }
            set { _PStaffCode = value; }
        }

        public string PStaffName
        {
            get { return _PStaffName; }
            set { _PStaffName = value; }
        }

        public string PStaffAddress
        {
            get { return _PStaffAddress; }
            set { _PStaffAddress = value; }
        }

        public string Contactno
        {
            get { return _contactno; }
            set { _contactno = value; }
        }

        public string Emailid
        {
            get { return _emailid; }
            set { _emailid = value; }
        }

        public int DesigNo
        {
            get { return _desigNo; }
            set { _desigNo = value; }
        }

        public int DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        public int InstituteNo
        {
            get { return _instituteNo; }
            set { _instituteNo = value; }
        }

        public string Internal_External
        {
            get { return _internal_external; }
            set { _internal_external = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string SupervisorName
        {
            get { return _supervisorname; }
            set { _supervisorname = value; }
        }

        public int SupervisorNo
        {
            get { return _supervisorno; }
            set { _supervisorno = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string TypeName
        {
            get { return _typename; }
            set { _typename = value; }
        }

        public string DRCName
        {
            get { return _drcname; }
            set { _drcname = value; }
        }

        public int DRCNo
        {
            get { return _drcno; }
            set { _drcno = value; }
        }

        public int Sessionno                     //added by reena
        {
            get { return _sessionno; }
            set { _sessionno = value; }
        }

        public int staffno                    //added by reena
        {
            get { return _staffno; }
            set { _staffno = value; }
        }

        public int Uno                    //added by Aayushi
        {
            get { return _uno; }
            set { _uno = value; }
        }

        public int Remuno            // added by dipali
        {
            get { return _Remuno; }
            set { _Remuno = value; }
        }

        public string Remuname
        {
            get { return _Remuname; }
            set { _Remuname = value; }
        }

        #endregion Public Property Fields
    }
}