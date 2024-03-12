<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseWise_Registration.aspx.cs" Inherits="CourseWise_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--      <h3 class="box-title">MODULE REGISTRATION REPORTS</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDyReport" runat="server" Font-Bold="true">Report Type</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlReport"
                                            Display="None" ErrorMessage="Please Select Report Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReport"
                                            Display="None" ErrorMessage="Please Select Report Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="PDFShow"></asp:RequiredFieldValidator>
                                        
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup runat="server" id="supsession">* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="PDFShow"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup id="sImp" runat="server">* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="College Name" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show1"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="PDFShow"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup runat="server" id="supProgram">*</sup>
                                            <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0,0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="Show">
                                         </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rvfEnrolldegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Program" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="PDFShow"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup  runat="server" id="supSemester">*</sup>
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true"
                                            ValidationGroup="Show" CssClass="form-control" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester/Year" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup  runat="server" id="supModule">*</sup>
                                            <asp:Label ID="lblDYModuleType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Show" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Subject Type" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup  runat="server" id="supCourse">*</sup>
                                            <asp:Label ID="lblICCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                          
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:ListBox ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                      
                                    </div>

                                </div>
                            </div>
							
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnCoursewiseSubjectlist" runat="server" OnClick="btnCoursewiseSubjectlist_Click1"
                                    ValidationGroup="Show1" CssClass="btn btn-outline-primary" Visible="false">
                                    <i class="fa fa-file-pdf-o" aria-hidden="true" ></i> Subject Wise Subject List Report</asp:LinkButton>

                                <asp:Button ID="btnPdfReport" runat="server" ToolTip="Enrollment Report" Text="Enrollment Report" ValidationGroup="PDFShow"
                                    CssClass="btn btn-outline-primary" OnClick="btnPdfReport_Click" />
                                <asp:Button ID="btnClassList" runat="server" ToolTip="Class List Report" Text="Class List PDF" Visible="false" CssClass="btn btn-outline-primary" OnClick="btnClassList_Click" />

                                  <asp:Button ID="btnPdf" runat="Server" ToolTip="PDF" Text="PDF Report" ValidationGroup="PDFShow"
                                    CssClass="btn btn-outline-primary" OnClick="btnPdf_Click" />

                                <asp:Button ID="btnExcel" runat="Server" ToolTip="Excel" Text="Excel Report" ValidationGroup="Show"
                                    CssClass="btn btn-outline-primary" OnClick="btnExcel_Click" />

                                <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show1" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="PDFShow" />
                            </div>
                          

                            <div class="col-12">
                                <asp:ListView ID="lvStudents" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Students List For Registered Courses</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Regno/EnrollNo
                                                        </th>
                                                        <th>Roll No
                                                        </th>
                                                        <th>Section Name
                                                        </th>
                                                        <th>Student Name
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
                                        <tr id="Tr1" runat="server">
                                            <td>
                                                <%# Eval("UNIQUENO")%>
                                            </td>
                                            <td>
                                                <%# Eval("ROLL_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("SECTIONNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCoursewiseSubjectlist" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnPdfReport" />
            <asp:PostBackTrigger ControlID="btnClassList" />
            <asp:PostBackTrigger ControlID="btnPdf" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <div id="divrpt" runat="server">
    </div>
  
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
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
</asp:Content>
