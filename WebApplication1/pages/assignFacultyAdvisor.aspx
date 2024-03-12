<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="assignFacultyAdvisor.aspx.cs" Inherits="Academic_assignFacultyAdvisor"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="udpFacultyAd"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                  <%--  <h3 class="box-title">FACULTY ADVISOR ALLOTMENT</h3>--%>
                    <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:UpdatePanel ID="udpFacultyAd" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <label><asp:Label ID="lblDYClgReg" runat="server" Font-Bold="true"></asp:Label></label>
                                           <%-- <label>College & Curriculum</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select College & Curriculum" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <label><asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label></label>
                                           <%-- <label>Degree</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label></label>
                                           <%-- <label>Branch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="3"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlBranch" Display="None"
                                            ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <label><asp:Label ID="lblDYSemesterOrYear" runat="server" Font-Bold="true"></asp:Label></label>
                                          <%--  <label>Semester / Year</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester / Year" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                               <label><asp:Label ID="lblSection" runat="server" Font-Bold="true"></asp:Label></label>
                                      <%--      <label>Section</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <label><asp:Label ID="lblFaculty" runat="server" Font-Bold="true"></asp:Label></label>
                                         <%--   <label>Faculty</label>--%>
                                        </div>  
                                        <asp:DropDownList ID="ddlAdvisor" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdvisor_SelectedIndexChanged" AutoPostBack="true"
                                            CausesValidation="True" TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">                                   
                                       <asp:RadioButton ID="rblAllFaculty" runat="server"  Text="All Dept Faculty" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <label><asp:Label ID="lblSelectType" runat="server" Font-Bold="true"></asp:Label></label>
                                          <%--  <label>Select Type</label>--%>
                                        </div>
                                        <%-- <div class="radio">--%>
                                        <asp:RadioButtonList ID="rblStudent" runat="server" TabIndex="7" RepeatDirection="Horizontal">                                        
                                            <asp:ListItem Selected="True" Value="0" Text="All Student"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Remaining Student"></asp:ListItem>
                                        </asp:RadioButtonList><%--</div>--%>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="8"
                            Text="Show Student" ValidationGroup="Show" class="btn btn-outline-info"><i class=" fa fa-eye"></i> Show Student</asp:LinkButton>
                        <asp:LinkButton ID="btnAssignFA0" runat="server" OnClick="btnAssignFA0_Click" ValidationGroup="Show"
                            TabIndex="9" Text="Assign FA"   CssClass="btn btn-outline-info"><i class=" fa fa-save"></i> Assign FA</asp:LinkButton>
                        <asp:LinkButton ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                            TabIndex="10" Text="Report" ValidationGroup="Show" CssClass="btn btn-outline-primary" Visible="false"><i class="fa fa-file"></i> Report</asp:LinkButton>
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                OnClick="btnCancel_Click" TabIndex="11" Text="Cancel" CssClass="btn btn-outline-danger" />
                    </div>

                    <div class=" col-12">
                        <asp:UpdatePanel ID="updPnlFaculty" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lvFaculty" runat="server"
                                    OnItemDataBound="lvfaculty_ItemDataBound">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>LIST OF STUDENT</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)"  TabIndex="10"/>
                                                        </th>
                                                        <th>SR. NO.
                                                        </th>
                                                        <th>UNIV. REG. NO.
                                                        </th>
                                                        <th>TAN
                                                        </th>
                                                        <th>PAN
                                                        </th>
                                                            <th>ROLLNO
                                                        </th>
                                                        <th>NAME
                                                        </th>
                                                        <th>FACULTY ADVISOR
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
                                        <td>
                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' TabIndex="11" />
                                            <asp:Label runat="server" Text='<%# Eval("IDNO")%>' ID="lblIdNo" Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%# Eval("REGNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("TANNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("ROLLNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%> 
                                        </td>
                                        <td>
                                            <%# Eval("TEACHER_NAME")%>
                                        </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div style="text-align: center;">
                                            <asp:Label ID="lblError" runat="server" Text="No Record Found" SkinID="Errorlbl"></asp:Label>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <table cellpadding="0" cellspacing="0" width="100%" style="display: none">
        <tr>
            <td colspan="2" class="vista_page_title_bar" valign="top" style="height: 30px">FACULTY ADVISOR
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
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
    <div style="padding: 10px; width: 97%; display: none">
        <fieldset class="fieldset">
            <legend class="legend">Faculty Allotment</legend>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="updpnl_details" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="form_left_label" style="width: 20%">Degree :
                                        </td>
                                        <td class="form_left_text"></td>

                                    </tr>
                                    <tr>

                                        <td class="form_left_label">Branch : </td>
                                        <td class="form_left_text">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="form_left_label">Semester :
                                        </td>
                                        <td class="form_left_text"></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Section:</td>
                                        <td class="form_left_text"></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Faculty Advisor :
                                        </td>
                                        <td class="form_left_text"></td>
                                        <td class="form_left_text">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">&nbsp;</td>
                                        <td class="form_left_text"></td>
                                        <td class="form_left_text">&nbsp;</td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="form_left_label">Total Students Selected :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtTotChk" runat="server" Enabled="False"
                                                ValidationGroup="courseLink" Width="30px" />
                                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                        </td>
                                        <td class="form_left_text">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td class="form_left_text">&nbsp;
                                           
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_text"></td>
                                        <tr>
                                            <td colspan="2">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="false"
                                                    ValidationGroup="Show" />
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div style="padding: 10px; width: 90%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="form_button"></td>
            </tr>
        </table>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');

        if (chk.checked == true)
            txtTot.value = Number(txtTot.value) + 1;
        else
            txtTot.value = Number(txtTot.value) - 1;

    }

    function totAllSubjects(headchk) {
        var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');
        var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

        var frm = document.forms[0]
        for (i = 0; i < document.forms[0].elements.length; i++) {
            var e = frm.elements[i];
            if (e.type == 'checkbox') {
                if (headchk.checked == true)
                    e.checked = true;
                else
                    e.checked = false;
            }
        }

    }
    </script>

</asp:Content>
