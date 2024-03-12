//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : BUSINESS ENTITIES FILE [SESSION MASTER]
// CREATION DATE : 18-MAY-2009
// CREATED BY    : SANJAY RATNAPARKHI
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
            public class Session
            {
                #region Private Members

                private int _is_holiday_event = 0;
                private int _sessionno = 0;
                private DateTime _session_sdate = DateTime.Now;
                private DateTime _session_edate = DateTime.Now;
                private string _session_name = string.Empty;
                private string _session_pname = string.Empty;
                private string _degree_name = string.Empty;
                private int _degree_no = 0;
                private int _odd_even = 0;

                //private int _seq_no = 0;
                private int _examtype = 0;

                private int _currentsessionno = 0;
                private int _holidayno = 0;
                private string _currentsession_name = string.Empty;
                private string _event_name = string.Empty;
                private string _event_detail = string.Empty;
                private int _Branch_no = 0;
                private int _Semester_no = 0;
                private int _Admbatch = 0;
                private string _StudId = string.Empty;
                private int _Status = 0;
                private decimal _TotAmount = 0.0M;
                private int _College_id = 0;

                private string _sessname_hindi = string.Empty;
                private int _session_status = 0;
                private string _college_code = string.Empty;
                private bool _flock = false;

                private string _college_id_str = string.Empty;
                private string _academic_year = string.Empty;
                private bool _IsActive = false;

                //private int _College_id = 0;

                #endregion Private Members

                #region Public Properties

                public int Holiday_Event
                {
                    get { return _is_holiday_event; }
                    set { _is_holiday_event = value; }
                }

                public string College_Id_str
                {
                    get { return _college_id_str; }
                    set { _college_id_str = value; }
                }

                public string academic_year
                {
                    get { return _academic_year; }
                    set { _academic_year = value; }
                }

                public bool IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public int CurrentSession
                {
                    get { return _currentsessionno; }
                    set { _currentsessionno = value; }
                }

                public int Holiday_No
                {
                    get { return _holidayno; }
                    set { _holidayno = value; }
                }

                public string CurrentSession_Name
                {
                    get { return _currentsession_name; }
                    set { _currentsession_name = value; }
                }

                public string Event_Name//For HOliday Entry page
                {
                    get { return _event_name; }
                    set { _event_name = value; }
                }

                public string Event_Detail//For HOliday Entry page
                {
                    get { return _event_detail; }
                    set { _event_detail = value; }
                }

                public DateTime Session_SDate
                {
                    get { return _session_sdate; }
                    set { _session_sdate = value; }
                }

                public DateTime Session_EDate
                {
                    get { return _session_edate; }
                    set { _session_edate = value; }
                }

                public string Session_Name
                {
                    get { return _session_name; }
                    set { _session_name = value; }
                }

                public string Session_PName
                {
                    get { return _session_pname; }
                    set { _session_pname = value; }
                }

                public string Degree_Name
                {
                    get { return _degree_name; }
                    set { _degree_name = value; }
                }

                public int Degree_No
                {
                    get { return _degree_no; }
                    set { _degree_no = value; }
                }

                public int Odd_Even
                {
                    get { return _odd_even; }
                    set { _odd_even = value; }
                }

                //public int Seq_No
                //{
                //    get { return _seq_no; }
                //    set { _seq_no = value; }
                //}

                public string Sessname_hindi
                {
                    get { return _sessname_hindi; }
                    set { _sessname_hindi = value; }
                }

                public int Session_status
                {
                    get { return _session_status; }
                    set { _session_status = value; }
                }

                public string College_code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }

                public int ExamType
                {
                    get { return _examtype; }
                    set { _examtype = value; }
                }

                public bool Flock
                {
                    get { return _flock; }
                    set { _flock = value; }
                }

                public int College_ID
                {
                    get { return _College_id; }
                    set { _College_id = value; }
                }

                public int Admbatch
                {
                    get { return _Admbatch; }
                    set { _Admbatch = value; }
                }

                public int Branch_No
                {
                    get { return _Branch_no; }
                    set { _Branch_no = value; }
                }

                public int Semester_No
                {
                    get { return _Semester_no; }
                    set { _Semester_no = value; }
                }

                public string StudId
                {
                    get { return _StudId; }
                    set { _StudId = value; }
                }

                public int Status
                {
                    get { return _Status; }
                    set { _Status = value; }
                }

                public decimal TotAmount
                {
                    get { return _TotAmount; }
                    set { _TotAmount = value; }
                }

                #endregion Public Properties
            }
        }
    }
}