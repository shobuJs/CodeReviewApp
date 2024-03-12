<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Advising_Details.aspx.cs" Inherits="ACADEMIC_Advising_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server">Advising Details</asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-5 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblStudName" runat="server"></asp:Label>
                                    </b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindStudentName" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblStudentId" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindStdID" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Date Of Birth </b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindDOB" runat="server" Font-Bold="true"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblGender" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindGender" runat="server" Font-Bold="true"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblMobile" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindMobNo" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblStudentEmail" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindEmailID" runat="server" Font-Bold="true"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-5 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDyProgram" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindProgram" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYSemester" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindSem" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYPreviousSchool" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindPreviousSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYOldCurriculum" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindOldCurriculum" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYNewCurriculum" runat="server"></asp:Label></b>
                                        <a class="sub-label">
                                            <asp:Label ID="BindNewCurriculum" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-2 col-md-6 col-12 mt-2 text-center">
                                <asp:Image ID="imgPhoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Style="width: 110px; height: auto;" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 mt-4" runat="server" id="DivExempted">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Credited </h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYOldCurriculumSubject" runat="server" Font-Bold="true"></asp:Label>

                                </div>
                                <asp:DropDownList ID="ddlOldCurriculumSubject" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOldCurriculumSubject"
                                    Display="None" ErrorMessage="Please Select Old Curriculum Subject" ValidationGroup="AddCourse"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYNewCurriculumSubject" runat="server" Font-Bold="true"></asp:Label>

                                </div>
                                <asp:DropDownList ID="ddlNewCurriculumSubject" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNewCurriculumSubject"
                                    Display="None" ErrorMessage="Please Select New Curriculum Subject" ValidationGroup="AddCourse"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-3 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true">Mapping Status</asp:Label>

                                </div>
                                <%--<asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Exempted</asp:ListItem>
                                    <asp:ListItem Value="2">Not Exempted</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMappingStatus"
                                    Display="None" ErrorMessage="Please Select Mapping Status" ValidationGroup="AddCourse"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-1 col-md-1 col-12">
                                <asp:LinkButton runat="server" ID="btnAddSubject" ValidationGroup="AddCourse" class="fa fa-plus text-success border border-success rounded p-2 mt-3" OnClick="btnAddSubject_Click"></asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddCourse"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <%-- <div class="col-12 table-responsive">
                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Old Curriculum Subject</th>
                                            <th>New Curriculum Subject</th>
                                            <th>Mapping Status</th>
                                            <th>Add</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlOldCulum" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewCulum" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%--       <asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Exempted</asp:ListItem>
                                                    <asp:ListItem Value="2">Not Exempted</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btnAddSubject" CssClass="fa fa-plus text-success border border-success rounded p-2"></asp:LinkButton>
                                                <%--<asp:LinkButton runat="server" ID="LinkButton1" OnClick="btnAddSubject_Click" ValidationGroup="AddCourse" class="fa fa-plus text-success border border-success rounded p-2 mt-3"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>

                       <%--     <div class="col-12 mt-3 table-responsive">
                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Old Curriculum Subject</th>
                                            <th>New Curriculum Subject</th>
                                            <th>Mapping Status</th>
                                            <th>Remove</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Music-003-Applied Music </td>
                                            <td>Music-001-Analysis of Tonal Music</td>
                                            <td>Exempted</td>
                                            <td><i class="fa fa-close text-danger" style="font-size: 25px;"></i></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>


                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvExemptedfromStudy" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading" id="dem">
                                                    <h5>
                                                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="lblDYOldCurriculumSubject" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblDYNewCurriculumSubject" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblDYMappingStatus" runat="server">Mapping Status</asp:Label></th>
                                                            <%--<th>
                                                                <asp:Label ID="lblGradeNew" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblCurriculumSubject" runat="server"></asp:Label></th>--%>
                                                            <th>
                                                                <asp:Label ID="lblDyRemove" runat="server">Remove</asp:Label></th>
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
                                                    <asp:Label runat="server" ID="lblOldCurriculumSubject" Text='<%# Eval("PSubjectName")%>'></asp:Label>
                                                    <asp:Label runat="server" ID="lblOldCourseNo" Text='<%# Eval("PCourseNo")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblNewCurriculumSubject" Text='<%# Eval("NSubjectName")%>'></asp:Label>
                                                    <asp:Label runat="server" ID="lblNewCourseNo" Text='<%# Eval("NCourseNo")%>' Visible="false"></asp:Label>
                                                </td>

                                                <%--<td>
                                                    <asp:Label runat="server" ID="lblpUnit" Text='<%# Eval("PUnits")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblpGrade" Text='<%# Eval("PGrade")%>'></asp:Label>
                                                </td>--%>

                                                <td>
                                                    <asp:Label runat="server" ID="lblMappingStatus" Text='<%# Eval("MappingStatus")%>'></asp:Label>
                                                    <asp:Label runat="server" ID="lblMappingID" Text='<%# Eval("MappingID")%>' Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lblPrev_No" Text='<%# Eval("PEquSubNo")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_Click" CommandArgument='<%#Eval("PCourseNo") %>' CommandName='<%#Eval("PEquSubNo")%>' OnClientClick="return confirm('Are you sure you want to delete Exempted Subject?');"><i class="fa fa-close" style="font-size:25px;color:red"></i></asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>




                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="0" OnClick="btnSubmit_Click" ValidationGroup="Submit">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btncanceldata" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btncanceldata_Click">Cancel</asp:LinkButton>

                         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

