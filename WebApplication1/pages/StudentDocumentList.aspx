<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentDocumentList.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_StudentDocumentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updreport"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <script type="text/javascript">
        function exefunction() {
            debugger;
            var count = 0;
            list = 'lvDocumentList';
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvDocumentList_ctrl" + i + "_chkOriCopy").checked) {
                        count++;
                    }
                }
                if (count == 0) {
                    alert("Please check atleast one check box !!!");
                    return false;
                }
            }
        }

    </script>
    <script type="text/javascript">
        try{
            RunThisAfterEachAsyncPostback();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
        }
        catch{
        }
    </script>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>Student Document List</b></h3>
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
                                <div class="form-group col-md-4">
                                    <label>SR NO <span style="color: red;">*</span></label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtApplicationID" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvApplicationID" runat="server" ErrorMessage="Please Enter Student Application ID."
                                            ControlToValidate="txtApplicationID" Display="None" SetFocusOnError="True" ValidationGroup="Show" />
                                        <span class="input-group-btn"><i class="material-icons left"></i>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-primary" TabIndex="2" ValidationGroup="Show"
                                                OnClick="btnShow_Click" />
                                            <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4">
                                </div>
                                <div class="col-md-12" id="divStudInfo" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Univ. Reg. No. :</b><a class="pull-right">
                                                    <asp:Label ID="lblRegNo" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Student's Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblStudName" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Gender :</b><a class="pull-right">
                                                    <asp:Label ID="lblSex" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Date of Admission :</b><a class="pull-right">
                                                    <asp:Label ID="lblDateOfAdm" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Payment Type :</b><a class="pull-right">
                                                    <asp:Label ID="lblPaymentType" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Degree :</b><a class="pull-right">
                                                    <asp:Label ID="lblDegree" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Branch :</b><a class="pull-right">
                                                    <asp:Label ID="lblBranch" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Year :</b><a class="pull-right">
                                                    <asp:Label ID="lblYear" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Semester :</b><a class="pull-right">
                                                    <asp:Label ID="lblSemester" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Batch :</b><a class="pull-right">
                                                    <asp:Label ID="lblBatch" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:ListView ID="lvDocumentList" runat="server">
                                            <LayoutTemplate>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h3 style="text-shadow: 2px 2px 3px #0b93f8;">Student Document List</h3>
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 23px">
                                                        <strong><span style="text-align: right; margin-right: 250px; margin-top: -30px"><span style="color: black;">Note:-</span><span style="color: darkred;"> 1. Only pdf,jpg,jpeg,doc,docx Files are Allowed<br />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2. Only Below or Equal to 40 KB Files are Allowed</span>
                                                        </span>

                                                        </strong>
                                                    </div>
                                                </div>
                                                <table class="table table-hovered table-bordered">
                                                    <tr class="bg-light-blue">
                                                        <th width="5%" style="text-align: center">Sl No.
                                                        </th>
                                                        <th width="5%" style="text-align: center">Select
                                                        </th>
                                                        <th width="30%">Document List
                                                        </th>
                                                        <th width="30%">Upload Documents
                                                        </th>
                                                        <th>Download
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="5%" style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>
                                                    <td width="5%" style="text-align: center">

                                                        <asp:CheckBox ID="chkOriCopy" runat="server" TabIndex="3" EnableViewState="true" RepeatDirection="Horizontal" CellPadding="5"></asp:CheckBox>
                                                        <br />
                                                    </td>
                                                    <td width="30%">
                                                        <asp:Label ID="lblDocument" runat="server" Text='<%# Eval("documentname") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="docid" Value='<%# Eval("DOCUMENTNO") %>' />
                                                    </td>
                                                    <td width="20%">
                                                        <asp:FileUpload ID="studentFileUpload" CssClass="btn btn-outline-info" TabIndex="3" runat="server" />

                                                    </td>
                                                    <td width="10%">
                                                        <asp:Button ID="btndownload" runat="server" OnClick="btndownload_Click" TabIndex="3" CssClass="btn btn-outline-info" Visible="false" Text="Download" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    &nbsp;
                          <div id="divMsg" runat="server"></div>
                                    <div class="row text-center" style="margin-right: 100px">
                                        <asp:Button ID="btnuploadDocuments" runat="server" CssClass="btn btn-outline-info"
                                            Text="Save and Upload" OnClick="btnuploadDocuments_Click" ToolTip="Click Here To Save and Upload" TabIndex="6" OnClientClick="return exefunction();" />
                                        <asp:Button ID="submit" runat="server" Text="submit" Visible="false" ValidationGroup="Show" CssClass="btn btn-outline-info" TabIndex="4"
                                            OnClick="submit_Click" />
                                        <br />
                                        &nbsp;&nbsp;
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="updreport">
                                        <ContentTemplate>
                                            <div style="margin-top: -55px; margin-left: 545px">
                                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary" TabIndex="6" ToolTip="Click Here For Report" Font-Bold="True" OnClick="btnReport_Click" ValidationGroup="Show" />&nbsp;
                                  <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel Student Information"
                                      CausesValidation="false" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancel_Click"
                                      Font-Bold="True" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <br />
                                    <p></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
