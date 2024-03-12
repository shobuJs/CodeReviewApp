using System;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ExamScheduling : IDisposable
            {
                #region Declare Variable

                private string _ccode;
                private int _sessionno;
                private int _college_id;
                private string _degreeno;
                private string _branchno;
                private string _semesterno;
                private int _exam;
                private int _courseno;
                private DateTime _examdate;
                private string _timefrom;
                private string _timeto;
                private int _exammode;
                private int _exdtno;
                private int _campusno;
                private int _supervisor;
                private string _invigilator;
                private int _roomno;
                private int _regular;
                private int _repeat;
                private int _prorata;
                private int _exemption;
                private int _createdby;
                private string _ip_address;
                private int _timeslot;
                private int _seatno;
                private DataTable _dt = null;

                #endregion Declare Variable

                #region Getter & Setter

                public DataTable dt
                {
                    get { return _dt; }
                    set { _dt = value; }
                }

                public int ExDtNo
                {
                    get { return _exdtno; }
                    set { _exdtno = value; }
                }

                public int Seat
                {
                    get { return _seatno; }
                    set { _seatno = value; }
                }

                public int Sessionno
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public int College_id
                {
                    get { return _college_id; }
                    set { _college_id = value; }
                }

                public string DegreeNo
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }

                public string Semesterno
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public string BranchNo
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }

                public int Exam
                {
                    get { return _exam; }
                    set { _exam = value; }
                }

                public int CourseNo
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }

                public DateTime ExamDate
                {
                    get { return _examdate; }
                    set { _examdate = value; }
                }

                public string TimeFrom
                {
                    get { return _timefrom; }
                    set { _timefrom = value; }
                }

                public string TimeTo
                {
                    get { return _timeto; }
                    set { _timeto = value; }
                }

                public int ExamMode
                {
                    get { return _exammode; }
                    set { _exammode = value; }
                }

                public int CampusNo
                {
                    get { return _campusno; }
                    set { _campusno = value; }
                }

                public int Supervisor
                {
                    get { return _supervisor; }
                    set { _supervisor = value; }
                }

                public string Invigilator
                {
                    get { return _invigilator; }
                    set { _invigilator = value; }
                }

                public int RoomNo
                {
                    get { return _roomno; }
                    set { _roomno = value; }
                }

                public int Regular
                {
                    get { return _regular; }
                    set { _regular = value; }
                }

                public int Repeat
                {
                    get { return _repeat; }
                    set { _repeat = value; }
                }

                public int Prorata
                {
                    get { return _prorata; }
                    set { _prorata = value; }
                }

                public int Exemption
                {
                    get { return _exemption; }
                    set { _exemption = value; }
                }

                public int CreatedBy
                {
                    get { return _createdby; }
                    set { _createdby = value; }
                }

                public string Ip_Address
                {
                    get { return _ip_address; }
                    set { _ip_address = value; }
                }

                public string CCode
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }

                public int TimeSlot
                {
                    get { return _timeslot; }
                    set { _timeslot = value; }
                }

                #endregion Getter & Setter

                private bool Disposed = false;

                ~ExamScheduling()
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

                public DataTable CreateDataTable()
                {
                    DataTable dt = null;

                    try
                    {
                        dt = new DataTable();
                        dt.Columns.Add(new DataColumn("CAMPUS", typeof(string)));
                        dt.Columns.Add(new DataColumn("CAMPUSNO", typeof(int)));
                        dt.Columns.Add(new DataColumn("SUPERVISOR", typeof(string)));
                        dt.Columns.Add(new DataColumn("SUPERVISORNO", typeof(int)));
                        dt.Columns.Add(new DataColumn("INVIGILATOR", typeof(string)));
                        dt.Columns.Add(new DataColumn("INVIGILATORNO", typeof(string)));
                        dt.Columns.Add(new DataColumn("ROOM", typeof(string)));
                        dt.Columns.Add(new DataColumn("ROOMNO", typeof(int)));
                        dt.Columns.Add(new DataColumn("REGULAR", typeof(int)));
                        dt.Columns.Add(new DataColumn("REPEAT", typeof(int)));
                        dt.Columns.Add(new DataColumn("PRORATA", typeof(int)));
                        dt.Columns.Add(new DataColumn("EXEMPTION", typeof(int)));
                        dt.AcceptChanges();
                        return dt;
                    }
                    catch
                    {
                        dt = null;
                        throw new Exception();
                    }
                    finally
                    {
                        dt = null;
                    }
                }
            }
        }
    }
}