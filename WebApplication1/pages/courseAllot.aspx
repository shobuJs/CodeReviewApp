<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="courseAllot.aspx.cs" Inherits="Academic_courseAllot" Title="" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">COURSE TEACHER ALLOTMENT</h3>
                        </div>

                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Degree</label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="2" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch." ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Regulation</label>
                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Regulation." ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Semester</label>
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="5">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Section</label>
                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="6">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                    Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Subject Type</label>
                                <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" TabIndex="7">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                    Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Theory/Practical/Tutorial</label>
                                <asp:DropDownList ID="ddltheorypractical" runat="server" TabIndex="8" OnSelectedIndexChanged="ddltheorypractical_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLTP" runat="server" ControlToValidate="ddltheorypractical"
                                    Display="None" ErrorMessage="Please Select Theory or Practical or Tutorial Type course" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Course Name</label>
                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="9">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course." ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Teacher from Department </label>
                                <asp:DropDownList ID="ddlDeptName" runat="server" TabIndex="10" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Teacher Name</label>
                                <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" TabIndex="11" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                    Display="None" InitialValue="0" ValidationGroup="course" ErrorMessage="Please Select Teacher."></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3" style="display: none">
                                <label>Total Student(s)</label>
                                <asp:TextBox ID="txtTot" runat="server" CssClass="watermarked" Enabled="False" Width="30px"></asp:TextBox>
                            </div>
                            <div class="col-md-12" id="dvAdt" runat="server" visible="false">
                                <label>Additional Teachers</label>
                                <p class="text-center">
                                    <asp:Panel ID="pnlList" runat="server" Height="200px" Width="600px" ScrollBars="Vertical">
                                        <asp:ListView ID="lvAdTeacher" runat="server">
                                            <LayoutTemplate>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 30px">
                                                        <asp:CheckBox ID="chkIDNo" runat="server" ToolTip='<%# Eval("UA_NO") %>' Text='<%# Eval("UA_FULLNAME") %>' /><br />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </p>
                            </div>
                            <br />
                            <div class="form-group col-md-3">
                                <br />
                                <br />
                                <label>Report in</label>
                                <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Vertical" TabIndex="12">
                                    <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                    <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                    <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div id="dvCount" runat="server" class="form-group col-md-12" visible="false">
                                <p class="text-center">
                                    <asp:Label ID="lbltotcount" runat="server" Font-Bold="true"></asp:Label>
                                </p>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnAd" runat="server" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="course"
                                    OnClick="btnAd_Click" TabIndex="13" />
                                <asp:Button ID="btnPrint" runat="server" Text="Report" ValidationGroup="course"
                                    CausesValidation="False" OnClick="btnPrint_Click" CssClass="btn btn-outline-primary" TabIndex="14" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click"
                                    CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="15" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="course"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                            <div class="col-md-12">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                            </div>
                            <div class="col-md-12" id="dvCourse" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Course Allotment</h4>
                                                <table class="table table-hover table-bordered">
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Subject Type
                                                        </th>
                                                        <th>Theory/Practical/Tutorial
                                                        </th>
                                                        <th>Sec
                                                        </th>
                                                        <th>Teacher Name
                                                        </th>
                                                        <th>Additional Teacher
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COURSENO") %>'
                                                        AlternateText='<%# Eval("UA_NO") %>' ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete();"
                                                        CausesValidation="False" />
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBNAME")%>
                                                    <asp:HiddenField ID="hdfthpr" runat="server" Value='<%# Eval("TH_PR")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("THE_PRE")%>
                                                    <asp:HiddenField ID="hdfsubid" runat="server" Value='<%# Eval("subid")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("SECTIONNAME") %>
                                                    <asp:HiddenField ID="hdfSecNo" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                                <td>
                                                    <%# GetAdTeachers(Eval("ADTEACHER"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" AlternateText="Warning" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>

                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        function ConfirmDelete() {
            var ret = confirm('Do You Want To Delete Selected Subject Allotment ???');
            if (ret == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <div runat="server" id="divMsg">
    </div>
</asp:Content>
