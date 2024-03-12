using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class New_Student
            {
                #region Private Member

                private int _idNo = 0;
                private string _firstName = string.Empty;
                private string _middleName = string.Empty;
                private string _lastName = string.Empty;
                private string _mobileNo = string.Empty;
                private string _emailId = string.Empty;
                private string _gender = string.Empty;
                private DateTime _dob;

                private string _lAddress = string.Empty;
                private int _city = 0;
                private string _district = string.Empty;
                private string _pin = string.Empty;
                private int _degreeNo = 0;
                private int _branchNo = 0;
                private string _remark = string.Empty;

                private string _userName = string.Empty;
                private string _password = string.Empty;

                private string _regNo = string.Empty;
                private string _uName = string.Empty;
                private string _phoneNo = string.Empty;
                private string _smsBody = string.Empty;
                private string _studName = string.Empty;
                private string _course = string.Empty;
                private string _catno = string.Empty;

                private string _fileName = string.Empty;
                private string _filePath = string.Empty;

                #endregion Private Member

                #region Private Property Fields

                public int IdNo
                {
                    get { return _idNo; }
                    set { _idNo = value; }
                }

                public string FirstName
                {
                    get { return _firstName; }
                    set { _firstName = value; }
                }

                public string MiddleName
                {
                    get { return _middleName; }
                    set { _middleName = value; }
                }

                public string LastName
                {
                    get { return _lastName; }
                    set { _lastName = value; }
                }

                public string MobileNo
                {
                    get { return _mobileNo; }
                    set { _mobileNo = value; }
                }

                public string EmailId
                {
                    get { return _emailId; }
                    set { _emailId = value; }
                }

                public string Gender
                {
                    get { return _gender; }
                    set { _gender = value; }
                }

                public DateTime Dob
                {
                    get { return _dob; }
                    set { _dob = value; }
                }

                public string LAddress
                {
                    get { return _lAddress; }
                    set { _lAddress = value; }
                }

                public int City
                {
                    get { return _city; }
                    set { _city = value; }
                }

                public string District
                {
                    get { return _district; }
                    set { _district = value; }
                }

                public string Pin
                {
                    get { return _pin; }
                    set { _pin = value; }
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

                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }

                public string UserName
                {
                    get { return _userName; }
                    set { _userName = value; }
                }

                public string Password
                {
                    get { return _password; }
                    set { _password = value; }
                }

                public string FileName
                {
                    get { return _fileName; }
                    set { _fileName = value; }
                }

                public string FilePath
                {
                    get { return _filePath; }
                    set { _filePath = value; }
                }

                #endregion Private Property Fields

                #region public

                public string RegNo
                {
                    get { return _regNo; }
                    set { _regNo = value; }
                }

                public string UName
                {
                    get { return _uName; }
                    set { _uName = value; }
                }

                public string PhoneNo
                {
                    get { return _phoneNo; }
                    set { _phoneNo = value; }
                }

                public string SmsBody
                {
                    get { return _smsBody; }
                    set { _smsBody = value; }
                }

                //Added By Pritish on 28/05/2019
                public string StudName
                {
                    get { return _studName; }
                    set { _studName = value; }
                }

                public string Course
                {
                    get { return _course; }
                    set { _course = value; }
                }

                public string CatNo
                {
                    get { return _catno; }
                    set { _catno = value; }
                }

                public int Degree { get; set; }
                public int Branch { get; set; }
                public int AdmissionType { get; set; }
                public int UA_NO { get; set; }

                #endregion public
            }
        }
    }
}