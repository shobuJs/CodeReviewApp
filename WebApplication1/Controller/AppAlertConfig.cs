namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class AppAlertConfig
    {
        #region Private

        private int _AlertsNo = 0;

        private string _Alerts_Name = string.Empty.Trim();

        private bool _Alert_Status = false;

        #endregion Private

        #region Public

        public int AlertsNo
        {
            get { return _AlertsNo; }
            set { _AlertsNo = value; }
        }

        public string Alerts_Name
        {
            get { return _Alerts_Name; }
            set { _Alerts_Name = value; }
        }

        public bool Alert_Status
        {
            get { return _Alert_Status; }
            set { _Alert_Status = value; }
        }

        #endregion Public
    }
}