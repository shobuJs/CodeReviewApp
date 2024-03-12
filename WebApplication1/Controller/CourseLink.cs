namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class CourseLink
            {
                #region Private Members

                private int _srno = 0;
                private string _ccode = string.Empty;
                private int _courseno = 0;
                private int _sectionno = 0;
                private int _schemeno = 0;
                private string _coursenos = string.Empty;
                private string _coursecodes = string.Empty;
                private string _colcode = string.Empty;

                #endregion Private Members

                #region Public Property Fields

                public int SrNo
                {
                    get { return _srno; }
                    set { _srno = value; }
                }

                public string CCode
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }

                public int CourseNo
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }

                public int SectionNo
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }

                public string CourseNos
                {
                    get { return _coursenos; }
                    set { _coursenos = value; }
                }

                public string CourseCodes
                {
                    get { return _coursecodes; }
                    set { _coursecodes = value; }
                }

                public string CollegeCode
                {
                    get { return _colcode; }
                    set { _colcode = value; }
                }

                #endregion Public Property Fields
            }
        }//END: BusinessLayer.BusinessEntities
    }//END: UAIMS
}//END: IITMS