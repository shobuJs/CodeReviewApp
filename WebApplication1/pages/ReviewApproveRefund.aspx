<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReviewApproveRefund.aspx.cs"
    Inherits="ACADEMIC_ReviewApproveRefund" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">


                        <div class="box-header with-border">
                            <h3 class="box-title">REVIEW APPROVAL & REFUND</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>



                        <div class="box-body" id="divCourses" runat="server" visible="false">

                            <div class="col-md-12" id="tblSession" runat="server" visible="false">
                                <div class="col-md-4">
                                    <label><span style="color: red;">* </span>Examination</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSession" runat="server" Display="None"
                                        InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Show" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSession" runat="server" Display="None"
                                        InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Submit" />
                                </div>
                                <div class="col-md-4">
                                    <label><span style="color: red;">* </span>Univ. Reg. No. / Adm. No.</label>
                                    <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                        Display="None" ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Show" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRollNo" runat="server" Display="None"
                                        ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Submit" />
                                </div>
                                <div class="col-md-4" style="margin-top: 25px">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-info" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" CssClass="btn btn-outline-danger" CausesValidation="false" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                </div>
                                <div class="col-md-12" style="margin-top: 25px">
                                    <p class="text-center">
                                    </p>
                                </div>
                            </div>

                            <br />

                            <div class="col-md-12" id="tblInfo" runat="server" visible="false">

                                <div class="col-md-6">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Student Name :</b><a class="pull-right">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                        </li>
                                        <li class="list-group-item">
                                            <b>Degree :</b><a class="pull-right">
                                                <asp:Label ID="lblDegree" runat="server" Font-Bold="True" /></a>
                                        </li>


                                    </ul>
                                </div>

                                <div class="col-md-6">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Branch :</b><a class="pull-right">
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            <asp:HiddenField ID="hfDegreeNo" runat="server" />
                                        </li>
                                        <li class="list-group-item">
                                            <b>Semester :</b><a class="pull-right">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-md-4"></div>

                            </div>

                            <div class="col-md-7 text-center" style="margin-left: -60px; color: white; font-size: 14px; font-weight: bold" id="divNote" runat="server" visible="false">
                                .
                            </div>

                            <div class="col-md-12">
                                <asp:Label ID="lblErrorMsg" runat="server" Style="color: red; font-size: medium; font-weight: bold;" Text="">
                                </asp:Label>
                            </div>

                        </div>

                        <div class="box-footer">

                            <div class="col-md-12" id="divAllCoursesFromHist" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCurrentSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h3>
                                                    <label class="label label-default">Subject Detail's</label></h3>
                                                <table id="example" class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Sr. No.</th>
                                                            <th style="text-align: center;">SUBJECT NAME
                                                            </th>
                                                            <th style="text-align: center;">SEMESTER
                                                            </th>
                                                            <th style="text-align: center;">DD DETAILS
                                                            </th>
                                                            <th style="text-align: center;">
                                                            </th>
                                                            <th style="text-align: center;">
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
                                            <tr id="trCurRow">

                                                <td style="text-align: center;"><%# Container.DataItemIndex+1 %></td>
                                               <%-- <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                </td>--%>
                                                <td style="text-align: left;"><%# Eval("COURSENAME")%><asp:HiddenField ID="hfCourseName" runat="server"  Value='<%# Eval("REFUND")%>'/></td>
                                              <%--  <td style="text-align: center;">
                                                    <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER") %>' />
                                                   
                                                </td>--%>
                                                 <td style="text-align: center;"><%# Eval("SEMESTER")%><asp:HiddenField ID="hfSem" runat="server"  Value='<%# Eval("APPROVE")%>'/></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblDDNO" runat="server" Text='<%# Eval("DDNO") %>' ToolTip='<%# Eval("DDNO") %>' />
                                                   <%-- <asp:TextBox ID="txtddno" runat="server" MaxLength="20" CssClass="form-control" Text='<%# Eval("DDNO") %>'></asp:TextBox>--%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnApprove" runat="server" Text="APPROVE" class="buttonStyle ui-corner-all btn btn-success" OnClick="btnApprove_Click" OnClientClick="return showConfirmApproval();" CommandArgument='<%# Eval("DDNO") %>' CommandName='<%# Eval("COURSENO") %>' Tooltip='<%# Eval("SEMESTER") %>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnRefund" runat="server" Text="REFUND" class="buttonStyle ui-corner-all btn btn-success" OnClientClick="return showConfirmRefund();" OnClick="btnRefund_Click"  CommandArgument='<%# Eval("DDNO") %>' CommandName='<%# Eval("COURSENO") %>' Tooltip='<%# Eval("SEMESTER") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>

                        </div>



                    </div>
                </div>
            </div>



        </ContentTemplate>
        <Triggers>
            <%-- <asp:AsyncPostBackTrigger ControlID="btnPrintRegSlip" EventName="Click"/>--%>
            <%--  <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
            <asp:PostBackTrigger ControlID="btnSubmit" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="JavaScript">

        //function FreezeScreen(msg) {
        //    scroll(0, 0);
        //    var outerPane = document.getElementById('FreezePane');
        //    var innerPane = document.getElementById('InnerFreezePane');
        //    if (outerPane) outerPane.className = 'FreezePaneOn';
        //    if (innerPane) innerPane.innerHTML = msg;
        //}

        function showConfirm() {
            var ret = confirm('Do you Really want to Submit this Subjects for Review Registration?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showPayConfirm() {
            var ret = confirm('Applied Revaluation details Will be confirmed only after successful payments.Do you Really want to Pay Amount Online ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showChallanConfirm() {
            var ret = confirm('Do you Really want to Print Revaluation Challan ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }

        function showConfirmApproval() {
            //var ret = confirm('Do you Really want to Confirm/Submit this Student Details for Re- Admission?');
            var ret = confirm('Do you Confirm the Approval?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function showConfirmRefund() {
            //var ret = confirm('Do you Really want to Confirm/Submit this Student Details for Re- Admission?');
            var ret = confirm('Do you Confirm the Refund?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>


    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });
    </script>


</asp:Content>
