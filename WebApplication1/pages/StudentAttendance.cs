using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class StudentAttendance
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
                private int _thpr = 0;
                private string _studIds = string.Empty;
                private string _attStatus = string.Empty;
                private DateTime _attDate = DateTime.MinValue;
                private DateTime _curDate = DateTime.MinValue;
                private int _period = 0;
                private int _hours = 0;
                private int _classType = 0;
                private string _collegeCode = string.Empty;
                private string _topic = string.Empty;
                private int _sectionno = 0;
                private int _MassBunk = 0;

                //extra
                private int _aguaNo = 0;

                private int _agCourseNo = 0;
                private int _agsectionNo = 0;
                private int _swap_status = 0;
                private string _Additional_Slot = string.Empty;

                #endregion Private Member

                #region Public Property Fields

                public string Additional_Slot
                {
                    get { return _Additional_Slot; }
                    set { _Additional_Slot = value; }
                }

                public int MassBunk
                {
                    get { return _MassBunk; }
                    set { _MassBunk = value; }
                }

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

                public int Th_Pr
                {
                    get { return _thpr; }
                    set { _thpr = value; }
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

                public int Sectionno
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                public string Topic
                {
                    get { return _topic; }
                    set { _topic = value; }
                }

                //extra
                public int AguaNo
                {
                    get { return _aguaNo; }
                    set { _aguaNo = value; }
                }

                public int AgsectionNo
                {
                    get { return _agsectionNo; }
                    set { _agsectionNo = value; }
                }

                public int AgCourseNo
                {
                    get { return _agCourseNo; }
                    set { _agCourseNo = value; }
                }

                public int Swap_status
                {
                    get { return _swap_status; }
                    set { _swap_status = value; }
                }

                #endregion Public Property Fields
            }
        }
    }
}