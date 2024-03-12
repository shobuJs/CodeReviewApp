namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class LeavingCertificate
            {
                #region Private members

                private string _collegeCode;

                #endregion Private members

                #region Public Property Fields

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                #endregion Public Property Fields
            }
        }
    }
}