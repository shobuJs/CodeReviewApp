namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class QualifiedExam
    {
        #region Private Fields

        private int stqexno = 0;
        private int idno = 0;
        private int qualifyno = 0;
        private string year_of_exam = string.Empty;
        private string regno = string.Empty;
        private string qexmrollno = string.Empty;
        private string school_college_name = string.Empty;
        private string board = string.Empty;
        private string grade = string.Empty;
        private string attempt = string.Empty;
        private int meritno = 0;
        private int marksObtained = 0;
        private int out_of_marks = 0;
        private int max_marks = 0;
        private decimal per = 0.0m;
        private decimal percentile = 0.0m;
        private int hsc_pcm = 0;
        private int hsc_pcm_max = 0;
        private int hsc_bio = 0;
        private int hsc_bio_max = 0;
        private int hsc_eng = 0;
        private int hsc_eng_max = 0;
        private int hsc_mat = 0;
        private int hsc_mat_max = 0;
        private int hsc_che = 0;
        private int hsc_che_max = 0;
        private int hsc_phy = 0;
        private int hsc_phy_max = 0;
        private string res_topic = string.Empty;
        private string supervisor_name1 = string.Empty;
        private string supervisor_name2 = string.Empty;
        private string college_code = string.Empty;
        private string college_address = string.Empty;
        private string qual_medium = string.Empty;

        #endregion Private Fields

        #region Public Properties

        public string College_code
        {
            get { return college_code; }
            set { college_code = value; }
        }

        public string Supervisor_name2
        {
            get { return supervisor_name2; }
            set { supervisor_name2 = value; }
        }

        public string Supervisor_name1
        {
            get { return supervisor_name1; }
            set { supervisor_name1 = value; }
        }

        public string Res_topic
        {
            get { return res_topic; }
            set { res_topic = value; }
        }

        public int Hsc_phy_max
        {
            get { return hsc_phy_max; }
            set { hsc_phy_max = value; }
        }

        public int Hsc_phy
        {
            get { return hsc_phy; }
            set { hsc_phy = value; }
        }

        public int Hsc_che_max
        {
            get { return hsc_che_max; }
            set { hsc_che_max = value; }
        }

        public int Hsc_che
        {
            get { return hsc_che; }
            set { hsc_che = value; }
        }

        public int Hsc_mat_max
        {
            get { return hsc_mat_max; }
            set { hsc_mat_max = value; }
        }

        public int Hsc_mat
        {
            get { return hsc_mat; }
            set { hsc_mat = value; }
        }

        public int Hsc_eng_max
        {
            get { return hsc_eng_max; }
            set { hsc_eng_max = value; }
        }

        public int Hsc_eng
        {
            get { return hsc_eng; }
            set { hsc_eng = value; }
        }

        public int Hsc_bio_max
        {
            get { return hsc_bio_max; }
            set { hsc_bio_max = value; }
        }

        public int Hsc_bio
        {
            get { return hsc_bio; }
            set { hsc_bio = value; }
        }

        public int Hsc_pcm_max
        {
            get { return hsc_pcm_max; }
            set { hsc_pcm_max = value; }
        }

        public int Hsc_pcm
        {
            get { return hsc_pcm; }
            set { hsc_pcm = value; }
        }

        public decimal Percentile
        {
            get { return percentile; }
            set { percentile = value; }
        }

        public decimal Per
        {
            get { return per; }
            set { per = value; }
        }

        public int Max_marks
        {
            get { return max_marks; }
            set { max_marks = value; }
        }

        public int Out_of_marks
        {
            get { return out_of_marks; }
            set { out_of_marks = value; }
        }

        public int MarksObtained
        {
            get { return marksObtained; }
            set { marksObtained = value; }
        }

        public int Meritno
        {
            get { return meritno; }
            set { meritno = value; }
        }

        public string Attempt
        {
            get { return attempt; }
            set { attempt = value; }
        }

        public string Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public string Board
        {
            get { return board; }
            set { board = value; }
        }

        public string School_college_name
        {
            get { return school_college_name; }
            set { school_college_name = value; }
        }

        public string Qexmrollno
        {
            get { return qexmrollno; }
            set { qexmrollno = value; }
        }

        public string Regno
        {
            get { return regno; }
            set { regno = value; }
        }

        public string Year_of_exam
        {
            get { return year_of_exam; }
            set { year_of_exam = value; }
        }

        public string College_address
        {
            get { return college_address; }
            set { college_address = value; }
        }

        public string Qual_medium
        {
            get { return qual_medium; }
            set { qual_medium = value; }
        }

        public int Qualifyno
        {
            get { return qualifyno; }
            set { qualifyno = value; }
        }

        public int Idno
        {
            get { return idno; }
            set { idno = value; }
        }

        public int Stqexno
        {
            get { return stqexno; }
            set { stqexno = value; }
        }

        #endregion Public Properties
    }
}