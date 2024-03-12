<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApproveScholarship.aspx.cs" Inherits="ACADEMIC_ApproveScholarship" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updScholarship" DynamicLayout="true" DisplayAfter="0">
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
    <asp:UpdatePanel ID="updScholarship" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Academic Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcademicSession" runat="server" CssClass="form-control" Enabled="false" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="ddlAcademicSession"
                                                Display="None" ErrorMessage="Please Select Academic Session" ValidationGroup="submit" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Student ID</label>
                                            </div>
                                            <asp:TextBox ID="txtStdID" runat="server" CssClass="form-control" Enabled="false" MaxLength="20" TabIndex="1" placeholder="Search Student"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStdID"
                                                Display="None" ErrorMessage="Please Enter Student ID" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divShowDetails" runat="server" visible="false">
                                    <div class="col-12 mt-3 mb-4">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Academic Session :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Program :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblProgram" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester/ Year :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblsem" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-3 offset-lg-3 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered border rounded p-2">
                                                    <li class="list-group-item border-0 p-1"><b>Total Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblTotalFees" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item border-0 p-1"><b>Lecture Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLectureFees" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hfdLectureFees" runat="server" Value="0" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item border-0 p-1"><b>Lab Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLabFees" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hdfLabFees" runat="server" Value="0" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item border-0 p-1"><b>Mis. Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMisFees" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hdfMisFees" runat="server" Value="0" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item border-0 p-1"><b>Paid Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPaidFees" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:ListView ID="lvScholarshipAmount" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Scholarship/Discount Type</th>
                                                            <th>Calculation Type</th>
                                                            <th>Calculation On</th>
                                                            <th>Value</th>
                                                            <th>Scholarship/Discount Amount</th>
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
                                                        <asp:HiddenField ID="hdfSrno" runat="server" Value='<%#Eval("id") %>' />
                                                        <asp:DropDownList ID="ddlScholarshipType" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true" TabIndex='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCalculationType" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true" TabIndex='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCalculation" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true" TabIndex='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdfRemainingLabAmount" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfRemainingLectureAmount" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfRemainingMiscAmount" runat="server" Value="0" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtValue" runat="server" MaxLength="10" CssClass="form-control" Enabled="false" TabIndex='<%# Container.DataItemIndex + 1 %>' placeholder="e.g. 10% / 20,000"></asp:TextBox>
                                                        <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender4" runat="server" targetcontrolid="txtValue"
                                                            validchars="1234567890." filtermode="ValidChars" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtScholarshipAmt" runat="server" CssClass="form-control" MaxLength="10" TabIndex="1" placeholder="e.g. 50,000" Enabled="false"></asp:TextBox>
                                                        <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server" targetcontrolid="txtScholarshipAmt"
                                                            validchars="1234567890." filtermode="ValidChars" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col-12 col-sm-6 col-lg-3 mt-3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scholarship Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlApproveType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Approve</asp:ListItem>
                                            <asp:ListItem Value="2">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlApproveType"
                                        Display="None" ErrorMessage="Please Select Scholarship Status" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnSave_Click" ValidationGroup="submit">Submit</asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvBindDetails" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SrNo</th>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                            <th>Semester</th>
                                                            <th>Scholarship Type</th>
                                                            <th>Calculation Type</th>
                                                            <th>Calculation On</th>
                                                            <th>Value</th>
                                                            <th>Scholarship Amount</th>
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
                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                    <td><%#Eval("ENROLLNO") %></td>
                                                    <td><%#Eval("STUDNAME") %></td>
                                                    <td><%#Eval("SEMESTERNAME") %></td>
                                                    <td><%#Eval("CONCESSION_TYPE") %></td>
                                                    <td><%#Eval("TYPENAME") %></td>
                                                    <td><%#Eval("CALCULATION_NAME") %></td>
                                                    <td><%#Eval("AMOUNT_PER") %></td>
                                                    <td><%#Eval("SCHOLARSHIP_AMOUNT") %></td>
                                                    <td><%#Eval("SCHOLARSHIP_STATUS") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

