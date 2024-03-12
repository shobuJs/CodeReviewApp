namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DocumentAcad
    {
        #region Private Member

        private string _documentname = string.Empty;
        private int _documentno = 0;
        private int _degree = 0;
        private int _ptype = 0;
        private int _collegeCode = 0;
        private int _USERNO = 0;
        private string _DOCNAME;
        private string _PATH;
        private int _DOCNO;
        private int _SESSIONNO = 0;
        private string _FILENAME;
        private string _branch = string.Empty;
        private string _preference = string.Empty;

        #endregion Private Member

        #region Public Member

        public string Documentname
        {
            get { return _documentname; }
            set { _documentname = value; }
        }

        public int Documentno
        {
            get { return _documentno; }
            set { _documentno = value; }
        }

        public int Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }

        public int Ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        public int CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int USERNO
        {
            get { return _USERNO; }
            set { _USERNO = value; }
        }

        public string DOCNAME
        {
            get { return _DOCNAME; }
            set { _DOCNAME = value; }
        }

        public string PATH
        {
            get { return _PATH; }
            set { _PATH = value; }
        }

        public int DOCNO
        {
            get { return _DOCNO; }
            set { _DOCNO = value; }
        }

        public int SESSIONNO
        {
            get { return _SESSIONNO; }
            set { _SESSIONNO = value; }
        }

        public string FILENAME
        {
            get { return _FILENAME; }
            set { _FILENAME = value; }
        }

        public string BRANCHNO
        {
            get { return _branch; }
            set { _branch = value; }
        }

        public string PREFERENCE
        {
            get { return _preference; }
            set { _preference = value; }
        }

        #endregion Public Member
    }
}