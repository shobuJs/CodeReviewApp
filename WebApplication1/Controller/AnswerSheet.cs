//=================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ANSWERSHEET ALLOTMENT
// CREATION DATE : 26-Dec-2015
// CREATED BY    : SUMIT L. WADASKAR
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class AnswerSheet
    {
        #region private

        private int _AdmBatchNo = 0;
        private int _SessionNo = 0;
        private int _DegreeNo = 0;
        private int _BranchNo = 0;
        private int _SchemeNo = 0;
        private int _ExamNo = 0;
        private int _SemesterNo = 0;
        private int _CourseNo = 0;
        private string _CourseName = string.Empty;
        private int _IssuerId = 0;
        private int _RecdId = 0;
        private int _FacultyNo = 0;
        private int _FacultyType = 0;
        private string _FacultyName = string.Empty;
        private int _TotalRecv = 0;
        private int _Remaining = 0;
        private string _Bundle = string.Empty;
        private DateTime _Issuer_Date = DateTime.MinValue;
        private DateTime? _Receiver_Date = null;
        private string _Remark = string.Empty;
        private int _Exam_Staff_No = 0;
        private DateTime _Reporting_Date = DateTime.MinValue;
        private int _UANO = 0;
        private int _ReceivedStaff_No = 0;
        private int _SubmitStaff_No = 0;
        private int _Balance = 0;
        private int _AnsSheetIssue = 0;
        private int _PerPaperRate = 0;
        private int _Quantity = 0;
        private int _Amount = 0;
        private int _ExamSlot = 0;

        private string _SplitAnsRecd = string.Empty;

        private string _SplitUanoRecd = string.Empty;

        private string _SplitUanoSub = string.Empty;

        private string _SplitReportTime = string.Empty;

        private string _SplitRemark = string.Empty;
        private string _SplitSlot = string.Empty;
        private string _SplitExamType = string.Empty;

        private string _RecdName = string.Empty;
        private string _IssuerName = string.Empty;

        private int _Examtype = 0;

        #endregion private

        #region public

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public int ExamSlot
        {
            get { return _ExamSlot; }
            set { _ExamSlot = value; }
        }

        public int SessionNo
        {
            get { return _SessionNo; }
            set { _SessionNo = value; }
        }

        public int DegreeNo
        {
            get { return _DegreeNo; }
            set { _DegreeNo = value; }
        }

        public int BranchNo
        {
            get { return _BranchNo; }
            set { _BranchNo = value; }
        }

        public int SchemeNo
        {
            get { return _SchemeNo; }
            set { _SchemeNo = value; }
        }

        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }

        public int ExamNo
        {
            get { return _ExamNo; }
            set { _ExamNo = value; }
        }

        public int SemesterNo
        {
            get { return _SemesterNo; }
            set { _SemesterNo = value; }
        }

        public int CourseNo
        {
            get { return _CourseNo; }
            set { _CourseNo = value; }
        }

        public int IssuerId
        {
            get { return _IssuerId; }
            set { _IssuerId = value; }
        }

        public int RecdId
        {
            get { return _RecdId; }
            set { _RecdId = value; }
        }

        public int FacultyNo
        {
            get { return _FacultyNo; }
            set { _FacultyNo = value; }
        }

        public int FacultyType
        {
            get { return _FacultyType; }
            set { _FacultyType = value; }
        }

        public string FacultyName
        {
            get { return _FacultyName; }
            set { _FacultyName = value; }
        }

        public int TotalRecv
        {
            get { return _TotalRecv; }
            set { _TotalRecv = value; }
        }

        public int Remaining
        {
            get { return _Remaining; }
            set { _Remaining = value; }
        }

        public string Bundle
        {
            get { return _Bundle; }
            set { _Bundle = value; }
        }

        public DateTime Issuer_Date
        {
            get { return _Issuer_Date; }
            set { _Issuer_Date = value; }
        }

        public DateTime? Receiver_Date
        {
            get { return _Receiver_Date; }
            set { _Receiver_Date = value; }
        }

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        public int Exam_Staff_No
        {
            get { return _Exam_Staff_No; }
            set { _Exam_Staff_No = value; }
        }

        public int UANO
        {
            get { return _UANO; }
            set { _UANO = value; }
        }

        public DateTime Reporting_Date
        {
            get { return _Reporting_Date; }
            set { _Reporting_Date = value; }
        }

        public int AdmBatchNo
        {
            get { return _AdmBatchNo; }
            set { _AdmBatchNo = value; }
        }

        public int SubmitStaff_No
        {
            get { return _SubmitStaff_No; }
            set { _SubmitStaff_No = value; }
        }

        public int ReceivedStaff_No
        {
            get { return _ReceivedStaff_No; }
            set { _ReceivedStaff_No = value; }
        }

        public int Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        public int AnsSheetIssue
        {
            get { return _AnsSheetIssue; }
            set { _AnsSheetIssue = value; }
        }

        public string SplitAnsRecd
        {
            get { return _SplitAnsRecd; }
            set { _SplitAnsRecd = value; }
        }

        public string SplitUanoRecd
        {
            get { return _SplitUanoRecd; }
            set { _SplitUanoRecd = value; }
        }

        public string SplitUanoSub
        {
            get { return _SplitUanoSub; }
            set { _SplitUanoSub = value; }
        }

        public string SplitReportTime
        {
            get { return _SplitReportTime; }
            set { _SplitReportTime = value; }
        }

        public string SplitSlot
        {
            get { return _SplitSlot; }
            set { _SplitSlot = value; }
        }

        public string SplitRemark
        {
            get { return _SplitRemark; }
            set { _SplitRemark = value; }
        }

        public string SplitExamtype
        {
            get { return _SplitExamType; }
            set { _SplitExamType = value; }
        }

        public string RecdName
        {
            get { return _RecdName; }
            set { _RecdName = value; }
        }

        public string IssuerName
        {
            get { return _IssuerName; }
            set { _IssuerName = value; }
        }

        public int PerPapeRate
        {
            get { return _PerPaperRate; }
            set { _PerPaperRate = value; }
        }

        public int Examtype
        {
            get { return _Examtype; }
            set { _Examtype = value; }
        }

        #endregion public
    }
}