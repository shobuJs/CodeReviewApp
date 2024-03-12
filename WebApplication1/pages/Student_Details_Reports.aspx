<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Details_Reports.aspx.cs" Inherits="ACADEMIC_Student_Details_Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <link href="../jquery/bootstrap-multiselect.css" rel="stylesheet" />   
    <script src="../jquery/jquery-3.2.1.min.js"></script>   
    <script src="../jquery/bootstrap-multiselect.js"></script>

    <%--Added By Abhinay Lad [24-02_2020]--%>
    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            $('.multiselect').css("width", "100%");
            $(".multiselect-container").css("width", "100%");
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    maxWidth: '100%',
                    includeSelectAllOption: true
                });
                $('.multiselect').css("width", "100%");
                $(".multiselect-container").css("width", "100%")
            });
        });
    </script>
   <%--Ended By Abhinay Lad [24-02_2020]--%>

    <script type="text/javascript">
        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                //   InitAutoCompl();
            }

        });

    </script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlStudReport" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="pnlStudReport" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT DETAILS REPORTS</h3>
                            <div style="color: Red; font-weight: bold">Note : * Marked fields are mandatory</div>
                        </div>

                        <div class="box-body">
                            <div class="col-sm-12">
                                 <div class="form-group col-md-4" id="divAdmYear" runat="server" visible="True">
                                    <label><span style="color: red;">*</span>&nbsp;Admission Year</label>
                                    <asp:DropDownList ID="ddlAdmYear" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="True" TabIndex="1"
                                        ToolTip="Please Select Admission Year" OnSelectedIndexChanged="ddlAdmYear_SelectedIndexChanged">                                        
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmYear" Display="None"
                                        ErrorMessage="Please Select Admission Year" SetFocusOnError="True" ValidationGroup="AdmReg" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label>Degree</label>
                                    <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control multi-select-demo"
                                        SelectionMode="multiple" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                </div>

                                <div class="form-group col-md-4">
                                    <label>Branch</label>
                                    <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                        SelectionMode="multiple"></asp:ListBox>
                                </div>

                                <div class="form-group col-md-4" id="divSem" runat="server">
                                    <label><%--<span style="color: red;">*</span>--%>&nbsp;Semester</label>
                                    <asp:DropDownList ID="ddlSem" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="4"
                                        ToolTip="Please Select Semester">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester"
                                        SetFocusOnError="True" ValidationGroup="vgShow" InitialValue="0">
                                    </asp:RequiredFieldValidator>                                    
                                </div>
                            </div>
                            <div class="box-footer col-sm-12">
                                <p class="text-center">
                                    <asp:Button ID="btnProject" runat="server" Text="Project Excel Report" OnClick="btnProject_Click" TabIndex="5" CssClass="btn btn-outline-primary" ToolTip="Project Excel Report" CausesValidation="true" ValidationGroup="AdmReg" />
                                    <asp:Button ID="btnInternshipExcel" runat="server" Text="Internship Excel Report" OnClick="btnInternshipExcel_Click" TabIndex="6" CssClass="btn btn-outline-primary" ToolTip="Internship Excel Report" CausesValidation="true" ValidationGroup="AdmReg" />
                                    <asp:Button ID="btnAwards" runat="server" Text="Awards and Achievements Excel Report" OnClick="btnAwards_Click" TabIndex="7" CssClass="btn btn-outline-primary" ToolTip="Awards and Achievements Excel Report" CausesValidation="true" ValidationGroup="AdmReg" />
                                    <asp:Button ID="btnPublications" runat="server" Text="Publications Excel Report" OnClick="btnPublications_Click" TabIndex="8" CssClass="btn btn-outline-primary" ToolTip="Publications Excel Report" CausesValidation="true" ValidationGroup="AdmReg" />
                                    <asp:Button ID="btnScholarship" runat="server" Text="Scholarship  Excel Report" OnClick="btnScholarship_Click" TabIndex="9" CssClass="btn btn-outline-primary" ToolTip="Scholarship  Excel Report" CausesValidation="true" ValidationGroup="AdmReg" />
                                 </p>
                                <p class="text-center">
                                    <asp:Button ID="btnIndusVisit" runat="server" Text="Industrial Visit Excel Report" OnClick="btnIndusVisit_Click" TabIndex="10" CssClass="btn btn-outline-primary" ToolTip="Industrial Visit Excel Report" CausesValidation="true" />
                                    <asp:Button ID="btnMouDetails" runat="server" Text="Mou Details Excel Report" OnClick="btnMouDetails_Click" TabIndex="11" CssClass="btn btn-outline-primary" ToolTip="Mou Details Excel Report" CausesValidation="true" />
                                    <asp:Button ID="btnExtension" runat="server" Text="Extension Details Excel Report" OnClick="btnExtension_Click" TabIndex="12" CssClass="btn btn-outline-primary" ToolTip="Extension Details Excel Report" CausesValidation="true" />   
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" TabIndex="13" ToolTip="Cancel" CausesValidation="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="AdmReg" ShowSummary="False" runat="server" ShowMessageBox="True" />
                                </p>
                            </div>

                            </div>
                        </div>
                    </div>
                <div id="divMsg" runat="server">
                </div>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProject" />
            <asp:PostBackTrigger ControlID="btnInternshipExcel" />
            <asp:PostBackTrigger ControlID="btnAwards" />
            <asp:PostBackTrigger ControlID="btnPublications" />
            <asp:PostBackTrigger ControlID="btnScholarship" />
            <asp:PostBackTrigger ControlID="btnIndusVisit" />
            <asp:PostBackTrigger ControlID="btnMouDetails" />
            <asp:PostBackTrigger ControlID="btnExtension" />
        </Triggers>
        </asp:UpdatePanel>


</asp:Content>

