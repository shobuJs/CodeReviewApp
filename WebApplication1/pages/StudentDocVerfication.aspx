<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="StudentDocVerfication.aspx.cs"
    Inherits="ACADEMIC_StudentDocVerfication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>

    <script type="text/javascript" src="../scripts/jquery.min.js"></script>

    <script type="text/javascript">
        function Validate() {
            var isValid = $("#dvListView input[type=checkbox]:checked").length > 0;
            if (!isValid) {
                alert("Please check atleast one checkbox.");
            }
            return isValid;
        }
    </script>


    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script language="javascript" type="text/javascript">
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
        function Validate() {
            var isValid = $("#dvListView input[type=checkbox]:checked").length > 0;
            if (!isValid) {
                alert("Please check atleast one checkbox.");
            }
            return isValid;
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>ADMISSION DOCUMENT VERIFICATION</b></h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-3">
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Admission Batch <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlSession" CssClass="form-control" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Session."
                                                ValidationGroup="submit" Font-Bold="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Application ID <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtAppID" runat="server" CssClass="form-control" ToolTip="Please Enter Application ID" ValidationGroup="submit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvappid" runat="server" Display="None" ErrorMessage="Please Enter Application ID"
                                                ValidationGroup="submit" ControlToValidate="txtAppID"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-outline-info" Text="Show Details" ValidationGroup="submit"
                                        OnClick="btnShow_Click" />&nbsp;                                
                                <asp:Button ID="btnPdfreport" runat="server" CssClass="btn btn-outline-primary" Text="Doc Verification Student List" OnClick="btnPdfreport_Click" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-outline-danger" Text="Cancel"
                                        OnClick="btncancel_Click" />&nbsp;
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
          ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ValidationGroup="submit1" ShowSummary="False" />
                                </p>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <b>
                                            <asp:Label ID="lblNote" runat="server" Text="" ForeColor="Green"></asp:Label></b>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <b>
                                            <asp:Label ID="lblNote2" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>

                                <div class="col-md-12" id="fldstudent" visible="false" runat="server">
                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                        <h3>Student Information</h3>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                    <p></p>
                                                    <b>JSS Application ID :</b><a class="pull-right">
                                                        <asp:Label ID="lblAppID" runat="server" Text=""></asp:Label>
                                                    </a>
                                                    <p></p>
                                                    <li class="list-group-item">
                                                        <b>Name of Candidate :</b><a class="pull-right">
                                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>

                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                <p></p>
                                                <b>Father's Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>
                                                </a>
                                                <hr />

                                                <p></p>
                                                <div id="TRDegreeName" runat="server" visible="false">
                                                    <li class="list-group-item">
                                                        <b>Degree Name :</b><a class="pull-right">
                                                            <asp:Label ID="lblDegreeName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="TREntranceDetails" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>Entrance Exam Details:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblexamname" runat="server"></asp:Label>
                                                </b>
                                            </div>
                                        </div>
                                        <p></p>
                                        <table class="table table-hover table-bordered table-responsive" border="1">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        <b>Roll No.</b>
                                                    </th>
                                                    <th>
                                                        <b>Total</b>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblJEERollNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJtotal" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div class="col-md-12" id="dvListView" runat="server">

                                            <asp:ListView ID="lvDocumentList" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="demo-grid">
                                                        <h4 style="text-shadow: 2px 2px 3px #0b93f8;">Checklist Of Documents </h4>
                                                        <table id="tblstudDetails" class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr.No.
                                                                    </th>
                                                                    <th>Particulars
                                                                    </th>
                                                                    <th>Checklist
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <%# Eval("SRNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DOCUMENTNAME")%>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("DOCUMENTTYPENO")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="row">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" OnClientClick="return Validate()" />&nbsp;&nbsp;
                                <asp:Button ID="btnSingleReport" runat="server" Text="Report" CssClass="btn btn-outline-primary" OnClick="btnSingleReport_Click" />
                                            </p>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="lblNote1" runat="server" Text=""></asp:Label></b>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>          
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSingleReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btncancel" />
            <asp:PostBackTrigger ControlID="btnPdfreport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
