<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DefineTotalCredit.aspx.cs" Inherits="ACADEMIC_MASTERS_DefineTotalCredit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--<style>
        #lbcredit {
              display: block;
        }
    </style>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                   <%-- <h3 class="box-title">Credit Definition for Subject Registration <small class="margin-r-5">(Add/Edit Credits)</small></h3>--%>
                     <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"><small class="margin-r-5">(Add/Edit Credits)</small></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                  <%--  <label>College/School Name</label>--%>
                                      <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server"  CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                    ToolTip="Please Select College" TabIndex="1">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Session Name</label>--%>
                                       <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" onchange="clearSearchKey();"  CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                     <asp:ListItem>Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valCounterUser" runat="server" ControlToValidate="ddlSession"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Session."
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                   <%-- <label>Degree</label>--%>
                                     <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server"  CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4">
                                     <asp:ListItem>Please Select</asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Degree."
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                  <%--  <label>Admission Type</label>--%>
                                    <asp:Label ID="lblAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddladm" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Regular</asp:ListItem>
                                    <asp:ListItem Value="2">Direct Second Year</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddladm"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Admission type."
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                  <%--  <label>Admission Type</label>--%>
                                    <asp:Label ID="lblModuleRegistrationType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlAdmissionType" runat="server"  CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmissionType_SelectedIndexChanged" 
                                   AutoPostBack="true"  AppendDataBoundItems="True" TabIndex="6" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmissionType"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Module Registration type."
                                    InitialValue="0" />
                            </div>
                             
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                 <%--   <label>Student  Result</label>--%>
                                   <asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">All Pass</asp:ListItem>
                                    <asp:ListItem Value="2">Backlog</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvStudentType" runat="server" ControlToValidate="ddlStudentType"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Student Result."
                                    InitialValue="0" />
                            </div>                           
                        
                            <div class="form-group col-lg-3 col-md-6 col-12" id="lbcredit" runat="server" visible="false">
                                <div class="label-dynamic" id="credit">
                                    <sup>* </sup>
                                     <asp:Label ID="LabelCredit" runat="server" Font-Bold="true"></asp:Label>
                                    <%--<label>Min Regular Credit limit</label>--%>
                                     <%-- <asp:Label ID="lblDYMinRegularCreditlimit" runat="server" Font-Bold="true"></asp:Label>--%>
                                </div>
                                <asp:TextBox ID="txtMinRegCredit" runat="server" MaxLength="4" placeholder="Minimum Regular Credit limit" CssClass="form-control" TabIndex="7"/>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMinRegCredit" ValidChars="0123456789." FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMinRegCredit"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Minimum Regular Credit Limit." />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                <div class="label-dynamic">
                                    <%--<label>Semester </label>--%>
                                      <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div>
                                    <asp:CheckBoxList ID="chkListTerm" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" TabIndex="8">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 mt-3" runat="server" visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Number of Elective Subjects allow as per Group</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <%-- <label>E1</label>--%>
                                    <asp:Label ID="lblE1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect1" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E1" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect1" runat="server" TargetControlID="txtElect1" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                 <%--   <label>E2</label>--%>
                                     <asp:Label ID="lblE2" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect2" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E2" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect2" runat="server" TargetControlID="txtElect2" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <%-- <label>E3</label>--%>
                                     <asp:Label ID="lblE3" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect3" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect3" runat="server" TargetControlID="txtElect3" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                  <%--  <label>E4</label>--%>
                                    <asp:Label ID="lblE4" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect4" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E4" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect4" runat="server" TargetControlID="txtElect4" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblE5" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect5" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E5" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect5" runat="server" TargetControlID="txtElect5" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblE6" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect6" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E6" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect6" runat="server" TargetControlID="txtElect6" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblE7" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect7" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E7" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect7" runat="server" TargetControlID="txtElect7" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblE8" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect8" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E8" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect8" runat="server" TargetControlID="txtElect8" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblE9" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect9" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E9" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect9" runat="server" TargetControlID="txtElect9" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                     <asp:Label ID="lblE10" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect10" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E10" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtElect10" runat="server" TargetControlID="txtElect10" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                     <asp:Label ID="lblE11" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect11" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E11" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtElect11" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                     <asp:Label ID="lblE12" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect12" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E12" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtElect12" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                     <asp:Label ID="lblE13" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect13" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E13" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtElect13" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblE14" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect14" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E14" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtElect14" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-6">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblE15" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtElect15" runat="server" MaxLength="1" CssClass="form-control" onkeyup="validateNumeric(this);" placeholder="E15" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtElect15" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                        </div>
                    </div>

                    <div class="col-12 mt-3">
                        <div class="row">
                            <div id="Div8" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblDYMinimumSchemeLimit" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div>
                                    <asp:CheckBox ID="chkMinimumSchemeLimit" runat="server" AutoPostBack="true"  />
                                    Minimum Scheme Limit
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" Visible="false" >
                                <div class="label-dynamic">
                                   <asp:Label ID="lblDYMaximumSchemeLimit" runat="server" Font-Bold="true" ></asp:Label>
                                </div>
                                <div>
                                    <asp:CheckBox ID="chkMaximumSchemeLimit" runat="server" AutoPostBack="true" />
                                    Maximum Scheme Limit
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblDYCreditLimit" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkCreditStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkCreditStatus_CheckedChanged"  TabIndex="9"/>
                                        Check For Active
                                    </label>
                                </div>
                            </div>
                            <div id="Div6" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                <div class="label-dynamic">
                                 <asp:Label ID="lblDYMinCreditLimit" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtFromCredit" runat="server" MaxLength="2" Enabled="false" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Minimum Credits" TabIndex="10" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtFromCredit" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblDYMaxCreditLimit" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtToCredits" runat="server" MaxLength="2" Enabled="false" ondrop="return false;" onpaste="return false;" AutoComplete="OFF" CssClass="form-control" placeholder="Maximum Credits" TabIndex="11" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtToCredits" ValidChars="0123456789" FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                              <div class="form-group col-lg-3 col-md-6 col-12" id="Div7" runat="server">
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server"  visible="false">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblDYCGPA" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkCGPAStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkCGPAStatus_CheckedChanged" />
                                        Check For Active
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server"  visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYFromCGPA" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtFromRange" runat="server" MaxLength="4" Enabled="false" CssClass="form-control" placeholder=" Range CGPA" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="fteFromrange" runat="server" TargetControlID="txtFromRange" ValidChars="0123456789." FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvFromrange" runat="server" ControlToValidate="txtFromRange"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter From CGPA." />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server"  visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                  <asp:Label ID="lblDYToCGPA" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtToRange" runat="server" MaxLength="4" Enabled="false" placeholder="Range CGPA" CssClass="form-control" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtToRange" ValidChars="0123456789." FilterMode="ValidChars">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToRange"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter To CGPA." />
                                <%-- <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFromRange" ControlToValidate="txtToRange" runat="server"  Operator="GreaterThan" Type="Double" Display="None"  ValidationGroup="Submit" ErrorMessage="To  CGPA  should be greater than From  CGPA"></asp:CompareValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" visible="false">
                                <div class="label-dynamic">
                                   <asp:Label ID="Label8" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="checkbox">
                                     <asp:Label ID="lblAdditionalSubjectsLearning" runat="server" Font-Bold="true"></asp:Label>
                                    <label>
                                        <asp:CheckBox ID="chkAddionalCourse" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddionalCourse_CheckedChanged" />
                                        Additional Subjects Learning
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="trAdditionalCourses">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                     <asp:Label ID="lblAdditionalCourseDegree" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlAdditionalCourseDegree" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">UG</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdditionalCourseDegree"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please select Additional Subjects Degree Type."
                                    InitialValue="0" />
                            </div>
                           
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                <div class="label-dynamic">
                                   <asp:Label ID="lblDYMovableSubject" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtMovableSubject" runat="server" MaxLength="4" CssClass="form-control" placeholder=""/>
                            </div>

                         <%--   <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                            </div>
                             <div class="form-group col-lg-3 col-md-6 col-12" id="Div9" runat="server">
                            </div>--%>


                            <div class="form-group col-lg-4 col-md-6 col-12" style="display:none
    ">
                                    <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Total Credit Definition for Subject Registration</span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                        
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit" CssClass="btn btn-outline-info" />
                        <asp:Button ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click" CssClass="btn btn-outline-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>

                    <div class="col-12 mt-3">
                        <div class="sub-heading">
                            <h5>Credit Definition</h5>
                        </div>
                        <asp:ListView ID="lvCredit" runat="server">
                            <LayoutTemplate>

                                <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit</th>
                                            <th>Session Name</th>
                                            <th>Degree</th>
                                            <th>Admission Type</th>
                                       <%-- <th>Student Type</th>--%>
                                            <th>Module Registration Type</th>
                                           <%-- <th>From CGPA</th>
                                            <th>To CGPA</th>--%>
                                         <%--   <th>Additional Subject</th>
                                            <th>Degree Type</th>--%>
                                            <th>Min.Cr. Limit</th>
                                            <th>Max. Cr. Limit</th>
                                      <%--      <th>Min. Scheme Limit </th>
                                            <th>Max. Scheme Limit </th>--%>
                                            <th>Semester </th>
                                            <th>Lock </th>
                                            <th>Sub./Cre./Mod. Lim. </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                            CommandArgument='<%# Eval("IDNO") %>' AlternateText="Edit Record"
                                            OnClick="btnEdit_Click" TabIndex="10" />
                                    </td>

                                    <td><%# ((Eval("SESSIONNAME").ToString() != string.Empty) ? Eval("SESSIONNAME") : "--") %></td>
                                    <td>
                                        <%# ((Eval("DEGREENAME").ToString() != string.Empty) ? Eval("DEGREENAME") : "--") %>
                                    </td>
                                    <%--<td>
                                        <%# ((Eval("STUDENT_TYPE_NAME").ToString() != string.Empty) ? Eval("STUDENT_TYPE_NAME") : "--") %>
                                    </td>--%>
                                    <td>
                                        <%# ((Eval("ADM_TYPE").ToString() != string.Empty) ? Eval("ADM_TYPE") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("MODULE_REGISTRATION_TYPE").ToString() != string.Empty) ? Eval("MODULE_REGISTRATION_TYPE") : "--") %>
                                    </td>
                                <%--   <td>
                                        <%# ((Eval("FROM_CGPA").ToString() != string.Empty) ? Eval("FROM_CGPA") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("TO_CGPA").ToString() != string.Empty) ? Eval("TO_CGPA") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("ADDITIONAL_COURSE_NAME").ToString() != string.Empty) ? Eval("ADDITIONAL_COURSE_NAME") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("DEGREE_TYPE").ToString() != string.Empty) ? Eval("DEGREE_TYPE") : "--") %>
                                    </td>--%>
                                    <td>
                                        <%# ((Eval("FROM_CREDIT").ToString() != string.Empty) ? Eval("FROM_CREDIT") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("TO_CREDIT").ToString() != string.Empty) ? Eval("TO_CREDIT") : "--") %>
                                    </td>
                                  <%--  <td>
                                        <%# ((Eval("MIN_SCHEME_LIMIT").ToString() != string.Empty) ? Eval("MIN_SCHEME_LIMIT") : "--") %>
                                    </td>

                                    <td>
                                        <%# ((Eval("MAX_SCHEME_LIMIT").ToString() != string.Empty) ? Eval("MAX_SCHEME_LIMIT") : "--") %>
                                    </td>--%>
                                    <td>
                                        <%# ((Eval("SEMESTER_TEXT").ToString() != string.Empty) ? Eval("SEMESTER_TEXT") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("LOCK").ToString() != string.Empty) ? Eval("LOCK") : "--") %>
                                    </td>
                                    <td>
                                        <%# ((Eval("MIN_REG_CREDIT_LIMIT").ToString() != string.Empty) ? Eval("MIN_REG_CREDIT_LIMIT") : "--") %>
                                    </td>
                                </tr>

                                <%--  </tbody>--%>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function Compare() {
            if (document.getElementById('<%=txtFromCredit.ClientID%>').value < document.getElementById('<%=txtToCredits.ClientID%>').value) {
                alert('From credit can not be greater than to credit');
            }

        }

        function clearSearchKey() {
            $('#<%=txtToCredits.ClientID%>').val('');
        }

    </script>

</asp:Content>

