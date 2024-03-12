<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Slot_Booking.aspx.cs" Inherits="Slot_Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <asp:UpdatePanel ID="updSlot" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentName1" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStdID" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date Of Birth :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDOB1" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblGender" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Email ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Program :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblProgram1" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSem" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item text-center">
                                                <asp:Image ID="imgPhoto" runat="server" Width="140px" Height="140px" />
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Activity</label>
                                        </div>
                                        <asp:ListBox ID="ddlContent" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Available Slot</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAvailableSlot" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <asp:ListView ID="LvSlot" runat="server">
                                    <LayoutTemplate>
                                        <%--<div class="sub-heading">
                                            <h5>Withdrawal Approval List</h5>
                                        </div>--%>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>                                                       
                                                     Sr.No.
                                                    </th>
                                                    <th>Intake</th>
                                                    <th>Date</th>
                                                    <th>Start Time - End Time</th>
                                                    <%--<th></th>--%>
                                                    <th>Activity</th>
                                                    <th>Capacity</th>
                                                    <th>Delivery Status</th>
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
                                                <%# Container.DataItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <%# Eval("BATCHNAME")%> 
                                            </td>
                                            <td>
                                               <%# Eval("APPLY_DATE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SLOTNAME")%>
                                            </td>
                                            <%--<td>
                                                <%# Eval("SLOTNAME")%>
                                            </td>--%>
                                            <td>
                                                  <%# Eval("ACTIVITY_NAME")%>
                                            </td>
                                            <td>
                                                 <asp:LinkButton ID="LnkREsch" runat="server" CssClass="btn btn-outline-info btn-sm" OnClick="LnkREsch_Click" CommandArgument='<%# Eval("SLOT_BOOK_NO")%>' CommandName='<%# Eval("SLOTNAME")%>'  Enabled='<%# Eval("USER_STATUS").ToString() == "1" ? false : true%>' >ReSchedule</asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                                <asp:HiddenField ID="hdfgetdate" runat="server" Value=' <%# Eval("USER_STATUS")%>' />
                                                <asp:HiddenField ID="hdfslotdate" runat="server" Value=' <%# Eval("SLOT_DATE")%>' />
                                            </td>                                                                                     
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                            <%--<div class="col-12 mt-3">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Intake</th>
                                            <th>Date</th>
                                            <th>Start Time</th>
                                            <th>End Time</th>
                                            <th>Activity</th>
                                            <th>Capacity </th>
                                            <th>Delivery Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Jan - June 2021-2022</td>
                                            <td>10-11-2022</td>
                                            <td>09:00</td>
                                            <td>10:00</td>
                                            <td>Module, Counselling, Pick Up</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-info btn-sm">ReSchedule</asp:LinkButton></td>
                                            <td>Approved</td>
                                        </tr>
                                        <tr>
                                            <td>Jan - June 2021-2022</td>
                                            <td>10-11-2022</td>
                                            <td>09:00</td>
                                            <td>10:00</td>
                                            <td>Module, Counselling, Pick Up</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-outline-info btn-sm">ReSchedule</asp:LinkButton></td>
                                            <td>Pending</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

</asp:Content>

