namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Batch
    {
        #region Private Member

        private int _batchNo = 0;
        private string _batchName = string.Empty;
        private int _subId = 0;
        private string _collegeCode = string.Empty;

        #endregion Private Member

        #region Private Property Fields

        public int BatchNo
        {
            get { return _batchNo; }
            set { _batchNo = value; }
        }

        public string BatchName
        {
            get { return _batchName; }
            set { _batchName = value; }
        }

        public int SubId
        {
            get { return _subId; }
            set { _subId = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        #endregion Private Property Fields
    }
}