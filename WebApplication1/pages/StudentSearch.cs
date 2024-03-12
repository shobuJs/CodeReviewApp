//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT SEARCH CLASS
// CREATION DATE : 30-JUN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentSearch
    {
        private string _searchText = string.Empty;
        private string _searchField = string.Empty;
        private string _orderByField = string.Empty;

        private string _studentName = string.Empty;
        private string _enrollmentNo = string.Empty;
        private int _degreeNo = 0;
        private int _branchNo = 0;
        private int _yearNo = 0;
        private int _semesterNo = 0;
        private string _idNo = string.Empty;
        private string _srno = string.Empty;
        private string _tanno = string.Empty;
        private string _regno = string.Empty;

        public string Tanno
        {
            get { return _tanno; }
            set { _tanno = value; }
        }

        public string Regno
        {
            get { return _regno; }
            set { _regno = value; }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        public string SearchField
        {
            get { return _searchField; }
            set { _searchField = value; }
        }

        public string OrderByField
        {
            get { return _orderByField; }
            set { _orderByField = value; }
        }

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string EnrollmentNo
        {
            get { return _enrollmentNo; }
            set { _enrollmentNo = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int YearNo
        {
            get { return _yearNo; }
            set { _yearNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public string IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }

        public string Srno
        {
            get { return _srno; }
            set { _srno = value; }
        }
    }
}