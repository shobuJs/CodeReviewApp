<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Coursewise_Consolidate_Report.aspx.cs" Inherits="ACADEMIC_Coursewise_Consolidate_Report"
    MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<%--    <script src="../JAVASCRIPTS/jquery.min_1.js" type="text/javascript" language="javascript"></script>
    <script src="../JAVASCRIPTS/jquery-ui.min_1.js" type="text/javascript" language="javascript"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeacher"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader"></div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%--<script type="text/javascript" language="javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <asp:UpdatePanel ID="updTeacher" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>CONSOLIDATED MARKS REPORT</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-md-8 form-group">
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Session </label>
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Institute / College Name</label>
                                    <asp:DropDownList ID="ddlCollegeName" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollegeName"
                                        Display="None" ErrorMessage="Please Select Institute / College " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollegeName"
                                        Display="None" ErrorMessage="Please Select Institute / College " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Degree </label>
                                    <asp:DropDownList ID="ddlDegree" TabIndex="3" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Branch </label>
                                    <asp:DropDownList ID="ddlBranch" TabIndex="4" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Regulation </label>
                                    <asp:DropDownList ID="ddlscheme" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="true"
                                        AppendDataBoundItems="true" TabIndex="5">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvscheme" Enabled="false" runat="server" ControlToValidate="ddlscheme"
                                        Display="None" ErrorMessage="Please Select Regulation" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlscheme"
                                        Display="None" ErrorMessage="Please Select Regulation" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Semester </label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="markreport"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;"></span>Section </label>
                                    <asp:DropDownList ID="ddlsectionno" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" TabIndex="6">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-md-4">
                                    <label>Export Type</label>
                                    <asp:RadioButtonList ID="rdbExporttyye" runat="server"
                                        RepeatDirection="Horizontal" TabIndex="7">
                                        <asp:ListItem Selected="True">&nbsp;&nbsp;PDF&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem>&nbsp;&nbsp;EXCEL&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-4 form-group">
                                <fieldset class="fieldset" style="padding: 5px; color: Green">
                                    <legend style="text-align: center">Note</legend>
                                    <span style="font-weight: bold; color: Red;">Consolidated Marks Report </span>
                                    <br />
                                    Please Select ->Session -> College -> Degree -> Branch -> Semester
                                        <br />
                                    <span style="font-weight: bold; color: Red;">Consolidated Marks & Attendance Report</span>
                                    <br />
                                    Please Select ->Session -> College -> Degree -> Branch -> Regulation -> Semester
                                </fieldset>
                            </div>

                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnReport1" runat="server" Text="Consolidated Marks Report" TabIndex="8" ValidationGroup="teacherreport"
                                    CssClass="btn btn-outline-primary" OnClick="btnReport1_Click" />
                                <asp:Button ID="btnconsolexcel" runat="server" Text="Consolidated Marks Excel Report" TabIndex="8" ValidationGroup="teacherreport"
                                    CssClass="btn btn-outline-primary" OnClick="btnconsolexcel_Click" />
                                <asp:Button ID="btnconcalculate" runat="server" Text="Calculate Attendance and Marks" TabIndex="8" ValidationGroup="markreport"
                                    CssClass="btn btn-outline-primary" OnClick="btnconcalculate_Click" />
                                <asp:Button ID="btnconmksrpt" runat="server" Text="Consolidated Marks & Attendance Report" TabIndex="9" ValidationGroup="markreport"
                                    CssClass="btn btn-outline-info" OnClick="btnconmksrpt_Click" />
                                <asp:Button ID="btnconmksrptexcel" runat="server" Text="Consolidated Marks & Attendance Excel Report " Visible="false" TabIndex="10" ValidationGroup="markreport"
                                    CssClass="btn btn-outline-primary" OnClick="btnconmksrptexcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8"
                                    OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="markreport" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnconsolexcel" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
