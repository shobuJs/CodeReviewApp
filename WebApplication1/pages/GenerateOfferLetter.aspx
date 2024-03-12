<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="GenerateOfferLetter.aspx.cs"
    Inherits="Academic_GenerateOfferLetter" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">


        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAllot']").attr('checked', this.checked);
            });
        });

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
                                <h3 class="box-title"><b>GENERATE OFFER LETTER</b></h3>
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
                                            <label>Admission Batch <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlSession" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                ValidationGroup="submit">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmissionBatch" runat="server" ErrorMessage="Please Select Admission Batch" InitialValue="0" ControlToValidate="ddlListType" ValidationGroup="submit" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Degree <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlDegree" CssClass="form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                AutoPostBack="true"
                                                ValidationGroup="submit">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree" InitialValue="0" ControlToValidate="ddlDegree" ValidationGroup="submit" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Entrance Exam Name <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlEntrance" CssClass="form-control" runat="server" AppendDataBoundItems="True" ValidationGroup="submit">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEnterance" runat="server" ControlToValidate="ddlEntrance"
                                                Display="None" ErrorMessage="Please Select Entrance Exam Name." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Select List Type <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlListType" CssClass="form-control" runat="server" AutoPostBack="true"
                                                ValidationGroup="submit"
                                                OnSelectedIndexChanged="ddlListType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Confirm Student List</asp:ListItem>
                                                <asp:ListItem Value="2">Waiting Student List</asp:ListItem>
                                                <asp:ListItem Value="3">Confirm-Waiting Student List</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvListType" runat="server" ErrorMessage="Please Select List Type" InitialValue="0" ControlToValidate="ddlListType" ValidationGroup="submit" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Select Round <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlRound" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Round-1</asp:ListItem>
                                                <asp:ListItem Value="2">Round-2</asp:ListItem>
                                                <asp:ListItem Value="3">Round-3</asp:ListItem>
                                                <asp:ListItem Value="4">Round-4</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Offer Letter Print  Date <span style="color: red;">*</span></label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <span class="fa fa-calendar text-green"></span>
                                                </div>
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:CompareValidator ID="valStartDateType" runat="server" ControlToValidate="txtDate"
                                                    Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                    SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>From Date <span style="color: red;">*</span></label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <span class="fa fa-calendar text-green"></span>
                                                </div>

                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtenderFromDate" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:CompareValidator ID="valFromDateType" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                    SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <span class="fa fa-calendar text-green"></span>
                                                </div>

                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtenderToDate" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="imgClaToDate" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:CompareValidator ID="valToDateType" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                    SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-info" ValidationGroup="submit" OnClick="btnShow_Click" />&nbsp; 
                       <asp:Button runat="server" ID="btnOfferletter" Text="Generate Offer Letter" CssClass="btn btn-outline-primary" OnClick="btnOfferletter_Click" />&nbsp;
                        <asp:Button runat="server" ID="btnSendEmail" Text="Send Mail" OnClick="btnSendEmail_Click" CssClass="btn btn-outline-primary" />&nbsp;
                          <asp:Button runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click" Visible="false" ToolTip="Download" CssClass="btn btn-outline-primary" />
                                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" CausesValidation="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                </p>
                                <div class="col-md-12" id="dvListView">
                                    <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="demo-grid">
                                                <h4 style="text-shadow: 2px 2px 3px #0b93f8;">Select Students to Generate Offer Letter</h4>
                                                <table class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" />
                                                                Plz Check
                                                            </th>
                                                            <th>Sr No.
                                                            </th>
                                                            <th>Merit list no.
                                                            </th>
                                                            <th>Application ID
                                                            </th>
                                                            <th>Email ID
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Branch Preference
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
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td>
                                                    <asp:CheckBox ID="chkAllot" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("USERNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SRNO")%>
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("MERIT_LIST_NO")%>
                                                </td>

                                                <td>
                                                    <%# Eval("APPLICATIONID")%>
                                                    <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("APPLICATIONID") %>' />

                                                </td>

                                                <td>
                                                    <%# Eval("FIRSTNAME")%>
                                                    <%# Eval("LASTNAME")%>
                                                    <asp:HiddenField ID="hdfirstname" runat="server" Value='<%# Eval("FIRSTNAME") %>' />
                                                    <asp:HiddenField ID="hdlastname" runat="server" Value='<%# Eval("LASTNAME") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                    <asp:HiddenField ID="hdfEmailid" runat="server" Value='<%# Eval("EMAILID") %>' />
                                                </td>
                                                <td>
                                                    <%#Eval("BRANCHNAME_PREF")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div id="divMsg" runat="server">
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnOfferletter" />
            <asp:PostBackTrigger ControlID="btnSendEmail" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
