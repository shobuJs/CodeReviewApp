<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GradeScheme.aspx.cs" Inherits="ACADEMIC_GradeScheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script>
        $(document).ready(function () {
            debugger;
            var table = $('#mytable1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                searching: false,

                //dom: 'lBfrtip',
                //buttons: [
                //    {
                //        extend: 'colvis',
                //        text: 'Column Visibility',
                //        columns: function (idx, data, node) {
                //            var arr = [0, 4];
                //            if (arr.indexOf(idx) !== -1) {
                //                return false;
                //            } else {
                //                return $('#mytable1').DataTable().column(idx).visible();
                //            }
                //        }
                //    }
                //],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            debugger;
            $(document).ready(function () {
                var table = $('#mytable1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    searching: false,

                    //dom: 'lBfrtip',
                    //buttons: [
                    //    {
                    //        extend: 'colvis',
                    //        text: 'Column Visibility',
                    //        columns: function (idx, data, node) {
                    //            var arr = [0, 4];
                    //            if (arr.indexOf(idx) !== -1)
                    //            {
                    //                return false;
                    //            }
                    //            else
                    //            {
                    //                return $('#mytable1').DataTable().column(idx).visible();
                    //            }
                    //        }
                    //    }
                    //],
                    "bDestroy": true,
                });
            });
        });
    </script>

    <script>

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                alert("Enter Only Numeric Value ");
            return false;
        }
        

        function fn_validateNumeric(thi, dec) {

            if (window.event) keycode = window.event.keyCode;

            else if (e) keycode = e.which;

            else return true;

            if (((keycode >= 65) && (keycode <= 90)) || ((keycode >= 97) && (keycode <= 122)) || (keycode == 32)) {

                return true;
            }

            else {
                alert("Please enter only alphabets");
                return false;
            }
        }


        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        function validate() {
            $('#hfdStat').val($('#switch').prop('checked'));
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });


        $(function () {
            $('#btnSubmit').click(function () {
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });


    </script>

    <%-- Links and styles here --%>
    <style>
        #addGrades {
            font-size: 18px;
        }

            #addGrades:hover {
                color: #0d70fd;
            }
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

        .fa-times {
            color: grey;
        }

            .fa-times:hover {
                color: #f94144;
                cursor: pointer;
            }
    </style>

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeScheme"
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
    </div>--%>

    <%-- <asp:UpdatePanel ID="updGradeScheme" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--  <h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                   <%-- <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item test">
                            <a class="nav-link active" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=1;" href="#tab_1">Grade Scheme</a>
                        </li>
                        <li class="nav-item test2">
                            <a class="nav-link" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=2;" href="#tab_2">Allotment</a>
                        </li>

                    </ul>--%>
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" />
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeScheme"
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

                            <asp:UpdatePanel ID="updGradeScheme" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <%-- Main Selection Options Here --%>
                                                <div class="col-12 mt-4" id="DivAdd" runat="server" visible="true">
                                                    <%--<asp:Button ID="btnAddPolicySlab" runat="server" CssClass="btn btn-outline-info pull-right"  Text="Add Policy Details" OnClick="btnAddPolicySlab_Click" />--%>

                                                    <div class="form-group col-12 text-center">
                                                        <asp:Button ID="btnAddGradeSlab" runat="server" CssClass="btn btn-outline-info" Text="Add Grade Details" OnClick="btnAddGradeSlab_Click" />
                                                    </div>
                                                </div>
                                                <div class="col col-lg-7 col-md-12 col-12">

                                                    <div class="col-12 pl-0 pr-0" id="gradingTable">
                                                        <asp:Panel ID="pnllst" runat="server">
                                                            <asp:ListView ID="lvGradeScheme" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Enter Grade Scheme Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center">SrNo
                                                                                </th>
                                                                                <th style="text-align: center">Grade Name
                                                                                </th>
                                                                                <th style="text-align: center">Min Marks
                                                                                </th>
                                                                                <th style="text-align: center">Max Marks
                                                                                </th>
                                                                                <th style="text-align: center">Grade Point
                                                                                </th>
                                                                                <th style="text-align: center">Fail Grade
                                                                                </th>
                                                                                <th style="display: none">Gradeno
                                                                                </th>
                                                                                <th style="text-align: center">Action
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>

                                                                        <td style="text-align: center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            <asp:HiddenField ID="hfdvalue" runat="server" Value='<%# Eval("ID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGradeName" runat="server" CssClass="form-control" MaxLength="25" Text='<%# Eval("Column1") %>' ToolTip="Please Enter Grade Name" placeholder="Grade Name"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMinMarks" runat="server" CssClass="form-control" MaxLength="15" Text='<%# Eval("Column2") %>' onkeypress="return functionx(event)" ToolTip="Please Enter Min Marks" type="number" placeholder="Min Marks"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMaxMarks" runat="server" CssClass="form-control" MaxLength="15" Text='<%# Eval("Column3") %>' onkeypress="return functionx(event)" ToolTip="Please Enter Max Marks" type="number" placeholder="Max Marks"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGradePoint" runat="server" CssClass="form-control" MaxLength="15" Text='<%# Eval("Column6") %>' onkeypress="return isNumberKey(event,this)" ToolTip="Please Enter Grade Point" placeholder="Grade Point"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="chkFailGrade" runat="server" CssClass="form-control" Checked='<%# Eval("Column4") %>' onkeypress="return functionx(event)" ToolTip="Please Check Uncheck for Fail Grade" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblGradeno" runat="server" Text='<%# Eval("Column5") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/IMAGES/remove.png" CommandArgument='<%#Eval("ID")%>' CommandName='<%# Container.DataItemIndex + 1 %>' OnClick="btnRemove_Click" ToolTip='<%# Eval("Column5") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>


                                                <div class="col col-lg-5 col-md-12 col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblGradeScheme" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:TextBox ID="txtGradeSchemeName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="1"
                                                                ToolTip="Please Enter Grade Scheme Name" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGradeSchemeName"
                                                                Display="None" ErrorMessage="Please Enter Grade Scheme"
                                                                ValidationGroup="submit" />
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12">
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
                                                    <%--  --%>
                                                </div>


                                                <%-- Main Selection Options End --%>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <%--  <asp:LinkButton ID="lnkbtn1" runat="server" ValidationGroup="report" type="button" CssClass="btn btn-outline-primary">New Btn</asp:LinkButton> 
                                <asp:Label ID="lblStatus1" runat="server" SkinID="Errorlbl"></asp:Label>--%>

                                            <asp:LinkButton ID="btnSubmit" runat="server" ToolTip="Submit" ValidationGroup="submit"
                                                CssClass="btn btn-outline-info btnX" TabIndex="10" OnClientClick="return validate()" OnClick="btnSubmit_Click">Submit</asp:LinkButton>

                                            <%--<asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" TabIndex="9" CssClass="btn btn-outline-primary">Submit</asp:LinkButton> <%--data-toggle="modal" data-target="#myModal-feedback" --%>
                                            <asp:Button ID="btnReport" runat="server" Text="Report" Visible="false"
                                                TabIndex="11" CssClass="btn btn-outline-primary" OnClick="btnReport_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                TabIndex="10" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="report" />
                                        </div>

                                        <%-- Table Here --%>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvData" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <%--<th>Grade</th>--%>
                                                                    <th>Scheme Name</th>
                                                                    <%--<th>Max Mark</th>--%>
                                                                    <%--<th>Min Mark</th>--%>
                                                                    <%--<th>DESC GRADE</th>--%>
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("GRADING_SCHEME_NO")%>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" />
                                                            </td>
                                                            <%--<td><%# Eval("GRADE")%></td>--%>
                                                            <td><%# Eval("GRADING_SCHEME_NAME") %> </td>
                                                            <%--<td><%# Eval("MAXMARK")%></td>--%>
                                                            <%--<td><%# Eval("MINMARK")%></td>--%>
                                                            <%--<td><%# Eval("DESC_GRADE")%></td>--%>
                                                            <td>
                                                                <%--  <%# Eval("STATUS")%> </td>--%>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("STATUS").ToString().Equals("1") ? "Active":"Inactive"%>' ForeColor='<%#Eval("STATUS").ToString().Equals("1") ? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <%-- Table End --%>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane" id="tab_2" onclick="document.getElemtbyId('sel-tab').value=2;" visible="false">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updAllotment"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div class="loader-container">
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                            </div>

                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <asp:UpdatePanel ID="updAllotment" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                        AppendDataBoundItems="True" AutoPostBack="true"
                                                        ToolTip="Please Select Faculty/School Name" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                        ValidationGroup="save" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYstudylevel" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStudyLevel" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Study Level" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlStudyLevel"
                                                        Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                                        ValidationGroup="save" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                        AppendDataBoundItems="True" AutoPostBack="true"
                                                        ToolTip="Please Select Program" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0"
                                                        ValidationGroup="save" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxScheme" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control multi-select-demo" SelectionMode="multiple" TabIndex="4" ToolTip="Please Select Curriculum"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvCurriculum" runat="server" ControlToValidate="lstbxScheme"
                                                        Display="None" ErrorMessage="Please Select Curriculum" InitialValue=""
                                                        ValidationGroup="save" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblGradeDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlGradeScheme" runat="server" TabIndex="5" CssClass="form-control select2 select-click"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Grade Name" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvNewSessionName" runat="server" ControlToValidate="ddlGradeScheme"
                                                        Display="None" ErrorMessage="Please Select Grade Name" InitialValue="0" ValidationGroup="save" />
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit"
                                                CssClass="btn btn-outline-info" TabIndex="6" ValidationGroup="save" OnClick="btnSave_Click">Submit</asp:LinkButton>

                                            <asp:Button ID="btnCancelAllotment" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                TabIndex="7" CssClass="btn btn-outline-danger" OnClick="btnCancelAllotment_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="save"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                        <%-- Table Here --%>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvAllotment" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="my_Table">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <%--<th>Edit</th>--%>
                                                                    <th>FACULTY/SCHOOL</th>
                                                                    <th>PROGRAM</th>
                                                                    <th>SCHEME</th>
                                                                    <th>GRADE SCHRME</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <%--<td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("SRNO")%>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click1"/>
                                                            </td>--%>
                                                            <td><%# Eval("COLLEGE_NAME")%></td>
                                                            <td><%# Eval("DEGREENAME") %> </td>

                                                            <%--  <td><%# Eval("SCHEMENAME")%>--%>
                                                            <td>
                                                                <asp:Label ID="lblSchemeNo" runat="server" Text='<%# Eval("SCHEMENAME") %>' ToolTip='<%# Eval("SCHEMENO") %>'></asp:Label>
                                                            </td>
                                                            <td><%# Eval("GRADING_SCHEME_NAME")%></td>
                                                            <td>
                                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("SRNO")%>' ToolTip='<%# Eval("SRNO") %>' ImageUrl="../IMAGES/delete.png" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <%-- Table End --%>
                                    </div>
                                </ContentTemplate>


                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modals Here -->
    <!--== Script for date picker Exam date ==-->



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
    <script>
        $(function () {
            $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').daterangepicker({
                singleDatePicker: true,
                locale: {
                    format: 'DD-MM-YYYY'
                },
            });
            $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').val('');

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(function () {
                $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').daterangepicker({
                    singleDatePicker: true,
                    locale: {
                        format: 'DD-MM-YYYY'
                    },
                });
                //$('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').val('');

            });
        });
    </script>

    <script>
        $(document).ready(function () {

            var table = $('#my_Table').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#my_Table').DataTable().column(idx).visible();

                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#my_Table').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#my_Table').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#my_Table').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                var table = $('#my_Table').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#my_Table').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
    </script>
    <script>

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <%-- Scripts Here -- No Special Requirement --%>
</asp:Content>



