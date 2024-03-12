namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class IntakeLeadMapping
    {
        /* ----------------------------------------------------------------------------------------------------------         *  Created By  : Bhagyashree Iskape         *  Created On  : 07-03-2022         *  Purpose     : To insert data          *  Version     : 1.0.0.0         *  ----------------------------------------------------------------------------------------------------------         *  Version Modified On Modified By                 Purpose         *  ----------------------------------------------------------------------------------------------------------         *  ----------------------------------------------------------------------------------------------------------        */

        #region Private Members

        private int _mappingid = 0;
        private int _intakeno = 0;
        private string _ua_section = string.Empty;
        private string _area_int_no = string.Empty;

        #endregion Private Members

        #region Public Properties

        public int mappingid
        {
            get { return _mappingid; }
            set { _mappingid = value; }
        }

        public int intakeno
        {
            get { return _intakeno; }
            set { _intakeno = value; }
        }

        public string ua_section
        {
            get { return _ua_section; }
            set { _ua_section = value; }
        }

        public string area_int_no
        {
            get { return _area_int_no; }
            set { _area_int_no = value; }
        }

        #endregion Public Properties
    }
}