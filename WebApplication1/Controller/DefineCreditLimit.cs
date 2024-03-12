namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class DefineCreditLimit
            {
                #region Private members

                private string _semester = string.Empty;
                private string _semester_text = string.Empty;
                private int _session = 0;
                private int _degreeNo = 0;
                private int _branchNo = 0;
                private int _student_type = 0;
                private int _adm_type = 0;
                private int _from_credit = 0;
                private int _to_credit = 0;
                private double _to_cgpa = 0.0;
                private double _from_cgpa = 0.0;
                private int _minRegCreditLimit = 0;
                private int _PubliishYesNo = 0;

                private int _additional_course = 0;
                private int _degree_type = 0;
                private int _min_schemeLimit = 0;
                private int _max_schemeLimit = 0;
                private int _recordNo = 0;
                private string _sessionName = string.Empty;
                private string branchnos = string.Empty;
                private string branchnosText = string.Empty;

                private int type = 0;
                private int _creditStatus = 0;
                private int _cgpaStatus = 0;
                private int _electGrp1 = 0;
                private int _electGrp2 = 0;
                private int _electGrp3 = 0;
                private int _electGrp4 = 0;
                private int _electGrp5 = 0;
                private int _electGrp6 = 0;
                private int _electGrp7 = 0;
                private int _electGrp8 = 0;
                private int _electGrp9 = 0;
                private int _electGrp10 = 0;
                private int _movableSub = 0;
                private int _electGrp11 = 0;
                private int _electGrp12 = 0;
                private int _electGrp13 = 0;
                private int _electGrp14 = 0;
                private int _electGrp15 = 0;

                #endregion Private members

                #region Public Property Fields

                public int PUBLIISH_YES_NO
                {
                    get { return _PubliishYesNo; }
                    set { _PubliishYesNo = value; }
                }

                public string Semester
                {
                    get { return _semester; }
                    set { _semester = value; }
                }

                public string Semester_Text
                {
                    get { return _semester_text; }
                    set { _semester_text = value; }
                }

                public int MIN_REG_CREDIT_LIMIT
                {
                    get { return _minRegCreditLimit; }
                    set { _minRegCreditLimit = value; }
                }

                public int RECORDNO
                {
                    get { return _recordNo; }
                    set { _recordNo = value; }
                }

                public int DEGREENO
                {
                    get { return _degreeNo; }
                    set { _degreeNo = value; }
                }

                public int DEGREE_TYPE
                {
                    get { return _degree_type; }
                    set { _degree_type = value; }
                }

                public int BRANCHNO
                {
                    get { return _branchNo; }
                    set { _branchNo = value; }
                }

                public string BRANCHNOS
                {
                    get { return branchnos; }
                    set { branchnos = value; }
                }

                public string BRANCHNOS_TEXT
                {
                    get { return branchnosText; }
                    set { branchnosText = value; }
                }

                public int SESSION
                {
                    get { return _session; }
                    set { _session = value; }
                }

                public string SESSIONNAME
                {
                    get { return _sessionName; }
                    set { _sessionName = value; }
                }

                public int ADM_TYPE
                {
                    get { return _adm_type; }
                    set { _adm_type = value; }
                }

                public int TYPE
                {
                    get { return type; }
                    set { type = value; }
                }

                public int STUDENT_TYPE
                {
                    get { return _student_type; }
                    set { _student_type = value; }
                }

                public int MIN_SCHEMELIMIT
                {
                    get { return _min_schemeLimit; }
                    set { _min_schemeLimit = value; }
                }

                public int MAX_SCHEMELIMIT
                {
                    get { return _max_schemeLimit; }
                    set { _max_schemeLimit = value; }
                }

                public int CreditStatus
                {
                    get { return _creditStatus; }
                    set { _creditStatus = value; }
                }

                public int FROM_CREDIT
                {
                    get { return _from_credit; }
                    set { _from_credit = value; }
                }

                public int TO_CREDIT
                {
                    get { return _to_credit; }
                    set { _to_credit = value; }
                }

                public int CgpaStatus
                {
                    get { return _cgpaStatus; }
                    set { _cgpaStatus = value; }
                }

                public double FROM_CGPA
                {
                    get { return _from_cgpa; }
                    set { _from_cgpa = value; }
                }

                public double TO_CGPA
                {
                    get { return _to_cgpa; }
                    set { _to_cgpa = value; }
                }

                public int ADDITIONAL_COURSE
                {
                    get { return _additional_course; }
                    set { _additional_course = value; }
                }

                public int ElectGrp1
                {
                    get { return _electGrp1; }
                    set { _electGrp1 = value; }
                }

                public int ElectGrp2
                {
                    get { return _electGrp2; }
                    set { _electGrp2 = value; }
                }

                public int ElectGrp3
                {
                    get { return _electGrp3; }
                    set { _electGrp3 = value; }
                }

                public int ElectGrp4
                {
                    get { return _electGrp4; }
                    set { _electGrp4 = value; }
                }

                public int ElectGrp5
                {
                    get { return _electGrp5; }
                    set { _electGrp5 = value; }
                }

                public int ElectGrp6
                {
                    get { return _electGrp6; }
                    set { _electGrp6 = value; }
                }

                public int ElectGrp7
                {
                    get { return _electGrp7; }
                    set { _electGrp7 = value; }
                }

                public int ElectGrp8
                {
                    get { return _electGrp8; }
                    set { _electGrp8 = value; }
                }

                public int ElectGrp9
                {
                    get { return _electGrp9; }
                    set { _electGrp9 = value; }
                }

                public int ElectGrp10
                {
                    get { return _electGrp10; }
                    set { _electGrp10 = value; }
                }

                public int MovableSub
                {
                    get { return _movableSub; }
                    set { _movableSub = value; }
                }

                public int ElectGrp11
                {
                    get { return _electGrp11; }
                    set { _electGrp11 = value; }
                }

                public int ElectGrp12
                {
                    get { return _electGrp12; }
                    set { _electGrp12 = value; }
                }

                public int ElectGrp13
                {
                    get { return _electGrp13; }
                    set { _electGrp13 = value; }
                }

                public int ElectGrp14
                {
                    get { return _electGrp14; }
                    set { _electGrp14 = value; }
                }

                public int ElectGrp15
                {
                    get { return _electGrp15; }
                    set { _electGrp15 = value; }
                }

                #endregion Public Property Fields
            }
        }
    }
}