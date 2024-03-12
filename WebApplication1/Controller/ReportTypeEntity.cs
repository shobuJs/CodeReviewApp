using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class ReportTypeEntity
    {
        public int PageID { get; set; }
        public string PageName { get; set; }
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public int ProcedureID { get; set; }
        public string ProcedureName { get; set; }
        public int Status { get; set; }
        public int UserNo { get; set; }
        public int CheckStatus { get; set; }
        public int SqNo { get; set; }
        public string URL_NAME { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int Session { get; set; }
        public int Campus { get; set; }
        public int College { get; set; }
        public int Course { get; set; }
        public int Semester { get; set; }
        public int Subject_Type { get; set; }
        public int Subject { get; set; }
    }
}
