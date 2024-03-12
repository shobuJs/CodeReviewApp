<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="StudentRegistration.aspx.cs"
    Inherits="ACADEMIC_StudentRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
               <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                    
                </div>   
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script language="javascript" type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                          <%--  <h3 class="box-title" id="heading" runat="server"></h3>--%>
                        </div>

                        <%--Validations Start--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTanno"
                            ErrorMessage="Please Enter Temporary Admission Number" Display="None" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="rdbQuestion"
                            ErrorMessage="Please Select Citizen" Display="None" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                          <asp:RequiredFieldValidator ID="rvffirstname" runat="server" ControlToValidate="txtFirstName"
                            Display="None" ErrorMessage="Please Enter First Name" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                          <asp:RequiredFieldValidator ID="rfvlastname" runat="server" ControlToValidate="txtLastName"
                            Display="None" ErrorMessage="Please Enter Last Name" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDateOfBirth"
                            Display="None" ErrorMessage="Please Select/Enter Date Of Birth" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDateOfAdmission"
                            Display="None" ErrorMessage="Please Select/Enter Date Of Birth" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNameInitial"
                            Display="None" ErrorMessage="Please Enter Name With Initials" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHometel"
                            Display="None" ErrorMessage="Please Enter Home Telephone Number" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rfvStudEmail" runat="server" ControlToValidate="txtStudEmail"
                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStudEmail"
                            Display="None" ErrorMessage="Please Enter Student Email " ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStudMobile"
                            Display="None" ErrorMessage="Please Enter Student Mobile Number" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. is Invalid"
                            ID="revMobile" ControlToValidate="txtStudMobile" ValidationExpression=".{10}.*"
                            Display="None" ValidationGroup="academic">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select College/ School Name"
                            ValidationGroup="academic">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Degree"
                            ValidationGroup="academic">
                        </asp:RequiredFieldValidator>
                                 
                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                            ErrorMessage="Please Select Program" Display="None" ValidationGroup="academic"
                            SetFocusOnError="true" InitialValue="0">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDD_Branch" runat="server" ControlToValidate="ddlBranch"
                            ErrorMessage="Please Select Branch" Display="None" ValidationGroup="ddinfo" SetFocusOnError="true"
                            InitialValue="0">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmType"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Student Type"
                            ValidationGroup="academic">
                        </asp:RequiredFieldValidator>                      
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlYearSem"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Year/Sem"
                            ValidationGroup="academic">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvddino_batch" runat="server" ControlToValidate="ddlBatch"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                            ValidationGroup="ddinfo">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Payment Type"
                            ValidationGroup="academic">\
                        </asp:RequiredFieldValidator>                    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFatherMobile"
                            Display="None" ErrorMessage="Please Enter Parents Mobile Number" ValidationGroup="academic"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Parents Mobile No. is Invalid"
                            ID="RegularExpressionValidator1" ControlToValidate="txtFatherMobile" ValidationExpression=".{10}.*"
                            Display="None" ValidationGroup="academic">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Remark!" ValidationGroup="academic"
                            Enabled="false">
                        </asp:RequiredFieldValidator>
                        <%--Validations End--%>


                        <div class="box-body">
                            <div class="col-12">
                                <div class="" id="divSearch" runat="server" visible="false" style="display: block;">
                                    <div class="row">
                                        <div class="form-group col-lg-9 col-md-7 col-12">
                                            <div class="label-dynamic">
                                                <%--<label> Temporary Admission Number </label>--%>
                                                    <asp:Label ID="lblTemporaryAdmissionNumber" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <span class="form-inline">
                                                <asp:TextBox ID="txtTanno" CssClass="form-control" runat="server" TabIndex="1" placeholder="Enter Temporary Admission Number" />
                                        
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex="2" OnClick="btnSearch_Click" CssClass="btn btn-outline-info m-top" ValidationGroup="Show" />
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-5 col-12" style="top: 15px" id="divFees" runat="server" visible="false">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Fees To Be Paid :</b>
                                                    <a class="sub-label"><span style="color: green">Rs.<asp:Label ID="lblFees" runat="server" Font-Bold="true"></asp:Label></span></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="" id="divGeneralInfo" style="display: block;">
                               
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                              <div class="label-dynamic">      
                                               <sup>*</sup> 
                                                  <asp:Label ID="lblfirstname" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>
                                                <asp:TextBox ID="txtFirstName" runat="server" Width="100%" TabIndex="3" ToolTip="First Name "  
                                                    onkeypress="return alphaOnly(event);" placeholder="Please Enter First Name"  MaxLength="50" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter First Name"
                                                    ControlToValidate="txtFirstName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFirstName"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />          
                                           
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                              <div class="label-dynamic">           
                                               <sup>*</sup> 
                                                    <asp:Label ID="lblLastname" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>
                                                <asp:TextBox ID="txtLastName" runat="server" Width="100%" TabIndex="4" ToolTip="Last Name " 
                                                    onkeypress="return alphaOnly(event);"  placeholder="Please Enter Last Name" MaxLength="50" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Last Name"
                                                    ControlToValidate="txtLastName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtLastName"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />          
                                            

                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                              <div class="label-dynamic">           
                                               <sup>*</sup> 
                                                   <asp:Label ID="lblnamewithinitials" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtNameInitial" runat="server" Width="100%" TabIndex="5" ToolTip="Name with Initials " 
                                                    onkeypress="return alphaOnly(event);" placeholder="Please Enter Name with Initials"  MaxLength="50" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Full Name with Initials"
                                                    ControlToValidate="txtNameInitial" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ajfFullName" runat="server" TargetControlID="txtNameInitial"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />          
                                           
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                 <asp:Label ID="lblStudentEmail" runat="server" Font-Bold="true"></asp:Label>
                                               <%-- <label> Student Email </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtStudEmail" runat="server" CssClass="form-control" ToolTip="Please Enter Student Email"
                                                TabIndex="6" placeholder="Please Enter Email ID" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                   <asp:Label ID="lblStudentMobileNumber" runat="server" Font-Bold="true"></asp:Label>
                                               <%-- <label> Student Mobile Number </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtStudMobile" CssClass="form-control" runat="server" TabIndex="7"
                                                MaxLength="10" onkeyup="chk(this);"  placeholder="Please Enter Mobile Number">
                                            </asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                   <asp:Label ID="lblhometel" runat="server" Font-Bold="true"></asp:Label>
                                               <%-- <label> Student Mobile Number </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtHometel" CssClass="form-control" runat="server" TabIndex="8"
                                                MaxLength="10"   placeholder="Please Enter Home Tel."  >
                                            </asp:TextBox>
                                        </div>
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">            
                                                <sup></sup>
                                                  <asp:Label ID="lblnic" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtNIC" runat="server" Width="100%" TabIndex="9" ToolTip="Please Enter NIC " 
                                                   MaxLength="30" CssClass="form-control"  placeholder="Please Enter NIC"/>                                          
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtNIC"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />          
                                           

                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">         
                                               <sup></sup> 
                                                  <asp:Label ID="lblpassportno" runat="server" Font-Bold="true"></asp:Label>
                                                 </div>
                                                <asp:TextBox ID="txtPassport" runat="server" Width="100%" TabIndex="10" ToolTip="Please Enter Passport No " 
                                                   MaxLength="30" CssClass="form-control"  placeholder="Please Enter Passport No"/>                                          
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPassport"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />          
                                           
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">  
                                                <sup>*</sup> 
                                                  <asp:Label ID="lbldob" runat="server" Font-Bold="true"></asp:Label>
                                                 </div>
                                                <asp:TextBox ID="txtDateOfBirth" name="dob" runat="server" TabIndex="11" CssClass="form-control dob" Width="100%"
                                                    ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY"  /> 
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Please Enter DOB (DD/MM/YY)"
                                                    ControlToValidate="txtDateOfBirth" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      
                                           
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                     <asp:Label ID="lblSelectGender" runat="server" Font-Bold="true"></asp:Label>
                                            <%--    <label> Select Gender </label>--%>
                                            </div>
                                            <div class="radio" style="margin-bottom: 9px;">
                                                <label>
                                                    <asp:RadioButton ID="rdoMale" runat="server" GroupName="Sex" TabIndex="12" Checked="true" />
                                                    Male
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Sex" TabIndex="13" />
                                                    Female
                                                </label>                                              
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">  
                                               <sup>*</sup> 
                                                   <asp:Label ID="lblfathername" runat="server" Font-Bold="true"></asp:Label>
                                                 </div>
                                                <asp:TextBox ID="txtfathername" runat="server" Width="100%" ToolTip="Please Enter Father Name"
                                                    onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Father Name" TabIndex="14"/>                                         
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtfathername"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars"  />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter Father Name"
                                                    ControlToValidate="txtfathername" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                           
                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">             
                                            <sup>*</sup> 
                                                    <asp:Label ID="lblmothername" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtMothersName" runat="server" Width="100%" TabIndex="15" ToolTip="Please Enter Mother's Name"
                                                    onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Mother's Name" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbemathername" runat="server" TargetControlID="txtMothersName"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Enter Mother's Name"
                                                    ControlToValidate="txtMothersName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                <asp:HiddenField runat="server" ID="HiddenField2" />
                                            
                                        </div> 
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                   <asp:Label ID="lblFatherMobileNumber" runat="server" Font-Bold="true"></asp:Label>
                                                <%--<label> Father Mobile Number </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtFatherMobile" CssClass="form-control" runat="server" TabIndex="16"
                                                MaxLength="10" onkeyup="chk(this);" placeholder="Please Enter Parents Mobile Nu.">
                                            </asp:TextBox>
                                        </div>
                                     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                              <%--  <label> College/ School Name </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlSchool" CssClass="form-control" runat="server" ValidationGroup="academic"
                                                ToolTip="Please Select College/ School Name" TabIndex="17" AppendDataBoundItems="True"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                    <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                           <%--     <label> Degree </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" ValidationGroup="academic" CssClass="form-control"
                                                ToolTip="Please Select Degree" TabIndex="18" AppendDataBoundItems="True" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                 <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                               <%-- <label> Branch </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" TabIndex="19"
                                                AppendDataBoundItems="True" ValidationGroup="academic" ToolTip="Please Select Branch">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                    <asp:Label ID="lblStudentType" runat="server" Font-Bold="true"></asp:Label>
                                            <%--    <label> Student Type </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmType" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                ToolTip="Please Select Admission Type" TabIndex="20">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYSemesterOrYear" runat="server" Font-Bold="true"></asp:Label>
                                              <%--<label> Year/Semester </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlYearSem" CssClass="form-control" runat="server" TabIndex="21"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlYearSem_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblAdmissionYear" runat="server" Font-Bold="true"></asp:Label>
                                              <%--  <label> Admission Year </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" TabIndex="22" CssClass="form-control"
                                                AppendDataBoundItems="true" ToolTip="Please Select Admission Year" AutoPostBack="True" />
                                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                ValidationGroup="academic" Enabled="false">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                     <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                 <%--   <label> Semester </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" CssClass="form-control" runat="server" TabIndex="23"
                                                AppendDataBoundItems="true" ToolTip="Please Select Semester" />
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" Enabled="false" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Semester"
                                                ValidationGroup="academic">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                       
                                       
                                                                                                                   
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                    <asp:Label ID="lblStudAdmDate" runat="server" Font-Bold="true"></asp:Label>
                                             <%--   <label> Date of Admission </label>--%>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue" id="imgCalDateOfAdmission"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfAdmission" runat="server" TabIndex="24" onchange="CheckFutureDate(this);"
                                                    CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfAdmission" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfAdmission" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                            </div>
                                        </div>
                                       
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                              <%--  <label> Admission Batch </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlBatch" CssClass="form-control" runat="server" TabIndex="25"
                                                AppendDataBoundItems="true" ToolTip="Please Select Admission Batch" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                  <asp:Label ID="lblPaymentType" runat="server" Font-Bold="true"></asp:Label>
                                               <%-- <label> Payment Type </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentType" CssClass="form-control" runat="server" TabIndex="26"
                                                AppendDataBoundItems="true" ToolTip="Please Select Payment Type" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged"
                                                AutoPostBack="true" />
                                        </div>
                                       
                                      
                                                                              
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                	<asp:Label ID="lblAreyou" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                    <asp:RadioButtonList ID="rdbQuestion" runat="server" TabIndex="27" RepeatDirection="Horizontal">                      
                                                        <asp:ListItem Text="&nbsp;Sri Lankan" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Foreign National" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Select Student sri Lankan or foreign national"
                                                         ControlToValidate="rdbQuestion" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>                            
                                                              
                                           
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                  <asp:Label ID="lblRemark" runat="server" Font-Bold="true"></asp:Label>
                                             <%--   <label> Remark </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" TabIndex="28"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="29" ValidationGroup="academic"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" Font-Bold="True" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="30" CausesValidation="false"
                                    CssClass="btn btn-outline-danger" ValidationGroup="academic" OnClick="btnCancel_Click"
                                    Font-Bold="True" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="academic"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
      <script>
          $(function () {
              var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
              var prev_date = new Date();
              prev_date.setDate(prev_date.getDate() - 1);
              $('.dob').daterangepicker({
                  singleDatePicker: true,
                  showDropdowns: true,
                  minDate: '01/1/1975',
                  maxDate: prev_date,
                  locale: {
                      format: 'DD/MM/YYYY'
                  },
              });
              if (dateval == "") {
                  $('.dob').val('');
              }
          });
    </script>

    <script type="text/javascript">
        /* To collapse and expand page sections */
        function toggleExpansion(image, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                image.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                image.src = "../images/collapse_blue.jpg";
            }
        }
    </script>

    <script type="text/javascript">
        function chk(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Enter Numbers Only!');
                txt.focus();
                return;
            }
        }
    </script>

    <script type="text/javascript">
        function ValidateBranch() {
            if (document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex == 0) {
                alert("Please Select Branch");
                return false;
            }
            else
                return true;
        }

        function UpdateCash_DD_Amount() {
            try {
                var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
                var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

                if (txtPayType != null && txtPaidAmt != null) {
                    if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                    }
                    else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                        var totalDDAmt = 0.00;
                        var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                        if (dataRows != null) {
                            for (i = 1; i < dataRows.length; i++) {
                                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                var dataCell = dataCellCollection.item(6);
                                if (dataCell != null) {
                                    var txtAmt = dataCell.innerHTML.trim();
                                    totalDDAmt += parseFloat(txtAmt);
                                }
                            }
                            if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }
    </script>

    <script language="JavaScript">
        function AlphaNumeric(evt) {
            debugger;
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            if ((charCode >= 48) && (charCode <= 57) || ((charCode >= 65) && (charCode <= 90)) || ((charCode >= 97) && (charCode <= 122))) {
                evt.value = evt.value.toUpperCase();
            }
            else {
                return false;
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
