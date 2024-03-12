<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RuleEngineDefiniton.aspx.cs" Inherits="ACADEMIC_RuleEngineDefiniton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>


        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRuleEngine"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updRuleEngine" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAMINATION RULE DEFINITION</h3>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="form-group col-md-4">
                                        <span style="color: red">*</span><label>Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Save">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-md-4">
                                        <span style="color: red">*</span><label>Branch</label>
                                        <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlbranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Save">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <span style="color: red">*</span><label>Regulation</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Regulation" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="Save">
                                        </asp:RequiredFieldValidator>
                                    </div>




                                    <div class="col-md-12 text-center">


                                        <asp:Button ID="btnSave" runat="server" TabIndex="15" Text="Save"
                                            Width="100px" OnClick="btnSave_Click" OnClientClick="return Validateddl()" ToolTip="SAVE" CssClass="btn btn-outline-info" />&nbsp;&nbsp;
                                   
                                    <asp:Button ID="btnreport" runat="server" TabIndex="15" Text="Report" ValidationGroup="Save"
                                        Width="100px" OnClick="btnreport_Click" CssClass="btn btn-outline-info" />&nbsp;&nbsp;
                                  
                                    <asp:Button ID="btnclear" runat="server" TabIndex="15" Text="Clear" ValidationGroup="Save"
                                        Width="100px" OnClick="btnclear_Click" ToolTip="Clear" CssClass="btn btn-outline-danger" />&nbsp;&nbsp;
                                
                                
                                 
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <asp:ListView ID="lvSubjectType" runat="server">
                                            <LayoutTemplate>
                                                <div id="listViewGrid" class="vista-grid">
                                                    <div class="titlebar">
                                                        SUBJECT TYPE LIST FOR ENTERING EXAMINATION RULES
                                                    </div>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th align="left">Subject Type 
                                                                </th>
                                                                <th align="left">CIE Scale Down %
                                                                </th>
                                                                <th align="left">SEE Scale Down % 
                                                                </th>
                                                                <th align="left">CIE Passing Criteria in % 
                                                                </th>
                                                                <th align="left">SEE Passing Criteria in % 
                                                                </th>
                                                                <th align="left">Total Passing Criteria
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblsubtype" Text='<%# Eval("SUBNAME")%>' ToolTip='<%# Eval("SUBID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalCie" runat="server" Text='<%# Eval("CIE_SCALE")%>' ToolTip="Enter Final CIE Scale Down " onkeypress="CheckNumeric(event);" MaxLength="10" Visible="true">

                                                         
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFinalCie"
                                                            ValidationGroup="Group1" runat="server" ErrorMessage="CIE Scale Down % is required." />


                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFInalEse" runat="server" Text='<%# Eval("ESE_SCALE")%>' ToolTip="SEE Scale Down" onkeypress="CheckNumeric(event);" MaxLength="10" Visible="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFInalEse"
                                                            ValidationGroup="Group1" runat="server" ErrorMessage="SEE Scale Down % is required." />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalCiePer" runat="server" Text='<%# Eval("CIE_PASSINGCRITERIA")%>' ToolTip="Enter CIE Passing Criteria in % " onkeypress="CheckNumeric(event);" MaxLength="10" Visible="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFinalCiePer"
                                                            ValidationGroup="Group1" runat="server" ErrorMessage="CIE Passing Criteria in %  is required." />


                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalEsePer" runat="server" Text='<%# Eval("ESE_PASSINGCRITERIA")%>' ToolTip="Enter SEE Passing Criteria in % " onkeypress="CheckNumeric(event);" MaxLength="10" Visible="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtFinalEsePer"
                                                            ValidationGroup="Group1" runat="server" ErrorMessage="SEE Criteria in %  is required." />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalPassingCriteria" runat="server" Text='<%# Eval("TOT_PASSINGCRITERIA")%>' ToolTip="Enter Passing Criteria" onkeypress="CheckNumeric(event);" MaxLength="10" Visible="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtFinalPassingCriteria"
                                                            ValidationGroup="Group1" runat="server" ErrorMessage="Total Passing Criteria is required." />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 46) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>
    <script type="text/javascript">
        function Validate() {
            debugger;

            var isValid = false;
            isValid = Page_ClientValidate('Group1');
            if (isValid) {
                isValid = Page_ClientValidate('Group2');
            }
            if (isValid) {
                isValid = Page_ClientValidate('Group3');
            }
            if (isValid) {
                isValid = Page_ClientValidate('Group4');
            }
            if (isValid) {
                isValid = Page_ClientValidate('Group5');
            }

            return isValid;

        }

    </script>

    <script>
        function Validateddl() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDegree").value == "0") {
                alert("Please select Degree");
                return false;

            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlbranch").value == "0") {
                alert("Please select Branch");
                return false;

            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlScheme").value == "0") {
                alert("Please select Scheme");
                return false;

            }
            else {
                Validate();
            }
        }
    </script>
</asp:Content>
