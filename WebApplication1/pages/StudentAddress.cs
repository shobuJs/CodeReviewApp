namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class StudentAddress
            {
                #region Private Member

                private int _IDNO = 0;

                private string _LADDRESS = string.Empty;
                private int _LCITY = 0;

                private string _LDISTRICT = string.Empty;
                private int _LSTATE = 0;

                private string _LPINCODE = string.Empty;
                private string _LTELEPHONE = string.Empty;
                private string _LMOBILE = string.Empty;
                private string _LEMAIL = string.Empty;
                private string _LPOSTOFF = string.Empty;
                private string _LPOLICESTATION = string.Empty;
                private string _LTEHSIL = string.Empty;
                private string _PADDRESS = string.Empty;
                private int _PCITY = 0;

                private int _PSTATE = 0;
                private string _GUARDIAN_DESIG = string.Empty;
                private string _ANNUAL_INCOME = string.Empty;
                private string _PDISTRICT = string.Empty;
                private string _PSTD = string.Empty;
                private string _PTELEPHONE = string.Empty;
                private string _PMOBILE = string.Empty;
                private string _PPINCODE = string.Empty;
                private string _PEMAIL = string.Empty;
                private string _PPOSTOFF = string.Empty;
                private string _PPOLICESTATION = string.Empty;
                private string _PTEHSIL = string.Empty;
                private string _FATHER_DESIG = string.Empty;
                private int _OCCUPATION = 0;
                private int _FATHERSJOBDETAIL = 0;
                private string _MOTHER_DESIG = string.Empty;
                private int _MOTHEROCCUPATION = 0;
                private int _MOTHERJOBDETAIL = 0;
                private string _GUARDIANNAME = string.Empty;
                private string _RELATION_GUARDIAN = string.Empty;
                private string _GADDRESS = string.Empty;
                private string _GOCCUPATIONNAME = string.Empty;
                private string _GCITY = string.Empty;
                private string _GDISTRICT = string.Empty;
                private string _GSTATE = string.Empty;
                private string _GLANDLINE = string.Empty;
                private string _GMOBILE = string.Empty;
                private string _GPINCODE = string.Empty;
                private string _GPHONE = string.Empty;
                private string _GEMAIL = string.Empty;
                private string _RAILWAY_STATION = string.Empty;
                private string _BUS_STATION = string.Empty;
                private string _LOCALNAME_STATION = string.Empty;
                private string _COLLEGE_CODE = string.Empty;

                #endregion Private Member

                #region Property Fields

                public int PCITY
                {
                    get { return _PCITY; }
                    set { _PCITY = value; }
                }

                public int LCITY
                {
                    get { return _LCITY; }
                    set { _LCITY = value; }
                }

                public string GOCCUPATIONNAME
                {
                    get { return _GOCCUPATIONNAME; }
                    set { _GOCCUPATIONNAME = value; }
                }

                public int LSTATE
                {
                    get { return _LSTATE; }
                    set { _LSTATE = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                public int OCCUPATION
                {
                    get { return _OCCUPATION; }
                    set { _OCCUPATION = value; }
                }

                public string LADDRESS
                {
                    get { return _LADDRESS; }
                    set { _LADDRESS = value; }
                }

                public string LPOSTOFF
                {
                    get { return _LPOSTOFF; }
                    set { _LPOSTOFF = value; }
                }

                public string LPOLICESTATION
                {
                    get { return _LPOLICESTATION; }
                    set { _LPOLICESTATION = value; }
                }

                public string LTEHSIL
                {
                    get { return _LTEHSIL; }
                    set { _LTEHSIL = value; }
                }

                public string LDISTRICT
                {
                    get { return _LDISTRICT; }
                    set { _LDISTRICT = value; }
                }

                public string PDISTRICT
                {
                    get { return _PDISTRICT; }
                    set { _PDISTRICT = value; }
                }

                public string PPOSTOFF
                {
                    get { return _PPOSTOFF; }
                    set { _PPOSTOFF = value; }
                }

                public string PPOLICESTATION
                {
                    get { return _PPOLICESTATION; }
                    set { _PPOLICESTATION = value; }
                }

                public string PTEHSIL
                {
                    get { return _PTEHSIL; }
                    set { _PTEHSIL = value; }
                }

                public string GCITY
                {
                    get { return _GCITY; }
                    set { _GCITY = value; }
                }

                public string GPINCODE
                {
                    get { return _GPINCODE; }
                    set { _GPINCODE = value; }
                }

                public string GMOBILE
                {
                    get { return _GMOBILE; }
                    set { _GMOBILE = value; }
                }

                public string GLANDLINE
                {
                    get { return _GLANDLINE; }
                    set { _GLANDLINE = value; }
                }

                public string GSTATE
                {
                    get { return _GSTATE; }
                    set { _GSTATE = value; }
                }

                public string GDISTRICT
                {
                    get { return _GDISTRICT; }
                    set { _GDISTRICT = value; }
                }

                public int PSTATE
                {
                    get { return _PSTATE; }
                    set { _PSTATE = value; }
                }

                public string LPINCODE
                {
                    get { return _LPINCODE; }
                    set { _LPINCODE = value; }
                }

                public string LTELEPHONE
                {
                    get { return _LTELEPHONE; }
                    set { _LTELEPHONE = value; }
                }

                public string LMOBILE
                {
                    get { return _LMOBILE; }
                    set { _LMOBILE = value; }
                }

                public string LEMAIL
                {
                    get { return _LEMAIL; }
                    set { _LEMAIL = value; }
                }

                public string PADDRESS
                {
                    get { return _PADDRESS; }
                    set { _PADDRESS = value; }
                }

                public string ANNUAL_INCOME
                {
                    get { return _ANNUAL_INCOME; }
                    set { _ANNUAL_INCOME = value; }
                }

                public string GUARDIANDESIGNATION
                {
                    get { return _GUARDIAN_DESIG; }
                    set { _GUARDIAN_DESIG = value; }
                }

                public string PSTD
                {
                    get { return _PSTD; }
                    set { _PSTD = value; }
                }

                public string PTELEPHONE
                {
                    get { return _PTELEPHONE; }
                    set { _PTELEPHONE = value; }
                }

                public string PMOBILE
                {
                    get { return _PMOBILE; }
                    set { _PMOBILE = value; }
                }

                public string PPINCODE
                {
                    get { return _PPINCODE; }
                    set { _PPINCODE = value; }
                }

                public string PEMAIL
                {
                    get { return _PEMAIL; }
                    set { _PEMAIL = value; }
                }

                public string FATHER_DESIG
                {
                    get { return _FATHER_DESIG; }
                    set { _FATHER_DESIG = value; }
                }

                public int FATHERJOBDETAIL
                {
                    get { return _FATHERSJOBDETAIL; }
                    set { _FATHERSJOBDETAIL = value; }
                }

                public string MOTHERDESIGNATION
                {
                    get { return _MOTHER_DESIG; }
                    set { _MOTHER_DESIG = value; }
                }

                public int MOTHEROCCUPATION
                {
                    get { return _MOTHEROCCUPATION; }
                    set { _MOTHEROCCUPATION = value; }
                }

                public int MOTHERJOBDETAIL
                {
                    get { return _MOTHERJOBDETAIL; }
                    set { _MOTHERJOBDETAIL = value; }
                }

                public string GUARDIANNAME
                {
                    get { return _GUARDIANNAME; }
                    set { _GUARDIANNAME = value; }
                }

                public string RELATION_GUARDIAN
                {
                    get { return _RELATION_GUARDIAN; }
                    set { _RELATION_GUARDIAN = value; }
                }

                public string GADDRESS
                {
                    get { return _GADDRESS; }
                    set { _GADDRESS = value; }
                }

                public string GPHONE
                {
                    get { return _GPHONE; }
                    set { _GPHONE = value; }
                }

                public string GEMAIL
                {
                    get { return _GEMAIL; }
                    set { _GEMAIL = value; }
                }

                public string RAILWAY_STATION
                {
                    get { return _RAILWAY_STATION; }
                    set { _RAILWAY_STATION = value; }
                }

                public string BUS_STATION
                {
                    get { return _BUS_STATION; }
                    set { _BUS_STATION = value; }
                }

                public string LOCALNAME_STATION
                {
                    get { return _LOCALNAME_STATION; }
                    set { _LOCALNAME_STATION = value; }
                }

                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }

                #endregion Property Fields
            }
        }
    }
}