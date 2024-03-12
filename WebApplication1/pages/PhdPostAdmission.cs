using System;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class PhdPostAdmission
            {
                #region Private Members

                private int _IsJournalScopus;
                private string _IDCARDNO = string.Empty;

                private int _QUALINO;
                private string _REG_NAME;
                private System.Nullable<System.DateTime> _REG_DATE;

                private string _EXPERIENCE;

                //[Table(Name="pay.PAY_SB_DEPTEXAM")]
                private int _DENO;

                private System.Nullable<int> _IDNO;

                private System.Nullable<int> _SRNO;

                private string _EXAM;

                private string _REGNO;

                private string _PASSYEAR;

                private string _OFFICER;

                private string _COLLEGE_CODE;

                private string _QSTATUS;
                //[Table(Name = "pay.PAY_SB_FAMILYINFO")]

                private int _FNNO;

                private string _MEMNAME;

                private string _ADDRESS;

                private string _RELATION;

                private System.Nullable<decimal> _AGE;

                private System.Nullable<System.DateTime> _DOB;

                private string _REMARK;

                private string _MEETINGNAME;
                private string _MEETINGVENUE;

                private int _ADDRESS_FLAG;

                private string _EDUCATION;

                private string _EMPLOYMENT;

                //[Table(Name = "pay.PAY_SB_FORSERVICE")]

                private int _FSNO;

                private string _POSTNAME;

                private System.Nullable<System.DateTime> _FDT;

                private System.Nullable<System.DateTime> _TDT;

                private string _LSC;

                private string _LSCR;

                //[Table(Name = "pay.PAY_SB_LOAN")]
                private int _LNO;

                private System.Nullable<int> _LOANNO;

                private string _ORDERNO;

                private System.Nullable<decimal> _AMOUNT;

                private System.Nullable<decimal> _INTEREST;

                private System.Nullable<decimal> _INSTAL;

                private System.Nullable<System.DateTime> _LOANDT;

                //[Table(Name="pay.PAY_SB_MATTER")]

                private int _MNO;

                private System.Nullable<System.DateTime> _EDT;

                private string _HEADING;

                private string _MATTER;

                //[Table(Name = "pay.PAY_SB_NOMINIFOR")]

                private int _NFNO;

                private System.Nullable<int> _NTNO;

                private string _NAME;

                //IsJournalScopus
                private System.Nullable<bool> _LAST;

                private System.Nullable<decimal> _PER;

                private string _CONTING;

                //[Table(Name="pay.PAY_SB_PAYREV")]

                private int _PRNO;

                private System.Nullable<int> _SUBDESIGNO;

                private System.Nullable<int> _SCALENO;

                private string _TYPE;

                //[Table(Name="pay.PAY_SB_PRESERVICE")]

                private int _PSNO;

                private string _INST;
                private string _UNIVERSITY_NAME;
                private string _LOCATION;
                private string _TERMINATION;

                //[Table(Name = "pay.PAY_SB_QUALI")]
                private int _QNO;

                private System.Nullable<int> _QUALILEVELNO;

                private string _EXAMNAME;

                private string _SPECI;

                private string _Grade;

                private System.Nullable<decimal> _Percentage;

                private System.Nullable<int> _UNITYPE;

                private System.Nullable<int> _INITYPE;

                //[Table(Name="pay.PAY_SB_SCALE")]

                private System.Nullable<int> _B1;

                private System.Nullable<int> _I1;

                private System.Nullable<int> _B2;

                private System.Nullable<int> _I2;

                private System.Nullable<int> _B3;

                private System.Nullable<int> _I3;

                private System.Nullable<int> _B4;

                private System.Nullable<int> _I4;

                private System.Nullable<int> _B5;

                private System.Nullable<int> _I5;

                private string _SCALE;

                private System.Nullable<decimal> _GRADEPAY;

                //[Table(Name="pay.PAY_SB_SERVICEBK")]

                private System.Nullable<int> _TRNO;

                private System.Nullable<System.DateTime> _DOJ;

                private System.Nullable<System.DateTime> _DOI;

                private System.Nullable<int> _SUBDEPTNO;

                private System.Nullable<int> _DESIGNATURENO;

                private System.Nullable<int> _APPOINTNO;

                private System.Nullable<System.DateTime> _ORDERDT;

                private System.Nullable<System.DateTime> _ORDEFFDT;

                private string _GRNO;

                private System.Nullable<System.DateTime> _GRDT;

                private System.Nullable<int> _OLDSCALENO;

                private System.Nullable<int> _TYPETRANNO;

                private System.Nullable<System.DateTime> _TERMIDT;

                private string _SINGAUTO;

                private System.Nullable<System.DateTime> _LTCFDT;

                private System.Nullable<System.DateTime> _LTCTDT;

                private string _STATUS;

                private System.Nullable<int> _ETRNO;

                private System.Nullable<int> _SEQNO;

                private System.Nullable<System.DateTime> _COMMENDT;

                private string _COMMENREM;

                private System.Nullable<decimal> _PAYALLOW;

                private string _TERMIREASON;

                //[Table(Name="pay.PAY_SB_TRAINING")]

                private int _TNO;

                private string _COURSE;

                private int _PROGRAM_LEVEL;
                private int _PROGRAM_TYPE;
                private int _PARTICIPATION_TYPE;
                private string _PRESENTATION_DETAILS;

                private string _SPONSORED_BY;
                private System.Nullable<decimal> _COST_INVOLVED;

                //[Table(Name="pay.PAY_LEAVETRAN")]
                private int _ENO;

                private System.Nullable<System.DateTime> _APPDT;

                private System.Nullable<decimal> _LEAVES;

                private string _ORDNO;

                private System.Nullable<int> _YEAR;

                private System.Nullable<int> _ST;

                private System.Nullable<System.DateTime> _END_DT;

                private System.Nullable<System.DateTime> _JOINDT;

                private System.Nullable<bool> _FIT;

                private System.Nullable<bool> _UNFIT;

                private System.Nullable<int> _PERIOD;

                private System.Nullable<bool> _FNAN;

                // [Table(Name = "pay.PAY_EMP_IMAGE")]
                private int _imagetrxid;

                private System.Nullable<int> _imageid;

                private string _imagetype;

                private byte[] _empimage;

                private System.Nullable<int> _ADMINTRXNO;

                private string _RESPONSIBILITY;

                private string _ORGANIZATION;

                private System.Nullable<System.DateTime> _FROMDATE;

                private System.Nullable<System.DateTime> _TODATE;

                private System.Nullable<System.DateTime> _SUBMISSIONDATE;

                //[Table(Name = "UAIMSPAY.PAY_SB_PUBLICATION_DETAILS")]

                private System.Nullable<int> _PUBTRXNO;

                private string _PUBLICATION;

                private string _PUBLICATION_TYPE;

                private string _AUTHOR2;

                private string _AUTHOR3;

                private string _CONNAME;

                private string _ORGANISOR;

                private string _PAGENO;

                private string _ATTACHMENTS;

                private string _TITLE;

                private string _SUBJECT;

                private System.Nullable<System.DateTime> _PUBLICATIONDATE;

                private string _DETAILS;

                private string _PUB_STATUS;

                private int _IS_CONFERENCE;

                private string _VOLUME_NO;

                private string _ISSUE_NO;

                private string _PUBLISHER;

                //[Table(Name = "UAIMSPAY.PAY_SB_INVITED_TALK")]

                private System.Nullable<int> _INVTRXNO;

                private string _VENU;

                private string _DURATION;

                private System.Nullable<System.DateTime> _DATEOFTALK;

                private System.Nullable<System.DateTime> _ENTRYDATE;

                private string _GUIDENAME;

                private string _CENTERNAME;

                private string _CATEGORY;

                private string _AUTHOR4;

                private string _ISBN;

                private int _STATUSNO;

                private int _DepID;
                private int _DesID;
                private string _NatOfApp;
                private int _IsCurrent;
                private DateTime _StartDate;
                private DateTime _EndDate;
                private string _Duration;
                private string _Attachments;
                private int _CollegeCode;
                private string _Action;
                private int _ID;
                private int _SVCNO;

                private string _HIndex;
                private int _MOSNO;
                private string _Bond;
                private int _PHDGUIDED;
                private int _PHDAWARD;

                # region Newly added

                private string _EXPERIENCETYPE;
                private System.Nullable<decimal> _SPONSORED_AMOUNT;
                private string _SPONSOREDBY;
                private System.Nullable<decimal> _NOOFPARTI;
                private int _NOOFPARTICIPANT;
                private string _NATUREOFWORK;
                private string _NAMEOFUNI;
                private string _PAYSCALE;
                private System.Nullable<bool> _EligibleCandidate;
                private System.Nullable<bool> _fulfilservice;
                private System.Nullable<decimal> _LASTSALARY;
                private string _IMPACTFACTORS;
                private string _CITATIONINDEX = string.Empty;

                private System.Nullable<int> _REGISTER_STATUS;
                private System.Nullable<int> _NO_GUIDED;
                private System.Nullable<int> _NO_GUIDING;
                private System.Nullable<int> _GRANT_YEAR;
                private System.Nullable<int> _RESEARCH_STATUS;
                private System.Nullable<int> _PROJECT_STATUS;

                private System.Nullable<decimal> _GRANT_AMOUNT;
                private System.Nullable<decimal> _GRANT_DUE;
                private System.Nullable<decimal> _RESEARCH_AMOUNT;
                private string _PI_CO = string.Empty;
                private string _FUNDED = string.Empty;
                private string _ORDER_NO = string.Empty;

                private string _ONGOING = string.Empty;
                private string _AUTHORITY = string.Empty;

                private System.Nullable<System.DateTime> _PROJECT_SANC_DATE;
                private System.Nullable<System.DateTime> _PROJECT_COMP_DATE;
                private System.Nullable<System.DateTime> _RESEARCH_SANC_DATE;

                private string _AwardName;
                private string _Organization;
                private System.Nullable<System.DateTime> _DOACH;
                private System.Nullable<decimal> _AMOUNT_REC;
                private string _Description;
                private int _ACNO;
                private int _MPNO;
                private string _NameOfProfBody;
                private string _NatureOfWorkText;
                private string _Department;

                private string _EISSN;
                private string _PUB_ADD;
                private string _Name_org;
                private string _Name_agency;
                private int _SCNO;
                private int _AGECATNO;
                private int _ROLE;
                private string _ROLENAME;
                private string _ProjectStatus;

                private System.Nullable<System.DateTime> _CONSDATE;
                private int _SFNO;
                private string _PatentTitle;
                private string _ApplicantName;
                private int _PatentStatus;
                private string _OtherRole;
                private int _Withdrawn;
                private int _PatentCategory;
                private int _PCNO;
                private int _FILENO;
                private int _APPLICATION_NO;
                private DateTime _STATUS_DATE;

                private System.Nullable<int> _GENDER;

                private System.Nullable<int> _MaritalStatus;

                # endregion

                // Add  by Sonali Ambedare
                private int _MemberShipNo;

                private string _MemberShipType;
                private string _Certificationtype;
                private int _ThemeOfTraining;
                private int _AwardLevel;
                private string _ProjectNature;
                private string _SchemeName;
                private int _ProjectLevel;
                private string _MONTH;

                //private int _NatOfApp;
                private string _SubjectOfPatent;

                private string _DOIN;
                private int _RECNO;

                private System.Nullable<System.DateTime> _MEETINGDATE;

                #region Previous details

                //Added by Vidisha K
                //On 11-02-2020
                private string _InternalFaculty = string.Empty;

                private string _ExternalFaculty = string.Empty;
                private string _InternalStudent = string.Empty;
                private string _ExternalStudent = string.Empty;
                private string _INDEXING_TYPE = string.Empty;

                public string InternalFaculty
                {
                    get
                    {
                        return this._InternalFaculty;
                    }
                    set
                    {
                        if ((this._InternalFaculty != value))
                        {
                            this._InternalFaculty = value;
                        }
                    }
                }

                public string ExternalFaculty
                {
                    get
                    {
                        return this._ExternalFaculty;
                    }
                    set
                    {
                        if ((this._ExternalFaculty != value))
                        {
                            this._ExternalFaculty = value;
                        }
                    }
                }

                public string InternalStudent
                {
                    get
                    {
                        return this._InternalStudent;
                    }
                    set
                    {
                        if ((this._InternalStudent != value))
                        {
                            this._InternalStudent = value;
                        }
                    }
                }

                public string ExternalStudent
                {
                    get
                    {
                        return this._ExternalStudent;
                    }
                    set
                    {
                        if ((this._ExternalStudent != value))
                        {
                            this._ExternalStudent = value;
                        }
                    }
                }

                public string INDEXING_TYPE
                {
                    get
                    {
                        return this._INDEXING_TYPE;
                    }
                    set
                    {
                        if (this._INDEXING_TYPE != value)
                        {
                            this._INDEXING_TYPE = value;
                        }
                    }
                }

                #endregion Previous details

                #endregion Private Members

                #region Public Members

                //_IsJournalScopus
                public int IsJournalScopus
                {
                    get
                    {
                        return this._IsJournalScopus;
                    }
                    set
                    {
                        if ((this._IsJournalScopus != value))
                        {
                            this._IsJournalScopus = value;
                        }
                    }
                }

                public string IDCARDNO
                {
                    get
                    {
                        return this._IDCARDNO;
                    }
                    set
                    {
                        if ((this._IDCARDNO != value))
                        {
                            this._IDCARDNO = value;
                        }
                    }
                }

                public int STATUSNO
                {
                    get
                    {
                        return this._STATUSNO;
                    }
                    set
                    {
                        if ((this._STATUSNO != value))
                        {
                            this._STATUSNO = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> REG_DATE
                {
                    get
                    {
                        return this._REG_DATE;
                    }
                    set
                    {
                        if ((this._REG_DATE != value))
                        {
                            this._REG_DATE = value;
                        }
                    }
                }

                public int QUALINO
                {
                    get
                    {
                        return this._QUALINO;
                    }
                    set
                    {
                        if ((this._QUALINO != value))
                        {
                            this._QUALINO = value;
                        }
                    }
                }

                public int RECNO
                {
                    get
                    {
                        return this._RECNO;
                    }
                    set
                    {
                        if ((this._RECNO != value))
                        {
                            this._RECNO = value;
                        }
                    }
                }

                public string REG_NAME
                {
                    get
                    {
                        return this._REG_NAME;
                    }
                    set
                    {
                        if ((this._REG_NAME != value))
                        {
                            this._REG_NAME = value;
                        }
                    }
                }

                //=================================
                public string EXPERIENCE
                {
                    get
                    {
                        return this._EXPERIENCE;
                    }
                    set
                    {
                        if ((this._EXPERIENCE != value))
                        {
                            this._EXPERIENCE = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_DEPTEXAM")]
                //[Column(Storage = "_DENO", DbType = "Int NOT NULL")]
                public int DENO
                {
                    get
                    {
                        return this._DENO;
                    }
                    set
                    {
                        if ((this._DENO != value))
                        {
                            this._DENO = value;
                        }
                    }
                }

                //[Column(Storage = "_IDNO", DbType = "Int")]
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

                //[Column(Storage = "_SRNO", DbType = "Int")]
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

                //[Column(Storage = "_EXAM", DbType = "NVarChar(400)")]
                public string EXAM
                {
                    get
                    {
                        return this._EXAM;
                    }
                    set
                    {
                        if ((this._EXAM != value))
                        {
                            this._EXAM = value;
                        }
                    }
                }

                //[Column(Storage = "_REGNO", DbType = "NVarChar(25)")]
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

                //[Column(Storage = "_PASSYEAR", DbType = "NVarChar(15)")]
                public string PASSYEAR
                {
                    get
                    {
                        return this._PASSYEAR;
                    }
                    set
                    {
                        if ((this._PASSYEAR != value))
                        {
                            this._PASSYEAR = value;
                        }
                    }
                }

                //[Column(Storage = "_OFFICER", DbType = "NVarChar(35)")]
                public string OFFICER
                {
                    get
                    {
                        return this._OFFICER;
                    }
                    set
                    {
                        if ((this._OFFICER != value))
                        {
                            this._OFFICER = value;
                        }
                    }
                }

                //[Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(15)")]
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

                //[Table(Name = "pay.PAY_SB_FAMILYINFO")]

                // [Column(Storage = "_FNNO", DbType = "Int NOT NULL")]
                public int FNNO
                {
                    get
                    {
                        return this._FNNO;
                    }
                    set
                    {
                        if ((this._FNNO != value))
                        {
                            this._FNNO = value;
                        }
                    }
                }

                // [Column(Storage = "_MEMNAME", DbType = "NVarChar(50)")]
                public string MEMNAME
                {
                    get
                    {
                        return this._MEMNAME;
                    }
                    set
                    {
                        if ((this._MEMNAME != value))
                        {
                            this._MEMNAME = value;
                        }
                    }
                }

                // [Column(Storage = "_ADDRESS", DbType = "NVarChar(100)")]
                public string ADDRESS
                {
                    get
                    {
                        return this._ADDRESS;
                    }
                    set
                    {
                        if ((this._ADDRESS != value))
                        {
                            this._ADDRESS = value;
                        }
                    }
                }

                //[Column(Storage = "_RELATION", DbType = "NVarChar(25)")]
                public string RELATION
                {
                    get
                    {
                        return this._RELATION;
                    }
                    set
                    {
                        if ((this._RELATION != value))
                        {
                            this._RELATION = value;
                        }
                    }
                }

                // [Column(Storage = "_AGE", DbType = "Decimal(6,2)")]
                public System.Nullable<decimal> AGE
                {
                    get
                    {
                        return this._AGE;
                    }
                    set
                    {
                        if ((this._AGE != value))
                        {
                            this._AGE = value;
                        }
                    }
                }

                // [Column(Storage = "_DOB", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DOB
                {
                    get
                    {
                        return this._DOB;
                    }
                    set
                    {
                        if ((this._DOB != value))
                        {
                            this._DOB = value;
                        }
                    }
                }

                // [Column(Storage = "_REMARK", DbType = "NVarChar(120)")]
                public string REMARK
                {
                    get
                    {
                        return this._REMARK;
                    }
                    set
                    {
                        if ((this._REMARK != value))
                        {
                            this._REMARK = value;
                        }
                    }
                }

                public string MEETINGNAME
                {
                    get
                    {
                        return this._MEETINGNAME;
                    }
                    set
                    {
                        if ((this._MEETINGNAME != value))
                        {
                            this._MEETINGNAME = value;
                        }
                    }
                }

                public string MEETINGVENUE
                {
                    get
                    {
                        return this._MEETINGVENUE;
                    }
                    set
                    {
                        if ((this._MEETINGVENUE != value))
                        {
                            this._MEETINGVENUE = value;
                        }
                    }
                }

                // [Column(Storage = "_ADDRESS_FLAG", DbType = "INT")]
                public int ADDRESS_FLAG
                {
                    get
                    {
                        return this._ADDRESS_FLAG;
                    }
                    set
                    {
                        if ((this._ADDRESS_FLAG != value))
                        {
                            this._ADDRESS_FLAG = value;
                        }
                    }
                }

                public string EDUCATION
                {
                    get
                    {
                        return this._EDUCATION;
                    }
                    set
                    {
                        if ((this._EDUCATION != value))
                        {
                            this._EDUCATION = value;
                        }
                    }
                }

                public string EMPLOYMENT
                {
                    get
                    {
                        return this._EMPLOYMENT;
                    }
                    set
                    {
                        if ((this._EMPLOYMENT != value))
                        {
                            this._EMPLOYMENT = value;
                        }
                    }
                }

                //[Table(Name = "pay.PAY_SB_FORSERVICE")]
                //[Column(Storage = "_FSNO", DbType = "Int NOT NULL")]
                public int FSNO
                {
                    get
                    {
                        return this._FSNO;
                    }
                    set
                    {
                        if ((this._FSNO != value))
                        {
                            this._FSNO = value;
                        }
                    }
                }

                // [Column(Storage = "_POSTNAME", DbType = "NVarChar(500)")]
                public string POSTNAME
                {
                    get
                    {
                        return this._POSTNAME;
                    }
                    set
                    {
                        if ((this._POSTNAME != value))
                        {
                            this._POSTNAME = value;
                        }
                    }
                }

                // [Column(Storage = "_FDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> FDT
                {
                    get
                    {
                        return this._FDT;
                    }
                    set
                    {
                        if ((this._FDT != value))
                        {
                            this._FDT = value;
                        }
                    }
                }

                // [Column(Storage = "_TDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> TDT
                {
                    get
                    {
                        return this._TDT;
                    }
                    set
                    {
                        if ((this._TDT != value))
                        {
                            this._TDT = value;
                        }
                    }
                }

                // [Column(Storage = "_LSC", DbType = "NVarChar(500)")]
                public string LSC
                {
                    get
                    {
                        return this._LSC;
                    }
                    set
                    {
                        if ((this._LSC != value))
                        {
                            this._LSC = value;
                        }
                    }
                }

                //[Column(Storage = "_LSCR", DbType = "NVarChar(500)")]
                public string LSCR
                {
                    get
                    {
                        return this._LSCR;
                    }
                    set
                    {
                        if ((this._LSCR != value))
                        {
                            this._LSCR = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_LOAN")]

                // [Column(Storage = "_LNO", DbType = "Int NOT NULL")]
                public int LNO
                {
                    get
                    {
                        return this._LNO;
                    }
                    set
                    {
                        if ((this._LNO != value))
                        {
                            this._LNO = value;
                        }
                    }
                }

                // [Column(Storage = "_LOANNO", DbType = "Int")]
                public System.Nullable<int> LOANNO
                {
                    get
                    {
                        return this._LOANNO;
                    }
                    set
                    {
                        if ((this._LOANNO != value))
                        {
                            this._LOANNO = value;
                        }
                    }
                }

                // [Column(Storage = "_ORDERNO", DbType = "NVarChar(35)")]
                public string ORDERNO
                {
                    get
                    {
                        return this._ORDERNO;
                    }
                    set
                    {
                        if ((this._ORDERNO != value))
                        {
                            this._ORDERNO = value;
                        }
                    }
                }

                // [Column(Storage = "_AMOUNT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> AMOUNT
                {
                    get
                    {
                        return this._AMOUNT;
                    }
                    set
                    {
                        if ((this._AMOUNT != value))
                        {
                            this._AMOUNT = value;
                        }
                    }
                }

                //[Column(Storage = "_INTEREST", DbType = "Decimal(6,2)")]
                public System.Nullable<decimal> INTEREST
                {
                    get
                    {
                        return this._INTEREST;
                    }
                    set
                    {
                        if ((this._INTEREST != value))
                        {
                            this._INTEREST = value;
                        }
                    }
                }

                // [Column(Storage = "_INSTAL", DbType = "Decimal(10,2)")]
                public System.Nullable<decimal> INSTAL
                {
                    get
                    {
                        return this._INSTAL;
                    }
                    set
                    {
                        if ((this._INSTAL != value))
                        {
                            this._INSTAL = value;
                        }
                    }
                }

                // [Column(Storage = "_LOANDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> LOANDT
                {
                    get
                    {
                        return this._LOANDT;
                    }
                    set
                    {
                        if ((this._LOANDT != value))
                        {
                            this._LOANDT = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_MATTER")]
                // [Column(Storage = "_MNO", DbType = "Int NOT NULL")]
                public int MNO
                {
                    get
                    {
                        return this._MNO;
                    }
                    set
                    {
                        if ((this._MNO != value))
                        {
                            this._MNO = value;
                        }
                    }
                }

                // [Column(Storage = "_EDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> EDT
                {
                    get
                    {
                        return this._EDT;
                    }
                    set
                    {
                        if ((this._EDT != value))
                        {
                            this._EDT = value;
                        }
                    }
                }

                // [Column(Storage = "_HEADING", DbType = "NVarChar(60)")]
                public string HEADING
                {
                    get
                    {
                        return this._HEADING;
                    }
                    set
                    {
                        if ((this._HEADING != value))
                        {
                            this._HEADING = value;
                        }
                    }
                }

                // [Column(Storage = "_MATTER", DbType = "NVarChar(4000)")]
                public string MATTER
                {
                    get
                    {
                        return this._MATTER;
                    }
                    set
                    {
                        if ((this._MATTER != value))
                        {
                            this._MATTER = value;
                        }
                    }
                }

                // [Table(Name="pay.PAY_SB_NOMINIFOR")]

                // [Column(Storage = "_NFNO", DbType = "Int NOT NULL")]
                public int NFNO
                {
                    get
                    {
                        return this._NFNO;
                    }
                    set
                    {
                        if ((this._NFNO != value))
                        {
                            this._NFNO = value;
                        }
                    }
                }

                //[Column(Storage = "_NTNO", DbType = "Int")]
                public System.Nullable<int> NTNO
                {
                    get
                    {
                        return this._NTNO;
                    }
                    set
                    {
                        if ((this._NTNO != value))
                        {
                            this._NTNO = value;
                        }
                    }
                }

                //[Column(Storage = "_NAME", DbType = "NVarChar(45)")]
                public string NAME
                {
                    get
                    {
                        return this._NAME;
                    }
                    set
                    {
                        if ((this._NAME != value))
                        {
                            this._NAME = value;
                        }
                    }
                }

                // [Column(Storage = "_LAST", DbType = "Bit")]
                public System.Nullable<bool> LAST
                {
                    get
                    {
                        return this._LAST;
                    }
                    set
                    {
                        if ((this._LAST != value))
                        {
                            this._LAST = value;
                        }
                    }
                }

                // [Column(Storage = "_PER", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> PER
                {
                    get
                    {
                        return this._PER;
                    }
                    set
                    {
                        if ((this._PER != value))
                        {
                            this._PER = value;
                        }
                    }
                }

                // [Column(Storage = "_CONTING", DbType = "NVarChar(65)")]
                public string CONTING
                {
                    get
                    {
                        return this._CONTING;
                    }
                    set
                    {
                        if ((this._CONTING != value))
                        {
                            this._CONTING = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_PAYREV")]
                // [Column(Storage = "_PRNO", DbType = "Int NOT NULL")]
                public int PRNO
                {
                    get
                    {
                        return this._PRNO;
                    }
                    set
                    {
                        if ((this._PRNO != value))
                        {
                            this._PRNO = value;
                        }
                    }
                }

                // [Column(Storage = "_SUBDESIGNO", DbType = "Int")]
                public System.Nullable<int> SUBDESIGNO
                {
                    get
                    {
                        return this._SUBDESIGNO;
                    }
                    set
                    {
                        if ((this._SUBDESIGNO != value))
                        {
                            this._SUBDESIGNO = value;
                        }
                    }
                }

                // [Column(Storage = "_SCALENO", DbType = "Int")]
                public System.Nullable<int> SCALENO
                {
                    get
                    {
                        return this._SCALENO;
                    }
                    set
                    {
                        if ((this._SCALENO != value))
                        {
                            this._SCALENO = value;
                        }
                    }
                }

                // [Column(Storage = "_TYPE", DbType = "NVarChar(2)")]
                public string TYPE
                {
                    get
                    {
                        return this._TYPE;
                    }
                    set
                    {
                        if ((this._TYPE != value))
                        {
                            this._TYPE = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_PRESERVICE")]
                //[Column(Storage = "_PSNO", DbType = "Int NOT NULL")]
                public int PSNO
                {
                    get
                    {
                        return this._PSNO;
                    }
                    set
                    {
                        if ((this._PSNO != value))
                        {
                            this._PSNO = value;
                        }
                    }
                }

                // [Column(Storage = "_INST", DbType = "NVarChar(120)")]
                public string INST
                {
                    get
                    {
                        return this._INST;
                    }
                    set
                    {
                        if ((this._INST != value))
                        {
                            this._INST = value;
                        }
                    }
                }

                public string UNIVERSITY_NAME
                {
                    get
                    {
                        return this._UNIVERSITY_NAME;
                    }
                    set
                    {
                        if ((this._UNIVERSITY_NAME != value))
                        {
                            this._UNIVERSITY_NAME = value;
                        }
                    }
                }

                public string LOCATION
                {
                    get
                    {
                        return this._LOCATION;
                    }
                    set
                    {
                        if ((this._LOCATION != value))
                        {
                            this._LOCATION = value;
                        }
                    }
                }

                // [Column(Storage = "_TERMINATION", DbType = "NVarChar(120)")]
                public string TERMINATION
                {
                    get
                    {
                        return this._TERMINATION;
                    }
                    set
                    {
                        if ((this._TERMINATION != value))
                        {
                            this._TERMINATION = value;
                        }
                    }
                }

                //[Table(Name = "pay.PAY_SB_QUALI")]
                //[Column(Storage = "_QNO", DbType = "Int NOT NULL")]
                public int QNO
                {
                    get
                    {
                        return this._QNO;
                    }
                    set
                    {
                        if ((this._QNO != value))
                        {
                            this._QNO = value;
                        }
                    }
                }

                // [Column(Storage = "_QUALILEVELNO", DbType = "Int")]
                public System.Nullable<int> QUALILEVELNO
                {
                    get
                    {
                        return this._QUALILEVELNO;
                    }
                    set
                    {
                        if ((this._QUALILEVELNO != value))
                        {
                            this._QUALILEVELNO = value;
                        }
                    }
                }

                //[Column(Storage = "_EXAMNAME", DbType = "NVarChar(60)")]
                public string EXAMNAME
                {
                    get
                    {
                        return this._EXAMNAME;
                    }
                    set
                    {
                        if ((this._EXAMNAME != value))
                        {
                            this._EXAMNAME = value;
                        }
                    }
                }

                //[Column(Storage = "_SPECI", DbType = "NVarChar(4000)")]
                public string SPECI
                {
                    get
                    {
                        return this._SPECI;
                    }
                    set
                    {
                        if ((this._SPECI != value))
                        {
                            this._SPECI = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_SCALE")]

                //[Column(Storage = "_B1", DbType = "Int")]
                public System.Nullable<int> B1
                {
                    get
                    {
                        return this._B1;
                    }
                    set
                    {
                        if ((this._B1 != value))
                        {
                            this._B1 = value;
                        }
                    }
                }

                //[Column(Storage = "_I1", DbType = "Int")]
                public System.Nullable<int> I1
                {
                    get
                    {
                        return this._I1;
                    }
                    set
                    {
                        if ((this._I1 != value))
                        {
                            this._I1 = value;
                        }
                    }
                }

                //[Column(Storage = "_B2", DbType = "Int")]
                public System.Nullable<int> B2
                {
                    get
                    {
                        return this._B2;
                    }
                    set
                    {
                        if ((this._B2 != value))
                        {
                            this._B2 = value;
                        }
                    }
                }

                //[Column(Storage = "_I2", DbType = "Int")]
                public System.Nullable<int> I2
                {
                    get
                    {
                        return this._I2;
                    }
                    set
                    {
                        if ((this._I2 != value))
                        {
                            this._I2 = value;
                        }
                    }
                }

                //[Column(Storage = "_B3", DbType = "Int")]
                public System.Nullable<int> B3
                {
                    get
                    {
                        return this._B3;
                    }
                    set
                    {
                        if ((this._B3 != value))
                        {
                            this._B3 = value;
                        }
                    }
                }

                //[Column(Storage = "_I3", DbType = "Int")]
                public System.Nullable<int> I3
                {
                    get
                    {
                        return this._I3;
                    }
                    set
                    {
                        if ((this._I3 != value))
                        {
                            this._I3 = value;
                        }
                    }
                }

                //[Column(Storage = "_B4", DbType = "Int")]
                public System.Nullable<int> B4
                {
                    get
                    {
                        return this._B4;
                    }
                    set
                    {
                        if ((this._B4 != value))
                        {
                            this._B4 = value;
                        }
                    }
                }

                // [Column(Storage = "_I4", DbType = "Int")]
                public System.Nullable<int> I4
                {
                    get
                    {
                        return this._I4;
                    }
                    set
                    {
                        if ((this._I4 != value))
                        {
                            this._I4 = value;
                        }
                    }
                }

                //[Column(Storage = "_B5", DbType = "Int")]
                public System.Nullable<int> B5
                {
                    get
                    {
                        return this._B5;
                    }
                    set
                    {
                        if ((this._B5 != value))
                        {
                            this._B5 = value;
                        }
                    }
                }

                // [Column(Storage = "_I5", DbType = "Int")]
                public System.Nullable<int> I5
                {
                    get
                    {
                        return this._I5;
                    }
                    set
                    {
                        if ((this._I5 != value))
                        {
                            this._I5 = value;
                        }
                    }
                }

                //[Column(Storage = "_SCALE", DbType = "NVarChar(100)")]
                public string SCALE
                {
                    get
                    {
                        return this._SCALE;
                    }
                    set
                    {
                        if ((this._SCALE != value))
                        {
                            this._SCALE = value;
                        }
                    }
                }

                //[Column(Storage = "_GRADEPAY", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> GRADEPAY
                {
                    get
                    {
                        return this._GRADEPAY;
                    }
                    set
                    {
                        if ((this._GRADEPAY != value))
                        {
                            this._GRADEPAY = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_SERVICEBK")]

                //[Column(Storage = "_TRNO", DbType = "Int")]
                public System.Nullable<int> TRNO
                {
                    get
                    {
                        return this._TRNO;
                    }
                    set
                    {
                        if ((this._TRNO != value))
                        {
                            this._TRNO = value;
                        }
                    }
                }

                // [Column(Storage = "_DOJ", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DOJ
                {
                    get
                    {
                        return this._DOJ;
                    }
                    set
                    {
                        if ((this._DOJ != value))
                        {
                            this._DOJ = value;
                        }
                    }
                }

                // [Column(Storage = "_DOI", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DOI
                {
                    get
                    {
                        return this._DOI;
                    }
                    set
                    {
                        if ((this._DOI != value))
                        {
                            this._DOI = value;
                        }
                    }
                }

                // [Column(Storage = "_SUBDEPTNO", DbType = "Int")]
                public System.Nullable<int> SUBDEPTNO
                {
                    get
                    {
                        return this._SUBDEPTNO;
                    }
                    set
                    {
                        if ((this._SUBDEPTNO != value))
                        {
                            this._SUBDEPTNO = value;
                        }
                    }
                }

                // [Column(Storage = "_DESIGNATURENO", DbType = "Int")]
                public System.Nullable<int> DESIGNATURENO
                {
                    get
                    {
                        return this._DESIGNATURENO;
                    }
                    set
                    {
                        if ((this._DESIGNATURENO != value))
                        {
                            this._DESIGNATURENO = value;
                        }
                    }
                }

                // [Column(Storage = "_APPOINTNO", DbType = "Int")]
                public System.Nullable<int> APPOINTNO
                {
                    get
                    {
                        return this._APPOINTNO;
                    }
                    set
                    {
                        if ((this._APPOINTNO != value))
                        {
                            this._APPOINTNO = value;
                        }
                    }
                }

                //[Column(Storage = "_ORDERDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> ORDERDT
                {
                    get
                    {
                        return this._ORDERDT;
                    }
                    set
                    {
                        if ((this._ORDERDT != value))
                        {
                            this._ORDERDT = value;
                        }
                    }
                }

                // [Column(Storage = "_ORDEFFDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> ORDEFFDT
                {
                    get
                    {
                        return this._ORDEFFDT;
                    }
                    set
                    {
                        if ((this._ORDEFFDT != value))
                        {
                            this._ORDEFFDT = value;
                        }
                    }
                }

                // [Column(Storage = "_GRNO", DbType = "NVarChar(25)")]
                public string GRNO
                {
                    get
                    {
                        return this._GRNO;
                    }
                    set
                    {
                        if ((this._GRNO != value))
                        {
                            this._GRNO = value;
                        }
                    }
                }

                // [Column(Storage = "_GRDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> GRDT
                {
                    get
                    {
                        return this._GRDT;
                    }
                    set
                    {
                        if ((this._GRDT != value))
                        {
                            this._GRDT = value;
                        }
                    }
                }

                // [Column(Storage = "_OLDSCALENO", DbType = "Int")]
                public System.Nullable<int> OLDSCALENO
                {
                    get
                    {
                        return this._OLDSCALENO;
                    }
                    set
                    {
                        if ((this._OLDSCALENO != value))
                        {
                            this._OLDSCALENO = value;
                        }
                    }
                }

                // [Column(Storage = "_TYPETRANNO", DbType = "Int")]
                public System.Nullable<int> TYPETRANNO
                {
                    get
                    {
                        return this._TYPETRANNO;
                    }
                    set
                    {
                        if ((this._TYPETRANNO != value))
                        {
                            this._TYPETRANNO = value;
                        }
                    }
                }

                // [Column(Storage = "_TERMIDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> TERMIDT
                {
                    get
                    {
                        return this._TERMIDT;
                    }
                    set
                    {
                        if ((this._TERMIDT != value))
                        {
                            this._TERMIDT = value;
                        }
                    }
                }

                // [Column(Storage = "_SINGAUTO", DbType = "NVarChar(60)")]
                public string SINGAUTO
                {
                    get
                    {
                        return this._SINGAUTO;
                    }
                    set
                    {
                        if ((this._SINGAUTO != value))
                        {
                            this._SINGAUTO = value;
                        }
                    }
                }

                // [Column(Storage = "_LTCFDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> LTCFDT
                {
                    get
                    {
                        return this._LTCFDT;
                    }
                    set
                    {
                        if ((this._LTCFDT != value))
                        {
                            this._LTCFDT = value;
                        }
                    }
                }

                // [Column(Storage = "_LTCTDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> LTCTDT
                {
                    get
                    {
                        return this._LTCTDT;
                    }
                    set
                    {
                        if ((this._LTCTDT != value))
                        {
                            this._LTCTDT = value;
                        }
                    }
                }

                // [Column(Storage = "_STATUS", DbType = "NVarChar(3)")]
                public string STATUS
                {
                    get
                    {
                        return this._STATUS;
                    }
                    set
                    {
                        if ((this._STATUS != value))
                        {
                            this._STATUS = value;
                        }
                    }
                }

                // [Column(Storage = "_ETRNO", DbType = "Int")]
                public System.Nullable<int> ETRNO
                {
                    get
                    {
                        return this._ETRNO;
                    }
                    set
                    {
                        if ((this._ETRNO != value))
                        {
                            this._ETRNO = value;
                        }
                    }
                }

                // [Column(Storage = "_SEQNO", DbType = "Int")]
                public System.Nullable<int> SEQNO
                {
                    get
                    {
                        return this._SEQNO;
                    }
                    set
                    {
                        if ((this._SEQNO != value))
                        {
                            this._SEQNO = value;
                        }
                    }
                }

                // [Column(Storage = "_COMMENDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> COMMENDT
                {
                    get
                    {
                        return this._COMMENDT;
                    }
                    set
                    {
                        if ((this._COMMENDT != value))
                        {
                            this._COMMENDT = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> MEETINGDATE
                {
                    get
                    {
                        return this._MEETINGDATE;
                    }
                    set
                    {
                        if ((this._MEETINGDATE != value))
                        {
                            this._MEETINGDATE = value;
                        }
                    }
                }

                // [Column(Storage = "_COMMENREM", DbType = "NVarChar(100)")]
                public string COMMENREM
                {
                    get
                    {
                        return this._COMMENREM;
                    }
                    set
                    {
                        if ((this._COMMENREM != value))
                        {
                            this._COMMENREM = value;
                        }
                    }
                }

                //  [Column(Storage = "_PAYALLOW", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> PAYALLOW
                {
                    get
                    {
                        return this._PAYALLOW;
                    }
                    set
                    {
                        if ((this._PAYALLOW != value))
                        {
                            this._PAYALLOW = value;
                        }
                    }
                }

                //  [Column(Storage = "_TERMIREASON", DbType = "NVarChar(100)")]
                public string TERMIREASON
                {
                    get
                    {
                        return this._TERMIREASON;
                    }
                    set
                    {
                        if ((this._TERMIREASON != value))
                        {
                            this._TERMIREASON = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_SB_TRAINING")]

                // [Column(Storage = "_TNO", DbType = "Int NOT NULL")]
                public int TNO
                {
                    get
                    {
                        return this._TNO;
                    }
                    set
                    {
                        if ((this._TNO != value))
                        {
                            this._TNO = value;
                        }
                    }
                }

                // [Column(Storage = "_COURSE", DbType = "NVarChar(60)")]
                public string COURSE
                {
                    get
                    {
                        return this._COURSE;
                    }
                    set
                    {
                        if ((this._COURSE != value))
                        {
                            this._COURSE = value;
                        }
                    }
                }

                public int PROGRAM_LEVEL
                {
                    get
                    {
                        return this._PROGRAM_LEVEL;
                    }
                    set
                    {
                        if ((this._PROGRAM_LEVEL != value))
                        {
                            this._PROGRAM_LEVEL = value;
                        }
                    }
                }

                public int PROGRAM_TYPE
                {
                    get
                    {
                        return this._PROGRAM_TYPE;
                    }
                    set
                    {
                        if ((this._PROGRAM_TYPE != value))
                        {
                            this._PROGRAM_TYPE = value;
                        }
                    }
                }

                public int PARTICIPATION_TYPE
                {
                    get
                    {
                        return this._PARTICIPATION_TYPE;
                    }
                    set
                    {
                        if ((this._PARTICIPATION_TYPE != value))
                        {
                            this._PARTICIPATION_TYPE = value;
                        }
                    }
                }

                public string PRESENTATION_DETAILS
                {
                    get
                    {
                        return this._PRESENTATION_DETAILS;
                    }
                    set
                    {
                        if ((this._PRESENTATION_DETAILS != value))
                        {
                            this._PRESENTATION_DETAILS = value;
                        }
                    }
                }

                public string SPONSORED_BY
                {
                    get
                    {
                        return this._SPONSORED_BY;
                    }
                    set
                    {
                        if ((this._SPONSORED_BY != value))
                        {
                            this._SPONSORED_BY = value;
                        }
                    }
                }

                public System.Nullable<decimal> COST_INVOLVED
                {
                    get
                    {
                        return this._COST_INVOLVED;
                    }
                    set
                    {
                        if ((this._COST_INVOLVED != value))
                        {
                            this._COST_INVOLVED = value;
                        }
                    }
                }

                //[Table(Name="pay.PAY_LEAVETRAN")]

                // [Column(Storage = "_ENO", DbType = "Int NOT NULL")]
                public int ENO
                {
                    get
                    {
                        return this._ENO;
                    }
                    set
                    {
                        if ((this._ENO != value))
                        {
                            this._ENO = value;
                        }
                    }
                }

                // [Column(Storage = "_APPDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> APPDT
                {
                    get
                    {
                        return this._APPDT;
                    }
                    set
                    {
                        if ((this._APPDT != value))
                        {
                            this._APPDT = value;
                        }
                    }
                }

                //  [Column(Storage = "_LEAVES", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> LEAVES
                {
                    get
                    {
                        return this._LEAVES;
                    }
                    set
                    {
                        if ((this._LEAVES != value))
                        {
                            this._LEAVES = value;
                        }
                    }
                }

                //  [Column(Storage = "_ORDNO", DbType = "NVarChar(30)")]
                public string ORDNO
                {
                    get
                    {
                        return this._ORDNO;
                    }
                    set
                    {
                        if ((this._ORDNO != value))
                        {
                            this._ORDNO = value;
                        }
                    }
                }

                //  [Column(Storage = "_YEAR", DbType = "Int")]
                public System.Nullable<int> YEAR
                {
                    get
                    {
                        return this._YEAR;
                    }
                    set
                    {
                        if ((this._YEAR != value))
                        {
                            this._YEAR = value;
                        }
                    }
                }

                public System.Nullable<int> ST
                {
                    get
                    {
                        return this._ST;
                    }
                    set
                    {
                        if ((this._ST != value))
                        {
                            this._ST = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> END_DT
                {
                    get
                    {
                        return this._END_DT;
                    }
                    set
                    {
                        if ((this._END_DT != value))
                        {
                            this._END_DT = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> JOINDT
                {
                    get
                    {
                        return this._JOINDT;
                    }
                    set
                    {
                        if ((this._JOINDT != value))
                        {
                            this._JOINDT = value;
                        }
                    }
                }

                //_IsJournalScopus
                public System.Nullable<bool> FIT
                {
                    get
                    {
                        return this._FIT;
                    }
                    set
                    {
                        if ((this._FIT != value))
                        {
                            this._FIT = value;
                        }
                    }
                }

                public System.Nullable<bool> UNFIT
                {
                    get
                    {
                        return this._UNFIT;
                    }
                    set
                    {
                        if ((this._UNFIT != value))
                        {
                            this._UNFIT = value;
                        }
                    }
                }

                public System.Nullable<int> PERIOD
                {
                    get
                    {
                        return this._PERIOD;
                    }
                    set
                    {
                        if ((this._PERIOD != value))
                        {
                            this._PERIOD = value;
                        }
                    }
                }

                public System.Nullable<bool> FNAN
                {
                    get
                    {
                        return this._FNAN;
                    }
                    set
                    {
                        if ((this._FNAN != value))
                        {
                            this._FNAN = value;
                        }
                    }
                }

                //[Table(Name = "pay.PAY_EMP_IMAGE")]

                public int imagetrxid
                {
                    get
                    {
                        return this._imagetrxid;
                    }
                    set
                    {
                        if ((this._imagetrxid != value))
                        {
                            this._imagetrxid = value;
                        }
                    }
                }

                public System.Nullable<int> imageid
                {
                    get
                    {
                        return this._imageid;
                    }
                    set
                    {
                        if ((this._imageid != value))
                        {
                            this._imageid = value;
                        }
                    }
                }

                public string imagetype
                {
                    get
                    {
                        return this._imagetype;
                    }
                    set
                    {
                        if ((this._imagetype != value))
                        {
                            this._imagetype = value;
                        }
                    }
                }

                public byte[] empimage
                {
                    get
                    {
                        return this._empimage;
                    }
                    set
                    {
                        if ((this._empimage != value))
                        {
                            this._empimage = value;
                        }
                    }
                }

                // [Column(Storage = "_ADMINTRXNO", DbType = "Int")]
                public System.Nullable<int> ADMINTRXNO
                {
                    get
                    {
                        return this._ADMINTRXNO;
                    }
                    set
                    {
                        if ((this._ADMINTRXNO != value))
                        {
                            this._ADMINTRXNO = value;
                        }
                    }
                }

                //[Column(Storage = "_RESPONSIBILITY", DbType = "NVarChar(50)")]
                public string RESPONSIBILITY
                {
                    get
                    {
                        return this._RESPONSIBILITY;
                    }
                    set
                    {
                        if ((this._RESPONSIBILITY != value))
                        {
                            this._RESPONSIBILITY = value;
                        }
                    }
                }

                //[Column(Storage = "_ORGANIZATION", DbType = "NVarChar(50)")]
                public string ORGANIZATION
                {
                    get
                    {
                        return this._ORGANIZATION;
                    }
                    set
                    {
                        if ((this._ORGANIZATION != value))
                        {
                            this._ORGANIZATION = value;
                        }
                    }
                }

                //[Column(Storage = "_FROMDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> FROMDATE
                {
                    get
                    {
                        return this._FROMDATE;
                    }
                    set
                    {
                        if ((this._FROMDATE != value))
                        {
                            this._FROMDATE = value;
                        }
                    }
                }

                //[Column(Storage = "_TODATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> TODATE
                {
                    get
                    {
                        return this._TODATE;
                    }
                    set
                    {
                        if ((this._TODATE != value))
                        {
                            this._TODATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> SUBMISSIONDATE
                {
                    get
                    {
                        return this._SUBMISSIONDATE;
                    }
                    set
                    {
                        if ((this._SUBMISSIONDATE != value))
                        {
                            this._SUBMISSIONDATE = value;
                        }
                    }
                }

                // [Column(Storage = "_PUBTRXNO", DbType = "Int")]
                public System.Nullable<int> PUBTRXNO
                {
                    get
                    {
                        return this._PUBTRXNO;
                    }
                    set
                    {
                        if ((this._PUBTRXNO != value))
                        {
                            this._PUBTRXNO = value;
                        }
                    }
                }

                //[Column(Storage = "_PUBLICATION", DbType = "NVarChar(15)")]
                public string PUBLICATION
                {
                    get
                    {
                        return this._PUBLICATION;
                    }
                    set
                    {
                        if ((this._PUBLICATION != value))
                        {
                            this._PUBLICATION = value;
                        }
                    }
                }

                //[Column(Storage = "PUBLICATION_TYPE", DbType = "NVarChar(15)")]
                public string PUBLICATION_TYPE
                {
                    get
                    {
                        return this._PUBLICATION_TYPE;
                    }
                    set
                    {
                        if ((this._PUBLICATION_TYPE != value))
                        {
                            this._PUBLICATION_TYPE = value;
                        }
                    }
                }

                //[Column(Storage = "_AUTHOR2", DbType = "NVarChar(50)")]
                public string AUTHOR2
                {
                    get
                    {
                        return this._AUTHOR2;
                    }
                    set
                    {
                        if ((this._AUTHOR2 != value))
                        {
                            this._AUTHOR2 = value;
                        }
                    }
                }

                //[Column(Storage = "_AUTHOR2", DbType = "NVarChar(50)")]
                public string AUTHOR3
                {
                    get
                    {
                        return this._AUTHOR3;
                    }
                    set
                    {
                        if ((this._AUTHOR3 != value))
                        {
                            this._AUTHOR3 = value;
                        }
                    }
                }

                //[Column(Storage = "_CONNAME", DbType = "NVarChar(50)")]
                public string CONFERENCE_NAME
                {
                    get
                    {
                        return this._CONNAME;
                    }
                    set
                    {
                        if ((this._CONNAME != value))
                        {
                            this._CONNAME = value;
                        }
                    }
                }

                //[Column(Storage = "_ORGANISOR", DbType = "NVarChar(50)")]
                public string ORGANISOR
                {
                    get
                    {
                        return this._ORGANISOR;
                    }
                    set
                    {
                        if ((this._ORGANISOR != value))
                        {
                            this._ORGANISOR = value;
                        }
                    }
                }

                //[Column(Storage = "_PAGENO", DbType = "NVarChar(50)")]
                public string PAGENO
                {
                    get
                    {
                        return this._PAGENO;
                    }
                    set
                    {
                        if ((this._PAGENO != value))
                        {
                            this._PAGENO = value;
                        }
                    }
                }

                //[Column(Storage = "_TITLE", DbType = "NVarChar(100)")]
                public string TITLE
                {
                    get
                    {
                        return this._TITLE;
                    }
                    set
                    {
                        if ((this._TITLE != value))
                        {
                            this._TITLE = value;
                        }
                    }
                }

                // [Column(Storage = "_SUBJECT", DbType = "NVarChar(100)")]
                public string SUBJECT
                {
                    get
                    {
                        return this._SUBJECT;
                    }
                    set
                    {
                        if ((this._SUBJECT != value))
                        {
                            this._SUBJECT = value;
                        }
                    }
                }

                // [Column(Storage = "_PUBLICATIONDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> PUBLICATIONDATE
                {
                    get
                    {
                        return this._PUBLICATIONDATE;
                    }
                    set
                    {
                        if ((this._PUBLICATIONDATE != value))
                        {
                            this._PUBLICATIONDATE = value;
                        }
                    }
                }

                // [Column(Storage = "_DETAILS", DbType = "NVarChar(1000)")]
                public string DETAILS
                {
                    get
                    {
                        return this._DETAILS;
                    }
                    set
                    {
                        if ((this._DETAILS != value))
                        {
                            this._DETAILS = value;
                        }
                    }
                }

                //[Column(Storage = "_ATTACHMENTS", DbType = "NVarChar(MAX)")]
                public string ATTACHMENTS
                {
                    get
                    {
                        return this._ATTACHMENTS;
                    }
                    set
                    {
                        if ((this._ATTACHMENTS != value))
                        {
                            this._ATTACHMENTS = value;
                        }
                    }
                }

                // [Column(Storage = "_INVTRXNO", DbType = "Int")]
                public System.Nullable<int> INVTRXNO
                {
                    get
                    {
                        return this._INVTRXNO;
                    }
                    set
                    {
                        if ((this._INVTRXNO != value))
                        {
                            this._INVTRXNO = value;
                        }
                    }
                }

                //[Column(Storage = "_VENU", DbType = "NVarChar(100)")]
                public string VENU
                {
                    get
                    {
                        return this._VENU;
                    }
                    set
                    {
                        if ((this._VENU != value))
                        {
                            this._VENU = value;
                        }
                    }
                }

                //[Column(Storage = "_DURATION", DbType = "NVarChar(10)")]
                public string DURATION
                {
                    get
                    {
                        return this._DURATION;
                    }
                    set
                    {
                        if ((this._DURATION != value))
                        {
                            this._DURATION = value;
                        }
                    }
                }

                //[Column(Storage = "_DATEOFTALK", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DATEOFTALK
                {
                    get
                    {
                        return this._DATEOFTALK;
                    }
                    set
                    {
                        if ((this._DATEOFTALK != value))
                        {
                            this._DATEOFTALK = value;
                        }
                    }
                }

                public string QSTATUS
                {
                    get
                    {
                        return this._QSTATUS;
                    }
                    set
                    {
                        if ((this._QSTATUS != value))
                        {
                            this._QSTATUS = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> ENTRYDATE
                {
                    get
                    {
                        return this._ENTRYDATE;
                    }
                    set
                    {
                        if ((this._ENTRYDATE != value))
                        {
                            this._ENTRYDATE = value;
                        }
                    }
                }

                public string GUIDENAME
                {
                    get
                    {
                        return this._GUIDENAME;
                    }
                    set
                    {
                        if ((this._GUIDENAME != value))
                        {
                            this._GUIDENAME = value;
                        }
                    }
                }

                public string CENTERNAME
                {
                    get
                    {
                        return this._CENTERNAME;
                    }
                    set
                    {
                        if ((this._CENTERNAME != value))
                        {
                            this._CENTERNAME = value;
                        }
                    }
                }

                public string CATEGORY
                {
                    get
                    {
                        return this._CATEGORY;
                    }
                    set
                    {
                        if ((this._CATEGORY != value))
                        {
                            this._CATEGORY = value;
                        }
                    }
                }

                public string AUTHOR4
                {
                    get
                    {
                        return this._AUTHOR4;
                    }
                    set
                    {
                        if ((this._AUTHOR4 != value))
                        {
                            this._AUTHOR4 = value;
                        }
                    }
                }

                public string ISBN
                {
                    get
                    {
                        return this._ISBN;
                    }
                    set
                    {
                        if ((this._ISBN != value))
                        {
                            this._ISBN = value;
                        }
                    }
                }

                // [Column(Storage = "IsConference", DbType = "int")]
                public int IS_CONFERENCE
                {
                    get
                    {
                        return this._IS_CONFERENCE;
                    }
                    set
                    {
                        if ((this._IS_CONFERENCE != value))
                        {
                            this._IS_CONFERENCE = value;
                        }
                    }
                }

                public string VOLUME_NO
                {
                    get
                    {
                        return this._VOLUME_NO;
                    }
                    set
                    {
                        if ((this._VOLUME_NO != value))
                        {
                            this._VOLUME_NO = value;
                        }
                    }
                }

                public string PUB_STATUS
                {
                    get
                    {
                        return this._PUB_STATUS;
                    }
                    set
                    {
                        if ((this._PUB_STATUS != value))
                        {
                            this._PUB_STATUS = value;
                        }
                    }
                }

                public string ISSUE_NO
                {
                    get
                    {
                        return this._ISSUE_NO;
                    }
                    set
                    {
                        if ((this._ISSUE_NO != value))
                        {
                            this._ISSUE_NO = value;
                        }
                    }
                }

                public string PUBLISHER
                {
                    get
                    {
                        return this._PUBLISHER;
                    }
                    set
                    {
                        if ((this._PUBLISHER != value))
                        {
                            this._PUBLISHER = value;
                        }
                    }
                }

                public string EXPERIENCETYPE
                {
                    get
                    {
                        return this._EXPERIENCETYPE;
                    }
                    set
                    {
                        if ((this._EXPERIENCETYPE != value))
                        {
                            this._EXPERIENCETYPE = value;
                        }
                    }
                }

                public string NATUREOFWORK
                {
                    get
                    {
                        return this._NATUREOFWORK;
                    }
                    set
                    {
                        if ((this._NATUREOFWORK != value))
                        {
                            this._NATUREOFWORK = value;
                        }
                    }
                }

                public System.Nullable<decimal> SPONSORED_AMOUNT
                {
                    get
                    {
                        return this._SPONSORED_AMOUNT;
                    }
                    set
                    {
                        if ((this._SPONSORED_AMOUNT != value))
                        {
                            this._SPONSORED_AMOUNT = value;
                        }
                    }
                }

                public System.Nullable<decimal> NOOFPARTI
                {
                    get
                    {
                        return this._NOOFPARTI;
                    }
                    set
                    {
                        if ((this._NOOFPARTI != value))
                        {
                            this._NOOFPARTI = value;
                        }
                    }
                }

                public int NOOFPARTICIPANT
                {
                    get
                    {
                        return this._NOOFPARTICIPANT;
                    }
                    set
                    {
                        if ((this._NOOFPARTICIPANT != value))
                        {
                            this._NOOFPARTICIPANT = value;
                        }
                    }
                }

                public string NAMEOFUNI
                {
                    get
                    {
                        return this._NAMEOFUNI;
                    }
                    set
                    {
                        if ((this._NAMEOFUNI != value))
                        {
                            this._NAMEOFUNI = value;
                        }
                    }
                }

                public string SPONSOREDBY
                {
                    get
                    {
                        return this._SPONSOREDBY;
                    }
                    set
                    {
                        if ((this._SPONSOREDBY != value))
                        {
                            this._SPONSOREDBY = value;
                        }
                    }
                }

                public string PAYSCALE
                {
                    get
                    {
                        return this._PAYSCALE;
                    }
                    set
                    {
                        if ((this._PAYSCALE != value))
                        {
                            this._PAYSCALE = value;
                        }
                    }
                }

                public System.Nullable<bool> EligibleCandidate
                {
                    get
                    {
                        return this._EligibleCandidate;
                    }
                    set
                    {
                        if ((this._EligibleCandidate != value))
                        {
                            this._EligibleCandidate = value;
                        }
                    }
                }

                public System.Nullable<bool> fulfilservice
                {
                    get
                    {
                        return this._fulfilservice;
                    }
                    set
                    {
                        if ((this._fulfilservice != value))
                        {
                            this._fulfilservice = value;
                        }
                    }
                }

                public string Grade
                {
                    get
                    {
                        return this._Grade;
                    }
                    set
                    {
                        if ((this._Grade != value))
                        {
                            this._Grade = value;
                        }
                    }
                }

                public System.Nullable<decimal> Percentage
                {
                    get
                    {
                        return this._Percentage;
                    }
                    set
                    {
                        if ((this._Percentage != value))
                        {
                            this._Percentage = value;
                        }
                    }
                }

                public System.Nullable<int> UNITYPE
                {
                    get
                    {
                        return this._UNITYPE;
                    }
                    set
                    {
                        if ((this._UNITYPE != value))
                        {
                            this._UNITYPE = value;
                        }
                    }
                }

                public System.Nullable<int> INITYPE
                {
                    get
                    {
                        return this._INITYPE;
                    }
                    set
                    {
                        if ((this._INITYPE != value))
                        {
                            this._INITYPE = value;
                        }
                    }
                }

                public System.Nullable<decimal> LASTSALARY
                {
                    get
                    {
                        return this._LASTSALARY;
                    }
                    set
                    {
                        if ((this._LASTSALARY != value))
                        {
                            this._LASTSALARY = value;
                        }
                    }
                }

                public string IMPACTFACTORS
                {
                    get
                    {
                        return this._IMPACTFACTORS;
                    }
                    set
                    {
                        if ((this._IMPACTFACTORS != value))
                        {
                            this._IMPACTFACTORS = value;
                        }
                    }
                }

                public string CITATIONINDEX
                {
                    get
                    {
                        return this._CITATIONINDEX;
                    }
                    set
                    {
                        if ((this._CITATIONINDEX != value))
                        {
                            this._CITATIONINDEX = value;
                        }
                    }
                }

                public System.Nullable<int> REGISTER_STATUS
                {
                    get
                    {
                        return this._REGISTER_STATUS;
                    }
                    set
                    {
                        if ((this._REGISTER_STATUS != value))
                        {
                            this._REGISTER_STATUS = value;
                        }
                    }
                }

                public System.Nullable<int> NO_GUIDED
                {
                    get
                    {
                        return this._NO_GUIDED;
                    }
                    set
                    {
                        if ((this._NO_GUIDED != value))
                        {
                            this._NO_GUIDED = value;
                        }
                    }
                }

                public System.Nullable<int> NO_GUIDING
                {
                    get
                    {
                        return this._NO_GUIDING;
                    }
                    set
                    {
                        if ((this._NO_GUIDING != value))
                        {
                            this._NO_GUIDING = value;
                        }
                    }
                }

                public System.Nullable<int> GRANT_YEAR
                {
                    get
                    {
                        return this._GRANT_YEAR;
                    }
                    set
                    {
                        if ((this._GRANT_YEAR != value))
                        {
                            this._GRANT_YEAR = value;
                        }
                    }
                }

                public System.Nullable<int> RESEARCH_STATUS
                {
                    get
                    {
                        return this._RESEARCH_STATUS;
                    }
                    set
                    {
                        if ((this._RESEARCH_STATUS != value))
                        {
                            this._RESEARCH_STATUS = value;
                        }
                    }
                }

                public System.Nullable<int> PROJECT_STATUS
                {
                    get
                    {
                        return this._PROJECT_STATUS;
                    }
                    set
                    {
                        if ((this._PROJECT_STATUS != value))
                        {
                            this._PROJECT_STATUS = value;
                        }
                    }
                }

                public System.Nullable<decimal> RESEARCH_AMOUNT
                {
                    get
                    {
                        return this._RESEARCH_AMOUNT;
                    }
                    set
                    {
                        if ((this._RESEARCH_AMOUNT != value))
                        {
                            this._RESEARCH_AMOUNT = value;
                        }
                    }
                }

                public System.Nullable<decimal> GRANT_DUE
                {
                    get
                    {
                        return this._GRANT_DUE;
                    }
                    set
                    {
                        if ((this._GRANT_DUE != value))
                        {
                            this._GRANT_DUE = value;
                        }
                    }
                }

                public System.Nullable<decimal> GRANT_AMOUNT
                {
                    get
                    {
                        return this._GRANT_AMOUNT;
                    }
                    set
                    {
                        if ((this._GRANT_AMOUNT != value))
                        {
                            this._GRANT_AMOUNT = value;
                        }
                    }
                }

                public string FUNDED
                {
                    get
                    {
                        return this._FUNDED;
                    }
                    set
                    {
                        if ((this._FUNDED != value))
                        {
                            this._FUNDED = value;
                        }
                    }
                }

                public string PI_CO
                {
                    get
                    {
                        return this._PI_CO;
                    }
                    set
                    {
                        if ((this._PI_CO != value))
                        {
                            this._PI_CO = value;
                        }
                    }
                }

                public string ORDER_NO
                {
                    get
                    {
                        return this._ORDER_NO;
                    }
                    set
                    {
                        if ((this._ORDER_NO != value))
                        {
                            this._ORDER_NO = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> PROJECT_SANC_DATE
                {
                    get
                    {
                        return this._PROJECT_SANC_DATE;
                    }
                    set
                    {
                        if ((this._PROJECT_SANC_DATE != value))
                        {
                            this._PROJECT_SANC_DATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> PROJECT_COMP_DATE
                {
                    get
                    {
                        return this._PROJECT_COMP_DATE;
                    }
                    set
                    {
                        if ((this._PROJECT_COMP_DATE != value))
                        {
                            this._PROJECT_COMP_DATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> RESEARCH_SANC_DATE
                {
                    get
                    {
                        return this._RESEARCH_SANC_DATE;
                    }
                    set
                    {
                        if ((this._RESEARCH_SANC_DATE != value))
                        {
                            this._RESEARCH_SANC_DATE = value;
                        }
                    }
                }

                public string ONGOING
                {
                    get
                    {
                        return this._ONGOING;
                    }
                    set
                    {
                        if ((this._ONGOING != value))
                        {
                            this._ONGOING = value;
                        }
                    }
                }

                public string AUTHORITY
                {
                    get
                    {
                        return this._AUTHORITY;
                    }
                    set
                    {
                        if ((this._AUTHORITY != value))
                        {
                            this._AUTHORITY = value;
                        }
                    }
                }

                //Newly Added By Shrikant Bharne.
                public string AwardName
                {
                    get
                    {
                        return this._AwardName;
                    }
                    set
                    {
                        if ((this._AwardName != value))
                        {
                            this._AwardName = value;
                        }
                    }
                }

                public string OrganizationAdd
                {
                    get
                    {
                        return this._Organization;
                    }
                    set
                    {
                        if ((this._Organization != value))
                        {
                            this._Organization = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> DOACH
                {
                    get
                    {
                        return this._DOACH;
                    }
                    set
                    {
                        if ((this._DOACH != value))
                        {
                            this._DOACH = value;
                        }
                    }
                }

                public System.Nullable<decimal> AMOUNT_REC
                {
                    get
                    {
                        return this._AMOUNT_REC;
                    }
                    set
                    {
                        if ((this._AMOUNT_REC != value))
                        {
                            this._AMOUNT_REC = value;
                        }
                    }
                }

                public string Description
                {
                    get
                    {
                        return this._Description;
                    }
                    set
                    {
                        if ((this._Description != value))
                        {
                            this._Description = value;
                        }
                    }
                }

                public int ACNO
                {
                    get
                    {
                        return this._ACNO;
                    }
                    set
                    {
                        if ((this._ACNO != value))
                        {
                            this._ACNO = value;
                        }
                    }
                }

                public string NameOfProfBody
                {
                    get
                    {
                        return this._NameOfProfBody;
                    }
                    set
                    {
                        if ((this._NameOfProfBody != value))
                        {
                            this._NameOfProfBody = value;
                        }
                    }
                }

                public int MPNO
                {
                    get
                    {
                        return this._MPNO;
                    }
                    set
                    {
                        if ((this._MPNO != value))
                        {
                            this._MPNO = value;
                        }
                    }
                }

                public string NatureOfWorkText
                {
                    get
                    {
                        return this._NatureOfWorkText;
                    }
                    set
                    {
                        if ((this._NatureOfWorkText != value))
                        {
                            this._NatureOfWorkText = value;
                        }
                    }
                }

                public string Department
                {
                    get
                    {
                        return this._Department;
                    }
                    set
                    {
                        if ((this._Department != value))
                        {
                            this._Department = value;
                        }
                    }
                }

                public string EISSN
                {
                    get
                    {
                        return this._EISSN;
                    }
                    set
                    {
                        if ((this._EISSN != value))
                        {
                            this._EISSN = value;
                        }
                    }
                }

                public string PUB_ADD
                {
                    get
                    {
                        return this._PUB_ADD;
                    }
                    set
                    {
                        if ((this._PUB_ADD != value))
                        {
                            this._PUB_ADD = value;
                        }
                    }
                }

                public string Name_org
                {
                    get
                    {
                        return this._Name_org;
                    }
                    set
                    {
                        if ((this._Name_org != value))
                        {
                            this._Name_org = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> CONSDATE
                {
                    get
                    {
                        return this._CONSDATE;
                    }
                    set
                    {
                        if ((this._CONSDATE != value))
                        {
                            this._CONSDATE = value;
                        }
                    }
                }

                public int SCNO
                {
                    get
                    {
                        return this._SCNO;
                    }
                    set
                    {
                        if ((this._SCNO != value))
                        {
                            this._SCNO = value;
                        }
                    }
                }

                public string Name_agency
                {
                    get
                    {
                        return this._Name_agency;
                    }
                    set
                    {
                        if ((this._Name_agency != value))
                        {
                            this._Name_agency = value;
                        }
                    }
                }

                public int AGECATNO
                {
                    get
                    {
                        return this._AGECATNO;
                    }
                    set
                    {
                        if ((this._AGECATNO != value))
                        {
                            this._AGECATNO = value;
                        }
                    }
                }

                public int ROLE
                {
                    get
                    {
                        return this._ROLE;
                    }
                    set
                    {
                        if ((this._ROLE != value))
                        {
                            this._ROLE = value;
                        }
                    }
                }

                public string ROLENAME
                {
                    get
                    {
                        return this._ROLENAME;
                    }
                    set
                    {
                        if ((this._ROLENAME != value))
                        {
                            this._ROLENAME = value;
                        }
                    }
                }

                public string ProjectStatus
                {
                    get
                    {
                        return this._ProjectStatus;
                    }
                    set
                    {
                        if ((this._ProjectStatus != value))
                        {
                            this._ProjectStatus = value;
                        }
                    }
                }

                public int SFNO
                {
                    get
                    {
                        return this._SFNO;
                    }
                    set
                    {
                        if ((this._SFNO != value))
                        {
                            this._SFNO = value;
                        }
                    }
                }

                public string PatentTitle
                {
                    get
                    {
                        return this._PatentTitle;
                    }
                    set
                    {
                        if ((this._PatentTitle != value))
                        {
                            this._PatentTitle = value;
                        }
                    }
                }

                public string ApplicantName
                {
                    get
                    {
                        return this._ApplicantName;
                    }
                    set
                    {
                        if ((this._ApplicantName != value))
                        {
                            this._ApplicantName = value;
                        }
                    }
                }

                public int PatentStatus
                {
                    get
                    {
                        return this._PatentStatus;
                    }
                    set
                    {
                        if ((this._PatentStatus != value))
                        {
                            this._PatentStatus = value;
                        }
                    }
                }

                public string OtherRole
                {
                    get
                    {
                        return this._OtherRole;
                    }
                    set
                    {
                        if ((this._OtherRole != value))
                        {
                            this._OtherRole = value;
                        }
                    }
                }

                public int Withdrawn
                {
                    get
                    {
                        return this._Withdrawn;
                    }
                    set
                    {
                        if ((this._Withdrawn != value))
                        {
                            this._Withdrawn = value;
                        }
                    }
                }

                public int PatentCategory
                {
                    get
                    {
                        return this._PatentCategory;
                    }
                    set
                    {
                        if ((this._PatentCategory != value))
                        {
                            this._PatentCategory = value;
                        }
                    }
                }

                public int PCNO
                {
                    get
                    {
                        return this._PCNO;
                    }
                    set
                    {
                        if ((this._PCNO != value))
                        {
                            this._PCNO = value;
                        }
                    }
                }

                public int FILENO
                {
                    get
                    {
                        return this._FILENO;
                    }
                    set
                    {
                        if ((this._FILENO != value))
                        {
                            this._FILENO = value;
                        }
                    }
                }

                public int APPLICATION_NO
                {
                    get
                    {
                        return this._APPLICATION_NO;
                    }
                    set
                    {
                        if ((this._APPLICATION_NO != value))
                        {
                            this._APPLICATION_NO = value;
                        }
                    }
                }

                public DateTime STATUS_DATE
                {
                    get
                    {
                        return _STATUS_DATE;
                    }
                    set
                    {
                        if ((this._STATUS_DATE != value))
                        {
                            _STATUS_DATE = value;
                        }
                    }
                }

                //

                public int DepID
                {
                    get
                    {
                        return _DepID;
                    }
                    set
                    {
                        if ((this._DepID != value))
                        {
                            _DepID = value;
                        }
                    }
                }

                public int DesID
                {
                    get
                    {
                        return _DesID;
                    }
                    set
                    {
                        if ((this._DesID != value))
                        {
                            _DesID = value;
                        }
                    }
                }

                public string NatOfApp
                {
                    get
                    {
                        return _NatOfApp;
                    }
                    set
                    {
                        if ((this._NatOfApp != value))
                        {
                            _NatOfApp = value;
                        }
                    }
                }

                public int IsCurrent
                {
                    get
                    {
                        return _IsCurrent;
                    }
                    set
                    {
                        if ((this._IsCurrent != value))
                        {
                            _IsCurrent = value;
                        }
                    }
                }

                public DateTime StartDate
                {
                    get
                    {
                        return _StartDate;
                    }
                    set
                    {
                        if ((this._StartDate != value))
                        {
                            _StartDate = value;
                        }
                    }
                }

                public DateTime EndDate
                {
                    get
                    {
                        return _EndDate;
                    }
                    set
                    {
                        if ((this._EndDate != value))
                        {
                            _EndDate = value;
                        }
                    }
                }

                public string Duration
                {
                    get
                    {
                        return _Duration;
                    }
                    set
                    {
                        if ((this._Duration != value))
                        {
                            _Duration = value;
                        }
                    }
                }

                public string Attachments
                {
                    get
                    {
                        return _Attachments;
                    }
                    set
                    {
                        if ((this._Attachments != value))
                        {
                            _Attachments = value;
                        }
                    }
                }

                public int CollegeCode
                {
                    get
                    {
                        return _CollegeCode;
                    }
                    set
                    {
                        if ((this._CollegeCode != value))
                        {
                            _CollegeCode = value;
                        }
                    }
                }

                public string Action
                {
                    get
                    {
                        return _Action;
                    }
                    set
                    {
                        if ((this._Action != value))
                        {
                            _Action = value;
                        }
                    }
                }

                public int ID
                {
                    get
                    {
                        return _ID;
                    }
                    set
                    {
                        if ((this._ID != value))
                        {
                            _ID = value;
                        }
                    }
                }

                public int SVCNO
                {
                    get
                    {
                        return this._SVCNO;
                    }
                    set
                    {
                        if ((this._SVCNO != value))
                        {
                            this._SVCNO = value;
                        }
                    }
                }

                public System.Nullable<int> GENDER
                {
                    get
                    {
                        return this._GENDER;
                    }
                    set
                    {
                        if ((this._GENDER != value))
                        {
                            this._GENDER = value;
                        }
                    }
                }

                public System.Nullable<int> MaritalStatus
                {
                    get
                    {
                        return this._MaritalStatus;
                    }
                    set
                    {
                        if ((this._MaritalStatus != value))
                        {
                            this._MaritalStatus = value;
                        }
                    }
                }

                public string HIndex
                {
                    get
                    {
                        return this._HIndex;
                    }
                    set
                    {
                        if ((this._HIndex != value))
                        {
                            this._HIndex = value;
                        }
                    }
                }

                public int MOSNO
                {
                    get
                    {
                        return this._MOSNO;
                    }
                    set
                    {
                        if ((this._MOSNO != value))
                        {
                            this._MOSNO = value;
                        }
                    }
                }

                public string Bond
                {
                    get
                    {
                        return this._Bond;
                    }
                    set
                    {
                        if ((this._Bond != value))
                        {
                            this._Bond = value;
                        }
                    }
                }

                public int PHDGUIDED
                {
                    get
                    {
                        return this._PHDGUIDED;
                    }
                    set
                    {
                        if ((this._PHDGUIDED != value))
                        {
                            this._PHDGUIDED = value;
                        }
                    }
                }

                public int PHDAWARD
                {
                    get
                    {
                        return this._PHDAWARD;
                    }
                    set
                    {
                        if ((this._PHDAWARD != value))
                        {
                            this._PHDAWARD = value;
                        }
                    }
                }

                //Add By Sonali Ambedare on 09-01-20
                public int MemberShipNo
                {
                    get
                    {
                        return this._MemberShipNo;
                    }
                    set
                    {
                        if ((this.MemberShipNo != value))
                        {
                            this._MemberShipNo = value;
                        }
                    }
                }

                public string MemberShipType
                {
                    get
                    {
                        return this._MemberShipType;
                    }
                    set
                    {
                        if ((this._MemberShipType != value))
                        {
                            this._MemberShipType = value;
                        }
                    }
                }

                public string CertificationType
                {
                    get
                    {
                        return this._Certificationtype;
                    }
                    set
                    {
                        if ((this._Certificationtype != value))
                        {
                            this._Certificationtype = value;
                        }
                    }
                }

                public int ThemeOfTraining
                {
                    get
                    {
                        return this._ThemeOfTraining;
                    }
                    set
                    {
                        if ((this._ThemeOfTraining != value))
                        {
                            this._ThemeOfTraining = value;
                        }
                    }
                }

                public int Awardlevel
                {
                    get
                    {
                        return this._AwardLevel;
                    }
                    set
                    {
                        if ((this._AwardLevel != value))
                        {
                            this._AwardLevel = value;
                        }
                    }
                }

                public int ProjectLevel
                {
                    get
                    {
                        return this._ProjectLevel;
                    }
                    set
                    {
                        if ((this._ProjectLevel != value))
                        {
                            this._ProjectLevel = value;
                        }
                    }
                }

                public string ProjectNature
                {
                    get
                    {
                        return this._ProjectNature;
                    }
                    set
                    {
                        if ((this._ProjectNature != value))
                        {
                            this._ProjectNature = value;
                        }
                    }
                }

                public string SchemeName
                {
                    get
                    {
                        return this._SchemeName;
                    }
                    set
                    {
                        if ((this._SchemeName != value))
                        {
                            this._SchemeName = value;
                        }
                    }
                }

                public string MONTH
                {
                    get
                    {
                        return this._MONTH;
                    }
                    set
                    {
                        if ((this._MONTH != value))
                        {
                            this._MONTH = value;
                        }
                    }
                }

                public string SubjectOfPatent
                {
                    get
                    {
                        return this._SubjectOfPatent;
                    }
                    set
                    {
                        if ((this._SubjectOfPatent != value))
                        {
                            this._SubjectOfPatent = value;
                        }
                    }
                }

                public string DOIN
                {
                    get
                    {
                        return this._DOIN;
                    }
                    set
                    {
                        if ((this._DOIN != value))
                        {
                            this._DOIN = value;
                        }
                    }
                }

                // DataTable Entity for Oral Examination
                public DataTable Phd_Oral_Exam { get; set; }

                //DataTable Entity for Viva Voce
                public DataTable Phd_Viva_Voce { get; set; }

                #endregion Public Members

                //Viva Voce Details
                public DateTime VivaDate { get; set; }

                public String Venue { get; set; }
                public String InternalExaminer { get; set; }
                public String ExternalExaminer { get; set; }
                public String VivaComments { get; set; }
                public String VivaStatus { get; set; }
                public String Filename { get; set; }

                //Oral Exam Details
                public DateTime Oralexamdate { get; set; }

                public String OralVenue { get; set; }
                public String OralComment { get; set; }
            }
        }//end namespace  BusinessLogicLayer.BusinessEntities
    }//end namespace UAIMS
}//end namespace IITMS