<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="UserInfoVerify.aspx.cs" Inherits="Academic_UserInfoVerify" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    </script>


    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarks"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <asp:UpdatePanel ID="updMarks" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>APPLICANT MARKS VERIFICATION</b></h3>
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
                                        <div class="form-group col-md-4">
                                            <label>Admission Batch <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlSession" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                ValidationGroup="submit" Font-Bold="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Admission Category <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlAdmcat" ValidationGroup="submit" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvAdmcat" runat="server" ControlToValidate="ddlAdmcat"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Select Admission Category." InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Application ID </label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAppID"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvAppID" ControlToValidate="txtAppID"
                                                Display="None" ErrorMessage="Please Enter Application ID" ValidationGroup="report"
                                                InitialValue="0" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblerrormsg" Visible="false" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Show" CssClass="btn btn-success" ValidationGroup="submit"
                                        OnClick="btnSubmit_Click" />&nbsp;
                                       <asp:Button ID="btnVerify" runat="server" Enabled="false" CssClass="btn btn-outline-info" Text="Verify" ValidationGroup="submit"
                                           OnClick="btnVerify_Click" />
                                    &nbsp;
                                       <asp:Button ID="btnReport" runat="server" ValidationGroup="submit" CssClass="btn btn-outline-primary" Text="Report" OnClick="btnReport_Click" />
                                    &nbsp;
                                       <asp:Button runat="server" ID="btnexport" Text="Export to Excel" CssClass="btn btn-outline-primary" OnClick="btnexport_Click" />
                                    &nbsp;
                                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" ValidationGroup="s" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                </p>
                                <div class="col-md-12">
                                    <div runat="server" id="divDetails" visible="false">

                                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" Height="500%">
                                            <asp:ListView ID="lvApplicantdata" Visible="false" runat="server">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="titlebar">
                                                            <h4 style="text-shadow: 2px 2px 3px #0b93f8;">Applicant Marks Verification </h4>

                                                        </div>
                                                        <table id="tblstudDetails" class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">

                                                                    <th>Sr No.
                                                                    </th>
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Application ID
                                                                    </th>
                                                                    <th>JEE Roll No
                                                                    </th>
                                                                    <th>DOB
                                                    <br />
                                                                        (dd/mm/yyyy)
                                                                    </th>
                                                                    <th>JEE Score
                                                                    </th>
                                                                    <th>Verify
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
                                                            <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("FIRSTNAME")%>
                                                            <asp:HiddenField ID="hiduserno" runat="server" Value='<%# Eval("USERNO") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("USERNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DOB")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" Width="90%" CssClass="form-control" ID="txtJEETotal" ValidationGroup="Submit"
                                                                Text='<%# Eval("ENTR_SCORE") %>'>
                                                            </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" Width="90%" ID="chkVerify" ToolTip=' <%# Eval("VERIFY_STATUS")%>' Font-Bold="true" BorderColor="Green"
                                                                Checked='<%# (Convert.ToInt32(Eval("VERIFY_STATUS"))==1 ? true : false) %>' Enabled='<%# (Convert.ToInt32(Eval("VERIFY_STATUS"))==1 ? false : true) %>'
                                                                ValidationGroup="Submit" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <div style="width: 100%; height: 19px; vertical-align: middle; text-align: center; font-family: Verdana; font-size: 10pt;">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvApplicantdata" PageSize="10"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="6" CurrentPageLabelCssClass="current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>

                                    </div>
                                    <br />
                                    <br />
                                    <div id="divMsg" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnVerify" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnexport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
