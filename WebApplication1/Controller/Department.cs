namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Department
    {
        #region Private Member

        private int _deptNo = 0;
        private string _deptName = string.Empty;
        private string _deptCode = string.Empty;
        private int _hodUaNo = 0;
        private string _joinDate = string.Empty;
        private string _collegeCode = string.Empty;

        #endregion Private Member

        #region Private Property Fields

        public int DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
        }

        public int HodUaNo
        {
            get { return _hodUaNo; }
            set { _hodUaNo = value; }
        }

        public string JoinDate
        {
            get { return _joinDate; }
            set { _joinDate = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        #endregion Private Property Fields
    }
}