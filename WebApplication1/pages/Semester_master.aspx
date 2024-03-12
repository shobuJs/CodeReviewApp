<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Semester_master.aspx.cs" Inherits="ACADEMIC_Semester_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
            height: 0;
            width: 0;
            visibility: hidden;
        }

        .switch label {
            cursor: pointer;
            width: 82px;
            height: 34px;
            background: #dc3545;
            display: block;
            border-radius: 4px;
            position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch label:hover {
                background-color: #c82333;
            }

            .switch label:before {
                content: attr(data-off);
                position: absolute;
                right: 0;
                font-size: 16px;
                padding: 4px 8px;
                font-weight: 400;
                color: #fff;
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch input:checked + label {
            background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch input:checked + label:hover {
                background: #218838;
            }

            .switch input:checked + label:after {
                transform: translateX(68px);
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSem"
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
    <asp:UpdatePanel runat="server" ID="updSem">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lab" runat="server" Font-Bold="true">Semester Name</asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtsemname" runat="server" TabIndex="1" CssClass="form-control"
                                            ToolTip="Please Enter Semester Name" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtsemname"
                                            Display="None" ErrorMessage="Please Enter Semester Name"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Semester Full Name</asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtsemfullname" runat="server" TabIndex="1" CssClass="form-control"
                                            ToolTip="Please Enter Semester Full Name" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsemfullname"
                                            Display="None" ErrorMessage="Please  Enter Semester Full Name"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true">Curriculum Type</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlcurrtype" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Curriculum Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcurrtype"
                                            Display="None" ErrorMessage="Please Select Curriculum Type" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-2 col-md-3 col-6">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="submit"
                                        CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="1" ClientIDMode="Static">Submit</asp:LinkButton>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="1" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" >
                                        <asp:ListView ID="lvSem" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Semester List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action </th>
                                                            <th>Sr.No. </th>
                                                            <th>Semester Name </th>
                                                            <th>Semester Full Name</th>
                                                            <th>Curriculum Type</th>
                                                            <th>Status</th>                                                           
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("SEMESTERNO") %>' AlternateText="Edit Record"
                                                             OnClick="btnEdit_Click"/>
                                                    </td>                                                  
                                                    <td>
                                                        <%# Container.DataItemIndex + 1 %>
                                                                              
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%> 
                                                    </td>
                                                   
                                                    <td>
                                                        <%# Eval("SEMFULLNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SCHEMETYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Active")%>
                                                    </td>
                                                   
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>


                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetStat(val) {

            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {

            $('#btnSave').click(function () {

                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                //ShowLoader('#btnSave');


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    // ShowLoader('#btnSave');


                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                });
            });
        });
    </script>
</asp:Content>

