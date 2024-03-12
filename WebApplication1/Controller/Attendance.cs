using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Attendance
            {
                #region Private Member

                private int _attNo = 0;
                private int _sessionNo = 0;
                private int _uaNo = 0;
                private int _schemeNo = 0;
                private int _semesterNo = 0;
                private int _courseNo = 0;
                private string _cCode = string.Empty;
                private int _batchNo = 0;
                private int _subId = 0;
                private string _studIds = string.Empty;
                private string _attStatus = string.Empty;
                private DateTime _attDate = DateTime.MinValue;
                private DateTime _curDate = DateTime.MinValue;
                private int _period = 0;
                private int _hours = 0;
                private int _classType = 0;
                private string _collegeCode = string.Empty;

                #endregion Private Member

                #region Public Property Fields

                public int AttNo
                {
                    get { return _attNo; }
                    set { _attNo = value; }
                }

                public int SessionNo
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }

                public int UaNo
                {
                    get { return _uaNo; }
                    set { _uaNo = value; }
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

                public int CourseNo
                {
                    get { return _courseNo; }
                    set { _courseNo = value; }
                }

                public string CCode
                {
                    get { return _cCode; }
                    set { _cCode = value; }
                }

                public int BatchNo
                {
                    get { return _batchNo; }
                    set { _batchNo = value; }
                }

                public int SubId
                {
                    get { return _subId; }
                    set { _subId = value; }
                }

                public string StudIds
                {
                    get { return _studIds; }
                    set { _studIds = value; }
                }

                public string AttStatus
                {
                    get { return _attStatus; }
                    set { _attStatus = value; }
                }

                public DateTime AttDate
                {
                    get { return _attDate; }
                    set { _attDate = value; }
                }

                public DateTime CurDate
                {
                    get { return _curDate; }
                    set { _curDate = value; }
                }

                public int Period
                {
                    get { return _period; }
                    set { _period = value; }
                }

                public int Hours
                {
                    get { return _hours; }
                    set { _hours = value; }
                }

                public int ClassType
                {
                    get { return _classType; }
                    set { _classType = value; }
                }

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