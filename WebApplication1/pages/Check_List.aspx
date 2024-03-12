<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Check_List.aspx.cs" Inherits="MockUps_Check_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlChecklist .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdCheckList"
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
    <asp:UpdatePanel runat="server" ID="UpdCheckList">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1">Enlistment</a>
                                    </li>
                                    <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Subject Wise</a>
                            </li>--%>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                    <div class="form-group">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblRASession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="ddlAcademicSession" Display="None" SetFocusOnError="true"
                                                            ErrorMessage="Please Select Academic Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                    <div class="form-group">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlSemester" Display="None" SetFocusOnError="true"
                                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Buttons -->
                                            <div class="text-center mt-2 mb-3">
                                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-primary" ValidationGroup="Show" OnClick="btnShow_Click" TabIndex="1">Show</asp:LinkButton>
                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="1">Cancel</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="pnlChecklist" runat="server" Visible="false">
                                                <asp:ListView ID="lvCheckList" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                                            <%--<h5>Check List</h5>--%>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>

                                                                    <th><asp:Label ID="lblDYCollege" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblDyProgram" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblAdmittedCount" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblCurriculumStatus" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblSubjectOffering" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblSectionMappingWithProgram" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblSectionOffering" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblUnitWiseFees" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblSubjectWiseFeeLab" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblStandardFeeDefine" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblInstallmentConfiguration" runat="server"></asp:Label></th>
                                                                    <th><asp:Label ID="lblClassSchedule" runat="server"></asp:Label></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>

                                                            <td><%# Eval("COLLEGE") %></td>
                                                            <td><%# Eval("PROGRAM") %></td>
                                                            <td><%# Eval("ADMITTED_COUNT") %></td>
                                                            <td><%# Eval("CURRICULUM_STATUS") %></td>
                                                            <td><%# Eval("SUBJECT_OFFERING") %></td>
                                                            <td><%# Eval("SECTION_MAPPING_WITH_PROGRAM") %></td>
                                                            <td><%# Eval("SECTION_OFFERING") %></td>
                                                            <td><%# Eval("UNIT_WISE_FEES") %></td>
                                                            <td><%# Eval("SUBJECT_WISE_FEE_LAB_TYPE") %></td>
                                                            <td><%# Eval("STANDARD_FEE_DEFINE") %></td>
                                                            <td><%# Eval("INSTALLMENT_CONFIGURATION") %></td>
                                                            <td><%# Eval("CLASS_SCHEDULE") %></td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <%--<div class="tab-pane fade" id="tab_2">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                            <div class="form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label><span>Academic Session</span></label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan-June 2023</asp:ListItem>
                                                    <asp:ListItem Value="2">Jan-June 2022</asp:ListItem>
                                                    <asp:ListItem Value="3">July-Dec 2022</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                              
                                    <div class="text-center mt-2 mb-3">
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Show" id="Button1" />
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit" id="Button2" />
                                        <input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="Button3" />
                                    </div>
                                </div>

                                <div class="col-12 mt-3">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered nowrap table-simple">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkbox" runat="server" /></th>
                                                    <th>Student ID</th>
                                                    <th>Student Name</th>
                                                    <th>College</th>
                                                    <th>Program</th>
                                                    <th>Block Section</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                    <td>ST00001</td>
                                                    <td>John Smith</td>
                                                    <td>College Of Science</td>
                                                    <td>Program</td>
                                                    <td>A</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

