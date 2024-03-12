using System;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ModuleRuleBook
            {
                #region Declare Variable

                private int _ProRataDegreeno;
                private int _ProRataBranchno;
                private int _Ca_Valid_Session;
                private int _rule_no;
                private int _registration_type;
                private int _sessionno;
                private int _college_id;
                private string _college_ids;
                private int _ua_section;
                private int _semesterno;
                private int _admbatch;
                private DateTime _commencement_date;
                private DateTime _reg_start_date;
                private DateTime _reg_end_date;
                private DateTime _fixed_late_fee_date;
                private decimal _fixed_late_fee;
                private DateTime _further_late_fee_date;
                private decimal _further_late_fee_per;
                private int _max_course_limit;
                private int _min_course_limit;
                private decimal _max_credit_limit;
                private decimal _min_credit_limit;
                private decimal _per_course_fee;
                private int _pro_reg_max_course;
                private decimal _pro_reg_per_course_fee;
                private int _pro_nrg_max_course;
                private decimal _pro_nrg_per;
                private decimal _pro_final_module_fees;
                private decimal _pro_nrg_after_max_course_fee;
                private decimal _pro_final_module_per;
                private int _cgpa_status;
                private decimal _from_cgpa;
                private decimal _to_cgpa;
                private int _lock;
                private DateTime _lock_date;
                private int _ua_no;
                private string _ip_address;
                private DateTime _auditdate;
                private int _active;

                private int _Pro_Non_Regular_Max_Credits;
                private decimal _Pro_Non_Regular_Max_Credits_fees;

                private int _Pro_Non_Regular_Max_Module_Count;
                private decimal _Pro_Non_Regular_Max_Module_Fees;

                private int _Pro_Non_Regular_Option;

                #endregion Declare Variable

                #region Getter & Setter

                public int ProRataDegreeno
                {
                    get { return _ProRataDegreeno; }
                    set { _ProRataDegreeno = value; }
                }

                public int ProRataBranchno
                {
                    get { return _ProRataBranchno; }
                    set { _ProRataBranchno = value; }
                }

                public int Ca_Valid_Session
                {
                    get { return _Ca_Valid_Session; }
                    set { _Ca_Valid_Session = value; }
                }

                public int Pro_Non_Regular_Option
                {
                    get { return _Pro_Non_Regular_Option; }
                    set { _Pro_Non_Regular_Option = value; }
                }

                public int Pro_Non_Regular_Max_Credits
                {
                    get { return _Pro_Non_Regular_Max_Credits; }
                    set { _Pro_Non_Regular_Max_Credits = value; }
                }

                public decimal Pro_Non_Regular_Max_Credits_fees
                {
                    get { return _Pro_Non_Regular_Max_Credits_fees; }
                    set { _Pro_Non_Regular_Max_Credits_fees = value; }
                }

                public int Pro_Non_Regular_Max_Module_Count
                {
                    get { return _Pro_Non_Regular_Max_Module_Count; }
                    set { _Pro_Non_Regular_Max_Module_Count = value; }
                }

                public decimal Pro_Non_Regular_Max_Module_Fees
                {
                    get { return _Pro_Non_Regular_Max_Module_Fees; }
                    set { _Pro_Non_Regular_Max_Module_Fees = value; }
                }

                public int Rule_No
                {
                    get { return _rule_no; }
                    set { _rule_no = value; }
                }

                public int Registration_Type
                {
                    get { return _registration_type; }
                    set { _registration_type = value; }
                }

                public int Sessionno
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public int College_id
                {
                    get { return _college_id; }
                    set { _college_id = value; }
                }

                public string College_ids
                {
                    get { return _college_ids; }
                    set { _college_ids = value; }
                }

                public int UA_Section
                {
                    get { return _ua_section; }
                    set { _ua_section = value; }
                }

                public int Semesterno
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public int Admbatch
                {
                    get { return _admbatch; }
                    set { _admbatch = value; }
                }

                public DateTime Commencement_Date
                {
                    get { return _commencement_date; }
                    set { _commencement_date = value; }
                }

                public DateTime Reg_Start_Date
                {
                    get { return _reg_start_date; }
                    set { _reg_start_date = value; }
                }

                public DateTime Reg_End_Date
                {
                    get { return _reg_end_date; }
                    set { _reg_end_date = value; }
                }

                public DateTime Fixed_Late_Fee_Date
                {
                    get { return _fixed_late_fee_date; }
                    set { _fixed_late_fee_date = value; }
                }

                public decimal Fixed_Late_Fee
                {
                    get { return _fixed_late_fee; }
                    set { _fixed_late_fee = value; }
                }

                public DateTime Further_Late_Fee_Date
                {
                    get { return _further_late_fee_date; }
                    set { _further_late_fee_date = value; }
                }

                public decimal Further_Late_Fee_Per
                {
                    get { return _further_late_fee_per; }
                    set { _further_late_fee_per = value; }
                }

                public int Max_Course_Limit
                {
                    get { return _max_course_limit; }
                    set { _max_course_limit = value; }
                }

                public int Min_Course_Limit
                {
                    get { return _min_course_limit; }
                    set { _min_course_limit = value; }
                }

                public decimal Max_Credit_Limit
                {
                    get { return _max_credit_limit; }
                    set { _max_credit_limit = value; }
                }

                public decimal Min_Credit_Limit
                {
                    get { return _min_credit_limit; }
                    set { _min_credit_limit = value; }
                }

                public decimal Per_Course_Fee
                {
                    get { return _per_course_fee; }
                    set { _per_course_fee = value; }
                }

                public int Pro_Reg_Max_Course
                {
                    get { return _pro_reg_max_course; }
                    set { _pro_reg_max_course = value; }
                }

                public decimal Pro_Reg_Per_Course_Fee
                {
                    get { return _pro_reg_per_course_fee; }
                    set { _pro_reg_per_course_fee = value; }
                }

                public int Pro_Nrg_Max_Course
                {
                    get { return _pro_nrg_max_course; }
                    set { _pro_nrg_max_course = value; }
                }

                public decimal Pro_Nrg_Per
                {
                    get { return _pro_nrg_per; }
                    set { _pro_nrg_per = value; }
                }

                public decimal Pro_final_module_fees
                {
                    get { return _pro_final_module_fees; }
                    set { _pro_final_module_fees = value; }
                }

                public decimal Pro_final_module_per
                {
                    get { return _pro_final_module_per; }
                    set { _pro_final_module_per = value; }
                }

                public decimal Pro_Nrg_After_Max_Course_Fee
                {
                    get { return _pro_nrg_after_max_course_fee; }
                    set { _pro_nrg_after_max_course_fee = value; }
                }

                public int CGPA_Status
                {
                    get { return _cgpa_status; }
                    set { _cgpa_status = value; }
                }

                public decimal From_CGPA
                {
                    get { return _from_cgpa; }
                    set { _from_cgpa = value; }
                }

                public decimal To_CGPA
                {
                    get { return _to_cgpa; }
                    set { _to_cgpa = value; }
                }

                public int Lock
                {
                    get { return _lock; }
                    set { _lock = value; }
                }

                public DateTime Lock_Date
                {
                    get { return _lock_date; }
                    set { _lock_date = value; }
                }

                public int UA_NO
                {
                    get { return _ua_no; }
                    set { _ua_no = value; }
                }

                public string Ip_Address
                {
                    get { return _ip_address; }
                    set { _ip_address = value; }
                }

                public DateTime AuditDate
                {
                    get { return _auditdate; }
                    set { _auditdate = value; }
                }

                public int Active
                {
                    get { return _active; }
                    set { _active = value; }
                }

                #endregion Getter & Setter
            }
        }
    }
}