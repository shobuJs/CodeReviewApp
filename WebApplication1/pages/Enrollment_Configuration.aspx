<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Enrollment_Configuration.aspx.cs" Inherits="Enrollment_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Enrollment Configuration</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Intake</label>
                                </div>
                                <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Study Level</label>
                                </div>
                                <asp:ListBox ID="level" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Program</label>
                                </div>
                                <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-info">Show</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                            </div>

                            <div class="col-12 mt-3">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Application ID</th>
                                            <th>Applicant Name</th>
                                            <th>Program</th>
                                            <th>Aptitude Score</th>
                                            <th>Eligibility Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Andrew Mendis</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-primary">Selected</span></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Rahul Patel</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-danger">Rejected</span></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Rahul Patel</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-success">Enrolled</span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>

</asp:Content>

