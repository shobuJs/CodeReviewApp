<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Aptitute_vise_students.aspx.cs" Inherits="ACADEMIC_Aptitute_vise_students" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        .dataTables_scrollHeadInner {
  width: max-content !important;
}
    </style>
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <asp:UpdatePanel ID="updCourseCreation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item active" id="divaplied" runat="server" visible="false">
                                    <asp:LinkButton ID="lkapplied" runat="server" OnClick="lkapplied_Click" CssClass="nav-link" TabIndex="1">Total Applied</asp:LinkButton></li>
                                <li class="nav-item" id="divreport" runat="server"  visible="false">
                                    <asp:LinkButton ID="lkreport" runat="server" OnClick="lkreport_Click" CssClass="nav-link" TabIndex="2">Enrolled Report</asp:LinkButton></li>
                                <li class="nav-item" id="divHigher" runat="server">
                                    <asp:LinkButton ID="btnHithersemster" runat="server" OnClick="btnHithersemster_Click" CssClass="nav-link" TabIndex="2">Higher Semester</asp:LinkButton></li>
                            </ul>
                        </div>
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="divapliedtab" role="tabpanel" runat="server" aria-labelledby="TotalAplied-tab">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updaplied"
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

                                <asp:UpdatePanel ID="updaplied" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Total Applied Student</h5>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlintake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Intake" OnSelectedIndexChanged="ddlintake_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <asp:RadioButtonList ID="rdbFilter" runat="server" RepeatColumns="8" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="1"><span style="padding-left:5px">Aptitute Center List</span></asp:ListItem>
                                                                    <asp:ListItem Value="2"><span style="padding-left:5px">Total Applied </span></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12" id="divapti" runat="server" visible="false">
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <asp:ListView ID="lvapti" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Aptitute Center List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <%-- <th style="text-align: center">
                                                            <asp:CheckBox ID="chkallot" runat="server" OnClick="checkbox(this)" />
                                                        </th>--%>
                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Application No.</th>
                                                                                    <th style="text-align: center">First Name</th>
                                                                                    <th style="text-align: center">Last Name</th>
                                                                                    <th style="text-align: center">Emailid</th>
                                                                                    <th style="text-align: center">Mobile No.</th>
                                                                                    <th style="text-align: center">Center</th>
                                                                                    <th style="text-align: center">Intake</th>
                                                                                    <th style="text-align: center">Mode</th>
                                                                                    <th style="text-align: center">Medium</th>
                                                                                    <th style="text-align: center">Program</th>
                                                                                    <th style="text-align: center">Fees Status</th>
                                                                                    <th style="text-align: center">Marks</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%-- <td style="text-align: center">
                                                    <asp:CheckBox ID="chkallotment" runat="server" ToolTip='<%# Eval("USERNO")%>' /></td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1 %>                                                 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("USERNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("FIRSTNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("LASTNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("EMAILID")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("MOBILENO")%> 
                                                                            </td>
                                                                            <td>

                                                                                <%# Eval("APTITUDE_CENTER_NAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%--  <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME") %>' ToolTip='<%# Eval("OLDINTAKE") %>'></asp:Label>--%>
                                                                                <%# Eval("BATCHNAME")%>                                                                                                      
                                                                            </td>
                                                                            <td>

                                                                                <%# Eval("MODE")%> 
                                                                            </td>
                                                                            <td>

                                                                                <%# Eval("MEDIUM_NAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PROGRAM")%> 
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("FEES_STATUS")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TOTAL_MARKS")%> 
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-md-12" id="divtotalapplied" runat="server" visible="false">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvtotalaplied" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Total Applied  List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <%-- <th style="text-align: center">
                                                            <asp:CheckBox ID="chkallot" runat="server" OnClick="checkbox(this)" />
                                                        </th>--%>
                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Total Applied  </th>
                                                                                    <th style="text-align: center">Total Qualified  </th>
                                                                                    <th style="text-align: center">Total Program Accepted  </th>
                                                                                    <th style="text-align: center">Total Enrollment Payment Paid  </th>
                                                                                    <th style="text-align: center">Total Enrolled  </th>
                                                                                    <th style="text-align: center">Intake</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%-- <td style="text-align: center">
                                                    <asp:CheckBox ID="chkallotment" runat="server" ToolTip='<%# Eval("USERNO")%>' /></td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1 %>                                                 
                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <%# Eval("TOTALAPPLIED")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("TOTALQUALIFIED")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("PRGMAPPLIED")%>                                                                                                     
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("TOTALENROLLPAID")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("TOTALENROLLED")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("BATCHNAME")%> 
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="divreporttab" runat="server" visible="false" role="tabpanel" aria-labelledby="Report-tab">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updreport"
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
                                <asp:UpdatePanel ID="updreport" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Summary Report</h5>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddladmbatch" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Intake">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="requiredfieldvalidator2" runat="server" ControlToValidate="ddladmbatch"
                                                                    Display="none" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                    ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlcollege" runat="server" TabIndex="2" CssClass="form-control select2 select-click" AutoPostBack="true"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Faculty/School Name" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="requiredfieldvalidator1" runat="server" ControlToValidate="ddlcollege"
                                                                    Display="none" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                                    ValidationGroup="submit" />--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlprogram" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Program">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="requiredfieldvalidator3" runat="server" ControlToValidate="ddlprogram"
                                                                    Display="none" ErrorMessage="Please Select Program" InitialValue="0"
                                                                    ValidationGroup="submit" />--%>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnshow" runat="server" ToolTip="Show"
                                                                CssClass="btn btn-outline-info" OnClick="btnshow_Click" TabIndex="4" ValidationGroup="submit">Show</asp:LinkButton>
                                                             <asp:LinkButton ID="btnTransferExcel" runat="server" ToolTip="Report"
                                                                CssClass="btn btn-outline-info" TabIndex="4" ValidationGroup="submit" OnClick="btnTransferExcel_Click">Transfer Excel</asp:LinkButton>

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOldIntake" runat="server" visible="false">
                                                            <asp:Label ID="lblIntakeOld" runat="server" Font-Bold="true">Total Count : </asp:Label>
                                                            <asp:Label ID="lblIntakeOldCount" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divNewIntake" runat="server" visible="false">
                                                            <asp:Label ID="lblIntakeNew" runat="server" Font-Bold="true">Transfer Count : </asp:Label>
                                                            <asp:Label ID="lblIntakeNewCount" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" id="divreportlv" runat="server" visible="false">
                                                            <asp:Panel ID="Panel3" runat="server">
                                                                <asp:ListView ID="lvreport" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Summary Report List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>

                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Application No.</th>
                                                                                    <th style="text-align: center">Enrolled No.</th>

                                                                                     <th style="text-align: center">RFID.</th>

                                                                                    <th style="text-align: center">Entered By</th>
                                                                                    <th style="text-align: center">Registration Status</th>

                                                                                    <th style="text-align: center">Name with Intital</th>
                                                                                    <th style="text-align: center">Student Name</th>
                                                                                    <th style="text-align: center">Gender</th>
                                                                                    <th style="text-align: center">Father Name</th>
                                                                                    <th style="text-align: center">Mother Name</th>
                                                                                    <th style="text-align: center">NIC</th>
                                                                                    <th style="text-align: center">Passport</th>

                                                                                    <th style="text-align: center">Emailid</th>
                                                                                    <th style="text-align: center">College Email</th>

                                                                                    <th style="text-align: center">Mobile No.</th>
                                                                                    <th style="text-align: center">Faculty/School</th>
                                                                                    <th style="text-align: center">Semester</th>
                                                                                    <th style="text-align: center">Program/Specialization</th>
                                                                                    <th style="text-align: center">Intake</th>
                                                                                    <th style="text-align: center">Permanent Address</th>
                                                                                    <th style="text-align: center">Local Address</th>
                                                                                    <th style="text-align: center">Country</th>
                                                                                    <th style="text-align: center">State</th>
                                                                                    <th style="text-align: center">District</th>

                                                                                    <th style="text-align: center">Aptitute Center Name</th>
                                                                                    <th style="text-align: center">Preferred Weekday/Weekend</th>
                                                                                    <th style="text-align: center">Campus Name</th>
                                                                                    <th style="text-align: center">Enrolled Weekday/Weekend</th>
                                                                                    <%--                                                                                    <th style="text-align: center">Amount</th>
                                                                                    <th style="text-align: center">Payment Type</th>
                                                                                    <th style="text-align: center">Payment Date</th>
                                                                                    <th style="text-align: center">Mode</th>--%>

                                                                                    <th style="text-align: center">Enrolled Date</th>

                                                                                    <th style="text-align: center">Medium</th>
                                                                                    <th>UG A/L syllabus</th>
                                                                                    <th>UG Sream</th>
                                                                                    <th>UG Attempt</th>
                                                                                    <th>Subject 1</th>
                                                                                    <th>Grade 1</th>
                                                                                    <th>Subject 2</th>
                                                                                    <th>Grade 2</th>
                                                                                    <th>Subject 3 </th>
                                                                                    <th>Grade 3</th>
                                                                                    <th>Subject 4</th>
                                                                                    <th>Grade 4</th>
                                                                                    <th>A/L year</th>
                                                                                    <th>A/L index number</th>
                                                                                    <th>PG Qualification Name</th>
                                                                                    <th>PG University/Institute with Country</th>
                                                                                    <th>PG Year of Award</th>
                                                                                    <th>PG Main Specialty/ Field</th>
                                                                                    <th>PG Class/GPA</th>
                                                                                    <th>PG Professional Qualification</th>
                                                                                    <th>PG University/Institute</th>
                                                                                    <th>PG Qualification Award of Date</th>
                                                                                    <th>PG Specilization of Qualification</th>
                                                                                    <th>Awarding Institute</th>
                                                                                    <th>Total Amount</th>
                                                                                    <th>Paid Amount</th>
                                                                                    <th>AL Verification</th>
                                                                                    <%--<th>PDP O/L Syllabus</th>
                                                                <th>PDP Index No.</th>
                                                                <th>PDP Medium</th>
                                                                <th>PDP Attempt</th>
                                                                <th>PDP O/L Results</th>
                                                                <th>PDP Institute</th>
                                                                <th>PDP Registration Number</th>
                                                                <th>PDP Name of the Programme</th>
                                                                <th>PDP Stream</th>
                                                                <th>PDP Grade Point Average</th>
                                                                <th>PDP Subject Results</th>--%>
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
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("REGNO")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ENROLLNO")%> 
                                                                            </td>
                                                                               <td>
                                                                                <%# Eval("RF_ID")%>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("UA_FULLNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ENTERED_BY_STATUS")%>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("STUDFIRSTNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("STUDNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("GENDER")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("FATHERNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("MOTHERNAME")%> 
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("NICNO")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PASSPORTNO")%> 
                                                                            </td>


                                                                            <td>
                                                                                <%# Eval("EMAILID")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COLLEGE_EMAIL")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("STUDENTMOBILE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("COLLEGE_NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("SEMESTERNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PROGRAMNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%--  <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME") %>' ToolTip='<%# Eval("OLDINTAKE") %>'></asp:Label>--%>
                                                                                <%# Eval("BATCHNAME")%>                                                                                                      
                                                                            </td>
                                                                            <td><%# Eval("PADDRESS")%> </td>
                                                                            <td><%# Eval("LADDRESS")%> </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("COUNTRYNAME")%> </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("STATENAME")%> </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("DISTRICTNAME")%> </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("APTITUDE_CENTER_NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("PREFERRED_WEEKDAYSNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("CAMPUSNAME")%> 
                                                                            </td>

                                                                                <td style="text-align: center">
                                                                                <%# Eval("WEEKDAYSNAME")%> 
                                                                            </td>

                                                                            <%--  <td style="text-align: center">
                                                                                <%# Eval("TOTAL_AMT")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("PAYTYPE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("RECONDATE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("MODE_NAME")%> 
                                                                            </td>--%>

                                                                            <td style="text-align: center">
                                                                                <%# Eval("APPROVED_DATE")%> 
                                                                            </td>


                                                                            <td style="text-align: center">
                                                                                <%# Eval("MEDIUM_NAME")%> 
                                                                            </td>
                                                                            <td><%# Eval("AL_TYPE")%> </td>
                                                                            <td><%# Eval("STREAMNAME")%> </td>
                                                                            <td><%# Eval("ATTEMPTS")%> </td>
                                                                            <td><%# Eval("SUBJECT1")%> </td>
                                                                            <td><%# Eval("GRADE1")%> </td>
                                                                            <td><%# Eval("SUBJECT2")%> </td>
                                                                            <td><%# Eval("GRADE2")%> </td>
                                                                            <td><%# Eval("SUBJECT3")%> </td>
                                                                            <td><%# Eval("GRADE3")%> </td>
                                                                            <td><%# Eval("SUBJECT4")%> </td>
                                                                            <td><%# Eval("GRADE4")%> </td>
                                                                            <td><%# Eval("ALYEAR")%> </td>
                                                                            <td><%# Eval("ALINDEXNO")%> </td>
                                                                            <td><%# Eval("PG_NAME_OF_QUALIFICATION")%> </td>
                                                                            <td><%# Eval("PG_UNIVERSITY")%> </td>
                                                                            <td><%# Eval("PG_YEAR_OF_AWARD")%> </td>
                                                                            <td><%# Eval("PG_MAIN_SPECIALTY")%> </td>
                                                                            <td><%# Eval("PG_GPA")%> </td>
                                                                            <td><%# Eval("PG_PROFESSIONAL_QUALIFICATION")%> </td>
                                                                            <td><%# Eval("PG_PROFESSIONAL_INSTITUTE")%> </td>
                                                                            <td><%# Eval("PG_QUALIFICATION_AWARD_OF_DATE")%> </td>
                                                                            <td><%# Eval("PG_SPECILIZATION_OF_QUALIFICATION")%> </td>
                                                                            <td><%# Eval("AFFILIATED_SHORTNAME")%> </td>
                                                                            <td><%# Eval("DEMAND_TOTAL_AMT")%> </td>
                                                                            <td><%# Eval("DCR_TOTAL_AMT")%> </td>
                                                                            <td><%# Eval("DOC_STATUS")%> </td>
                                                                            <%-- <td><%# Eval("PDP_OL_SYLLABUS")%> </td>
                                                        <td><%# Eval("PDP_INDEX_NO")%> </td>
                                                        <td><%# Eval("PDP_MEDIUM")%> </td>
                                                        <td><%# Eval("PDP_ATTEMPTS")%> </td>
                                                        <td><%# Eval("PDP_OL_RESULTS")%> </td>
                                                        <td><%# Eval("PDP_INSTITUTE")%> </td>
                                                        <td><%# Eval("PDP_REGISTRATION_NO")%> </td>
                                                        <td><%# Eval("PDP_PROGRAM_NAME")%> </td>
                                                        <td><%# Eval("PDP_STREAMNAME")%> </td>
                                                        <td><%# Eval("PDP_GPA")%> </td>
                                                        <td><%# Eval("PDP_SUBJECT_RESULTS")%> </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnTransferExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade show active"  id="divHigherSem" runat="server" visible="false" role="tabpanel" aria-labelledby="Report-tab">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updHigherSemester"
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
                                <asp:UpdatePanel ID="updHigherSemester" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Higher Semester</h5>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemsterName" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Semester">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="requiredfieldvalidator1" runat="server" ControlToValidate="ddlSemsterName"
                                                                    Display="none" ErrorMessage="Please Select Semester" InitialValue="0"
                                                                    ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true">Faculty /School Name</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlfacultyname" runat="server" TabIndex="2" CssClass="form-control select2 select-click" AutoPostBack="true"
                                                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlfacultyname_SelectedIndexChanged"
                                                                    ToolTip="Please Select Faculty/School Name">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="requiredfieldvalidator1" runat="server" ControlToValidate="ddlcollege"
                                                                    Display="none" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                                    ValidationGroup="submit" />--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true">Specialization</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlProgramName" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Program">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="requiredfieldvalidator3" runat="server" ControlToValidate="ddlprogram"
                                                                    Display="none" ErrorMessage="Please Select Program" InitialValue="0"
                                                                    ValidationGroup="submit" />--%>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnShowHigher" runat="server" ToolTip="Show" OnClick="btnShowHigher_Click"
                                                                CssClass="btn btn-outline-info" TabIndex="4" ValidationGroup="submit">Show</asp:LinkButton>

                                                            <asp:Button ID="btnCancelHigher" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btnCancelHigher_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>
                                                        <div class="col-md-12" id="divListHigher" runat="server" visible="false">
                                                            <asp:Panel ID="Panel4" runat="server">
                                                                <asp:ListView ID="lvhigherSemester" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Summary Report List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>

                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Application No.</th>
                                                                                    <th style="text-align: center">Enrolled No.</th>
                                                                                   <%-- <th style="text-align: center">Name with Intital</th>--%>
                                                                                    <th style="text-align: center">Student Name</th>
                                                                                    <th style="text-align: center">Gender</th>
                                                                                    <th style="text-align: center">Father Name</th>
                                                                                    <th style="text-align: center">Mother Name</th>
                                                                                    <th style="text-align: center">NIC</th>
                                                                                    <th style="text-align: center">Passport</th>

                                                                                    <th style="text-align: center">Emailid</th>
                                                                             <%--       <th style="text-align: center">College Email</th>--%>

                                                                                    <th style="text-align: center">Mobile No.</th>
                                                                                    <th style="text-align: center">Faculty/School</th>
                                                                                    <th style="text-align: center">Semester</th>
                                                                                    <th style="text-align: center">Program/Specialization</th>
                                                                                    <th style="text-align: center">Intake</th>
                                                                                    <th style="text-align: center">Permanent Address</th>
                                                                                    <th style="text-align: center">Local Address</th>
                                                                                    <th style="text-align: center">Country</th>
                                                                                    <th style="text-align: center">State</th>
                                                                                    <th style="text-align: center">District</th>

                                                                              <%--      <th style="text-align: center">Aptitute Center Name</th>
                                                                                    <th style="text-align: center">Weekday/Weekend</th>
                                                                                    <th style="text-align: center">Campus Name</th>--%>
                                                                                    <%--                                                                                    <th style="text-align: center">Amount</th>
                                                                                    <th style="text-align: center">Payment Type</th>
                                                                                    <th style="text-align: center">Payment Date</th>
                                                                                    <th style="text-align: center">Mode</th>--%>

                                                                                    <th style="text-align: center">Enrolled Date</th>

                                                                                    <th style="text-align: center">Medium</th>
                                                                                  
                                                                                    <th>Awarding Institute</th>
                                                                                    <%--<th>PDP O/L Syllabus</th>
                                                                <th>PDP Index No.</th>
                                                                <th>PDP Medium</th>
                                                                <th>PDP Attempt</th>
                                                                <th>PDP O/L Results</th>
                                                                <th>PDP Institute</th>
                                                                <th>PDP Registration Number</th>
                                                                <th>PDP Name of the Programme</th>
                                                                <th>PDP Stream</th>
                                                                <th>PDP Grade Point Average</th>
                                                                <th>PDP Subject Results</th>--%>
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
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("REGNO")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ENROLLNO")%> 
                                                                            </td>

                                                                            <%--<td>
                                                                                <%# Eval("STUDFIRSTNAME")%> 
                                                                            </td>--%>
                                                                            <td>
                                                                                <%# Eval("STUDNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("GENDER")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("FATHERNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("MOTHERNAME")%> 
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("NICNO")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PASSPORTNO")%> 
                                                                            </td>


                                                                            <td>
                                                                                <%# Eval("EMAILID")%> 
                                                                            </td>
                                                                          <%--  <td>
                                                                                <%# Eval("COLLEGE_EMAIL")%> 
                                                                            </td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("STUDENTMOBILE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("COLLEGE_NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("SEMESTERNAME")%> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PROGRAMNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%--  <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME") %>' ToolTip='<%# Eval("OLDINTAKE") %>'></asp:Label>--%>
                                                                                <%# Eval("BATCHNAME")%>                                                                                                      
                                                                            </td>
                                                                            <td><%# Eval("PADDRESS")%> </td>
                                                                            <td><%# Eval("LADDRESS")%> </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("COUNTRYNAME")%> </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("STATENAME")%> </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("DISTRICTNAME")%> </td>
                                                                         <%--   <td style="text-align: center">
                                                                                <%# Eval("APTITUDE_CENTER_NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("WEEKDAYSNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("CAMPUSNAME")%> 
                                                                            </td>--%>
                                                                            <%--  <td style="text-align: center">
                                                                                <%# Eval("TOTAL_AMT")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("PAYTYPE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("RECONDATE")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("MODE_NAME")%> 
                                                                            </td>--%>

                                                                            <td style="text-align: center">
                                                                                <%# Eval("APPROVED_DATE")%> 
                                                                            </td>


                                                                            <td style="text-align: center">
                                                                                <%# Eval("MEDIUM_NAME")%> 
                                                                            </td>
                                                                            <td><%# Eval("AFFILIATED_SHORTNAME")%> </td>
                                                                            <%-- <td><%# Eval("PDP_OL_SYLLABUS")%> </td>
                                                        <td><%# Eval("PDP_INDEX_NO")%> </td>
                                                        <td><%# Eval("PDP_MEDIUM")%> </td>
                                                        <td><%# Eval("PDP_ATTEMPTS")%> </td>
                                                        <td><%# Eval("PDP_OL_RESULTS")%> </td>
                                                        <td><%# Eval("PDP_INSTITUTE")%> </td>
                                                        <td><%# Eval("PDP_REGISTRATION_NO")%> </td>
                                                        <td><%# Eval("PDP_PROGRAM_NAME")%> </td>
                                                        <td><%# Eval("PDP_STREAMNAME")%> </td>
                                                        <td><%# Eval("PDP_GPA")%> </td>
                                                        <td><%# Eval("PDP_SUBJECT_RESULTS")%> </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
