//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : BUSINESS ENTITIES FILE [SLOT MASTER]
// CREATION DATE : 27/12/2011
// CREATED BY    : ASHISH DHAKATE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class SlotMaster
    {
        #region private

        private int _SLOTNO = 0;

        private int _IDNO = 0;

        private int _DEGREENO = 0;

        private int _SESSIONNO = 0;
        private string _SLOTNAME = string.Empty.Trim();

        private string _TIMEFROM = string.Empty.Trim();

        private string _TIMETO = string.Empty.Trim();
        private string _COLLEGE_CODE = string.Empty.Trim();

        private int _SlotTypNo = 0;//Added By Dileep Kare on 19012021
        private int _College_id = 0;// Added By Dileep Kare 16.04.2021

        private int _SequenceNo = 0; //Added Mahesh on Dated 19-05-2021

        #endregion private

        public string Degrees //add by maithili [23-08-2022]
        {
            get { return _Degrees; }
            set { _Degrees = value; }
        }

        public string College_Ids //add by maithili [23-08-2022]
        {
            get { return _College_ids; }
            set { _College_ids = value; }
        }

        private string _Degrees = string.Empty.Trim(); // Add by maithili [23-08-2022]
        private string _College_ids = string.Empty.Trim(); // Add by maithili [23-08-2022]

        #region public

        public int SLOTNO
        {
            get { return _SLOTNO; }
            set { _SLOTNO = value; }
        }

        public int IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public int DEGREENO
        {
            get { return _DEGREENO; }
            set { _DEGREENO = value; }
        }

        public int SESSIONNO
        {
            get { return _SESSIONNO; }
            set { _SESSIONNO = value; }
        }

        public string COLLEGE_CODE
        {
            get { return _COLLEGE_CODE; }
            set { _COLLEGE_CODE = value; }
        }

        public string SLOTNAME
        {
            get { return _SLOTNAME; }
            set { _SLOTNAME = value; }
        }

        public string TIMEFROM
        {
            get { return _TIMEFROM; }
            set { _TIMEFROM = value; }
        }

        public string TIMETO
        {
            get { return _TIMETO; }
            set { _TIMETO = value; }
        }

        public int SlotTypeNo //Added By Dileep on 19012021
        {
            get { return _SlotTypNo; }
            set { _SlotTypNo = value; }
        }

        public int College_Id //Added By Dileep Kare 16.04.2021
        {
            get { return _College_id; }
            set { _College_id = value; }
        }

        public int SequenceNo
        {
            get { return _SequenceNo; }
            set { _SequenceNo = value; }
        }

        #endregion public
    }

    public class GlobalTimeTable
    {
        #region private

        private int _dayno = 0;
        private int _slotno = 0;
        private int _roomno = 0;
        private int _facultyno = 0;
        private int _ttno = 0;

        #endregion private

        #region Public

        public int DayNo
        {
            get { return _dayno; }
            set { _dayno = value; }
        }

        public int SlotNo
        {
            get { return _slotno; }
            set { _slotno = value; }
        }

        public int RoomNo
        {
            get { return _roomno; }
            set { _roomno = value; }
        }

        public int FacultyNo
        {
            get { return _facultyno; }
            set { _facultyno = value; }
        }

        public int TTno
        {
            get { return _ttno; }
            set { _ttno = value; }
        }

        #endregion Public
    }
}