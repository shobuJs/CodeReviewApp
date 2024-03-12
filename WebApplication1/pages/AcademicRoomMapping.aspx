<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicRoomMapping.aspx.cs" Inherits="ACADEMIC_AcademicRoomMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAcademroom"
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

    <asp:UpdatePanel ID="updAcademroom" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server"></div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                          <%--  <h3 class="box-title">ROOM ALLOTMENT DEPARTMENT WISE</h3>--%>
                                <h3 class="box-title">
                               <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <%--   <div class="form-group col-lg-3 col-md-6 col-12">
                                            --<div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College & Regulation</label>
                                            </div>--
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <%--  <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>--%>
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldept"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <%--<div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Room</label>
                                            </div>--%>
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYRoom" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <div class="well" style="max-height: 182px; overflow: auto;">
                                                    <ul id="check-list-box" class="list-group checked-list-box">
                                                        <li class="list-group-item">
                                                            <asp:CheckBoxList ID="chkroom" runat="server" AppendDataBoundItems="true" BorderStyle="None" CssClass="form-control">
                                                            </asp:CheckBoxList>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnsubmit_Click" ValidationGroup="submit" OnClientClick="return Validate();" />
                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-outline-primary" OnClick="btnreport_Click" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btncancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Room Department Mapping List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>Department Name
                                                            </th>
                                                            <th>Room Name
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
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldept" runat="server" Text='<%# Eval("DEPTNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblroom" runat="server" Text='<%# Eval("value") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var atLeast = 1
        function Validate() {
            if (document.getElementById("<%=ddldept.ClientID%>").value == "0") {
                alert("Please Select Department");
                return false;
            }
            var CHK = document.getElementById("<%=chkroom.ClientID%>");

            var checkbox = CHK.getElementsByTagName("input");

            var counter = 0;

            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast " + atLeast + " item(s)");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

