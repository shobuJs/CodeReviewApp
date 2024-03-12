//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : MARK ENTRY
// CREATION DATE : 01-AUG-2010
// CREATED BY    : SANAJY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class MarkEntry
            {
                #region Private Member

                private int _idno = 0;
                private string _regno = string.Empty;
                private int _sessionNo = 0;
                private int _schemeNo = 0;
                private int _semesterNo = 0;
                private int _sectionno = 0;
                private DateTime _resultDate = DateTime.MinValue;
                private string _collegeCode = string.Empty;
                private int _rCredits = 0;
                private int _eCredits = 0;
                private decimal _sgpa = 0.0m;
                private decimal _egp = 0.0m;
                private decimal _cEgp = 0.0m;
                private decimal _cgpa = 0.0m;
                private string _resultPF = string.Empty;
                private string _result = string.Empty;
                private int _totSubject = 0;
                private string _studName = string.Empty;

                #endregion Private Member

                #region Public Property Fields

                public int SessionNo
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeNo; }
                    set { _schemeNo = value; }
                }

                public int SemesterNo
                {
                    get { return _semesterNo; }
                    set { _semesterNo = value; }
                }

                public int SectionNo
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }

                public DateTime ResultDate
                {
                    get { return _resultDate; }
                    set { _resultDate = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                public decimal Egp
                {
                    get { return _egp; }
                    set { _egp = value; }
                }

                public decimal CEgp
                {
                    get { return _cEgp; }
                    set { _cEgp = value; }
                }

                public decimal Sgpa
                {
                    get { return _sgpa; }
                    set { _sgpa = value; }
                }

                public decimal Cgpa
                {
                    get { return _cgpa; }
                    set { _cgpa = value; }
                }

                public int ECredits
                {
                    get { return _eCredits; }
                    set { _eCredits = value; }
                }

                public int RCredits
                {
                    get { return _rCredits; }
                    set { _rCredits = value; }
                }

                public string ResultPF
                {
                    get { return _resultPF; }
                    set { _resultPF = value; }
                }

                public int Idno
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public string Regno
                {
                    get { return _regno; }
                    set { _regno = value; }
                }

                public string Result
                {
                    get { return _result; }
                    set { _result = value; }
                }

                public int TotSubject
                {
                    get { return _totSubject; }
                    set { _totSubject = value; }
                }

                public string StudName
                {
                    get { return _studName; }
                    set { _studName = value; }
                }

                #endregion Public Property Fields
            }
        }
    }
}