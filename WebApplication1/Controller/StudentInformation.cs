using System;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentInformation
    {
        #region Properties

        //Student Project Details

        #region Project Details

        public int IDNO { get; set; }
        public int Semesterno { get; set; }
        public string ProjectName { get; set; }
        public string IndustryName { get; set; }
        public string IndustryAddress { get; set; }
        public string Project_Details { get; set; }
        public string Duration { get; set; }

        //public string Grant_Received    { get; set; }
        public string Supervisor_Details { get; set; }

        public string Mentor_College { get; set; }
        public string Project_Type { get; set; }
        public int Industry_Type { get; set; }
        public string ProjectFilename { get; set; }
        public string ProjectFilePath { get; set; }
        public int Ua_no { get; set; }
        public string Ip_address { get; set; }
        public string College_code { get; set; }

        #endregion Project Details

        #region Internship Details

        public string Stud_Par { get; set; }
        public string Teacher_par { get; set; }
        public string Link { get; set; }
        public string Industry_name { get; set; }
        public string Address_industry { get; set; }
        public string Internship_details { get; set; }

        //public DateTime From_date			{ get; set; }
        //public DateTime To_date			    { get; set; }
        //  public string   Duration			{ get; set; }
        public string Remarks { get; set; }

        //public string      Stipend			{ get; set; }
        //public string Stipend { get; set; }
        public int Degreeno { get; set; }

        public int Branchno { get; set; }
        public int Approved_By { get; set; }
        public string Technical_person { get; set; }
        public string Mobile_no { get; set; }
        public string Emailid { get; set; }

        #endregion Internship Details

        #region Publications Details

        public string Title_name { get; set; }
        public string Journal_name { get; set; }
        public string Volume { get; set; }
        public int Page_no { get; set; }
        public string Issue_no { get; set; }
        public string Authors { get; set; }

        //public DateTime     Date_received   { get; set; }
        public string Award_details { get; set; }

        public double Amt_received { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Author_role { get; set; }
        public string Author_name { get; set; }

        #endregion Publications Details

        // Added By Naresh Beerla on 29/08/2020

        #region Extension Activities

        public string Activity_Name { get; set; }
        public string Award_Name { get; set; }
        public string Award_GovtName { get; set; }
        private System.Nullable<System.DateTime> _Award_date;

        public System.Nullable<System.DateTime> Award_Date
        {
            get { return _Award_date; }
            set { _Award_date = value; }
        }

        public string Organization_unit { get; set; }
        public string Scheme_Name { get; set; }
        public int Student_Participated { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Activity_No { get; set; }

        #endregion Extension Activities

        #region Student Bank Details

        public int Bankno { get; set; }
        public string Acc_no { get; set; }
        public string Bank_branch { get; set; }
        public string Ifsc_code { get; set; }

        #endregion Student Bank Details

        #region Scholarship Details

        public string Scholarship_name { get; set; }
        public string Organization_name { get; set; }

        //public DateTime         Apply_date                  { get; set; }
        //public DateTime         Sanction_date               { get; set; }
        public string Organization_type { get; set; }

        public Double Amt_applied { get; set; }
        public Double Amt_sanctioned { get; set; }
        public string Payment_details { get; set; }
        public string Student_bank_details { get; set; }
        public int feewavier { get; set; }

        #endregion Scholarship Details

        #region Achievements Details

        public string Certificate_name { get; set; }

        // public string Organization_name     { get; set; }
        public string Organization_address { get; set; }

        // public string Award_details         { get; set; }
        //  public Double Amt_received          { get; set; }

        #endregion Achievements Details

        #region Industrial Visit

        public string Visit_purpose { get; set; }
        public DateTime Visit_date { get; set; }
        public string no_Students { get; set; }
        public string no_Faculities { get; set; }
        public int branchno { get; set; }
        public int semesterno { get; set; }
        public int sectiono { get; set; }
        public int admyear { get; set; }
        public string Indus_file_name { get; set; }
        public string Indus_file_path { get; set; }
        public DataTable Student_data { get; set; }

        #endregion Industrial Visit

        #region Industrial Link

        public int _Indlinkno = 0;
        public string Company_name { get; set; }
        public string Company_address { get; set; }
        public DateTime Mou_from { get; set; }
        public DateTime Mou_to { get; set; }
        public int Mou_type { get; set; }
        public string Activities { get; set; }

        //  public string   Remarks         { get; set; }
        public int Live_status { get; set; }

        public int Indlinkno
        {
            get { return _Indlinkno; }
            set { _Indlinkno = value; }
        }

        #endregion Industrial Link

        public Double Stipend { get; set; }

        public Double Grant_Received { get; set; }

        private System.Nullable<System.DateTime> _From_date;
        private System.Nullable<System.DateTime> _To_date;
        private System.Nullable<System.DateTime> _Date_received;
        private System.Nullable<System.DateTime> _Apply_date;
        private System.Nullable<System.DateTime> _Sanction_date;

        public System.Nullable<System.DateTime> From_date
        {
            get { return _From_date; }
            set { _From_date = value; }
        }

        public System.Nullable<System.DateTime> To_date
        {
            get { return _To_date; }
            set { _To_date = value; }
        }

        public System.Nullable<System.DateTime> Date_received
        {
            get { return _Date_received; }
            set { _Date_received = value; }
        }

        public System.Nullable<System.DateTime> Apply_date
        {
            get { return _Apply_date; }
            set { _Apply_date = value; }
        }

        public System.Nullable<System.DateTime> Sanction_date
        {
            get { return _Sanction_date; }
            set { _Sanction_date = value; }
        }

        #endregion Properties
    }
}