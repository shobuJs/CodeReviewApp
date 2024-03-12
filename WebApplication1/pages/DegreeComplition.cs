namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class DegreeComplition
            {
                #region Private Members

                private string _Rollno = string.Empty;
                private int _SessionNo = 0;
                private string _EnrollNo = string.Empty;

                #endregion Private Members

                #region Public Properties

                public string Rollno
                {
                    get { return _Rollno; }
                    set { _Rollno = value; }
                }

                public int SessionNo
                {
                    get { return _SessionNo; }
                    set { _SessionNo = value; }
                }

                public string EnrollNo
                {
                    get { return _EnrollNo; }
                    set { _EnrollNo = value; }
                }

                #endregion Public Properties
            }
        }//END: BusinessLayer.BusinessEntities
    }//END: UAIMS
}//END: IITMS