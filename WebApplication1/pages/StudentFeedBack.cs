//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT FEED BACK
// CREATION DATE : 24-SEPT-2009
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
            public class StudentFeedBack
            {
                #region Private Member

                private string _CidComments = string.Empty;
                private string _FeedbackNo = string.Empty;

                private string _ccode = string.Empty;
                private int _college_id = 0;
                private int _fbNo = 0;
                private int _tokenNo = 0;
                private int _idno = 0;
                private string _feedBack = string.Empty;
                private DateTime _feedbackDate = DateTime.MinValue;
                private char _status = ' ';
                private string _collegeCode = string.Empty;

                //FeedBack By Student
                private int _subid = 0;

                private string _questionname = string.Empty;
                private int _out = 0;
                private int _sessionno = 0;
                private string _ipaddress = string.Empty;

                private int _answerid = 0;
                private int _questionid = 0;

                private string _answerids = string.Empty;
                private string _questionids = string.Empty;
                private DateTime _date = DateTime.MinValue;
                private int _ctid = 0;
                private string _remark = string.Empty;
                private int _courseno = 0;
                private bool _fb_status = false;
                private int _ua_no = 0;

                private string _ansOptions = string.Empty;
                private string _value = string.Empty;

                private string _overallImpression = string.Empty;

                private string _suggestionA = string.Empty;
                private string _suggestionB = string.Empty;
                private string _suggestionC = string.Empty;
                private string _suggestionD = string.Empty;

                private int _semesterno = 0;
                private string _exitquestionbestteacher = string.Empty;
                private string _fromdepartment = string.Empty;
                private string _otherdepartment = string.Empty;
                private int _activestatus = 0;
                private int _examno = 0;
                private int _to_ua_no = 0;
                private int _from_ua_no = 0;

                #endregion Private Member

                #region Public Properties

                public string CidComments
                {
                    get { return _CidComments; }
                    set { _CidComments = value; }
                }

                public string FeedbackNo
                {
                    get { return _FeedbackNo; }
                    set { _FeedbackNo = value; }
                }

                public string CCode
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }

                public int Collegeid
                {
                    get { return _college_id; }
                    set { _college_id = value; }
                }

                public int ExamNo
                {
                    get { return _examno; }
                    set { _examno = value; }
                }

                public int ActiveStatus
                {
                    get { return _activestatus; }
                    set { _activestatus = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public string Ipaddress
                {
                    get { return _ipaddress; }
                    set { _ipaddress = value; }
                }

                public int QuestionId
                {
                    get { return _questionid; }
                    set { _questionid = value; }
                }

                public string QuestionIds
                {
                    get { return _questionids; }
                    set { _questionids = value; }
                }

                public int AnswerId
                {
                    get { return _answerid; }
                    set { _answerid = value; }
                }

                public string AnswerIds
                {
                    get { return _answerids; }
                    set { _answerids = value; }
                }

                public DateTime Date
                {
                    get { return _date; }
                    set { _date = value; }
                }

                public int FbNo
                {
                    get { return _fbNo; }
                    set { _fbNo = value; }
                }

                public int TokenNo
                {
                    get { return _tokenNo; }
                    set { _tokenNo = value; }
                }

                public int Idno
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public string FeedBack
                {
                    get { return _feedBack; }
                    set { _feedBack = value; }
                }

                public DateTime FeedbackDate
                {
                    get { return _feedbackDate; }
                    set { _feedbackDate = value; }
                }

                public char Status
                {
                    get { return _status; }
                    set { _status = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                public int SubId
                {
                    get { return _subid; }
                    set { _subid = value; }
                }

                public string QuestionName
                {
                    get { return _questionname; }
                    set { _questionname = value; }
                }

                public int Out
                {
                    get { return _out; }
                    set { _out = value; }
                }

                public int CTID
                {
                    get { return _ctid; }
                    set { _ctid = value; }
                }

                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }

                public int CourseNo
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }

                public bool FB_Status
                {
                    get { return _fb_status; }
                    set { _fb_status = value; }
                }

                public int UA_NO
                {
                    get { return _ua_no; }
                    set { _ua_no = value; }
                }

                public string AnsOptions
                {
                    get { return _ansOptions; }
                    set { _ansOptions = value; }
                }

                public string Value
                {
                    get { return _value; }
                    set { _value = value; }
                }

                public string OverallImpression
                {
                    get { return _overallImpression; }
                    set { _overallImpression = value; }
                }

                public string Suggestion_A
                {
                    get { return _suggestionA; }
                    set { _suggestionA = value; }
                }

                public string Suggestion_B
                {
                    get { return _suggestionB; }
                    set { _suggestionB = value; }
                }

                public string Suggestion_C
                {
                    get { return _suggestionC; }
                    set { _suggestionC = value; }
                }

                public string Suggestion_D
                {
                    get { return _suggestionD; }
                    set { _suggestionD = value; }
                }

                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public string ExitQuestionBestTeacher
                {
                    get { return _exitquestionbestteacher; }
                    set { _exitquestionbestteacher = value; }
                }

                public string FromDepartment
                {
                    get { return _fromdepartment; }
                    set { _fromdepartment = value; }
                }

                public string OtherDepartment
                {
                    get { return _otherdepartment; }
                    set { _otherdepartment = value; }
                }

                public int TO_UA_NO
                {
                    get { return _to_ua_no; }
                    set { _to_ua_no = value; }
                }

                public int FROM_UA_NO
                {
                    get { return _from_ua_no; }
                    set { _from_ua_no = value; }
                }

                #endregion Public Properties
            }
        }
    }
}