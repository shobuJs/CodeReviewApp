using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class StudentRegist
            {
                #region Private Memebers

                private System.Nullable<int> _IDNO;

                private string _GRADES;

                private string _REGNO;

                private string _ROLLNO;

                private string _FINAL_DETEND;
                private string _SUSPENSION_REASON;

                private System.Nullable<int> _USERTTYPE;

                private System.Nullable<int> _EXAM_REGISTERED;

                private System.Nullable<int> _DEC;

                private System.Nullable<int> _ROLL_NO;

                private System.Nullable<int> _SECTIONNO;

                private System.Nullable<int> _BATCHNO;

                private System.Nullable<int> _SEMESTERNO;

                private System.Nullable<int> _SESSIONNO;

                private System.Nullable<int> _ABSORPTION_STATUS = 0;

                private System.Nullable<int> _SCHEMENO;

                private System.Nullable<int> _UA_NO;

                private System.Nullable<int> _COURSENO;

                private string _COURSENAME;

                private System.Nullable<int> _SUBID;

                private string _CCODE;

                private System.Nullable<int> _SRNO;

                private System.Nullable<bool> _PREV_STATUS;

                private string _GRADE;

                private string _ATTENDANCE;

                private System.Nullable<decimal> _INTERMARK;

                private System.Nullable<decimal> _EXTERMARK;

                private System.Nullable<bool> _LOCKI;

                private System.Nullable<bool> _LOCKE;

                private System.Nullable<decimal> _MARKTOT;

                private System.Nullable<decimal> _S1MARK;

                private System.Nullable<decimal> _S2MARK;

                private System.Nullable<decimal> _S3MARK;

                private System.Nullable<decimal> _S4MARK;

                private System.Nullable<decimal> _S5MARK;

                private System.Nullable<decimal> _S6MARK;

                private System.Nullable<decimal> _S7MARK;

                private System.Nullable<decimal> _S8MARK;

                private System.Nullable<decimal> _S9MARK;

                private System.Nullable<decimal> _S10MARK;

                private System.Nullable<decimal> _ASSIGNMARK;

                private System.Nullable<bool> _LOCKS1 = false;

                private System.Nullable<bool> _LOCKS2 = false;

                private System.Nullable<bool> _LOCKS3 = false;

                private System.Nullable<bool> _LOCKS4 = false;

                private System.Nullable<bool> _LOCKS5 = false;

                private System.Nullable<bool> _LOCKS6 = false;

                private System.Nullable<bool> _LOCKS7 = false;

                private System.Nullable<bool> _LOCKS8 = false;

                private System.Nullable<bool> _LOCKS9 = false;

                private System.Nullable<bool> _LOCKS10 = false;

                private System.Nullable<bool> _LOCKASSIGN = false;

                private System.Nullable<System.DateTime> _LCDATEI = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATEE = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE1 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE2 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE3 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE4 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE5 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE6 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE7 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE8 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE9 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATE10 = DateTime.MinValue;

                private System.Nullable<System.DateTime> _LCDATEASS = DateTime.MinValue;

                private System.Nullable<int> _PROGRESS_ST;

                private System.Nullable<bool> _OPASS;

                private System.Nullable<int> _OLDSESNO;

                private string _OLDSESNAME;

                private string _IPADDRESS;

                private System.Nullable<int> _CREDITS;

                private System.Nullable<int> _ATTEMPT;

                private System.Nullable<int> _ACCEPTED;

                private System.Nullable<int> _REGISTERED;

                private Decimal _GDPOINT;

                private string _COURSENOS;

                private string _EXTERMARKS;
                private string _VALUER1_MKS;
                private string _VALUER2_MKS;

                private string _IDNOS;

                private string _COURSENAMES;

                private string _SUBIDS;

                private string _CCODES;

                private string _CSRNO;

                private string _CCREDITS;

                private string _BATCHNOS;

                private string _SECTIONNOS;

                private string STUD_UA_NO;

                private string _ACEEPTSUB;

                private string _FAC_REMARK = string.Empty;

                private string _FACREMARKS = string.Empty;

                private string _S1IND = string.Empty;

                private string _S2IND = string.Empty;

                private string _S3IND = string.Empty;

                private string _S4IND = string.Empty;

                private System.Nullable<int> _CONTROLSHEET_NO = 0;

                private string _COLLEGE_CODE;

                // FOR CONTROLSHEET NO
                private int _START_NO = 0;

                private int _RECORD = 0;
                private string _ELECTIVE;
                private string _MARKDIFFS = string.Empty;
                private string _NEWMARKS = string.Empty;

                // BRANCH CHANGE
                private int _BRANCHNO = 0;

                private string _BRANCH_REF = string.Empty;
                private int _SELECT_COURSE;
                private string _Backlog_course = string.Empty;
                private string _Audit_course = string.Empty;
                private int _DEGREENO = 0;
                private int _COLLEGEID = 0;
                private string _STUDNAME = string.Empty;
                private int _CORSUBID = 0;
                private string _Re_Appeared = string.Empty;

                // DROUP COURSE

                private string _ADDITIONAL_COURSENOS_UG;
                private string _ADDITIONAL_COURSENOS_PG;
                private string _ADDITIONAL_COURSENOS_UG_SECTIONNOS;
                private string _ADDITIONAL_COURSENOS_PG_SECTIONNOS;
                private string _GPCOURSES;
                private string _GPNOS;

                private string _semesterNos = string.Empty;
                private string _Prev_semesterNos = string.Empty;
                private string _BackSectionos = string.Empty;
                private string _detainSectionos = string.Empty;

                private string _BackCourseNos;
                private string _detainNos;
                private int _OPNELECTNO;
                private string _Rule22;
                private string _Rule11;
                private string _COURSENNO;
                private string _CATEGORY3;

                private string _SEMESTERNOS;
                private string _WDNO;

                #endregion Private Memebers

                #region Public Properties

                public string GRADES
                {
                    get { return _GRADES; }
                    set { _GRADES = value; }
                }

                public string WITHDRAWNO
                {
                    get { return _WDNO; }
                    set { _WDNO = value; }
                }

                public string SEMESTERNOS
                {
                    get { return _SEMESTERNOS; }
                    set { _SEMESTERNOS = value; }
                }

                private string EXT_TH;

                public string EXT_TH1
                {
                    get { return EXT_TH; }
                    set { EXT_TH = value; }
                }

                private string EXT_PR;

                public string EXT_PR1
                {
                    get { return EXT_PR; }
                    set { EXT_PR = value; }
                }

                public int DEGREENO
                {
                    get { return _DEGREENO; }
                    set { _DEGREENO = value; }
                }

                public int COLLEGEID
                {
                    get { return _COLLEGEID; }
                    set { _COLLEGEID = value; }
                }

                public string STUDNAME
                {
                    get { return _STUDNAME; }
                    set { _STUDNAME = value; }
                }

                public int CORSUBID
                {
                    get { return _CORSUBID; }
                    set { _CORSUBID = value; }
                }

                public System.Nullable<int> IDNO
                {
                    get
                    {
                        return this._IDNO;
                    }
                    set
                    {
                        if ((this._IDNO != value))
                        {
                            this._IDNO = value;
                        }
                    }
                }

                public string Audit_course
                {
                    get
                    {
                        return this._Audit_course;
                    }
                    set
                    {
                        if ((this._Audit_course != value))
                        {
                            this._Audit_course = value;
                        }
                    }
                }

                public string Backlog_course
                {
                    get
                    {
                        return this._Backlog_course;
                    }
                    set
                    {
                        if ((this._Backlog_course != value))
                        {
                            this._Backlog_course = value;
                        }
                    }
                }

                public System.Nullable<int> EXAM_REGISTERED
                {
                    get
                    {
                        return this._EXAM_REGISTERED;
                    }
                    set
                    {
                        if ((this._EXAM_REGISTERED != value))
                        {
                            this._EXAM_REGISTERED = value;
                        }
                    }
                }

                public string REGNO
                {
                    get
                    {
                        return this._REGNO;
                    }
                    set
                    {
                        if ((this._REGNO != value))
                        {
                            this._REGNO = value;
                        }
                    }
                }

                public string ROLLNO
                {
                    get
                    {
                        return this._ROLLNO;
                    }
                    set
                    {
                        if ((this._ROLLNO != value))
                        {
                            this._ROLLNO = value;
                        }
                    }
                }

                public System.Nullable<int> USERTTYPE
                {
                    get { return _USERTTYPE; }
                    set { _USERTTYPE = value; }
                }

                public System.Nullable<int> DEC
                {
                    get { return _DEC; }
                    set { _DEC = value; }
                }

                public System.Nullable<int> ROLL_NO
                {
                    get
                    {
                        return this._ROLL_NO;
                    }
                    set
                    {
                        if ((this._ROLL_NO != value))
                        {
                            this._ROLL_NO = value;
                        }
                    }
                }

                public System.Nullable<int> SECTIONNO
                {
                    get
                    {
                        return this._SECTIONNO;
                    }
                    set
                    {
                        if ((this._SECTIONNO != value))
                        {
                            this._SECTIONNO = value;
                        }
                    }
                }

                public System.Nullable<int> BATCHNO
                {
                    get
                    {
                        return this._BATCHNO;
                    }
                    set
                    {
                        if ((this._BATCHNO != value))
                        {
                            this._BATCHNO = value;
                        }
                    }
                }

                public System.Nullable<int> SEMESTERNO
                {
                    get
                    {
                        return this._SEMESTERNO;
                    }
                    set
                    {
                        if ((this._SEMESTERNO != value))
                        {
                            this._SEMESTERNO = value;
                        }
                    }
                }

                public System.Nullable<int> SESSIONNO
                {
                    get
                    {
                        return this._SESSIONNO;
                    }
                    set
                    {
                        if ((this._SESSIONNO != value))
                        {
                            this._SESSIONNO = value;
                        }
                    }
                }

                public System.Nullable<int> ABSORPTION_STATUS
                {
                    get
                    {
                        return this._ABSORPTION_STATUS;
                    }
                    set
                    {
                        if ((this._ABSORPTION_STATUS != value))
                        {
                            this._ABSORPTION_STATUS = value;
                        }
                    }
                }

                public System.Nullable<int> SCHEMENO
                {
                    get
                    {
                        return this._SCHEMENO;
                    }
                    set
                    {
                        if ((this._SCHEMENO != value))
                        {
                            this._SCHEMENO = value;
                        }
                    }
                }

                public System.Nullable<int> UA_NO
                {
                    get
                    {
                        return this._UA_NO;
                    }
                    set
                    {
                        if ((this._UA_NO != value))
                        {
                            this._UA_NO = value;
                        }
                    }
                }

                public System.Nullable<int> COURSENO
                {
                    get
                    {
                        return this._COURSENO;
                    }
                    set
                    {
                        if ((this._COURSENO != value))
                        {
                            this._COURSENO = value;
                        }
                    }
                }

                public string COURSENAME
                {
                    get
                    {
                        return this._COURSENAME;
                    }
                    set
                    {
                        if ((this._COURSENAME != value))
                        {
                            this._COURSENAME = value;
                        }
                    }
                }

                public System.Nullable<int> SUBID
                {
                    get
                    {
                        return this._SUBID;
                    }
                    set
                    {
                        if ((this._SUBID != value))
                        {
                            this._SUBID = value;
                        }
                    }
                }

                public string CCODE
                {
                    get
                    {
                        return this._CCODE;
                    }
                    set
                    {
                        if ((this._CCODE != value))
                        {
                            this._CCODE = value;
                        }
                    }
                }

                public System.Nullable<int> SRNO
                {
                    get
                    {
                        return this._SRNO;
                    }
                    set
                    {
                        if ((this._SRNO != value))
                        {
                            this._SRNO = value;
                        }
                    }
                }

                public System.Nullable<bool> PREV_STATUS
                {
                    get
                    {
                        return this._PREV_STATUS;
                    }
                    set
                    {
                        if ((this._PREV_STATUS != value))
                        {
                            this._PREV_STATUS = value;
                        }
                    }
                }

                public string GRADE
                {
                    get
                    {
                        return this._GRADE;
                    }
                    set
                    {
                        if ((this._GRADE != value))
                        {
                            this._GRADE = value;
                        }
                    }
                }

                public string ATTENDANCE
                {
                    get
                    {
                        return this._ATTENDANCE;
                    }
                    set
                    {
                        if ((this._ATTENDANCE != value))
                        {
                            this._ATTENDANCE = value;
                        }
                    }
                }

                public System.Nullable<decimal> INTERMARK
                {
                    get
                    {
                        return this._INTERMARK;
                    }
                    set
                    {
                        if ((this._INTERMARK != value))
                        {
                            this._INTERMARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> EXTERMARK
                {
                    get
                    {
                        return this._EXTERMARK;
                    }
                    set
                    {
                        if ((this._EXTERMARK != value))
                        {
                            this._EXTERMARK = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKI
                {
                    get
                    {
                        return this._LOCKI;
                    }
                    set
                    {
                        if ((this._LOCKI != value))
                        {
                            this._LOCKI = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKE
                {
                    get
                    {
                        return this._LOCKE;
                    }
                    set
                    {
                        if ((this._LOCKE != value))
                        {
                            this._LOCKE = value;
                        }
                    }
                }

                public System.Nullable<decimal> MARKTOT
                {
                    get
                    {
                        return this._MARKTOT;
                    }
                    set
                    {
                        if ((this._MARKTOT != value))
                        {
                            this._MARKTOT = value;
                        }
                    }
                }

                public System.Nullable<decimal> S1MARK
                {
                    get
                    {
                        return this._S1MARK;
                    }
                    set
                    {
                        if ((this._S1MARK != value))
                        {
                            this._S1MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S2MARK
                {
                    get
                    {
                        return this._S2MARK;
                    }
                    set
                    {
                        if ((this._S2MARK != value))
                        {
                            this._S2MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S3MARK
                {
                    get
                    {
                        return this._S3MARK;
                    }
                    set
                    {
                        if ((this._S3MARK != value))
                        {
                            this._S3MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S4MARK
                {
                    get
                    {
                        return this._S4MARK;
                    }
                    set
                    {
                        if ((this._S4MARK != value))
                        {
                            this._S4MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S5MARK
                {
                    get
                    {
                        return this._S5MARK;
                    }
                    set
                    {
                        if ((this._S5MARK != value))
                        {
                            this._S5MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S6MARK
                {
                    get
                    {
                        return this._S6MARK;
                    }
                    set
                    {
                        if ((this._S6MARK != value))
                        {
                            this._S6MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S7MARK
                {
                    get
                    {
                        return this._S7MARK;
                    }
                    set
                    {
                        if ((this._S7MARK != value))
                        {
                            this._S7MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S8MARK
                {
                    get
                    {
                        return this._S8MARK;
                    }
                    set
                    {
                        if ((this._S8MARK != value))
                        {
                            this._S8MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S9MARK
                {
                    get
                    {
                        return this._S9MARK;
                    }
                    set
                    {
                        if ((this._S9MARK != value))
                        {
                            this._S9MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> S10MARK
                {
                    get
                    {
                        return this._S10MARK;
                    }
                    set
                    {
                        if ((this._S10MARK != value))
                        {
                            this._S10MARK = value;
                        }
                    }
                }

                public System.Nullable<decimal> ASSIGNMARK
                {
                    get
                    {
                        return this._ASSIGNMARK;
                    }
                    set
                    {
                        if ((this._ASSIGNMARK != value))
                        {
                            this._ASSIGNMARK = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS1
                {
                    get
                    {
                        return this._LOCKS1;
                    }
                    set
                    {
                        if ((this._LOCKS1 != value))
                        {
                            this._LOCKS1 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS2
                {
                    get
                    {
                        return this._LOCKS2;
                    }
                    set
                    {
                        if ((this._LOCKS2 != value))
                        {
                            this._LOCKS2 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS3
                {
                    get
                    {
                        return this._LOCKS3;
                    }
                    set
                    {
                        if ((this._LOCKS3 != value))
                        {
                            this._LOCKS3 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS4
                {
                    get
                    {
                        return this._LOCKS4;
                    }
                    set
                    {
                        if ((this._LOCKS4 != value))
                        {
                            this._LOCKS4 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS5
                {
                    get
                    {
                        return this._LOCKS5;
                    }
                    set
                    {
                        if ((this._LOCKS5 != value))
                        {
                            this._LOCKS5 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS6
                {
                    get
                    {
                        return this._LOCKS6;
                    }
                    set
                    {
                        if ((this._LOCKS6 != value))
                        {
                            this._LOCKS6 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS7
                {
                    get
                    {
                        return this._LOCKS7;
                    }
                    set
                    {
                        if ((this._LOCKS7 != value))
                        {
                            this._LOCKS7 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS8
                {
                    get
                    {
                        return this._LOCKS8;
                    }
                    set
                    {
                        if ((this._LOCKS8 != value))
                        {
                            this._LOCKS8 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS9
                {
                    get
                    {
                        return this._LOCKS9;
                    }
                    set
                    {
                        if ((this._LOCKS9 != value))
                        {
                            this._LOCKS9 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKS10
                {
                    get
                    {
                        return this._LOCKS10;
                    }
                    set
                    {
                        if ((this._LOCKS10 != value))
                        {
                            this._LOCKS10 = value;
                        }
                    }
                }

                public System.Nullable<bool> LOCKASSIGN
                {
                    get
                    {
                        return this._LOCKASSIGN;
                    }
                    set
                    {
                        if ((this._LOCKASSIGN != value))
                        {
                            this._LOCKASSIGN = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATEI
                {
                    get
                    {
                        return this._LCDATEI;
                    }
                    set
                    {
                        if ((this._LCDATEI != value))
                        {
                            this._LCDATEI = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATEE
                {
                    get
                    {
                        return this._LCDATEE;
                    }
                    set
                    {
                        if ((this._LCDATEE != value))
                        {
                            this._LCDATEE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE1
                {
                    get
                    {
                        return this._LCDATE1;
                    }
                    set
                    {
                        if ((this._LCDATE1 != value))
                        {
                            this._LCDATE1 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE2
                {
                    get
                    {
                        return this._LCDATE2;
                    }
                    set
                    {
                        if ((this._LCDATE2 != value))
                        {
                            this._LCDATE2 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE3
                {
                    get
                    {
                        return this._LCDATE3;
                    }
                    set
                    {
                        if ((this._LCDATE3 != value))
                        {
                            this._LCDATE3 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE4
                {
                    get
                    {
                        return this._LCDATE4;
                    }
                    set
                    {
                        if ((this._LCDATE4 != value))
                        {
                            this._LCDATE4 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE5
                {
                    get
                    {
                        return this._LCDATE5;
                    }
                    set
                    {
                        if ((this._LCDATE5 != value))
                        {
                            this._LCDATE5 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE6
                {
                    get
                    {
                        return this._LCDATE6;
                    }
                    set
                    {
                        if ((this._LCDATE6 != value))
                        {
                            this._LCDATE6 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE7
                {
                    get
                    {
                        return this._LCDATE7;
                    }
                    set
                    {
                        if ((this._LCDATE7 != value))
                        {
                            this._LCDATE7 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE8
                {
                    get
                    {
                        return this._LCDATE8;
                    }
                    set
                    {
                        if ((this._LCDATE8 != value))
                        {
                            this._LCDATE8 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE9
                {
                    get
                    {
                        return this._LCDATE9;
                    }
                    set
                    {
                        if ((this._LCDATE9 != value))
                        {
                            this._LCDATE9 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATE10
                {
                    get
                    {
                        return this._LCDATE10;
                    }
                    set
                    {
                        if ((this._LCDATE10 != value))
                        {
                            this._LCDATE10 = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> LCDATEASS
                {
                    get
                    {
                        return this._LCDATEASS;
                    }
                    set
                    {
                        if ((this._LCDATEASS != value))
                        {
                            this._LCDATEASS = value;
                        }
                    }
                }

                public System.Nullable<int> PROGRESS_ST
                {
                    get
                    {
                        return this._PROGRESS_ST;
                    }
                    set
                    {
                        if ((this._PROGRESS_ST != value))
                        {
                            this._PROGRESS_ST = value;
                        }
                    }
                }

                public System.Nullable<bool> OPASS
                {
                    get
                    {
                        return this._OPASS;
                    }
                    set
                    {
                        if ((this._OPASS != value))
                        {
                            this._OPASS = value;
                        }
                    }
                }

                public System.Nullable<int> OLDSESNO
                {
                    get
                    {
                        return this._OLDSESNO;
                    }
                    set
                    {
                        if ((this._OLDSESNO != value))
                        {
                            this._OLDSESNO = value;
                        }
                    }
                }

                public string OLDSESNAME
                {
                    get
                    {
                        return this._OLDSESNAME;
                    }
                    set
                    {
                        if ((this._OLDSESNAME != value))
                        {
                            this._OLDSESNAME = value;
                        }
                    }
                }

                public string IPADDRESS
                {
                    get
                    {
                        return this._IPADDRESS;
                    }
                    set
                    {
                        if ((this._IPADDRESS != value))
                        {
                            this._IPADDRESS = value;
                        }
                    }
                }

                public System.Nullable<int> CREDITS
                {
                    get
                    {
                        return this._CREDITS;
                    }
                    set
                    {
                        if ((this._CREDITS != value))
                        {
                            this._CREDITS = value;
                        }
                    }
                }

                public System.Nullable<int> ATTEMPT
                {
                    get
                    {
                        return this._ATTEMPT;
                    }
                    set
                    {
                        if ((this._ATTEMPT != value))
                        {
                            this._ATTEMPT = value;
                        }
                    }
                }

                public System.Nullable<int> ACCEPTED
                {
                    get
                    {
                        return this._ACCEPTED;
                    }
                    set
                    {
                        if ((this._ACCEPTED != value))
                        {
                            this._ACCEPTED = value;
                        }
                    }
                }

                public System.Nullable<int> REGISTERED
                {
                    get
                    {
                        return this._REGISTERED;
                    }
                    set
                    {
                        if ((this._REGISTERED != value))
                        {
                            this._REGISTERED = value;
                        }
                    }
                }

                public Decimal GDPOINT
                {
                    get
                    {
                        return this._GDPOINT;
                    }
                    set
                    {
                        if ((this._GDPOINT != value))
                        {
                            this._GDPOINT = value;
                        }
                    }
                }

                public string COURSENOS
                {
                    get { return _COURSENOS; }
                    set { _COURSENOS = value; }
                }

                public string COURSENAMES
                {
                    get { return _COURSENAMES; }
                    set { _COURSENAMES = value; }
                }

                public string SUBIDS
                {
                    get { return _SUBIDS; }
                    set { _SUBIDS = value; }
                }

                public string CCODES
                {
                    get { return _CCODES; }
                    set { _CCODES = value; }
                }

                public string CSRNO
                {
                    get { return _CSRNO; }
                    set { _CSRNO = value; }
                }

                public string CCREDITS
                {
                    get { return _CCREDITS; }
                    set { _CCREDITS = value; }
                }

                public string BATCHNOS
                {
                    get { return _BATCHNOS; }
                    set { _BATCHNOS = value; }
                }

                public string SECTIONNOS
                {
                    get { return _SECTIONNOS; }
                    set { _SECTIONNOS = value; }
                }

                public string _STUD_UA_NO
                {
                    get { return STUD_UA_NO; }
                    set { STUD_UA_NO = value; }
                }

                public string ACEEPTSUB
                {
                    get { return _ACEEPTSUB; }
                    set { _ACEEPTSUB = value; }
                }

                public string FACREMARKS
                {
                    get { return _FACREMARKS; }
                    set { _FACREMARKS = value; }
                }

                public string FAC_REMARK
                {
                    get { return _FAC_REMARK; }
                    set { _FAC_REMARK = value; }
                }

                public string EXTERMARKS
                {
                    get { return _EXTERMARKS; }
                    set { _EXTERMARKS = value; }
                }

                public string VALUER1_MKS
                {
                    get { return _VALUER1_MKS; }
                    set { _VALUER1_MKS = value; }
                }

                public string VALUER2_MKS
                {
                    get { return _VALUER2_MKS; }
                    set { _VALUER2_MKS = value; }
                }

                public string MARKDIFFS
                {
                    get { return _MARKDIFFS; }
                    set { _MARKDIFFS = value; }
                }

                public string NEWMARKS
                {
                    get { return _NEWMARKS; }
                    set { _NEWMARKS = value; }
                }

                public string IDNOS
                {
                    get { return _IDNOS; }
                    set { _IDNOS = value; }
                }

                public System.Nullable<int> CONTROLSHEET_NO
                {
                    get
                    {
                        return this._CONTROLSHEET_NO;
                    }
                    set
                    {
                        if ((this._CONTROLSHEET_NO != value))
                        {
                            this._CONTROLSHEET_NO = value;
                        }
                    }
                }

                public string COLLEGE_CODE
                {
                    get
                    {
                        return this._COLLEGE_CODE;
                    }
                    set
                    {
                        if ((this._COLLEGE_CODE != value))
                        {
                            this._COLLEGE_CODE = value;
                        }
                    }
                }

                public int START_NO
                {
                    get { return _START_NO; }
                    set { _START_NO = value; }
                }

                public int RECORD
                {
                    get { return _RECORD; }
                    set { _RECORD = value; }
                }

                public string S1IND
                {
                    get { return _S1IND; }
                    set { _S1IND = value; }
                }

                public string S2IND
                {
                    get { return _S2IND; }
                    set { _S2IND = value; }
                }

                public string S3IND
                {
                    get { return _S3IND; }
                    set { _S3IND = value; }
                }

                public string S4IND
                {
                    get { return _S4IND; }
                    set { _S4IND = value; }
                }

                public string ELECTIVE
                {
                    get { return _ELECTIVE; }
                    set { _ELECTIVE = value; }
                }

                public int BRANCHNO
                {
                    get { return _BRANCHNO; }
                    set { _BRANCHNO = value; }
                }

                public string BRANCH_REF
                {
                    get { return _BRANCH_REF; }
                    set { _BRANCH_REF = value; }
                }

                public int SELECT_COURSE
                {
                    get { return _SELECT_COURSE; }
                    set { _SELECT_COURSE = value; }
                }

                public string Re_Appeared
                {
                    get
                    {
                        return this._Re_Appeared;
                    }
                    set
                    {
                        if ((this._Re_Appeared != value))
                        {
                            this._Re_Appeared = value;
                        }
                    }
                }

                public string ADDITIONAL_COURSENOS_UG
                {
                    get { return _ADDITIONAL_COURSENOS_UG; }
                    set { _ADDITIONAL_COURSENOS_UG = value; }
                }

                public string ADDITIONAL_COURSENOS_UG_SECTIONNOS
                {
                    get { return _ADDITIONAL_COURSENOS_UG_SECTIONNOS; }
                    set { _ADDITIONAL_COURSENOS_UG_SECTIONNOS = value; }
                }

                public string ADDITIONAL_COURSENOS_PG
                {
                    get { return _ADDITIONAL_COURSENOS_PG; }
                    set { _ADDITIONAL_COURSENOS_PG = value; }
                }

                public string ADDITIONAL_COURSENOS_PG_SECTIONNOS
                {
                    get { return _ADDITIONAL_COURSENOS_PG_SECTIONNOS; }
                    set { _ADDITIONAL_COURSENOS_PG_SECTIONNOS = value; }
                }

                public string GPCOURSES
                {
                    get { return _GPCOURSES; }
                    set { _GPCOURSES = value; }
                }

                public string GPNOS
                {
                    get { return _GPNOS; }
                    set { _GPNOS = value; }
                }

                public string SemesterNos
                {
                    get { return _semesterNos; }
                    set { _semesterNos = value; }
                }

                public string Prev_SemesterNos
                {
                    get { return _Prev_semesterNos; }
                    set { _Prev_semesterNos = value; }
                }

                public string BackSectionos
                {
                    get { return _BackSectionos; }
                    set { _BackSectionos = value; }
                }

                public string detainSectionos
                {
                    get { return _detainSectionos; }
                    set { _detainSectionos = value; }
                }

                public string BackCourseNos
                {
                    get { return _BackCourseNos; }
                    set { _BackCourseNos = value; }
                }

                public string detainNos
                {
                    get { return _detainNos; }
                    set { _detainNos = value; }
                }

                public int OpenElectiveCourseNo
                {
                    get { return _OPNELECTNO; }
                    set { _OPNELECTNO = value; }
                }

                public string Rule11
                {
                    get { return _Rule11; }
                    set { _Rule11 = value; }
                }

                public string Rule22
                {
                    get { return _Rule22; }
                    set { _Rule22 = value; }
                }

                public string COURSENNO
                {
                    get { return _COURSENNO; }
                    set { _COURSENNO = value; }
                }

                public string CATEGORY3
                {
                    get { return _CATEGORY3; }
                    set { _CATEGORY3 = value; }
                }

                public string FINAL_DETEND
                {
                    get { return _FINAL_DETEND; }
                    set { _FINAL_DETEND = value; }
                }

                public string SUSPENSION_REASON
                {
                    get { return _SUSPENSION_REASON; }
                    set { _SUSPENSION_REASON = value; }
                }

                #endregion Public Properties
            }
        }
    }
}