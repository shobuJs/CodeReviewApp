<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="HigherStudies.aspx.cs" Inherits="ACADEMIC_HigherStudies" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        table, th {
            text-align: center;
        }
    </style>

    <div style="z-index: 1; position: relative;">
        <asp:UpdateProgress ID="updProg" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: fixed; margin-left: 40%; margin-top: 18%">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:Panel ID="pnlShow" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <!--academic Calendar-->
                    <div class="col-md-12">

                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Higher Studies</h3>
                            </div>

                            <div class="box-body" id="firstDiv" runat="server">
                                <div class="col-md-12" id="DivSearch" runat="server">

                                    <div class="form-group col-md-4" id="trRollNo" runat="server">
                                        <label for="city"><span style="color: red">*</span> Univ. Reg. No. </label>
                                        <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="50" TabIndex="1" placeholder="Enter Registration No." />
                                        <asp:RequiredFieldValidator ID="rfvtxtRollNo" runat="server" ControlToValidate="txtRollNo" Display="None" ValidationGroup="Submit"
                                            ErrorMessage="Please Enter Registration No."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4" style="padding-top: 25px;">
                                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" ValidationGroup="Submit" TabIndex="2" CssClass="btn btn-outline-info"><i class="fa fa-eye"></i> Show</asp:LinkButton>
                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" TabIndex="3" CssClass="btn btn-outline-danger" />

                                        <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                </div>
                                <div class=" col-md-12" style="text-align: center">
                                    <br />
                                </div>
                                <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                                    <div class="col-md-4">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Student Name</b><a class="pull-right"><asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Section</b><a class="pull-right"><asp:Label ID="lblSection" runat="server" Font-Bold="False" /></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-md-4">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Univ. Reg. No.</b><a class="pull-right"><asp:Label ID="lblEnrollNo" runat="server"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Degree</b><a class="pull-right">
                                                <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-md-4">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Semester/Year</b><a class="pull-right"><asp:Label ID="lblSemester" runat="server"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Branch</b><a class="pull-right"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item" id="trscheme" runat="server">
                                                <b>Regulation</b><a class="pull-right">
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-info" id="DivDetails" runat="server" visible="false">
                                <div class="box-header with-border">
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>

                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12">

                                        <div class="form-group col-md-12">

                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red">*</span> Name of Institution/University </label>
                                                <asp:TextBox ID="txtUnivercityNm" runat="server" CssClass="form-control" ToolTip="Enter Name of Institution/University" TabIndex="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvHEUNm" runat="server" ControlToValidate="txtUnivercityNm"
                                                    Display="None" ErrorMessage="Please Enter Name of Institution/University" ValidationGroup="Placement">
                                                </asp:RequiredFieldValidator>                                                 
                                            </div>

                                            <div class="form-group col-md-4" >
                                                <label for="city"><span style="color: red">*</span> Country </label>&nbsp;&nbsp;<u><asp:LinkButton ID="lnkOtherCountry" runat="server" Text="Other Country" OnClientClick="return ShowHideCountry(1)" ToolTip="To Enter Other Country"></asp:LinkButton></u>
                                                <asp:TextBox ID="txtOtherCountry" runat="server" CssClass="form-control" placeholder="Please Enter Country Name" ToolTip="Enter Country Name" style="display:none"></asp:TextBox>
                                                <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" ToolTip="Select Country" TabIndex="5" ValidationGroup="Placement">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvddlHECountry" runat="server" ControlToValidate="ddlCountry"
                                                                        Display="None" ErrorMessage="Please Select/Enter Higher Studies Country" InitialValue="0" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtCountry" runat="server" ControlToValidate="txtOtherCountry"
                                                                        Display="None" ErrorMessage="Please Enter Higher Studies Country" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red">*</span> State </label>&nbsp;&nbsp;<u><asp:LinkButton ID="lnkOtherState" runat="server" Text="Other State" OnClientClick="return ShowHideCountry(2)" ToolTip="To Enter Other State"></asp:LinkButton></u>
                                                <asp:TextBox ID="txtOtherState" runat="server" CssClass="form-control" placeholder="Please Enter State Name" ToolTip="Enter State Name" style="display:none"></asp:TextBox>
                                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" ValidationGroup="Placement" 
                                                    CssClass="form-control" ToolTip="Select State" TabIndex="6">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvddlHEState" runat="server" ControlToValidate="ddlState"
                                                                        Display="None" ErrorMessage="Please Select/Enter Higher Studies State" InitialValue="0" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>--%>
                                            </div>

                                        </div>

                                        <div class="form-group col-md-12">

                                            <div class="col-md-4">
                                                <label><span style="color: red">*</span> Name of the Program Joining </label>
                                                <asp:TextBox ID="txtProgramNm" runat="server" CssClass="form-control" ToolTip="Enter Programme Name" TabIndex="7"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtProgramNm" runat="server" ControlToValidate="txtProgramNm"
                                                                        Display="None" ErrorMessage="Please Enter Programme Name" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4">
                                                <label><span style="color: red">*</span> Details of Financial Aid </label>
                                                <asp:TextBox ID="txtFinancialAds" runat="server" CssClass="form-control" ToolTip="Enter Details of Financial Aid" TabIndex="8"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvFinancialAds" runat="server" ControlToValidate="txtFinancialAds"
                                                                        Display="None" ErrorMessage="Please Enter Financial Aid Details" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4">
                                                <label><span style="color: red">*</span> Marks Scored in Entrance Exam </label>
                                                <asp:TextBox ID="txtMarksScore" runat="server" CssClass="form-control" ToolTip="Enter Entrance Exam Score" TabIndex="9"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvMarksScore" runat="server" ControlToValidate="txtMarksScore"
                                                                        Display="None" ErrorMessage="Please Enter Marks Scored" ValidationGroup="Placement">
                                                                    </asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtMarksScore"
                                                    ValidChars='"1","2","3","4","5","6","7","8","9","0","."' FilterType="Custom" />
                                            </div>
                                        </div>
                                        <%--for multiple records add --%>
                                        <div class="form-group col-md-12">
                                            <p style="text-align: center">
                                                <asp:Button ID="btnHigherAdd" runat="server" Text="Add" ValidationGroup="Placement" CssClass="btn btn-outline-info" TabIndex="10"
                                                    ToolTip="Click here to Add" OnClick="btnHigherAdd_Click" />
                                                <%--<asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="Placement"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                                            </p>
                                        </div>
                                        <%--Ends here for add btn--%>

                                        <div style="margin-top: 200px">
                                            <asp:ListView ID="lvHigherstudies" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Details of Higher Studies Found"></asp:Label>
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4 class="box-title">Higher Studies Details List
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th class="text-center">Action</th>
                                                                    <th class="text-center">Institute Name</th>
                                                                    <th class="text-center">Country</th>
                                                                    <th class="text-center">State</th>
                                                                    <th class="text-center">Program Name</th>
                                                                    <th class="text-center">Financial Details</th>
                                                                    <th class="text-center">Marks Scored</th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </thead>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center">
                                                             <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                                AlternateText="Edit Record" CommandName='<%# Eval("RECNO") %>'
                                                                ToolTip='<%# Eval("RECNO") %>' OnClick="btnEdit_Click" Visible="false"
                                                                 />
                                                            <asp:ImageButton ID="btnHigherdelete" runat="server" ImageUrl="~/images/delete.gif"
                                                                AlternateText="Delete Record" CommandName='<%# Eval("RECNO") %>'
                                                                ToolTip='<%# Eval("RECNO") %>' OnClick="btnHigherdelete_Click"
                                                                OnClientClick="return showConfirmDel(this);" />
                                                            <%--CommandArgument='<%# Eval("RECNO") %>'--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblInstitue" runat="server" Text='<%#Eval("InstituteName")%>'></asp:Label>
                                                            <%--<asp:HiddenField ID="hdnsrnoMeeting" runat="server" Value='<%# Eval("RECNO") %>' />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("COUNTRYNAME")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("STATENAME")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("PROGRAMNAME")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("FINANCIAL_ADD_DETAILS")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblScore" runat="server" Text='<%#Eval("ENTRANCE_SCORE")%>'></asp:Label>
                                                        </td>		
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <div class="box-footer">
                                <p class="text-center">
                                    <%--<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" TabIndex="11" OnClientClick="return confirm ('Do you want to save the details!');"
                                        ValidationGroup="Placement" Visible="False" class="btn btn-outline-info" />--%>
                                      <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" TabIndex="11" OnClientClick="return check();" CssClass="btn btn-outline-info"
                                        Visible="False" class="btn btn-outline-info" />
                                    <asp:Button ID="btn_Cancel" runat="server" OnClick="btn_Cancel_Click" Text="Cancel" TabIndex="12"
                                        Visible="False" class="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="vdTestMark" runat="server" ValidationGroup="Placement"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            </div>                            
                        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function IsNumeric(txt) {
            //var ValidChars = "0123456789.-,";
            var ValidChars = "0123456789,";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>
     <script type="text/javascript">

         function showConfirmDel() {
             if (confirm("Are you sure you want to Delete this File?"))
                 return true;
             else
                 return false;
         }

         function check() {
                  if (confirm("Do you want to save the details!"))
                      return true;
                  else
                      return false;
         }
         function ShowHideCountry(chk) {
             if (chk == "1") {
                 var control = document.getElementById("<%= txtOtherCountry.ClientID %>");
                 document.getElementById("<%=ddlCountry.ClientID%>").style.display = "none";
                 document.getElementById("<%=ddlCountry.ClientID%>").value = "0";
                 if (control.style.display == "none") {
                     control.value = '';
                     control.style.display = "block";
                 }
                 else {
                     control.style.display = "none";
                     control.value = '';
                     document.getElementById("<%=ddlCountry.ClientID%>").style.display = "block";
                 }
             }
             else {
                 var txtotherState = document.getElementById("<%=txtOtherState.ClientID%>");
                 document.getElementById("<%=ddlState.ClientID%>").style.display = "none";
                 document.getElementById("<%=ddlState.ClientID%>").value = "0";
                 if (txtotherState.style.display == "none") {
                     txtotherState.value = '';
                     txtotherState.style.display = "block";
                 }
                 else {
                     txtotherState.style.display = "none";
                     txtotherState.value = '';
                     document.getElementById("<%=ddlState.ClientID%>").style.display = "block";
                 }
             }
             return false;
         }
         function isnotEmpty() {
             debugger;
                 var isCddl = document.getElementById("<%=ddlCountry.ClientID%>").value;
                 var txtnoC = document.getElementById("<%=txtOtherCountry.ClientID%>").value;
                 var isSddl = document.getElementById("<%=ddlState.ClientID%>").value;
                 var txtnoS = document.getElementById("<%=txtOtherState.ClientID%>").value;
                 if (txtnoC=="" || txtnoC=='' || isCddl == "0") {
                     alert("Please Select/Enter Higher Studies Country");
                     return;
                 }
                 else {
                     if (isSddl=="0" || txtnoS == "" || txtnoS == '') {
                         alert("Please Select/Enter Higher Studies State");
                         return;
                     }
                 }
                 return false;
         }
    </script>

</asp:Content>
