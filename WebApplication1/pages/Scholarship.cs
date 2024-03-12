namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Scholarship
    {
        private int _IDNO = 0;
        private int _UANO = 0;
        private int _SESSIONNO = 0;
        private int _DEGREENO = 0;
        private int _BRANCHNO = 0;

        private decimal _AMOUNT = 0;
        private decimal _ACTAMT = 0;
        private decimal _HRA = 0;
        private decimal _NETAMT = 0;
        private string _ACC_NO = string.Empty;
        private int _TOTDAYS = 0;
        private int _SEMESTERNO = 0;
        private string _DATE = string.Empty;

        private string _IPADDRESS = string.Empty;
        private int _MONTHNO = 0;
        private int _YEARNO = 0;

        // Added by Pritish on 01/04/2021
        private int _College_id = 0;

        public string IPADDRESS
        {
            get { return _IPADDRESS; }
            set { _IPADDRESS = value; }
        }

        public string DATE
        {
            get { return _DATE; }
            set { _DATE = value; }
        }

        public int YEARNO
        {
            get { return _YEARNO; }
            set { _YEARNO = value; }
        }

        public int MONTHNO
        {
            get { return _MONTHNO; }
            set { _MONTHNO = value; }
        }

        public int SEMESTERNO
        {
            get { return _SEMESTERNO; }
            set { _SEMESTERNO = value; }
        }

        public int SESSIONNO
        {
            get { return _SESSIONNO; }
            set { _SESSIONNO = value; }
        }

        public int DEGREENO
        {
            get { return _DEGREENO; }
            set { _DEGREENO = value; }
        }

        public int BRANCHNO
        {
            get { return _BRANCHNO; }
            set { _BRANCHNO = value; }
        }

        public decimal AMOUNT
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }

        public decimal ACTAMT
        {
            get { return _ACTAMT; }
            set { _ACTAMT = value; }
        }

        public decimal HRA
        {
            get { return _HRA; }
            set { _HRA = value; }
        }

        public decimal NETAMT
        {
            get { return _NETAMT; }
            set { _NETAMT = value; }
        }

        public string ACC_NO
        {
            get { return _ACC_NO; }
            set { _ACC_NO = value; }
        }

        public int TOTDAYS
        {
            get { return _TOTDAYS; }
            set { _TOTDAYS = value; }
        }

        public int IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public int UANO
        {
            get { return _UANO; }
            set { _UANO = value; }
        }

        public int College_ID
        {
            get { return _College_id; }
            set { _College_id = value; }
        }
    }
}