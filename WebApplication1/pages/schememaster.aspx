<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="schememaster.aspx.cs" Inherits="Academic_schememaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updScheme"
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
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<script type="text/javascript" charset="utf-8">
         $(document).ready(function () {
                 bindDataTable();             
                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
             });

             function bindDataTable() {
                 var myDT = $('#example').DataTable({
                     destroy:true
                 });
             }
             $('#example').dataTable({
                 destroy: true,
                 aaData: response.data
             });

    </script>--%>

    <asp:UpdatePanel ID="updScheme" runat="server" UpdateMode="Conditional">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            ToolTip="Please Select College">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Department">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="chkreport" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblProgram" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlDegreeNo" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegreeNo_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegreeNo"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegreeNo"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="chkreport" />
                                    </div>

                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Department">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="chkreport" />
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            ToolTip="Please Select Specialization">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Specialization"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Specialization"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="chkreport" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label for="city">Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Semester">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>

                                            <asp:Label ID="lblDYYear" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                            ToolTip="Please Select  With Effect from " CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatchNo" runat="server" ErrorMessage="Please Select  With Effect from "
                                            ControlToValidate="ddlYear" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCurriculumType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSchemeType_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchemeType" runat="server" ControlToValidate="ddlSchemeType"
                                            Display="None" ErrorMessage="Please Select Curriculum Type" InitialValue="0"
                                            ValidationGroup="submit" />
                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSchemeType"
                                            Display="None" ErrorMessage="Please Select Curriculum Type" InitialValue="0"
                                            ValidationGroup="chkreport" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%-- <label>Grade Name</label>--%>
                                            <asp:Label ID="lblGradeDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlGradeScheme" runat="server" TabIndex="5" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Grade Name">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvNewSessionName" runat="server" ControlToValidate="ddlGradeScheme"
                                            Display="None" ErrorMessage="Please Select Grade Name" InitialValue="0" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Intake</label>
                                            <%--    <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" TabIndex="5" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Intake" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="submit" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bridging </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal" meta:resourcekey="rdPaymentOptionResource1" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="&nbsp;Yes &nbsp;" Value="1" meta:resourcekey="ListItemResource50"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;No" Value="2" Selected="True" meta:resourcekey="ListItemResource51"></asp:ListItem>
                                        </asp:RadioButtonList>


                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select  Option"
                                            ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Is Latest</label>
                                        </div>
                                        &nbsp;&nbsp;
                                         <asp:CheckBox ID="ChkIsLatest" runat="server" AutoPostBack="True" />

                                    </div>
                                    <div class="form-group col-lg-8 col-md-6 col-12" runat="server" visible="false" id="divCurricu">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Curriculum Name</asp:Label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtCurricu" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCurricu"
                                            Display="None" ErrorMessage="Please Enter Curriculum Name" InitialValue="" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="yearname" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true">Year Name</asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlyearName" runat="server" TabIndex="5" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Year" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlyearName"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Grade Marks </label>
                                        </div>
                                        <asp:DropDownList ID="ddlgrademarks" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" ToolTip="Please Select Pattern name">

                                            <asp:ListItem Value="G">Grade</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvgrademarks" runat="server" ControlToValidate="ddlgrademarks" Display="None" ErrorMessage="Please Select Grade/Marks" InitialValue="0" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Pattern Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPatternName" runat="server" AppendDataBoundItems="true" AutoPostBack="false"
                                            ToolTip="Please Select Pattern name" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPatternName" runat="server" ControlToValidate="ddlPatternName"
                                            Display="None" ErrorMessage="Please Select Pattern Name" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" OnClientClick="return isLatestChk();" />

                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                    Text="Report" CssClass="btn btn-outline-primary" Enabled="False" Visible="false" />
                                <asp:Button ID="btnCheckListReport" runat="server" OnClick="btnCheckListReport_Click" ValidationGroup="chkreport"
                                    Text="Check List Report" CssClass="btn btn-outline-primary" Visible="false" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="chkreport" />
                            </div>

                            <div class="col-12">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl" Visible="false"></asp:Label>
                            </div>

                            <%--<div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                     <table id="example" class="table table-striped table-bordered" style="width:100%">
                                    <asp:Repeater ID="lvScheme" runat="server">
                                        <HeaderTemplate>
                                            <div id="demo-grid">
                                                <h4>CURRICULUM LIST</h4>
                                            </div>
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Curriculum Name
                                                        </th>
                                                        <th>Branch Name
                                                        </th>
                                                        <th>Batch
                                                        </th>
                                                        <th>Pattern Name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SchemeNo") %>'
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>

                                                    <asp:Label ID="lblscheme" runat="server" Text='<%#Eval("SCHEMENAME")%>'></asp:Label>
                                                    
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbllname" runat="server" Text='<%# Eval("LONGNAME")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbatch" runat="server" Text='<%# Eval("BATCH")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpattname" runat="server" Text='<%# Eval("PATTERN_NAME")%>'></asp:Label>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                         </table>
                                </asp:Panel>
                            </div>--%>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvScheme" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Subject List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" id="scheme-data" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Curriculum Name
                                                        </th>
                                                        <th>Program 
                                                        </th>
                                                        <th>Grade Scheme
                                                        </th>
                                                        <th>Year Name</th>
                                                        <th>Is Latest</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SchemeNo") %>'
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>

                                                    <asp:Label ID="lblscheme" runat="server" Text='<%#Eval("SCHEMENAME")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbllname" runat="server" Text='<%# Eval("PROGRAM")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeName" runat="server" Text='<%# Eval("GRADING_SCHEME_NAME")%>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblyearname" runat="server" Text='<%# Eval("YEARNAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblbridging" Font-Bold="true" runat="server" ForeColor='<%# Eval("IS_LATEST").ToString()=="Yes"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("IS_LATEST")%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({

            });
        }

    </script>--%>
    <script>
        function isLatestChk() {
            var ret = true;

            if ($('#ctl00_ContentPlaceHolder1_ChkIsLatest').prop('checked') == true) {

                ret = confirm('Do you want to set this Curriculum Latest!!!');
            }
            return ret;
        }
    </script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            bindDataTable();
            $.noConflict();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        //function bindDataTable() {
        //    var myDT = $('#example').DataTable({
        //        "bDestroy": true,
        //    });
        //}    
    </script>
</asp:Content>
