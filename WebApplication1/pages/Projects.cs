namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Projects_Coordinator
    {
        public int AllotmentNo { get; set; }
        public int AcademicSessionNo { get; set; }
        public int FacultyNo { get; set; }
        public int ProgramNo { get; set; }
        public int CoordinatorNo { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ProjectGroupCreation
    {
        public int ProjectGroupNo { get; set; }
        public int AcademicSessionNo { get; set; }
        public int FacultyNo { get; set; }//CollegeId
        public int ProgramNo { get; set; }//DegreeBranch
        public int ModuleNo { get; set; }//subjectId
        public string ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string GroupMemberIds { get; set; }//IDNOS
        public int SupervisorId { get; set; }//UA_NO
        public int CoSupervisorId { get; set; }//UA_NO
        public int ExaminerId { get; set; }//UA_NO
        public int CoExaminerId { get; set; }//UA_NO
        public int CreatedBy { get; set; }
    }
}