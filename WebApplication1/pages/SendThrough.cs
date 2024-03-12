namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic
{
    public class SendThrough
    {
        #region Private

        private int _SendThroughNo = 0;

        private string _SendThrough_Name = string.Empty.Trim();

        private int _SendThrough_Status = 0;

        #endregion Private

        #region Public

        public int SendThroughNo
        {
            get { return _SendThroughNo; }
            set { _SendThroughNo = value; }
        }

        public string SendThrough_Name
        {
            get { return _SendThrough_Name; }
            set { _SendThrough_Name = value; }
        }

        public int SendThrough_Status
        {
            get { return _SendThrough_Status; }
            set { _SendThrough_Status = value; }
        }

        #endregion Public
    }
}