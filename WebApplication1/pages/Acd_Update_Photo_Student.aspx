<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acd_Update_Photo_Student.aspx.cs" Inherits="ACADEMIC_Acd_Update_Photo_Student" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  Shrink the info panel out of view --%>
    <script>
        $(document).ready(function () {
            var table = $('#tablehead').DataTable({
                responsive: true,
                lengthChange: true,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export <span class="fa fa-angle-down"></span>',
                        buttons: [
                            'copy',
                            'excel',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
            new $.fn.dataTable.FixedHeader(table);
        });
    </script>
 <%--   <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
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
                                    <div class="form-group col-md-3 col-sm-6 col-xs-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Admission Batch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" AppendDataBoundItems="true" TabIndex="1" runat="server" CssClass="form-control select2 select-click">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvddlAdmbatch" runat="server"
                                            ControlToValidate="ddlAdmbatch" Display="None" ErrorMessage="Please Select  Intake"
                                            ValidationGroup="Acd" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 col-sm-6 col-xs-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlfaculty" AppendDataBoundItems="true" TabIndex="2" runat="server" CssClass="form-control select2 select-click"
                                          OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlfaculty" Display="None" ErrorMessage="Please Select Faculty /School Name"
                                            ValidationGroup="Acd" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 col-sm-6 col-xs-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Admission Batch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" AppendDataBoundItems="true" TabIndex="3" runat="server" CssClass="form-control select2 select-click">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddldegree" Display="None" ErrorMessage="Please Select Program"
                                            ValidationGroup="Acd" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 col-sm-6 col-xs-12">
                                        <div class="label-dynamic">                                       
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" AppendDataBoundItems="true" runat="server" TabIndex="4" CssClass="form-control select2 select-click">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butShow" runat="server" Text="Show" TabIndex="5" CssClass="btn btn-outline-info" OnClick="butShow_Click"
                                    ValidationGroup="Acd" />

                                <asp:Button ID="butSubmit" CssClass="btn btn-outline-info" TabIndex="6" runat="server" Text="Submit" Width="80px" ValidationGroup="Acd"
                                    OnClick="butSubmit_Click" />

                                <asp:Button ID="butReport" CssClass="btn btn-outline-primary" TabIndex="7" runat="server" Text="Show Report"
                                    ValidationGroup="Acd" OnClick="butReport_Click1" />

                                <asp:Button ID="btnClear" CssClass="btn btn-outline-danger" TabIndex="8" runat="server" OnClick="btnClear_Click"
                                    Text="Clear" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    DisplayMode="List" CssClass="btn btn-outline-info" ValidationGroup="Acd" />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlUpdatePhoto" runat="server">
                                    <asp:ListView ID="lvUpdatePhoto" runat="server">

                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Reg.No  </th>
                                                        <th>Student Name </th>

                                                        <th>Photo </th>
                                                        <th>Update Photo
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">

                                                <td>
                                                    <%#Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <asp:Image ToolTip='<%#Eval("STUDNAME")%>' ID="ImgPhoto" Height="50px" Width="80px"
                                                        runat="server" />
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuStudPhoto" CssClass="btn btn-outline-info" runat="server" />
                                                </td>
                                                <asp:HiddenField ID="hididno" Value='<%#Eval("IDNO")%>' runat="server" />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="butSubmit" />
            <asp:PostBackTrigger ControlID="lvUpdatePhoto" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <%#Eval("REGNO")%>
    <div id="divMsg" runat="server"></div>
</asp:Content>

