<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BackBulkRegistration.aspx.cs" Inherits="ACADEMIC_BackBulkRegistration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; background-color: Aqua; padding-left: 5px">
                    <img src="../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar">BACK BULK COURSE REGISTRATION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                            <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                <Move Horizontal="150" Vertical="-50" />
                                <Resize Width="260" Height="280" />
                                <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                            </Parallel>
                            
                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <%--  Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <%--  Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%--  Enable the button so it can be played again --%>
                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                        </Sequence>
                    </OnClick>
                    <OnMouseOver>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                    </OnMouseOver>
                    <OnMouseOut>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                    </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
    </table>
    <br />
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Course Registration</legend>
                    <asp:UpdatePanel ID="updBulkReg" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 15%;">
                                        <span class="validstar">*</span>Admission Batch :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        <span class="validstar">*</span>Session :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        <span class="validstar">*</span>College Name :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 15%;">
                                        <span class="validstar">*</span>Department:
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            Width="400px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Degree :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlDegree" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Regulation Type :
                                    </td>
                                    <td style="width: 35%;" valign="top">
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Branch :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Regulation :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlScheme" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Regulation"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Semester :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 10%;">
                                        <span class="validstar">&nbsp</span>Student Status :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 10%;">
                                        <span class="validstar">&nbsp</span>Section :
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hftot" runat="server" />
                                    </td>
                                    <td class="form_left_label">Total Students Selected:
                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="unwatermarked"
                                            Style="text-align: center;" Width="10%" Height="20px" BackColor="#FFFFCC" Font-Bold="True"
                                            Font-Size="Small" ForeColor="#000066"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="font-weight: bold; font-style: italic; color: Red;">** Disabled Checkboxes indicated that Students are already Registered..!!!
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">&nbsp;
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" ValidationGroup="submit"
                                            OnClick="btnSubmit_Click" Font-Bold="True" OnClientClick="return validateAssign();"
                                            Enabled="False" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                            Width="80px" OnClick="btnCancel_Click" Font-Bold="True" />

                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ValidationGroup="submit" ShowSummary="false" />
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 50%; vertical-align: top; padding: 5px">
                                                    <asp:Panel ID="pnlStudents" runat="server" Visible="true" Width="100%">
                                                        <asp:ListView ID="lvStudent" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="titlebar">
                                                                        Student List
                                                                    </div>
                                                                    <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <thead>
                                                                            <tr class="header" id="trRow">
                                                                                <th id="thHead" style="width: 10%">
                                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />
                                                                                </th>
                                                                                <th style="text-align: center; width: 20%">Roll No.
                                                                                </th>
                                                                                <th style="text-align: left; width: 80%">Student Name
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="width: 10%">
                                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" />
                                                                    </td>
                                                                    <td style="text-align: center; width: 20%">
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                                    </td>
                                                                    <td style="text-align: left; width: 80%">
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td style="width: 10%">
                                                                        <asp:CheckBox ID="cbRow" runat="server" onClick="totStudents(this);" GroupName="BoxChk" />
                                                                    </td>
                                                                    <td style="text-align: center; width: 20%">
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                                    </td>

                                                                    <td style="text-align: left; width: 80%">
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 50%; vertical-align: top; padding: 5px">
                                                    <asp:Panel ID="pnlCourses" runat="server" Visible="true" Width="100%">
                                                        <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div id="divlvPaidReceipts" class="vista-grid">
                                                                    <div class="titlebar">
                                                                        Offered Courses
                                                                    </div>
                                                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                                                        <tr class="header">
                                                                            <th>Select
                                                                            </th>
                                                                            <th>Course Code - Course Name
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                                        &nbsp;
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 30%; vertical-align: top; padding: 5px">
                                                    <asp:Panel ID="pnlStudentsReamin" runat="server" Visible="true" Width="100%">
                                                        <asp:ListView ID="lvStudentsRemain" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="titlebar">
                                                                        Student List (Demand Not Found)
                                                                    </div>
                                                                    <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <thead>
                                                                            <tr class="header" id="trRow">
                                                                                <th style="text-align: left; width: 20%">HT No.
                                                                                </th>
                                                                                <th style="text-align: left; width: 80%">Student Name
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="text-align: left; width: 20%">
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                                    </td>
                                                                    <td style="text-align: left; width: 80%">
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td style="text-align: left; width: 20%">
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' />
                                                                    </td>
                                                                    <td style="text-align: left; width: 80%">
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 10px; text-align: center; vertical-align: top"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
	    var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }

                    }
                    else {
                        lst.checked = false;

                    }
                }

            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
	    }
	    else {
	        document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
	    }
    }
    function validateAssign() {
        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
        if (txtTot == 0) {
            alert('Please Check atleast one student ');
            return false;
        }
        else
            return true;
    }

    function totStudents(chk) {
        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

        if (chk.checked == true)
            txtTot.value = Number(txtTot.value) + 1;
        else
            txtTot.value = Number(txtTot.value) - 1;

    }
    </script>
</asp:Content>
