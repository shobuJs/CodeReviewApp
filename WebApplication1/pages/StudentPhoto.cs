namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class StudentPhoto
            {
                #region Private Member

                private int _idno = 0;
                private string _photoPath = string.Empty;
                private int _photoSize = 0;

                //Pending to Declare variable.....
                private byte[] _photo;

                private byte[] _signphoto;
                private string _collegeCode = string.Empty;

                #endregion Private Member

                #region Public Property Fields

                public int Idno
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public string PhotoPath
                {
                    get { return _photoPath; }
                    set { _photoPath = value; }
                }

                public int PhotoSize
                {
                    get { return _photoSize; }
                    set { _photoSize = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                public byte[] Photo1
                {
                    get { return _photo; }
                    set { _photo = value; }
                }

                public byte[] SignPhoto
                {
                    get { return _signphoto; }
                    set { _signphoto = value; }
                }

                #endregion Public Property Fields
            }
        }
    }
}