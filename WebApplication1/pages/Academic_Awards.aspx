<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Academic_Awards.aspx.cs" Inherits="Academic_Awards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<style>
        .table-striped tbody table tbody tr:nth-of-type(odd),
        .table-striped tbody table tbody tr:nth-of-type(even) {
            background-color: transparent !important;
            border-bottom: 0px solid black !important;
        }

        .table-bordered tbody table tbody td, .table-bordered tbody table tbody th {
            border: 0px solid #dee2e6;
        }

        .list-group .list-group-item .sub-label {
            float: none;
            margin-left: 1rem;
        }
    </style>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Academic Awards </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Award Title</label>
                                </div>

                                <asp:DropDownList ID="ddlAwardTitle" runat="server" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAwardTitle_SelectedIndexChanged" AutoPostBack="true" TabIndex="18">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnAwardId" runat="server" Value="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session</label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Semester</label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>

                                <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-outline-info" OnClick="btnView_Click">View Rules</asp:LinkButton>

                                <%-- <asp:LinkButton ID="btnView" runat="server" class="fa fa-eye" Style="color: #28a745; font-size: 20px;" aria-hidden="true"  
                                         OnClick="btnView_Click">View Rules</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-outline-primary" Visible="false">Report</asp:LinkButton>
                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShow_Click">Show</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                    </div>

                    <div class="col-md-12">
                        <div id="divapti" runat="server">
                            <div class="sub-heading" id="dem">
                                                    <h5>Academic Awards List</h5>
                                                </div>
                            <asp:Panel ID="Panel5" runat="server">
                                <asp:ListView ID="lvAcdAward" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SRNO </th>
                                                        <th>Faculty</th>
                                                        <th>Program</th>
                                                        <th>RegNo</th>
                                                        <th>Student name</th>
                                                        <th>Semester</th>
                                                        <th>WGPA</th>

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
                                            <td><%#Container.DataItemIndex+1 %> </td>
                                            <td><%# Eval("Faculty") %>  </td>
                                            <td><%# Eval("PROGRAM") %></td>
                                            <td><%# Eval("RegNo") %></td>
                                            <td><%# Eval("Student_Name") %></td>
                                            <td><%# Eval("Semester") %></td>
                                            <td><%# Eval("WGPA") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                    </div>

                </div>

                <!-- The Modal -->
                <div class="modal fade" id="myModal_view">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title">View Rules</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="form-group col-lg-12 col-md-12 col-12 mt-2 table table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td>Consider Postponement Student </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider Students who were not graduated in their Regular Batch </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider IC Status in any module </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider students with Repeat attempts </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider highest WGPA in relevant Specialization </td>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Enabled="true"> 1st &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0" Enabled="true"> 2nd </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            ShowPUP();
        });
    </script>

    <script type="text/javascript">

        function ShowPUP() {
            debugger
            //var myval = document.getElementById(ctl00_ContentPlaceHolder1_hdnAwardId).value; //document.getElementById('ctl00_ContentPlaceHolder1_hdnAwardId').va;
            //var myval = $("#hdnAwardId").val();


            var myval = $("#ctl00_ContentPlaceHolder1_hdnAwardId").val();

            //alert(myval)
            if (myval != 0) {

                $('#myModal_view').modal('show');
                $('#myModal_view').fadeIn();
            }
            else {
                $('#myModal_view').modal('hide');
            }

        }
    </script>

</asp:Content>

