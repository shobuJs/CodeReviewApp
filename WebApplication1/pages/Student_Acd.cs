using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Student_Acd : IDisposable
            {
                #region Private Members

                //ADDED BY AASHNA 27-11-2021
                private string _collegeid = string.Empty;

                private string _degree = string.Empty;
                private string _degreenoo = string.Empty;
                private string _DegreePrefs = string.Empty;
                private string _Amounts = string.Empty;
                private string _OrdereIDs = string.Empty;
                private string _OrdereID = string.Empty;
                private string _TranDates = string.Empty;
                private string _StudIPAddress = string.Empty;
                private int _chk = 0;
                private string _AdminIPAddress = string.Empty;
                private string _college = string.Empty;
                private string _DegreePref = string.Empty;
                private string _Amount = string.Empty;
                private string _TranDate = string.Empty;
                //END

                private decimal _seatingCapacity = 0.0m;
                private decimal _examcapacity = 0.0m;
                private int _Roomcategory = 0;
                private int _idno = 0;

                private int _source = 0;
                private string _studid = string.Empty;
                private string _enrollmentno;
                private string _regno = string.Empty;
                private string _rollno = string.Empty;
                private string _sem = string.Empty;
                private int _schemeno = 0;
                private int _batchno = 0;
                private int _thbatchno = 0;
                private string _passfail = string.Empty;
                private decimal _credit = 0.0M;
                private decimal _egp = 0.0M;
                private decimal _spi = 0.0M;
                private decimal _totcredit = 0.0M;
                private decimal _totegp = 0.0M;
                private decimal _totcpi = 0.0M;
                private int _sessionno = 0;
                private char _result = ' ';
                private int _fac_advisor = 0;
                private char _flag = '0';  //0=False, 1=True
                private int ua_no = 0;
                private int _courseNo = 0;
                private int _pract_theory = 0;
                private string _adteacher = string.Empty;
                private int _th_pr = 0;
                private int subid = 0;
                private string _ua_no1 = string.Empty;
                private int _sectionno = 0;
                private int _degreeno = 0;
                private int _branchno = 0;
                private int _semesterno = 0;

                private int _seatno = 0;
                private string _studname = string.Empty;
                private string _coursename = string.Empty;
                private string _ccode = string.Empty;
                private string _punishment = string.Empty;
                private int _status = 0;
                private int _punishmentno = 0;
                private int _COLLEGE_ID = 0;
                private int _INTEXT = 0;
                /*for withheld page*/
                private string _remark = string.Empty;
                /*for withheld page*/
                private string _Reason = string.Empty;
                private string _IpAddress = string.Empty;
                private string _coursenos = string.Empty;
                private string _juniorname = string.Empty;
                private string _Seniorname = string.Empty;
                private string _ufmdetails = string.Empty;
                private string _remarks = string.Empty;
                private int _WFor = 0;

                //added by aashna 30-10-2021

                private int _ugpg = 0;
                private int _userno = 0;
                private string _mobileno = string.Empty;
                private string _emailid = string.Empty;
                private string _username = string.Empty;
                private string _userpwd = string.Empty;
                private string _firstname = string.Empty;
                private string _lastname = string.Empty;
                private int _AdmBatch = 0;
                private string _PassportNo = string.Empty;
                private string _NIC = string.Empty;
                private string _Gender = string.Empty;
                private string _Homemobileno = string.Empty;
                private int _admtype = 0;
                private string _MobileCode = string.Empty;
                private string _programIntrests = string.Empty;
                //end

                #endregion Private Members

                #region Public Property Fields

                public int wfor
                {
                    get { return _WFor; }
                    set { _WFor = value; }
                }

                public string Reason
                {
                    get { return _Reason; }
                    set { _Reason = value; }
                }

                public string Ipaddress
                {
                    get { return _IpAddress; }
                    set { _IpAddress = value; }
                }

                public string Coursenos
                {
                    get { return _coursenos; }
                    set { _coursenos = value; }
                }

                public int PUNISHMENTNO
                {
                    get { return _punishmentno; }
                    set { _punishmentno = value; }
                }

                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }

                public int INTEXT
                {
                    get { return _INTEXT; }
                    set { _INTEXT = value; }
                }

                public int IdNo
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public string StudId
                {
                    get { return _studid; }
                    set { _studid = value; }
                }

                public string EnrollmentNo
                {
                    get { return _enrollmentno; }
                    set { _enrollmentno = value; }
                }

                public string RegNo
                {
                    get { return _regno; }
                    set { _regno = value; }
                }

                public string RollNo
                {
                    get { return _rollno; }
                    set { _rollno = value; }
                }

                public string Sem
                {
                    get { return _sem; }
                    set { _sem = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }

                public int BatchNo
                {
                    get { return _batchno; }
                    set { _batchno = value; }
                }

                public int ThBatchNo
                {
                    get { return _thbatchno; }
                    set { _thbatchno = value; }
                }

                public string PassFail
                {
                    get { return _passfail; }
                    set { _passfail = value; }
                }

                public decimal Credit
                {
                    get { return _credit; }
                    set { _credit = value; }
                }

                public decimal EGP
                {
                    get { return _egp; }
                    set { _egp = value; }
                }

                public decimal SPI
                {
                    get { return _spi; }
                    set { _spi = value; }
                }

                public decimal TotCredit
                {
                    get { return _totcredit; }
                    set { _totcredit = value; }
                }

                public decimal TotEGP
                {
                    get { return _totegp; }
                    set { _totegp = value; }
                }

                public decimal TotCPI
                {
                    get { return _totcpi; }
                    set { _totcpi = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public char Result
                {
                    get { return _result; }
                    set { _result = value; }
                }

                public int Fac_Advisor
                {
                    get { return _fac_advisor; }
                    set { _fac_advisor = value; }
                }

                public char Flag
                {
                    get { return _flag; }
                    set { _flag = value; }
                }

                public int UA_No
                {
                    get { return ua_no; }
                    set { ua_no = value; }
                }

                public int CourseNo
                {
                    get { return _courseNo; }
                    set { _courseNo = value; }
                }

                public int Pract_Theory
                {
                    get { return _pract_theory; }
                    set { _pract_theory = value; }
                }

                public string AdTeacher
                {
                    get { return _adteacher; }
                    set { _adteacher = value; }
                }

                public int Th_Pr
                {
                    get { return _th_pr; }
                    set { _th_pr = value; }
                }

                public int sub_id
                {
                    get { return subid; }
                    set { subid = value; }
                }

                public string Ua_no1
                {
                    get { return _ua_no1; }
                    set { _ua_no1 = value; }
                }

                public int Sectionno
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }

                public int DegreeNo
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }

                public int BranchNo
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }

                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public int Seatno
                {
                    get { return _seatno; }
                    set { _seatno = value; }
                }

                public string StudName
                {
                    get { return _studname; }
                    set { _studname = value; }
                }

                public string CourseName
                {
                    get { return _coursename; }
                    set { _coursename = value; }
                }

                public string CCODE
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }

                public string Punishment
                {
                    get { return _punishment; }
                    set { _punishment = value; }
                }

                public int Status
                {
                    get { return _status; }
                    set { _status = value; }
                }

                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }

                public string Juniorname
                {
                    get { return _juniorname; }
                    set { _juniorname = value; }
                }

                public string Seniorname
                {
                    get { return _Seniorname; }
                    set { _Seniorname = value; }
                }

                public string Ufmdetails
                {
                    get { return _ufmdetails; }
                    set { _ufmdetails = value; }
                }

                public string Remarks
                {
                    get { return _remarks; }
                    set { _remarks = value; }
                }

                public decimal SeatingCapacity
                {
                    get { return _seatingCapacity; }
                    set { _seatingCapacity = value; }
                }

                public decimal Examcapacity
                {
                    get { return _examcapacity; }
                    set { _examcapacity = value; }
                }

                public int Roomcategory
                {
                    get { return _Roomcategory; }
                    set { _Roomcategory = value; }
                }

                public string EMAILID
                {
                    get { return _emailid; }
                    set { _emailid = value; }
                }

                public string MOBILENO
                {
                    get { return _mobileno; }
                    set { _mobileno = value; }
                }

                public string FIRSTNAME
                {
                    get { return _firstname; }
                    set { _firstname = value; }
                }

                public string LASTNAME
                {
                    get { return _lastname; }
                    set { _lastname = value; }
                }

                public int BRANCHNO
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }

                public int DEGREENO
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }

                public string MobileCode
                {
                    get { return _MobileCode; }
                    set { _MobileCode = value; }
                }

                public int AdmBatch
                {
                    get { return _AdmBatch; }
                    set { _AdmBatch = value; }
                }

                public int ADMTYPE
                {
                    get { return _admtype; }
                    set { _admtype = value; }
                }

                public string PassportNo
                {
                    get { return _PassportNo; }
                    set { _PassportNo = value; }
                }

                public string NIC
                {
                    get { return _NIC; }
                    set { _NIC = value; }
                }

                public int USERNO
                {
                    get { return _userno; }
                    set { _userno = value; }
                }

                public int UGPG
                {
                    get { return _ugpg; }
                    set { _ugpg = value; }
                }

                public int source
                {
                    get { return _source; }
                    set { _source = value; }
                }

                public string Gender
                {
                    get { return _Gender; }
                    set { _Gender = value; }
                }

                public string Homemobileno
                {
                    get { return _Homemobileno; }
                    set { _Homemobileno = value; }
                }

                public string programIntrests
                {
                    get { return _programIntrests; }
                    set { _programIntrests = value; }
                }

                ///end aashna 30102021

                //STARTED BY AASHNA 27-11-2021

                public string TranDate
                {
                    get { return _TranDate; }
                    set { _TranDate = value; }
                }

                public string OrdereID
                {
                    get { return _OrdereID; }
                    set { _OrdereID = value; }
                }

                public string Amount
                {
                    get { return _Amount; }
                    set { _Amount = value; }
                }

                public string DegreePref
                {
                    get { return _DegreePref; }
                    set { _DegreePref = value; }
                }

                public string collegeid
                {
                    get { return _collegeid; }
                    set { _collegeid = value; }
                }

                public string degree
                {
                    get { return _degree; }
                    set { _degree = value; }
                }

                public string degreenoo
                {
                    get { return _degreenoo; }
                    set { _degreenoo = value; }
                }

                public string DegreePrefs
                {
                    get { return _DegreePrefs; }
                    set { _DegreePrefs = value; }
                }

                public string Amounts
                {
                    get { return _Amounts; }
                    set { _Amounts = value; }
                }

                public string OrdereIDs
                {
                    get { return _OrdereIDs; }
                    set { _OrdereIDs = value; }
                }

                //public string OrdereID
                //{
                //    get { return _OrdereID; }
                //    set { _OrdereID = value; }
                //}
                public string TranDates
                {
                    get { return _TranDates; }
                    set { _TranDates = value; }
                }

                public string StudIPAddress
                {
                    get { return _StudIPAddress; }
                    set { _StudIPAddress = value; }
                }

                public int chk
                {
                    get { return _chk; }
                    set { _chk = value; }
                }

                public string AdminIPAddress
                {
                    get { return _AdminIPAddress; }
                    set { _AdminIPAddress = value; }
                }

                public string college
                {
                    get { return _college; }
                    set { _college = value; }
                }

                //END

                #endregion Public Property Fields

                private bool Disposed = false;

                ~Student_Acd()
                {
                    //Destructor Called
                    Dispose(false);
                }

                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }

                public void Dispose(bool disposing)
                {
                    if (!Disposed)
                    {
                        if (disposing)
                        {
                            //Called From Dispose
                            //Clear all the managed resources here
                        }
                        else
                        {
                            //Clear all the unmanaged resources here
                        }
                        Disposed = true;
                    }
                }
            }
        }
    }
}