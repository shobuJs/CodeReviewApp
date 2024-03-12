namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class College
    {
        #region private members

        //aashna//
        private int _University;

        private int _InstituteType;
        private string _Location;
        private int _State;
        private byte[] _uploadLogo = null;
        // end

        private int _COLLEGE_ID;
        private int _centerno = 0;

        private string _collegeCode;

        private int _Capacity;

        private string _name;

        private string _address;

        private string _contactPersonName;

        private int _personDesignation;

        private string _personContactNo;

        private string _collegeType;

        private string _degreeOffered;

        private string _departmentOffered;
        private string _branchOffered;
        private string _ShortName;

        private string _College_Address;
        private string _College_Logo;

        // Added by swapnil thakare on dated 12-07-2021
        private int _campusNo = 0;

        private string _email;
        private string _mobile;
        private int _active = 0;

        #endregion private members

        #region public members

        //aashna//
        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }

        public bool ActiveStatus
        {
            get;
            set;
        }

        public int OrgId
        {
            get;
            set;
        }

        public int University
        {
            get
            {
                return _University;
            }
            set
            {
                _University = value;
            }
        }

        public int State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        public int InstituteType
        {
            get
            {
                return _InstituteType;
            }
            set
            {
                _InstituteType = value;
            }
        }

        public byte[] UploadLogo
        {
            get
            {
                return _uploadLogo;
            }
            set
            {
                _uploadLogo = value;
            }
        }

        //end//

        public int COLLEGE_ID
        {
            get { return _COLLEGE_ID; }
            set { _COLLEGE_ID = value; }
        }

        public int Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set { _contactPersonName = value; }
        }

        public int PersonDesignation
        {
            get { return _personDesignation; }
            set { _personDesignation = value; }
        }

        public string PersonContactNo
        {
            get { return _personContactNo; }
            set { _personContactNo = value; }
        }

        public string Collegetype
        {
            get { return _collegeType; }
            set { _collegeType = value; }
        }

        public string DegreeOffered
        {
            get { return _degreeOffered; }
            set { _degreeOffered = value; }
        }

        public string DepartmentOffered
        {
            get { return _departmentOffered; }
            set { _departmentOffered = value; }
        }

        public string BranchOffered
        {
            get { return _branchOffered; }
            set { _branchOffered = value; }
        }

        public string Short_Name
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }

        public string College_Address
        {
            get { return _College_Address; }
            set { _College_Address = value; }
        }

        public string College_Logo
        {
            get { return _College_Logo; }
            set { _College_Logo = value; }
        }

        // Added by swapnil thakare on dated 12-07-2021
        public int CampusNo
        {
            get { return _campusNo; }
            set { _campusNo = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        public int Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public int CenterNo
        {
            get { return _centerno; }
            set { _centerno = value; }
        }

        #endregion public members
    }
}