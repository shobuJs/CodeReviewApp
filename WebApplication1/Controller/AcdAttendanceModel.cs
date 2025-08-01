using System;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        public class AcdAttendanceModel
        {
            //in the region region Private Members for Attendance entry
            private int _tutorial = 0;

            private int _College_Id = 0;

            //replace if this region is already exists
            //test

            #region Attendance configuration

            //Added for Attendance configuration

            private int _degree_no = 0;
            private DateTime _attendanceStartDate = DateTime.Now;
            private DateTime _attendanceEndDate = DateTime.Now;
            private int _attendanceLockDay = 0;
            public int _collegeId = 0;
            private string _attendanceLockHrs = string.Empty;
            private bool _smsfacility = false;
            private bool _emailfacility = false;
            private bool _cregStatus = false;
            private bool _TeachingPlan = false;

            private bool _ActiveStatus = false;

            //private int _coursenos = 0;
            //public int CourseNos
            //{
            //    get { return _coursenos; }
            //    set { _coursenos = value; }
            //}
            public int Degree_No
            {
                get { return _degree_no; }
                set { _degree_no = value; }
            }

            //Attendance Config

            public DateTime AttendanceStartDate
            {
                get { return _attendanceStartDate; }
                set { _attendanceStartDate = value; }
            }

            public DateTime AttendanceEndDate
            {
                get { return _attendanceEndDate; }
                set { _attendanceEndDate = value; }
            }

            public int AttendanceLockDay
            {
                get { return _attendanceLockDay; }
                set { _attendanceLockDay = value; }
            }

            public int CollegeId
            {
                get { return _collegeId; }
                set { _collegeId = value; }
            }

            public string AttendanceLockHrs
            {
                get { return _attendanceLockHrs; }
                set { _attendanceLockHrs = value; }
            }

            public bool SMSFacility
            {
                get { return _smsfacility; }
                set { _smsfacility = value; }
            }

            public bool EmailFacility
            {
                get { return _emailfacility; }
                set { _emailfacility = value; }
            }

            public bool TeachingPlan
            {
                get { return _TeachingPlan; }
                set { _TeachingPlan = value; }
            }

            public bool CRegStatus
            {
                get { return _cregStatus; }
                set { _cregStatus = value; }
            }

            //Added by Rishabh on 29/10/2021
            public bool ActiveStatus
            {
                get
                {
                    return _ActiveStatus;
                }
                set
                {
                    _ActiveStatus = value;
                }
            }

            #endregion Attendance configuration

            #region TimeTable

            private int _SESSIONNO = 0;
            private int _DEGREENO = 0;
            private int _SCHEMENO = 0;
            private int _SEMESTERNO = 0;
            private int _SECTIONNO = 0;
            private int _UA_NO = 0;

            private DateTime LOCKDATE = DateTime.MinValue;

            public int SESSIONNO
            {
                get { return _SESSIONNO; }
                set { _SESSIONNO = value; }
            }

            public int DEGREENO
            {
                get { return _DEGREENO; }
                set { _DEGREENO = value; }
            }

            public int SCHEMENO
            {
                get { return _SCHEMENO; }
                set { _SCHEMENO = value; }
            }

            public int SEMESTERNO
            {
                get { return _SEMESTERNO; }
                set { _SEMESTERNO = value; }
            }

            public int SECTIONNO
            {
                get { return _SECTIONNO; }
                set { _SECTIONNO = value; }
            }

            public int UA_NO
            {
                get { return _UA_NO; }
                set { _UA_NO = value; }
            }

            public DateTime LOCK_DATE
            {
                get { return LOCKDATE; }
                set { LOCKDATE = value; }
            }

            #endregion TimeTable

            #region Private Members for Bulk Course allotment

            //Bulk Cousre Allotment

            private int _sessionno = 0;
            private int _schemeno = 0;
            private int _semesterno = 0;
            private string _ipAddress = string.Empty;
            private int _ua_no = 0;

            //private int _coursenos = string.Empty;
            private int _coursenos = 0;

            private string _teachernos = string.Empty;
            private string _sectionnos = string.Empty;
            private int _collegeCode = 0;
            private string _batchnos = string.Empty;
            private string _is_adteacher = string.Empty;
            private string _roomnos = string.Empty;
            private string _ct_no = string.Empty;
            private string _slot_no = string.Empty;
            private string _day_no = string.Empty;
            private int _TTNO = 0;

            #endregion Private Members for Bulk Course allotment

            #region Private Members for Leave Module

            private DateTime _leaveStartDate = DateTime.Now;
            private DateTime _leaveEndDate = DateTime.Now;
            private string _college_code = string.Empty;
            private string _event_detail = string.Empty;
            private int _Leaveno = 0;
            private int _holidayno = 0;
            private int _ua_no_tran = 0;

            #endregion Private Members for Leave Module

            #region Private Members for Attendance entry

            private DateTime _att_date = DateTime.MinValue;
            private int _courseno = 0;
            private DateTime _curdate = DateTime.MinValue;
            private string _studID = string.Empty;
            private string _att_status = string.Empty;
            private string _att_LateTime = string.Empty;
            private int status = 0;
            private string _topic_Covered = string.Empty;
            private int _period = 0;
            private int _class_type = 0;
            private int _lecture_status = 0;
            private int _tpNo = 0;
            private int _coNo = 0;//Course Object Number..

            #endregion Private Members for Attendance entry

            #region Public Members for Bulk Course Allotment

            //Bulk Course Allotment
            public int Sessionno
            {
                get { return _sessionno; }
                set { _sessionno = value; }
            }

            public int Schemeno
            {
                get { return _schemeno; }
                set { _schemeno = value; }
            }

            public int Semesterno
            {
                get { return _semesterno; }
                set { _semesterno = value; }
            }

            public string Ipaddress
            {
                get { return _ipAddress; }
                set { _ipAddress = value; }
            }

            public int UA_No
            {
                get { return _ua_no; }
                set { _ua_no = value; }
            }

            public int CourseNos
            {
                get { return _coursenos; }
                set { _coursenos = value; }
            }

            public string TeacherNos
            {
                get { return _teachernos; }
                set { _teachernos = value; }
            }

            public string SectionNos
            {
                get { return _sectionnos; }
                set { _sectionnos = value; }
            }

            public int CollegeCode
            {
                get { return _collegeCode; }
                set { _collegeCode = value; }
            }

            public string BatchNos
            {
                get { return _batchnos; }
                set { _batchnos = value; }
            }

            public string Is_ADTeacher
            {
                get { return _is_adteacher; }
                set { _is_adteacher = value; }
            }

            public string RoomNos
            {
                get { return _roomnos; }
                set { _roomnos = value; }
            }

            #endregion Public Members for Bulk Course Allotment

            #region Public Members for Leave module

            public DateTime LeaveStartDate
            {
                get { return _leaveStartDate; }
                set { _leaveStartDate = value; }
            }

            public DateTime LeaveEndDate
            {
                get { return _leaveEndDate; }
                set { _leaveEndDate = value; }
            }

            public string College_code
            {
                get { return _college_code; }
                set { _college_code = value; }
            }

            public string Event_Detail
            {
                get { return _event_detail; }
                set { _event_detail = value; }
            }

            public int LEAVENO
            {
                get { return _Leaveno; }
                set { _Leaveno = value; }
            }

            public int Holiday_No
            {
                get { return _holidayno; }
                set { _holidayno = value; }
            }

            public int UA_NO_TRAN
            {
                get { return _ua_no_tran; }
                set { _ua_no_tran = value; }
            }

            #endregion Public Members for Leave module

            #region Public Members for Attendance Entry

            public DateTime Att_date
            {
                get { return _att_date; }
                set { _att_date = value; }
            }

            public int CourseNo
            {
                get { return _courseno; }
                set { _courseno = value; }
            }

            public DateTime Curdate
            {
                get { return _curdate; }
                set { _curdate = value; }
            }

            public string StudID
            {
                get { return _studID; }
                set { _studID = value; }
            }

            public string Att_status
            {
                get { return _att_status; }
                set { _att_status = value; }
            }

            public string Att_LateTime
            {
                get { return _att_LateTime; }
                set { _att_LateTime = value; }
            }

            public int Status
            {
                get { return status; }
                set { status = value; }
            }

            public string Topic_Covered
            {
                get { return _topic_Covered; }
                set { _topic_Covered = value; }
            }

            public int Period
            {
                get { return _period; }
                set { _period = value; }
            }

            public int ClassType
            {
                get { return _class_type; }
                set { _class_type = value; }
            }

            public int LectStatus
            {
                get { return _lecture_status; }
                set { _lecture_status = value; }
            }

            public int TpNo
            {
                get { return _tpNo; }
                set { _tpNo = value; }
            }

            public int CoNo
            {
                get { return _coNo; }
                set { _coNo = value; }
            }

            #endregion Public Members for Attendance Entry

            #region for special leave

            private string _studid = string.Empty;
            private DateTime _fromDate = DateTime.Now;
            private DateTime _toDate = DateTime.Now;
            private string _leaveDetail = string.Empty;

            /*for special leave*/

            public string StudId
            {
                get { return _studid; }
                set { _studid = value; }
            }

            public DateTime FromDate
            {
                get { return _fromDate; }
                set { _fromDate = value; }
            }

            public DateTime ToDate
            {
                get { return _toDate; }
                set { _toDate = value; }
            }

            public string LeaveDetail
            {
                get { return _leaveDetail; }
                set { _leaveDetail = value; }
            }

            #endregion for special leave

            #region Public Memebers for  TeachingPlan Master

            private int _slot = 0;
            private int _tp_no = 0;
            private DateTime _date = DateTime.Now;
            private int _sectionno = 0;
            private int _batchno = 0;
            private int _unit_no = 0;
            private int _lecture_no = 0;
            private string _remark = string.Empty;
            private string _slotTeaching = string.Empty;

            public int Slot
            {
                get { return _slot; }
                set { _slot = value; }
            }

            public int TP_NO
            {
                get { return _tp_no; }
                set { _tp_no = value; }
            }

            public string SlotTeaching
            {
                get { return _slotTeaching; }
                set { _slotTeaching = value; }
            }

            public DateTime Date
            {
                get { return _date; }
                set { _date = value; }
            }

            public int Sectionno
            {
                get { return _sectionno; }
                set { _sectionno = value; }
            }

            public int BatchNo
            {
                get { return _batchno; }
                set { _batchno = value; }
            }

            public int UnitNo
            {
                get { return _unit_no; }
                set { _unit_no = value; }
            }

            public int Lecture_No
            {
                get { return _lecture_no; }
                set { _lecture_no = value; }
            }

            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }

            #endregion Public Memebers for  TeachingPlan Master

            public int TTNO
            {
                get { return _TTNO; }
                set { _TTNO = value; }
            }

            public class AttendanceJsonModel
            {
                public string SlotNo { get; set; }
                public string CTNO { get; set; }
                public string DayNo { get; set; }
                public string EffectiveDate { get; set; }

                public string StartDate1 { get; set; }
                public string EndDate1 { get; set; }
                public string RoomId { get; set; }
                public string Revised_Remark { get; set; }
            }

            public class AttendanceDataAddModel
            {
                public DataTable DTTimeTable { get; set; }
                public int UserId { get; set; }
                public string IPADDRESS { get; set; }

                public DateTime EFFECTIVEDATE { get; set; }
                public int WEEKNO { get; set; }

                public DateTime? TimeTableDate { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public string CollegeId { get; set; }

                public int Schemeno { get; set; }
                public int Degreeno { get; set; }
                public int Semesterno { get; set; }
                public int Sectionno { get; set; }
                public int Slottype { get; set; }
                public DataTable dtRevisedFac { get; set; }
                public string Remark { get; set; } //Added By Rishabh
                public string OrgId { get; set; }
            }

            #region Public Memebers for Holiday Master

            private int _holidayNo = 0;
            private string _holidayname = string.Empty;
            private DateTime _holidaydate = DateTime.MinValue;
            private int _HsessionNo = 0;
            private int _HdegreeNo = 0;
            private int _HbranchNo = 0;
            private int _HschemeNo = 0;
            private int _HsemesterNo = 0;
            private int _HsubId = 0;
            private int _HsectionNo = 0;
            private int _HbatchNo = 0;
            private int _HdayNo = 0;
            private int _lockHoliday = 0;
            private string _Hreason = string.Empty;
            private string _Hslots = string.Empty;

            public int holidayNo
            {
                get { return _holidayNo; }
                set { _holidayNo = value; }
            }

            public string holidayname
            {
                get { return _holidayname; }
                set { _holidayname = value; }
            }

            public DateTime holidaydate
            {
                get { return _holidaydate; }
                set { _holidaydate = value; }
            }

            public int HsessionNo
            {
                get { return _HsessionNo; }
                set { _HsessionNo = value; }
            }

            public int HdegreeNo
            {
                get { return _HdegreeNo; }
                set { _HdegreeNo = value; }
            }

            public int HbranchNo
            {
                get { return _HbranchNo; }
                set { _HbranchNo = value; }
            }

            public int HschemeNo
            {
                get { return _HschemeNo; }
                set { _HschemeNo = value; }
            }

            public int HsemesterNo
            {
                get { return _HsemesterNo; }
                set { _HsemesterNo = value; }
            }

            public int HsubId
            {
                get { return _HsubId; }
                set { _HsubId = value; }
            }

            public int HSectionNo
            {
                get { return _HsectionNo; }
                set { _HsectionNo = value; }
            }

            public int HBatchNo
            {
                get { return _HbatchNo; }
                set { _HbatchNo = value; }
            }

            public int HdayNo
            {
                get { return _HdayNo; }
                set { _HdayNo = value; }
            }

            public string Hreason
            {
                get { return _Hreason; }
                set { _Hreason = value; }
            }

            public string Hslots
            {
                get { return _Hslots; }
                set { _Hslots = value; }
            }

            public int lockHoliday
            {
                get { return _lockHoliday; }
                set { _lockHoliday = value; }
            }

            #endregion Public Memebers for Holiday Master

            //in the region region Public Members for Attendance entry
            public int Tutorial
            {
                get { return _tutorial; }
                set { _tutorial = value; }
            }

            public int College_Id
            {
                get { return _College_Id; }
                set { _College_Id = value; }
            }
        }
    }
}
