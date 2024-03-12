<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DailyTimeTable.aspx.cs" Inherits="ACADEMIC_DailyTimeTable" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTimetable"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updTimetable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">TIME TABLE</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Degree</label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="course"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Regulation</label>
                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="course"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Semester</label>
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Section</label>
                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Time Table View</label>
                                <asp:DropDownList ID="ddlHorVerTimetable" runat="server" CssClass="form-control"
                                    AppendDataBoundItems="True" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlHorVerTimetable_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Vertical Time Table</asp:ListItem>
                                    <asp:ListItem Value="2">Horizontal Time Table</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4">
                                <label>Subject Type</label>
                                <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Course Name</label>
                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Theory/Practical/Tutorial</label>
                                <asp:DropDownList ID="ddltheoryPractical" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddltheoryPractical_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvthPr" runat="server" ControlToValidate="ddltheoryPractical"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Theory or Practical or Tutorial course" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Teacher Name</label>
                                <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Teacher" ValidationGroup="course">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4" style="margin-top: 25px">
                                <asp:CheckBoxList ID="chkTeacher" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="course"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnPrint" runat="server" Enabled="false" Text="Vertical Report"
                                    ValidationGroup="course" CausesValidation="False" OnClick="btnPrint_Click" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnExcel" runat="server" Enabled="False" OnClick="btnExcel_Click" Text="Vertical Excel Report" CssClass="btn btn-outline-primary" />
                                <br />
                                <br />
                                <asp:Button ID="btnHorPrint" runat="server" Enabled="false" Text="Horizontal Report" CssClass="btn btn-outline-primary"
                                    ValidationGroup="course" CausesValidation="False" OnClick="btnHorPrint_Click" />
                                <asp:Button ID="btnHorExcel" runat="server" Enabled="False" CssClass="btn btn-outline-primary"
                                    Text="Horizontal Excel Report" OnClick="btnHorExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="course"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                            <div class="col-md-12">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                            </div>
                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="pnlSlots" runat="server" ScrollBars="Auto" Height="250px">
                                    <asp:ListView ID="lvSlots" runat="server" OnItemDataBound="lvSlots_ItemDataBound">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Slots </h4>
                                                <table id="table1" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Monday
                                                            </th>
                                                            <th>Tuesday
                                                            </th>
                                                            <th>Wednesday
                                                            </th>
                                                            <th>Thursday
                                                            </th>
                                                            <th>Friday
                                                            </th>
                                                            <th>Saturday
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
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkMon" Text='<%# Eval("SLOTNAME")%>' ToolTip='<%# Eval("SLOTNAME") %>'
                                                        runat="server" onchange="CheckIsAlloted(this)" />
                                                    <asp:CheckBoxList ID="chkBatchMon" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnMon" runat="server" Value='<%# Bind("SLOTNO") %>' />


                                                    <asp:DropDownList ID="ddlroommon" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroommon" runat="server" />
                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkTues" Text='<%# Eval("SLOTNAME") %>' onchange="CheckIsAllotedTue(this)" runat="server" />
                                                    <asp:CheckBoxList ID="chkBatchTue" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnTue" runat="server" Value='<%# Bind("SLOTNO") %>' />

                                                    <asp:DropDownList ID="ddlroomtue" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroomtue" runat="server" />
                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkWed" Text='<%# Eval("SLOTNAME") %>' runat="server" onchange="CheckIsAllotedTue(this)" />
                                                    <asp:CheckBoxList ID="chkBatchWed" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnWed" runat="server" Value='<%# Bind("SLOTNO") %>' />

                                                    <asp:DropDownList ID="ddlroomwed" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroomwed" runat="server" />
                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkThurs" runat="server" Text='<%# Eval("SLOTNAME") %>' onchange="CheckIsAllotedTue(this)" />
                                                    <asp:CheckBoxList ID="chkBatchThurs" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnThurs" runat="server" Value='<%# Bind("SLOTNO") %>' />

                                                    <asp:DropDownList ID="ddlroomthur" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroomthur" runat="server" />
                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkFri" runat="server" Text='<%# Eval("SLOTNAME") %>' onchange="CheckIsAllotedTue(this)" />
                                                    <asp:CheckBoxList ID="chkBatchFri" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnFri" runat="server" Value='<%# Bind("SLOTNO") %>' />

                                                    <asp:DropDownList ID="ddlroomfri" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroomfri" runat="server" />
                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chkSat" runat="server" Text='<%# Eval("SLOTNAME") %>' onchange="CheckIsAllotedTue(this)" />
                                                    <asp:CheckBoxList ID="chkBatchSat" runat="server" RepeatColumns="4" />
                                                    <asp:HiddenField ID="hdnSat" runat="server" Value='<%# Bind("SLOTNO") %>' />

                                                    <asp:DropDownList ID="ddlroomsat" CssClass="form-control" Style="width: 90px" runat="server">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="hdnroomsat" runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlView" runat="server" ScrollBars="Auto" Height="250px">
                                    <div id="table2 table table-responsive">
                                        <div class="table table-responsive" id="trHor" runat="server">
                                            <asp:ListView ID="lvView" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <h4>Time Table </h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Time
                                                                    </th>
                                                                    <th>Monday
                                                                    </th>
                                                                    <th>Tuesday
                                                                    </th>
                                                                    <th>Wednesday
                                                                    </th>
                                                                    <th>Thursday
                                                                    </th>
                                                                    <th>Friday
                                                                    </th>
                                                                    <th>Saturday
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbltime" runat="server" Text='<%#Eval("TIME")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdntime" runat="server" Value='<%#Bind("SRNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMonday" runat="server" Text='<%#Eval("MONDAY") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnMonday" runat="server" Value='<%#Bind("MONDAY") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbltuesday" runat="server" Text='<%#Eval("TUESDAY")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdntuesday" runat="server" Value='<%#Bind("TUESDAY") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblwednesday" runat="server" Text='<%#Eval("WEDNESDAY")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnwednesday" runat="server" Value='<%#Bind("WEDNESDAY") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblthursday" runat="server" Text='<%#Eval("THURSDAY")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnthursday" runat="server" Value='<%#Bind("THURSDAY") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblfriday" runat="server" Text='<%#Eval("FRIDAY")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnfriday" runat="server" Value='<%#Bind("FRIDAY") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsaturday" runat="server" Text='<%#Eval("SATURDAY")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnsaturday" runat="server" Value='<%#Bind("SATURDAY") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>

                                        <div class="table table-responsive" id="trVer" runat="server">
                                            <asp:ListView ID="lvVertical" runat="server" OnDataBound="lvVertical_DataBound">
                                                <LayoutTemplate>
                                                    <div>
                                                        <h4>Time Table</h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Days
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl4" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl5" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl6" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl7" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl8" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl9" runat="server"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl10" runat="server"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </div>
                                                </LayoutTemplate>

                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDayName" runat="server" Text='<%#Eval("DAYNAME") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnMonday" runat="server" Value='<%#Bind("DAYNAME") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot1" runat="server" Text='<%#Eval ("SLOT1")  %>'></asp:Label>
                                                            <asp:HiddenField ID="hdntuesday" runat="server" Value='<%#Bind("SLOT1") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot2" runat="server" Text='<%#Eval("SLOT2")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnwednesday" runat="server" Value='<%#Bind("SLOT2") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot3" runat="server" Text='<%#Eval("SLOT3")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnthursday" runat="server" Value='<%#Bind("SLOT3") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot4" runat="server" Text='<%#Eval("SLOT4")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnfriday" runat="server" Value='<%#Bind("SLOT4") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot5" runat="server" Text='<%#Eval("SLOT5")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnsaturday" runat="server" Value='<%#Bind("SLOT5") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot6" runat="server" Text='<%#Eval("SLOT6")%>'></asp:Label>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Bind("SLOT6") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlot7" runat="server" Text='<%#Eval("SLOT7")%>'></asp:Label>
                                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Bind("SLOT7") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        function CheckIsAlloted(check) {

            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkMon');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lblMonday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }



            }
        }



        function CheckIsAllotedWed(check) {
            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkWed');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lblwednesday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }

            }
        }


        function CheckIsAllotedThurs(check) {
            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkThurs');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lblthursday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }

            }
        }

        function CheckIsAllotedFri(check) {
            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkFri');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lblfriday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }

            }
        }

        function CheckIsAllotedSat(check) {
            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkSat');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lblsaturday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }

            }
        }

        function CheckIsAllotedTue(check) {
            var table1 = document.getElementById("table1");
            var table2 = document.getElementById("table2");

            var cbID = check.id;

            for (i = 0; i < table1.getElementsByTagName("tr").length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSlots_ctrl' + i + '_chkTues');

                if (chk.id == check.id) {
                    if (chk.checked == true) {
                        var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvView_ctrl' + i + '_lbltuesday');
                        if (lbl.innerHTML != '' && lbl.innerHTML != '--') {
                            alert('Slot is already alloted');
                            chk.checked = false;
                        }
                    }
                }


            }

        }
    </script>

    <%--  Enable the button so it can be played again --%>
</asp:Content>
