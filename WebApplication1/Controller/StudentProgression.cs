namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentProgression
    {
        /* ---------------------------------------------------------------------------------------------------------- *  Created By  : Bhagyashree Iskape *  Created On  : 10-03-2022 *  Purpose     : To insert and update data of student progression *  Version     : 1.0.0.0 *  ---------------------------------------------------------------------------------------------------------- *  Version Modified On Modified By                 Purpose *  ---------------------------------------------------------------------------------------------------------- *  ----------------------------------------------------------------------------------------------------------*/

        #region Private Members
        private int _student_progression_ruleid = 0;
        private string _rulename = string.Empty;
        private int _college_id = 0;
        private int _ua_section = 0;
        private int _max_fail_module = 0;
        private int _degreeno = 0;
        private int _branchno = 0;
        private int _affiliated_no = 0;
        private int _user_id = 0;

        #endregion Private Members
        #region Public Properties
        public int student_progression_ruleid
        {
            get { return _student_progression_ruleid; }
            set { _student_progression_ruleid = value; }
        }

        public string rulename
        {
            get { return _rulename; }
            set { _rulename = value; }
        }

        public int college_id
        {
            get { return _college_id; }
            set { _college_id = value; }
        }

        public int ua_section
        {
            get { return _ua_section; }
            set { _ua_section = value; }
        }

        public int max_fail_module
        {
            get { return _max_fail_module; }
            set { _max_fail_module = value; }
        }

        public int degreeno
        {
            get { return _degreeno; }
            set { _degreeno = value; }
        }

        public int branchno
        {
            get { return _branchno; }
            set { _branchno = value; }
        }

        public int affiliated_no
        {
            get { return _affiliated_no; }
            set { _affiliated_no = value; }
        }

        public int userid
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        #endregion Public Properties                                                                                                                    }}