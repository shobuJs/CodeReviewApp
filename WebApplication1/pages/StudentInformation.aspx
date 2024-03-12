<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentInformation.aspx.cs" Inherits="Academic_StudentInformation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px;">
                STUDENT INFORMATION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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
        <tr>
            <td style="padding: 5px">
                <ajaxToolKit:TabContainer ID="TabContainer1" runat="server" CssClass="linkedin-tab" ActiveTabIndex="0"
                    Width="100%">
                    <ajaxToolKit:TabPanel ID="tabPerInfo" runat="server" HeaderText="Personal Information">
                        <ContentTemplate>
                            <div style="padding-left: 10px; padding-top: 10px; padding-right: 10px">
                                <fieldset class="fieldset">
                                    <legend class="legend">Personal Details</legend>
                                    <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td width="25%">
                                                Enrollment No. :
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox Width="90%" ID="txtRegNo" runat="server" Enabled="False" />
                                            </td>
                                            <td width="20%">
                                                &nbsp;
                                            </td>
                                            <td width="20%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Student Name :
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox Width="90%" ID="txtStudentName" runat="server" Enabled="False" />
                                            </td>
                                            <td width="20%">
                                                Blood Group :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlBloodGroupNo" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Father&#39;s Name :
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox Width="90%" ID="txtFatherName" runat="server" Enabled="False" />
                                            </td>
                                            <td width="20%">
                                                Nationality :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlNationality" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Mother&#39;s Name :
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox Width="90%" ID="txtMotherName" runat="server" Enabled="False" />
                                            </td>
                                            <td width="20%">
                                                Caste :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlCaste" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Date of Birth :
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox Width="90%" ID="txtDateOfBirth" runat="server" Enabled="False" />
                                            </td>
                                            <td width="20%">
                                                Category :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlCasteCategory" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                &nbsp;Gender :
                                            </td>
                                            <td width="20%">
                                                <asp:RadioButton ID="rdoMale" runat="server" Checked="True" GroupName="Sex" Text="Male" />
                                                <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Sex" Text="Female" />
                                            </td>
                                            <td width="20%">
                                                Religion :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlReligion" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                &nbsp;Maritial Status :
                                            </td>
                                            <td width="30%">
                                                <asp:RadioButton ID="rdoSingle" runat="server" GroupName="MartialStatus" Text="Single" />
                                                <asp:RadioButton ID="rdoMarried" runat="server" GroupName="MartialStatus" Text="Married" />
                                            </td>
                                            <td width="25%" rowspan="5" valign="top">
                                                Photo :
                                            </td>
                                            <td width="25%" rowspan="5">
                                                <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Email ID :
                                            </td>
                                            <td width="30%">
                                                <asp:TextBox Width="90%" ID="txtStudentEmail" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                &nbsp;
                                            </td>
                                            <td width="30%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                &nbsp;
                                            </td>
                                            <td width="30%">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                &nbsp;
                                            </td>
                                            <td width="30%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; padding-right: 10px">
                                <fieldset class="fieldset">
                                    <legend class="legend">Address Details</legend>
                                    <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td width="15%" valign="top">
                                                Local Address :
                                            </td>
                                            <td colspan="4" style="width: 55%">
                                                <asp:TextBox Width="90%" ID="txtLocalAddress" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                City :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlLocalCity" runat="server" Enabled="False" />
                                            </td>
                                            <td width="15%">
                                                PIN :
                                            </td>
                                            <td width="20%" colspan="2">
                                                <asp:TextBox Width="90%" ID="txtLocalPIN" runat="server" Enabled="False" />
                                            </td>
                                            <td width="30%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                District :
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox Width="90%" ID="txtLdistrict" runat="server" Enabled="False" />
                                            </td>
                                            <td width="15%">
                                                State :
                                            </td>
                                            <td width="20%">
                                                <asp:DropDownList Width="90%" ID="ddlLocalState" runat="server" Enabled="False" />
                                            </td>
                                            <td width="30%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                Landline Number :
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox Width="90%" ID="txtLocalLandlineNo" runat="server" Enabled="False" />
                                            </td>
                                            <td width="15%">
                                                Mobile Number :
                                            </td>
                                            <td width="20%" colspan="2">
                                                <asp:TextBox Width="90%" ID="txtLocalMobileNo" runat="server" Enabled="False" />
                                            </td>
                                            <td width="30%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                E-Mail Address :
                                            </td>
                                            <td width="20%" colspan="3">
                                                <asp:TextBox Width="90%" ID="txtLocalEmail" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; padding-right: 10px; padding-bottom: 10px">
                                <fieldset class="fieldset">
                                    <legend class="legend">Admission Details</legend>
                                    <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td width="16%">
                                                Date of Admission :
                                            </td>
                                            <td width="16%">
                                                <asp:TextBox Width="90%" ID="txtDateOfAdmission" runat="server" Enabled="False" />
                                            </td>
                                            <td width="16%">
                                                Degree :
                                            </td>
                                            <td width="16%" colspan="3">
                                                <asp:DropDownList Width="90%" ID="ddlDegree" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="16%">
                                                Payment Type :
                                            </td>
                                            <td width="16%">
                                                <asp:DropDownList Width="90%" ID="ddlPaymentType" runat="server" Enabled="False" />
                                            </td>
                                            <td width="16">
                                                Branch :
                                            </td>
                                            <td width="16%" colspan="3">
                                                <asp:DropDownList Width="90%" ID="ddlBranch" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="16%">
                                                State of Eligibility :
                                            </td>
                                            <td width="16%">
                                                <asp:DropDownList Width="90%" ID="ddlStateOfEligibility" runat="server" Enabled="False" />
                                            </td>
                                            <td width="16%">
                                                Year :
                                            </td>
                                            <td width="16%" colspan="3">
                                                <asp:DropDownList Width="90%" ID="ddlYear" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="16%">
                                                Admission Batch :
                                            </td>
                                            <td width="16%">
                                                <asp:DropDownList Width="90%" ID="ddlBatch" runat="server" Enabled="False" />
                                            </td>
                                            <td width="16%">
                                                Semester :
                                            </td>
                                            <td width="16%" colspan="3">
                                                <asp:DropDownList Width="90%" ID="ddlSemester" runat="server" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="16%">
                                                &nbsp;Hosteler :
                                            </td>
                                            <td width="16%">
                                                <asp:RadioButton ID="rdoHostelerYes" runat="server" Text="Yes" GroupName="Hosteler" />
                                                <asp:RadioButton ID="rdoHostelerNo" runat="server" Text="No" GroupName="Hosteler" />
                                            </td>
                                            <td width="16%">
                                            </td>
                                            <td width="16%">
                                            </td>
                                            <td width="16%">
                                            </td>
                                            <td width="16%">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </ajaxToolKit:TabPanel>
                    <ajaxToolKit:TabPanel ID="tabRegCourses" runat="server" HeaderText="Registered Courses">
                        <ContentTemplate>
                            <div style="padding: 10px">
                                <asp:ListView ID="lvCurrentSem" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">
                                                Regular Courses</div>
                                            <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                <thead>
                                                    <tr class="header">
                                                        <th>
                                                            Code
                                                        </th>
                                                        <th>
                                                            Course Name
                                                        </th>
                                                        <th>
                                                            Credits
                                                        </th>
                                                        <th>
                                                            Subject Type
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <%# Eval("CCODE") %>
                                            </td>
                                            <td>
                                                <%# Eval("COURSENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CREDITS")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBTYPE")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem">
                                            <td>
                                                <%# Eval("CCODE") %>
                                            </td>
                                            <td>
                                                <%# Eval("COURSENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CREDITS")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBTYPE")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                                <br />
                                <br />
                                <asp:ListView ID="lvFailCourses" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">
                                                Fail Courses List</div>
                                            <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                <thead>
                                                    <tr class="header">
                                                        <th>
                                                            Code
                                                        </th>
                                                        <th>
                                                            Course Name
                                                        </th>
                                                        <th>
                                                            Credits
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" >
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SRNO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("SUBID") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolKit:TabPanel>
                    <ajaxToolKit:TabPanel ID="tabFees" runat="server" HeaderText="Fees">
                        <ContentTemplate>
                            <div style="padding: 10px">
                                <asp:ListView ID="lvPaidReceipts" runat="server">
                                    <LayoutTemplate>
                                        <div id="divlvPaidReceipts" class="vista-grid">
                                            <div class="titlebar">
                                                Previous Receipt Information</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <tr class="header">
                                                    <th style="width:10%">
                                                        Receipt No
                                                    </th>
                                                    <th style="width:10%">
                                                        Date
                                                    </th>
                                                    <th style="width:10%">
                                                        Semester
                                                    </th>
                                                    <th style="width:10%">
                                                        Amount
                                                    </th>
                                                    <th style="width:10%">
                                                        Receipt Type
                                                    </th>
                                                    <th style="width:50%">
                                                        Remark
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td style="width:10%">
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td style="width:10%">
                                                <%# ((DateTime)Eval("REC_DT")).ToString("dd-MMM-yyyy") %>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("FEE_LONGNAME")%>
                                            </td>
                                            <td style="width:50%">
                                                <%# Eval("REMARK")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem">
                                            <td style="width:10%">
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td style="width:10%">
                                                <%# ((DateTime)Eval("REC_DT")).ToString("dd-MMM-yyyy") %>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                            <td style="width:10%">
                                                <%# Eval("FEE_LONGNAME")%>
                                            </td>
                                            <td style="width:50%">
                                                <%# Eval("REMARK")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolKit:TabPanel>
                </ajaxToolKit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
