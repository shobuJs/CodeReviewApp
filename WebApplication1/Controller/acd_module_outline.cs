using System;

/* ----------------------------------------------------------------------------------------------------------
 *  Created By  : Sachin Lohakare
 *  Created On  :
 *  Purpose     :
 *  Version     : 1.0.0.0
 *  ----------------------------------------------------------------------------------------------------------
 *  Version Modified On Modified By                 Purpose
 *  ----------------------------------------------------------------------------------------------------------

 *  ----------------------------------------------------------------------------------------------------------
*/

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class acd_module_outline
    {
        #region Private Members

        private int _moduleoutlineid = 0;
        private int _deptno = 0;
        private int _courseno = 0;
        private string _document_path = string.Empty;
        private DateTime _with_effect_from;
        private string _outline;
        private int _created_by = 0;
        private DateTime _created_on;
        private int _modified_by = 0;
        private int _userid = 0;
        private DateTime? _modified_on;

        #endregion Private Members

        #region Public Properties

        public int moduleoutlineid
        {
            get { return _moduleoutlineid; }
            set { _moduleoutlineid = value; }
        }

        public int deptno
        {
            get { return _deptno; }
            set { _deptno = value; }
        }

        public int courseno
        {
            get { return _courseno; }
            set { _courseno = value; }
        }

        public string document_path
        {
            get { return _document_path; }
            set { _document_path = value; }
        }

        public DateTime with_effect_from
        {
            get { return _with_effect_from; }
            set { _with_effect_from = value; }
        }

        public string outline
        {
            get { return _outline; }
            set { _outline = value; }
        }

        public int created_by
        {
            get { return _created_by; }
            set { _created_by = value; }
        }

        public DateTime created_on
        {
            get { return _created_on; }
            set { _created_on = value; }
        }

        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        public int modified_by
        {
            get { return _modified_by; }
            set { _modified_by = value; }
        }

        public DateTime? modified_on
        {
            get { return _modified_on; }
            set { _modified_on = value; }
        }

        #endregion Public Properties
    }
}