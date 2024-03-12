//=================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : COURSE CREATION
// CREATION DATE : 27-Dec-2011
// CREATED BY    : ASHISH V. DHAKATE
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class AllotmentMaster
    {
        #region private

        private int _SRNO = 0;
        private string _IDNO = string.Empty.Trim();

        private int _SESSIONNO = 0;

        private int _SUBJECTNO = 0;

        private int _COURSESRNO = 0;
        private string _CCODE = "";

        private int _UA_NO = 0;

        private int _SECTIONNO = 0;

        private int _THEORYPRAC = 0;

        private string _SLOT1 = string.Empty.Trim();

        private string _SLOT2 = string.Empty.Trim();
        private string _SLOT3 = string.Empty.Trim();
        private string _SLOT4 = string.Empty.Trim();
        private string _SLOT5 = string.Empty.Trim();
        private string _SLOT6 = string.Empty.Trim();
        private string _SLOT7 = string.Empty.Trim();

        private int _DAY1 = 0;
        private int _DAY2 = 0;
        private int _DAY3 = 0;
        private int _DAY4 = 0;
        private int _DAY5 = 0;
        private int _DAY6 = 0;
        private int _DAY7 = 0;

        private string _BATCH1 = string.Empty.Trim();
        private string _BATCH2 = string.Empty.Trim();
        private string _BATCH3 = string.Empty.Trim();
        private string _BATCH4 = string.Empty.Trim();
        private string _BATCH5 = string.Empty.Trim();
        private string _BATCH6 = string.Empty.Trim();
        private string _BATCH7 = string.Empty.Trim();

        private string _ROOM1 = string.Empty.Trim();
        private string _ROOM2 = string.Empty.Trim();
        private string _ROOM3 = string.Empty.Trim();
        private string _ROOM4 = string.Empty.Trim();
        private string _ROOM5 = string.Empty.Trim();
        private string _ROOM6 = string.Empty.Trim();
        private string _ROOM7 = string.Empty.Trim();

        #endregion private

        #region public

        public int SRNO
        {
            get { return _SRNO; }
            set { _SRNO = value; }
        }

        public string IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public int SESSIONNO
        {
            get { return _SESSIONNO; }
            set { _SESSIONNO = value; }
        }

        public int SECTIONNO
        {
            get { return _SECTIONNO; }
            set { _SECTIONNO = value; }
        }

        public int THEORYPRAC
        {
            get { return _THEORYPRAC; }
            set { _THEORYPRAC = value; }
        }

        public int SUBJECTNO
        {
            get { return _SUBJECTNO; }
            set { _SUBJECTNO = value; }
        }

        public int COURSESRNO
        {
            get { return _COURSESRNO; }
            set { _COURSESRNO = value; }
        }

        public string CCODE
        {
            get { return _CCODE; }
            set { _CCODE = value; }
        }

        public int UA_NO
        {
            get { return _UA_NO; }
            set { _UA_NO = value; }
        }

        public string SLOT1
        {
            get { return _SLOT1; }
            set { _SLOT1 = value; }
        }

        public string SLOT2
        {
            get { return _SLOT2; }
            set { _SLOT2 = value; }
        }

        public string SLOT3
        {
            get { return _SLOT3; }
            set { _SLOT3 = value; }
        }

        public string SLOT4
        {
            get { return _SLOT4; }
            set { _SLOT4 = value; }
        }

        public string SLOT5
        {
            get { return _SLOT5; }
            set { _SLOT5 = value; }
        }

        public string SLOT6
        {
            get { return _SLOT6; }
            set { _SLOT6 = value; }
        }

        public string SLOT7
        {
            get { return _SLOT7; }
            set { _SLOT7 = value; }
        }

        public int DAY1
        {
            get { return _DAY1; }
            set { _DAY1 = value; }
        }

        public int DAY2
        {
            get { return _DAY2; }
            set { _DAY2 = value; }
        }

        public int DAY3
        {
            get { return _DAY3; }
            set { _DAY3 = value; }
        }

        public int DAY4
        {
            get { return _DAY4; }
            set { _DAY4 = value; }
        }

        public int DAY5
        {
            get { return _DAY5; }
            set { _DAY5 = value; }
        }

        public int DAY6
        {
            get { return _DAY6; }
            set { _DAY6 = value; }
        }

        public int DAY7
        {
            get { return _DAY7; }
            set { _DAY7 = value; }
        }

        public string BATCH1
        {
            get { return _BATCH1; }
            set { _BATCH1 = value; }
        }

        public string BATCH2
        {
            get { return _BATCH2; }
            set { _BATCH2 = value; }
        }

        public string BATCH3
        {
            get { return _BATCH3; }
            set { _BATCH3 = value; }
        }

        public string BATCH4
        {
            get { return _BATCH4; }
            set { _BATCH4 = value; }
        }

        public string BATCH5
        {
            get { return _BATCH5; }
            set { _BATCH5 = value; }
        }

        public string BATCH6
        {
            get { return _BATCH6; }
            set { _BATCH6 = value; }
        }

        public string BATCH7
        {
            get { return _BATCH7; }
            set { _BATCH7 = value; }
        }

        public string ROOM1
        {
            get { return _ROOM1; }
            set { _ROOM1 = value; }
        }

        public string ROOM2
        {
            get { return _ROOM2; }
            set { _ROOM2 = value; }
        }

        public string ROOM3
        {
            get { return _ROOM3; }
            set { _ROOM3 = value; }
        }

        public string ROOM4
        {
            get { return _ROOM4; }
            set { _ROOM4 = value; }
        }

        public string ROOM5
        {
            get { return _ROOM5; }
            set { _ROOM5 = value; }
        }

        public string ROOM6
        {
            get { return _ROOM6; }
            set { _ROOM6 = value; }
        }

        public string ROOM7
        {
            get { return _ROOM7; }
            set { _ROOM7 = value; }
        }

        #endregion public
    }
}