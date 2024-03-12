namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic
{
    public class GradeEntry
    {
        #region Private Member

        private int _gradeNo = 0;
        private string _grade = string.Empty;
        private string _gradePoint = string.Empty;
        private string _collegeCode = string.Empty;
        private int _subid = 0;
        private int _maxMark = 0;
        private int _minMark = 0;
        private int _degreeNo = 0;
        private int _gradeType = 0;
        private string _gradeDesc = string.Empty;

        #endregion Private Member

        #region Public Member

        public int GradeNo
        {
            get { return _gradeNo; }
            set { _gradeNo = value; }
        }

        public string Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public string GradePoint
        {
            get { return _gradePoint; }
            set { _gradePoint = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int Subid
        {
            get { return _subid; }
            set { _subid = value; }
        }

        public int MaxMark
        {
            get { return _maxMark; }
            set { _maxMark = value; }
        }

        public int MinMark
        {
            get { return _minMark; }
            set { _minMark = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int GradeType
        {
            get { return _gradeType; }
            set { _gradeType = value; }
        }

        public string GradeDesc
        {
            get { return _gradeDesc; }
            set { _gradeDesc = value; }
        }

        #endregion Public Member
    }
}