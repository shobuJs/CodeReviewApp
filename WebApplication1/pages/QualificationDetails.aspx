<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualificationDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_QualificationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box box-info">
        <div class="box-header with-border">
            <span class="glyphicon glyphicon-user text-blue"></span>
            <h3 class="box-title">STUDENT INFORMATION</h3>
            <div class="box-tools pull-right">
            </div>
        </div>
        <div class="box-body">

            <div class="row">
                <div class="col-md-3" id="divtabs" runat="server">
                    <div class="col-md-12">
                        <div class="panel panel-info" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                            <div class="panel panel-heading"><b>Click To Open Respective Information</b></div>
                            <div class="panel-body">
                                <aside class="sidebar">

                                    <!-- sidebar: style can be found in sidebar.less -->
                                    <section class="sidebar" style="background-color: #12aae2">
                                        <ul class="sidebar-menu">
                                            <!-- Optionally, you can add icons to the links -->
                                            <br />
                                            <div id="divhome" runat="server">

                                                <li class="treeview">&nbsp; <i class="fa fa-search"><span>
                                                    <asp:LinkButton runat="server" ID="lnkGoHome"
                                                        ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Search New Student"> 

                                                    </asp:LinkButton>
                                                </span>
                                                </i>
                                                    <hr />
                                                </li>
                                            </div>
                                            <li class="treeview">&nbsp <i class="fa fa-user"><span>
                                                <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                    ToolTip="Please select Personal Details." OnClick="lnkPersonalDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Personal Details"> 

                                                </asp:LinkButton>
                                            </span>
                                            </i>
                                                <hr />
                                            </li>

                                            <li class="treeview">&nbsp <i class="fa fa-map-marker"><span>
                                                <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                    ToolTip="Please select Address Details." OnClick="lnkAddressDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
                                                </asp:LinkButton>
                                            </span>
                                            </i>

                                                <hr />
                                            </li>

                                            <div id="divadmissiondetailstreeview" runat="server">
                                                <li class="treeview">&nbsp<i class="fa fa-university"><span>
                                                    <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                        ToolTip="Please Click Here For Personal Details." OnClick="lnkAdmissionDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Admission Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                </i>
                                                    <hr />
                                                </li>
                                            </div>
                                            <li class="treeview">
                                                <i class="fa fa-info-circle"><span>
                                                    <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                        ToolTip="Please Click Here For DASA Student Information." OnClick="lnkDasaStudentInfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="DASA Student Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                </i>
                                                <hr />
                                            </li>
                                            <li class="treeview">&nbsp<i class="fa fa-graduation-cap"><span>
                                                <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                    ToolTip="Please Click Here For Qualification Details." OnClick="lnkQualificationDetail_Click" Style="color: yellow; font-size: 16px;" Text="Qualification Details"> 
                                                </asp:LinkButton>
                                            </span>
                                            </i>
                                                <hr />
                                            </li>
                                            <li class="treeview">&nbsp<i class="fa fa-link"><span>
                                                <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                    ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                                                </asp:LinkButton>
                                            </span>
                                            </i>
                                                <p></p>
                                                <hr />
                                            </li>
                                            <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
                                            </span>

                                            </i>
                                                <p></p>
                                            </li>
                                        </ul>
                                    </section>
                                </aside>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="box box-info">
                        <div class="box-header with-border" id="trLastQual1" runat="server">
                            <h2 class="box-title"><b>Student Last Qualification</b></h2>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <br />
                        <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <div class="box-body">
                            <div id="divStudentLastQualification" runat="server">
                                <asp:Panel ID="pnlHssc" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>SSLC Marks</b></h4>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>School/College Name</label>

                                            <asp:TextBox ID="txtSchoolCollegeNameSsc" runat="server" CssClass="form-control" TabIndex="136" onkeypress="return alphaOnly(event);" ToolTip="Please Enter School/College Name"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteSchoolCollegeNameSsc" runat="server"
                                                TargetControlID="txtSchoolCollegeNameSsc" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Board</label>

                                            <asp:TextBox ID="txtBoardSsc" runat="server" TabIndex="137" ToolTip="Please Enter SSC Board" onkeypress="return alphaOnly(event);"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteBoardSsc" runat="server" TargetControlID="txtBoardSsc"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Year Of Exam</label>
                                            <asp:TextBox ID="txtYearOfExamSsc" runat="server" onkeyup="validateNumeric(this);"
                                                TabIndex="138" ToolTip="Please Enter SSC Year Of Exam" CssClass="form-control" MaxLength="4"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Medium</label>
                                            <asp:TextBox ID="txtSSCMedium" runat="server" CssClass="form-control" ToolTip="Please Enter Medium in SSC Exam" TabIndex="139"
                                                MaxLength="50"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server"
                                                TargetControlID="txtSSCMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />

                                        </div>
                                        <div class="form-group col-md-2">
                                            <div class="row">
                                                <label>&nbsp;&nbsp;&nbsp;Marks Obtained</label>
                                            </div>
                                            <asp:TextBox ID="txtMarksObtainedSsc" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter Marks Obtained"
                                                MaxLength="4" TabIndex="140" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMarksObtainedSsc">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Out Of Marks</label>
                                            <asp:TextBox ID="txtOutOfMarksSsc" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'ssc'),validateSscMarksCam(this)" ToolTip="Please Enter Out Of Marks"
                                                runat="server" MaxLength="4" TabIndex="141" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Percentage</label>
                                            <asp:TextBox ID="txtPercentageSsc" CssClass="form-control" runat="server" onkeyup="validateNumeric(this);"
                                                ToolTip="Please Enter Percentage" TabIndex="142"></asp:TextBox>
                                            <asp:CompareValidator ID="rfvPercentageSSC" runat="server" ControlToValidate="txtPercentageSsc"
                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Exam Roll No.</label>
                                            <asp:TextBox ID="txtExamRollNoSsc" runat="server" CssClass="form-control" TabIndex="143" ToolTip="Please Enter Exam Roll No."></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteExamRollNoSsc" runat="server" FilterType="Custom"
                                                TargetControlID="txtExamRollNoSsc" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>CGPA</label>
                                            <asp:TextBox ID="txtPercentileSsc" onkeyup="validateNumeric(this);" runat="server"
                                                ToolTip="Please Enter CGPA" CssClass="form-control" TabIndex="144"></asp:TextBox>
                                            <asp:CompareValidator ID="rfvPercentileSSC" runat="server" ControlToValidate="txtPercentileSsc"
                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Grade</label>
                                            <asp:TextBox ID="txtGradeSsc" runat="server" ToolTip="Please Enter Grade" TabIndex="145"
                                                CssClass="form-control" MaxLength="5"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server"
                                                TargetControlID="txtGradeSsc" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'1234567890" />

                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Attempts</label>
                                            <asp:TextBox ID="txtAttemptSsc" runat="server" ToolTip="Please Enter Attempts" CssClass="form-control" TabIndex="146" MaxLength="3"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPercentileSsc"
                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server"
                                                TargetControlID="txtAttemptSsc" FilterType="Numbers" />
                                        </div>
                                        <div class="form-group col-md-8">
                                            <div class="row">
                                                <label>&nbsp;&nbsp;&nbsp;School/College Address</label>
                                            </div>
                                            <asp:TextBox ID="txtSSCSchoolColgAdd" runat="server" TextMode="MultiLine" Rows="2"
                                                ToolTip="School/College Address" MaxLength="100" TabIndex="147"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>PUC/Diploma Marks</b></h4>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label>School/College Name</label>
                                            <asp:TextBox ID="txtSchoolCollegeNameHssc" runat="server" CssClass="form-control" TabIndex="148" ToolTip="Please Enter HSSC School/College Name" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteSchoolCollegeHssc" runat="server" TargetControlID="txtSchoolCollegeNameHssc"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />


                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Board</label>

                                            <asp:TextBox ID="txtBoardHssc" runat="server" ToolTip="Please Enter Board" TabIndex="149"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteBoardHssc" runat="server" TargetControlID="txtBoardHssc"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />


                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Year Of Exam</label>

                                            <asp:TextBox ID="txtYearOfExamHssc" onkeyup="validateNumeric(this);" runat="server" TabIndex="150"
                                                ToolTip="Please Enter HSSC Year Of Exam" CssClass="form-control" MaxLength="4"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Medium</label>

                                            <asp:TextBox ID="txtHSSCMedium" runat="server" CssClass="form-control" ToolTip="Please Enter Medium in HSSC Exam" TabIndex="151" onkeypress="return alphaOnly(event);"
                                                MaxLength="50"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                                                TargetControlID="txtHSSCMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                        </div>
                                        <div class="form-group col-md-2">
                                            <div class="row">
                                                <label>&nbsp;&nbsp;&nbsp;Marks Obtained</label>
                                            </div>
                                            <asp:TextBox ID="txtMarksObtainedHssc" runat="server" CssClass="form-control" MaxLength="4" TabIndex="152" ToolTip="Please Enter Marks Obtained"
                                                onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtmnarksobtain" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMarksObtainedHssc">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Out Of Marks</label>
                                            <asp:TextBox ID="txtOutOfMarksHssc" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'hsc'),validateHscMarksCam(this)" ToolTip="Please Enter Out Of Marks"
                                                runat="server" MaxLength="4" CssClass="form-control" TabIndex="153"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Percentage</label>
                                            <asp:TextBox ID="txtPercentageHssc" onkeyup="validateNumeric(this);" runat="server"
                                                ToolTip="Please Enter Percentage" CssClass="form-control" ValidationGroup="Academic" TabIndex="154"></asp:TextBox>
                                            <asp:CompareValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentageHssc"
                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Exam Roll No.</label>
                                            <asp:TextBox ID="txtExamRollNoHssc" CssClass="form-control" runat="server" TabIndex="155" ToolTip="Please Enter Exam Roll No."></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server"
                                                TargetControlID="txtExamRollNoHssc" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>CGPA</label>
                                            <asp:TextBox ID="txtPercentileHssc" runat="server" ToolTip="Please Enter HSSC CGPA"
                                                CssClass="form-control" onkeyup="validateNumeric(this);" ValidationGroup="Academic" TabIndex="156"></asp:TextBox>
                                            <asp:CompareValidator ID="rfvPercentile" runat="server" ControlToValidate="txtPercentileHssc"
                                                Display="None" ErrorMessage="Please Enter HSSC CGPA" Operator="DataTypeCheck"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="Academic"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Grade</label>
                                            <asp:TextBox ID="txtGradeHssc" runat="server" ToolTip="Please Enter HSSC Grade" onkeypress="return alphaOnly(event);"
                                                CssClass="form-control" TabIndex="157"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server"
                                                TargetControlID="txtGradeHssc" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Attempts</label>
                                            <asp:TextBox ID="txtAttemptHssc" runat="server" ToolTip="Please Enter Attempt"
                                                CssClass="form-control" TabIndex="158" MaxLength="3"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server"
                                                TargetControlID="txtAttemptHssc" FilterType="Numbers" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <div class="row">
                                                <label>&nbsp;&nbsp;&nbsp;School/College Address</label>
                                            </div>
                                            <asp:TextBox ID="txtHSCColgAddress" runat="server" CssClass="form-control" Rows="2" TextMode="MultiLine" TabIndex="159"
                                                ToolTip="School/College Address" MaxLength="100" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server"
                                                TargetControlID="txtHSCColgAddress" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>Enter PM and CHE/B/COMP/E/DIPLOMA FINAL YEAR MARK</b></h4>
                                            </div>
                                            <div class="col-md-12 form-group">
                                                <div class="form-group col-md-3">
                                                    <label>Marks Obtained</label>
                                                    <asp:TextBox ID="txtHscChe" onkeyup="validateNumeric(this);" runat="server"
                                                        ToolTip="Please Enter HSC CHE" MaxLength="4" CssClass="form-control" TabIndex="160"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="row">
                                                        <label>&nbsp;&nbsp;&nbsp;Out Of Marks</label>
                                                    </div>
                                                    <asp:TextBox ID="txtHscCheMax" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'pcm'),validateHscMarksCam(this)" runat="server" MaxLength="4"
                                                        ToolTip="Please Enter HSC CHE MAX" CssClass="form-control" TabIndex="161"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label>Percentage</label>
                                                    <asp:TextBox ID="txtHscPhy" onkeyup="validateNumeric(this);" runat="server"
                                                        MaxLength="3" CssClass="form-control" TabIndex="162"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none">
                                                    <div class="row">
                                                        <label>&nbsp;&nbsp;&nbsp;PCM-PHY-MAX</label>
                                                    </div>
                                                    <asp:TextBox ID="txtHscPhyMax" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter HSC PHY MAX"
                                                        MaxLength="3" CssClass="form-control" TabIndex="131"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none">
                                                    <label>HSSC-ENG</label>
                                                    <asp:TextBox ID="txtHscEngHssc" onkeyup="validateNumeric(this);" runat="server"
                                                        ToolTip="Please Enter HSC ENG" MaxLength="3" CssClass="form-control" TabIndex="132"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none">
                                                    <div class="row">
                                                        <label>&nbsp;&nbsp;&nbsp;HSSC-ENG-MAX</label>
                                                    </div>
                                                    <asp:TextBox ID="txtHscEngMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                        ToolTip="Please Enter HSC ENG MAX" MaxLength="3" CssClass="form-control" TabIndex="133"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-2" style="display: none">
                                                    <label>PCM-MATHS</label>
                                                    <asp:TextBox ID="txtHscMaths" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter HSC MATHS"
                                                        MaxLength="3" CssClass="form-control" TabIndex="134"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3" style="display: none">
                                                    <div class="row">
                                                        <label>&nbsp;&nbsp;&nbsp;PCM-MATHS-MAX</label>
                                                    </div>
                                                    <asp:TextBox ID="txtHscMathsMax" onkeyup="validateNumeric(this);" Width="60%" runat="server"
                                                        MaxLength="3" ToolTip="Please Enter HSC MATHS MAX" CssClass="form-control" TabIndex="135"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div style="display: none">
                                            <div class="form-group col-md-1">
                                                <label>HSSC-PCM</label>
                                                <asp:TextBox ID="txtHscPcmHssc" onkeyup="validateNumeric(this);" runat="server"
                                                    MaxLength="3" ToolTip="Please Enter HSC PCM Marks" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">
                                                <label>HSSC-PCM-MAX</label>
                                                <asp:TextBox ID="txtHscPcmMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                    MaxLength="3" ToolTip="Please Enter HSC PCM MAX" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">
                                                <label>HSSC-BIO</label>
                                                <asp:TextBox ID="txtHscBioHssc" onkeyup="validateNumeric(this);" runat="server" ToolTip="Please Enter HSC BIO"
                                                    MaxLength="3" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">
                                                <label>HSSC-BIO-MAX</label>
                                                <asp:TextBox ID="txtHscBioMaxHssc" onkeyup="validateNumeric(this);" runat="server"
                                                    MaxLength="3" ToolTip="Please enter HSC BIO MAX" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                        <h4><b>Entrance Exam Scores</b></h4>
                                    </div>
                                </div>
                                <br />
                                <div id="divEntranceExamScores" runat="server">
                                    <div class="form-group col-md-3">
                                        <label>Exam Name</label>
                                        <asp:DropDownList ID="ddlExamNo" runat="server" CssClass="form-control" Enabled="false" AppendDataBoundItems="True"
                                            ToolTip="Please Select Exam no." TabIndex="163">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Exam Roll No.</label>
                                        <asp:TextBox ID="txtQExamRollNo" runat="server" CssClass="form-control" Enabled="false" ToolTip="Please Enter Qualifying Exam Roll No"
                                            TabIndex="164"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Year of Exam</label>
                                        <asp:TextBox ID="txtYearOfExam" runat="server" CssClass="form-control" TabIndex="165" onkeyup="validateNumeric(this);" ToolTip="Please Enter Year of Exam"
                                            MaxLength="4"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-md-2" style="display: none;">
                                        <label>State Rank</label>
                                        <asp:TextBox ID="txtStateRank" runat="server" CssClass="form-control" ToolTip="Please Enter State Rank"
                                            TabIndex="109"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteStateRank" runat="server" FilterType="Numbers"
                                            TargetControlID="txtStateRank">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Percentage</label>
                                        <asp:TextBox ID="txtPer" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage"
                                            onkeyup="validateNumeric(this);" TabIndex="166"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>CGPA</label>
                                        <asp:TextBox ID="txtPercentile" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" ToolTip="Please Enter CGPA"
                                            TabIndex="167">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamNo"
                                            ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                            ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Rank</label>
                                        <asp:TextBox ID="txtAllIndiaRank" runat="server" CssClass="form-control" ToolTip="Please Enter Rank"
                                            TabIndex="168"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteAllIndiaRank" runat="server" FilterType="Numbers"
                                            TargetControlID="txtAllIndiaRank">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-2" style="display: none;">
                                        <label>Quota</label>
                                        <asp:DropDownList ID="ddlQuota" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                            TabIndex="113">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Score</label>
                                        <asp:TextBox ID="txtScore" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" ToolTip="Please Enter Score"
                                            TabIndex="169"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2" id="tdpaper" style="display: none;" runat="server">
                                        <label id="tdpapertxt" runat="server">Paper</label>
                                        <asp:TextBox ID="txtPaper" CssClass="form-control" runat="server" TabIndex="115"></asp:TextBox>
                                    </div>
                                    <asp:Panel ID="pnlpapercode" runat="server" Visible="false">
                                        <div class="form-group col-md-2" id="tdpapercode" runat="server">
                                            <label id="tdpapercodetxt" runat="server">Paper Code</label>
                                            <asp:TextBox ID="txtpaperCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                            <%--Entrance Exam Scores--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                        <h4><b>Other Entrance Exam Scores(P.G)</b></h4>
                                    </div>
                                </div>
                                <br />
                                <div id="div1" runat="server">
                                    <div class="form-group col-md-3">
                                        <label>Exam Name</label>
                                        <asp:DropDownList ID="ddlpgentranceno" runat="server" CssClass="form-control" Enabled="false" AppendDataBoundItems="True"
                                            ToolTip="Please Select Exam no." TabIndex="163">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Exam Roll No.</label>
                                        <asp:TextBox ID="txtpgrollno" runat="server" CssClass="form-control" Enabled="false" ToolTip="Please Enter Qualifying Exam Roll No"
                                            TabIndex="164"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Year of Exam</label>
                                        <asp:TextBox ID="txtpgexamyear" runat="server" CssClass="form-control" TabIndex="165" onkeyup="validateNumeric(this);" ToolTip="Please Enter Year of Exam"
                                            MaxLength="4"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2" style="display: none;">
                                        <label>State Rank</label>
                                        <asp:TextBox ID="txtpgsrank" runat="server" CssClass="form-control" ToolTip="Please Enter State Rank"
                                            TabIndex="109"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtStateRank">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Percentage</label>
                                        <asp:TextBox ID="txtpgpercentage" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage"
                                            onkeyup="validateNumeric(this);" TabIndex="166"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>CGPA</label>
                                        <asp:TextBox ID="txtpgpercentile" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" ToolTip="Please Enter CGPA"
                                            TabIndex="167">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamNo"
                                            ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                            ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Rank</label>
                                        <asp:TextBox ID="txtpgrank" runat="server" CssClass="form-control" ToolTip="Please Enter Rank"
                                            TabIndex="168"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                            TargetControlID="txtAllIndiaRank">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-2" style="display: none;">
                                        <label>Quota</label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                            TabIndex="113">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>Score</label>
                                        <asp:TextBox ID="txtpgscore" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" ToolTip="Please Enter Score"
                                            TabIndex="169"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2" id="Div2" style="display: none;" runat="server">
                                        <label id="Label1" runat="server">Paper</label>
                                        <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server" TabIndex="115"></asp:TextBox>
                                    </div>
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <div class="form-group col-md-2" id="Div3" runat="server">
                                            <label id="Label2" runat="server">Paper Code</label>
                                            <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                            <%--Entrance Exam Scores--%>
                        </div>
                        <div class="box box-footer">
                            <div class="box box-info">
                                <div class="box-header with-border" id="trLastQual" runat="server">
                                    <h3 class="box-title"><b>Student Last(UG or PG) Qualification</b></h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:UpdatePanel ID="upEditQualExm" runat="server">
                                        <ContentTemplate>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <label>School / College Name</label>
                                                        <asp:TextBox ID="txtSchoolCollegeNameQualifying" runat="server" CssClass="form-control" TabIndex="170" ToolTip="Please Enter School / College Name"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                            TargetControlID="txtSchoolCollegeNameQualifying" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label>Board</label>

                                                        <asp:TextBox ID="txtBoardQualifying" runat="server" TabIndex="171" ToolTip="Please Enter Board"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                            TargetControlID="txtBoardQualifying" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label>Qualifying Exam.</label>

                                                        <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="True"
                                                            CssClass="form-control" TabIndex="172" ToolTip="Please Select Qualifying Exam">
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <div class="row">
                                                            <label>&nbsp;&nbsp;&nbsp;Medium in Qualify Exam</label></div>
                                                        <asp:TextBox ID="txtQualiMedium" runat="server" CssClass="form-control" ToolTip="Please Enter Medium in Qualify Exam" TabIndex="173"
                                                            MaxLength="50"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server"
                                                            TargetControlID="txtQualiMedium" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1234567890" />
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label>Exam Roll No.</label>

                                                        <asp:TextBox ID="txtQualExamRollNo" runat="server" MaxLength="15" CssClass="form-control" TabIndex="174" ToolTip="Please Enter Exam Roll No."></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server"
                                                            TargetControlID="txtQualExamRollNo" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label>Year of Exam</label>
                                                        <asp:TextBox ID="txtYearOfExamQualifying" runat="server" ToolTip="Please Enter Year Of Exam"
                                                            onkeyup="validateNumeric(this);" CssClass="form-control" MaxLength="4" TabIndex="175"></asp:TextBox>
                                                    </div>


                                                    <div class="form-group col-md-3">
                                                        <label>Marks Obtained</label>
                                                        <asp:TextBox ID="txtMarksObtainedQualifying" onkeyup="validateNumeric(this);" runat="server"
                                                            MaxLength="3" CssClass="form-control" TabIndex="176"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtMarksObtainedQualifying">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>

                                                    <div class="form-group col-md-2">
                                                        <label>Out Of Marks</label>
                                                        <asp:TextBox ID="txtOutOfMarksQualifying" onkeyup="validateNumeric(this);" onblur="calPercentage(this,'other'),validateOtherMarksCam(this)"
                                                            runat="server" MaxLength="4" CssClass="form-control" TabIndex="177"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <label>Percentage</label>
                                                        <asp:TextBox ID="txtPercentageQualifying" onkeyup="validateNumeric(this);" runat="server"
                                                            ToolTip="Please Enter Percentage" CssClass="form-control" ValidationGroup="Academic" TabIndex="178"></asp:TextBox>

                                                        <asp:CompareValidator ID="rfvPercentageQualifying1" runat="server" ControlToValidate="txtPercentageQualifying"
                                                            Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <label>Grade</label>
                                                        <asp:TextBox ID="txtGradeQualifying" runat="server" ToolTip="Please Enter Grade" CssClass="form-control" TabIndex="179" MaxLength="5"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server"
                                                            TargetControlID="txtGradeQualifying" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|&&quot;'1234567890" />
                                                    </div>



                                                    <div class="form-group col-md-2">
                                                        <label>CGPA</label>
                                                        <asp:TextBox ID="txtPercentileQualifying" runat="server" onkeyup="validateNumeric(this);"
                                                            ToolTip="Please Enter Qualifying  CGPA" CssClass="form-control" ValidationGroup="Academic" TabIndex="180"></asp:TextBox>
                                                        <asp:CompareValidator ID="rfvPercentileQualifying" runat="server" ControlToValidate="txtPercentileQualifying"
                                                            Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                    </div>

                                                    <div class="form-group col-md-2">
                                                        <label>Attempt</label>
                                                        <asp:TextBox ID="txtAttemptQualifying" runat="server" ToolTip="Please Enter Attempt" TabIndex="181" MaxLength="3"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server"
                                                            TargetControlID="txtAttemptQualifying" FilterType="Numbers" />
                                                    </div>

                                                    <div class="form-group col-md-4">
                                                        <label>School/College Address</label>
                                                        <asp:TextBox ID="txtQualExmAddress" runat="server" TextMode="MultiLine"
                                                            ToolTip="School/College Address" MaxLength="100" TabIndex="182"></asp:TextBox>
                                                    </div>
                                                    <asp:Panel ID="pnlsupervision" runat="server" Visible="false">
                                                        <div class="form-group col-md-3">
                                                            <label>Research Topic</label>
                                                            <asp:TextBox ID="txtResearchTopic" runat="server" ToolTip="Please Enter Percentage"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-md-3">
                                                            <label>Supervisor Name1</label>
                                                            <asp:TextBox ID="txtSupervisorName1" runat="server" ToolTip="Please Enter Percentage"
                                                                CssClass="form-control" TabIndex="158"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                                TargetControlID="txtSupervisorName1" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="1234567890" />
                                                        </div>

                                                        <div class="form-group col-md-3">
                                                            <label>Supervisor Name2</label>
                                                            <asp:TextBox ID="txtSupervisorName2" runat="server" ToolTip="Please Enter Percentile"
                                                                CssClass="form-control" TabIndex="159"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                                                TargetControlID="txtSupervisorName2" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="1234567890" />
                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPercentileQualifying"
                                                                Display="None" ErrorMessage="Please Enter Numeric Number" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Double" ValidationGroup="Qual"></asp:CompareValidator>
                                                        </div>

                                                    </asp:Panel>

                                                </div>
                                                <div class="col-md-12">
                                                    <p class="text-center">

                                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="183" Text="Add" ToolTip="Add Qualification Detail"
                                                            ValidationGroup="Qual" CssClass="btn btn-outline-info" />
                                                    </p>
                                                    <asp:Panel ID="divlstugpgqual" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvQualExm" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <table class="table table-hover table-bordered table-responsive">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th style="text-align: center">Edit
                                                                                </th>
                                                                                <th style="text-align: center">Delete
                                                                                </th>
                                                                                <th>Qualifying Exam Name
                                                                                </th>
                                                                                <th>Year of Exam
                                                                                </th>
                                                                                <th>School/College Name
                                                                                </th>
                                                                                <th>Percentage
                                                                                </th>
                                                                                <th>Board
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
                                                                <tr class="item">
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnEditQualDetail" runat="server" OnClick="btnEditQualDetail_Click"
                                                                            CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("QUALIFYNAME")%>' />
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnDeleteQualDetail" runat="server" OnClick="btnDeleteQualDetail_Click"
                                                                            CommandArgument='<%# Eval("QUALIFYNO") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete Record" />
                                                                    </td>
                                                                    <td id="qualifyno" runat="server">
                                                                        <%# Eval("QUALIFYNAME")%>
                                                                    </td>
                                                                    <td id="year_of_exam" runat="server">
                                                                        <%# Eval("YEAR_OF_EXAMHSSC")%>
                                                                    </td>
                                                                    <td id="school_college_name" runat="server">
                                                                        <%# Eval("SCHOOL_COLLEGE_NAME")%>
                                                                    </td>
                                                                    <td id="per" runat="server">
                                                                        <%# Eval("PER")%>
                                                                    </td>
                                                                    <td id="board" runat="server">
                                                                        <%# Eval("BOARD") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>

                                                </div>

                                                <hr />
                                                <div class="text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="184" Text="Save & Continue >>" ToolTip="Click to Save"
                                                        class="btn btn-outline-primary" UseSubmitBehavior="false" OnClick="btnSubmit_Click" ValidationGroup="Submit"
                                                        OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" />

                                                    <button runat="server" id="btnGohome" visible="false" tabindex="185" onserverclick="btnGohome_Click" class="btn btn-outline-danger btnGohome" tooltip="Click to Go Back Home">
                                                        <i class="fa fa-home"></i>Go Back Home
                                                    </button>

                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />



                                        </Triggers>
                                    </asp:UpdatePanel>





                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }

        //validate HSC TOTAL MARKS CAMPARISION
        function validateHscMarksCam() {
            var totalMarks = (document.getElementById('<%= txtMarksObtainedHssc.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtMarksObtainedHssc.ClientID %>').value);
            var outOfMarks = (document.getElementById('<%= txtOutOfMarksHssc.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtOutOfMarksHssc.ClientID %>').value);

            if (parseInt(totalMarks) > parseInt(outOfMarks)) {
                alert('Please Enter HSC Total marks is less than HSC out of marks');
                document.getElementById('<%= txtPercentageHssc.ClientID %>').value = "";
                outOfMarks.focus();
            }
        }

        //validate SSC TOTAL MARKS CAMPARISION
        function validateSscMarksCam() {
            var totalMarks = (document.getElementById('<%= txtMarksObtainedSsc.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtMarksObtainedSsc.ClientID %>').value);
            var outOfMarks = (document.getElementById('<%= txtOutOfMarksSsc.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtOutOfMarksSsc.ClientID %>').value);

            if (parseInt(totalMarks) > parseInt(outOfMarks)) {
                alert('Please Enter SSC Total marks is less than SSC out of marks');
                document.getElementById('<%= txtPercentageSsc.ClientID %>').value = "";

            }
        }

        //validate OTHERS TOTAL MARKS CAMPARISION
        function validateOtherMarksCam() {
            var totalMarks = (document.getElementById('<%= txtMarksObtainedQualifying.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtMarksObtainedQualifying.ClientID %>').value);
            var outOfMarks = (document.getElementById('<%= txtOutOfMarksQualifying.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtOutOfMarksQualifying.ClientID %>').value);

            if (parseInt(totalMarks) > parseInt(outOfMarks)) {
                alert('Please Enter Total marks is less than out of marks');
                document.getElementById('<%= txtPercentageQualifying.ClientID %>').value = "";
            }
        }
        //Round to two digits
        fixedTo = function (number, n) {
            var k = Math.pow(10, n);
            return (Math.round(number * k) / k);
        }


        //calculate Percentage
        function calPercentage(txt, perType) {
            if (perType == "ssc") {
                if (document.getElementById('<%= txtMarksObtainedSsc.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtPercentageSsc.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtMarksObtainedSsc.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }
            else if (perType == "hsc") {
                if (document.getElementById('<%= txtMarksObtainedHssc.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtPercentageHssc.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtMarksObtainedHssc.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }
            else if (perType == "pcm") {
                if (document.getElementById('<%= txtHscCheMax.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtHscPhy.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtHscChe.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }

            else if (perType == "other") {
                if (document.getElementById('<%= txtMarksObtainedQualifying.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtPercentageQualifying.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtMarksObtainedQualifying.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }
}

function conver_uppercase(text) {
    text.value = text.value.toUpperCase();
}


    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
