namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class RoomConfig
    {
        #region Private Members

        private int _buildingNo = 0;
        private string _building_Code = string.Empty;
        private string _building_Name = string.Empty;
        private int _floor_Capacity = 0;
        private string _block_Name = string.Empty;
        private string _college_Code = string.Empty;
        private int _no_Of_Rooms = 0;
        private int _defined_FloorNo = 0;
        private string _room_name = string.Empty;
        private int _preferenceno = 0;
        private int _room_capacity = 0;
        private int _room_No = 0;
        private int _floor_No = 0;
        private int _room = 0;
        private int _roomcno = 0;
        private int _rows = 0;
        private int _columns = 0;
        private int _actual_Capacity = 0;
        private string _statusStudId = string.Empty;
        private string _DisbStudId = string.Empty;
        private string _status = string.Empty;
        private int _rowIndex = 0;
        private int _columnIndex = 0;

        #endregion Private Members

        #region Public Property Fields

        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }

        public string DisbStudId
        {
            get { return _DisbStudId; }
            set { _DisbStudId = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string StatusStudId
        {
            get { return _statusStudId; }
            set { _statusStudId = value; }
        }

        public int Actual_Capacity
        {
            get { return _actual_Capacity; }
            set { _actual_Capacity = value; }
        }

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Roomcno
        {
            get { return _roomcno; }
            set { _roomcno = value; }
        }

        public int Room_No
        {
            get { return _room_No; }
            set { _room_No = value; }
        }

        public int Room_capacity
        {
            get { return _room_capacity; }
            set { _room_capacity = value; }
        }

        public int Preferenceno
        {
            get { return _preferenceno; }
            set { _preferenceno = value; }
        }

        public string Room_name
        {
            get { return _room_name; }
            set { _room_name = value; }
        }

        public int BuildingNo
        {
            get { return _buildingNo; }
            set { _buildingNo = value; }
        }

        public string BuildingCode
        {
            get { return _building_Code; }
            set { _building_Code = value; }
        }

        public string BuildingName
        {
            get { return _building_Name; }
            set { _building_Name = value; }
        }

        public int FloorCapacity
        {
            get { return _floor_Capacity; }
            set { _floor_Capacity = value; }
        }

        public int Room
        {
            get { return _room; }
            set { _room = value; }
        }

        public int NoOfRooms
        {
            get { return _no_Of_Rooms; }
            set { _no_Of_Rooms = value; }
        }

        public int FloorNo
        {
            get { return _floor_No; }
            set { _floor_No = value; }
        }

        public int DefinedFloorNo
        {
            get { return _defined_FloorNo; }
            set { _defined_FloorNo = value; }
        }

        public string CollegeCode
        {
            get { return _college_Code; }
            set { _college_Code = value; }
        }

        #endregion Public Property Fields
    }
}