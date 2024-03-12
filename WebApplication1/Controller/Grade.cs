using System;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Grade
    {
        #region Private Member

        private int _gradeType = 0;
        private string _gradeName = string.Empty;
        private string _collegeCode = string.Empty;

        private int _sr_no = 0;
        private int _college_id = 0;
        private int _degree_no = 0;
        private int _branch_no = 0;

        // private int _scheme_no = 0;
        private int _scheme_id = 0;

        private int _user = 0;

        #endregion Private Member

        #region Public Member

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string GradeName
        {
            get { return _gradeName; }
            set { _gradeName = value; }
        }

        public int GradeType
        {
            get { return _gradeType; }
            set { _gradeType = value; }
        }

        public int SrNo
        {
            get { return _sr_no; }
            set { _sr_no = value; }
        }

        public int CollegeId
        {
            get { return _college_id; }
            set { _college_id = value; }
        }

        public int DegreeNo
        {
            get { return _degree_no; }
            set { _degree_no = value; }
        }

        public int BranchNo
        {
            get { return _branch_no; }
            set { _branch_no = value; }
        }

        //public int SchemeNo
        //{
        //    get { return _scheme_no; }
        //    set { _scheme_no = value; }
        //}
        public int SchemeId
        {
            get { return _scheme_id; }
            set { _scheme_id = value; }
        }

        public int User
        {
            get { return _user; }
            set { _user = value; }
        }

        #endregion Public Member

        public CustomStatus AddGrade(Grade objGrade)
        {
            throw new NotImplementedException();
        }
    }
}