<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Program_Transfer.aspx.cs" ViewStateEncryptionMode="Always" EnableViewStateMac="true" Inherits="Projects_Program_Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
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
    <style>
        .search-icon #ctl00_ContentPlaceHolder1_imgSearch {
            box-shadow: 0px 0px 12px #ccc;
            padding: 8px;
            border-radius: 50%;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updprogram"
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

    <asp:UpdatePanel ID="updprogram" runat="server">
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
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="text-center search-icon">
                                            <a href="#" title="Search Student" data-toggle="modal" data-target="#myModal2">
                                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search-svg.png" TabIndex="1"
                                                    AlternateText="Search Student by Name, ID" Style="padding-left: -500px" meta:resourcekey="imgSearchResource1" /></a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="paneldetails" runat="server" Visible="false">
                                <div class="col-12 mb-3">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Current Program Details</h5>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblApplicationId" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblstudentid" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>College :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFaculty" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Awarding Institute :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAwardingInstitute" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Program :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblProgram" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student ID :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblstdid" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblstudname" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Email Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblemailid" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Campus :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCampus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 mb-3">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Transfer Program Details</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="ddlSession" Display="None"
                                                ErrorMessage="Please Select Session." InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="transfer">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Faculty</label>--%>
                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValida" runat="server"
                                                ControlToValidate="ddlFaculty" Display="None"
                                                ErrorMessage="Please Select College." InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="transfer">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>  </sup>
                                                <label >Change Student ID</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbChangeRegNo" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" OnSelectedIndexChanged="rdbChangeRegNo_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Yes &nbsp;&nbsp</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Program</label>--%>
                                                <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlProgram" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidato1" runat="server"
                                                ControlToValidate="ddlProgram" Display="None"
                                                ErrorMessage="Please Select Program" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="transfer">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="True"
                                                ToolTip="Please Select Curriculum" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" ErrorMessage="Please Select Curriculum" ValidationGroup="transfer"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Awarding Institute</label>--%>
                                                <asp:Label ID="lblAwarding" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAwardingInstitute" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAwardingInstitute_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="ddlAwardingInstitute" Display="None"
                                                ErrorMessage="Please Select Awarding Institute" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="transfer">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="newenroll" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <%--<label>New Student ID</label>--%>
                                                <asp:Label ID="lblNewStud" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" Enabled="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <asp:ListView ID="lvprogramlist" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Program Transfer List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%--<th>Edit </th>--%>
                                                            <th>SrNo </th>
                                                            <th>Student ID</th>
                                                            <th>Old Enrollnmentno</th>
                                                            <th>New Enrollnmentno</th>
                                                            <th>Student Name</th>
                                                            <th>Old Program</th>
                                                            <th>New Program</th>
                                                            <th>Old Intake</th>
                                                            <th>New Intake</th>
                                                            <th>Old Faculty/School Name</th>
                                                            <th>New Faculty/School Name</th>
                                                            <th>Old Campus</th>
                                                            <th>New Campus</th>
                                                            <th>Old Weekday/Weekend</th>
                                                            <th>New Weekday/Weekend</th>
                                                            <th>Date</th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("COURSENO") %>' AlternateText="Edit Record"
                                                            OnClick="btnEdit_Click" />
                                                    </td>--%>
                                                    <td>
                                                        <%# Container.DataItemIndex + 1 %>                                                                           
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERNAME")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDENROLL")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWENROLL")%> 
                                                    </td>

                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDPROGRAM")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWPROGRAM")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDBATCH")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWBATCH")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDCOLLEGE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWCOLLEGE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDCAMPUS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWCAMPUS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLDWEEK")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEWWEEK")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BCHANGE_DATE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnTransfer" runat="server" CssClass="btn btn-outline-info" OnClick="btnTransfer_Click" ValidationGroup="transfer">Transfer</asp:LinkButton>
                                    <asp:LinkButton ID="lnkPrintReport" runat="server" CssClass="btn btn-outline-primary" OnClick="lnkPrintReport_Click" Visible="false">Summary Sheet</asp:LinkButton>
                                   <asp:LinkButton ID="lnkCertiAdmis" runat="server" CssClass="btn btn-outline-info" OnClick="lnkCertiAdmis_Click">Certificate of Admission</asp:LinkButton>
                                     <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="transfer" />
                                </div>
                            </asp:Panel>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">

                <div class="modal-header">
                    <h4>Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <asp:UpdatePanel ID="updEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-group col-md-12">
                                <label>Search Criteria</label>
                                <br />
                                <asp:RadioButtonList ID="rdselect" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rdselectResource1" AutoPostBack="true" OnSelectedIndexChanged="rdselect_SelectedIndexChanged">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource52">&amp;nbsp Name</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource53">&amp;nbsp	Application ID / Student ID </asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Please Select Search Criteria"
                                    ControlToValidate="rdselect" Display="None" ValidationGroup="search" meta:resourcekey="RequiredFieldValidator24Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-6">
                                    <label>Search String</label>
                                    <%--<asp:TextBox ID="txtSearch" runat="server" meta:resourcekey="txtSearchResource1"></asp:TextBox>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Enter Deatils"
                                        ControlToValidate="txtSearch" Display="None" ValidationGroup="search" meta:resourcekey="RequiredFieldValidator25Resource1"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtSearchCandidateProgram" runat="server" MaxLength="100" CssClass="form-control" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtSearchCandidateProgram" Display="None"
                                        ErrorMessage="Please Enter Search String"
                                        SetFocusOnError="True" ValidationGroup="search">
                                    </asp:RequiredFieldValidator>

                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <%--<span class="input-group-btn">--%>
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-outline-info" OnClick="btnsearch_Click" ValidationGroup="search" />

                                    <asp:Button ID="btnSearchName" runat="server" Text="Search" ToolTip="Search By Name" CssClass="btn btn-outline-info" ValidationGroup="search" Visible="false" OnClick="btnSearchName_Click" />
                                    <asp:Button ID="btnCancelModal" runat="server" Text="Clear" OnClick="btnCancelModal_Click" CssClass="btn btn-outline-danger" meta:resourcekey="btnCancelModalResource1" />

                                    <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="search" />
                                    <p>
                                        <%--</span>--%><%--<asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-info" ValidationGroup="search" meta:resourcekey="btnSearchResource1" />--%><%--<asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClick="btnCancelModal_Click" CssClass="btn btn-outline-danger" meta:resourcekey="btnCancelModalResource1" />--%><%--<asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="search" meta:resourcekey="ValidationSummary3Resource1" />--%>
                                        <div>
                                            <asp:Label ID="lblNoRecords" runat="server" meta:resourcekey="lblNoRecordsResource1" SkinID="lblmsg" />
                                        </div>
                                        <div>
                                            <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                                <ProgressTemplate>
                                                    <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" meta:resourcekey="imgProgResource1" />
                                                    Loading.. Please Wait!
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel5" runat="server" meta:resourcekey="Panel5Resource1" ScrollBars="Auto">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Login Details</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server" Height="300px" meta:resourcekey="Panel2Resource1" ScrollBars="Auto">
                                                            <table class="table table-hover table-bordered">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Student Name </th>
                                                                        <th>Application ID / Student ID</th>
                                                                        <th>Specialization </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" OnClick="btnsearch_Click" CommandArgument='<%# Eval("REGNO") %>' Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("USERNO") %>'></asp:LinkButton></td>
                                                            <td>
                                                                <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblprogram" runat="server" Text='<%# Eval("LONGNAME") %>' ToolTip='<%# Eval("USERNO") %>'></asp:Label>
                                                            </td>
                                                            <%--<td><%# Eval("SEMESTERNAME")%></td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                    </p>
                                </p>

                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lvStudent" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>

                        <%--<asp:AsyncPostBackTrigger ControlID="lnkId"  />--%>

                        <%--<asp:AsyncPostBackTrigger ControlID="lvStudent"  />--%>

                        <%--<asp:PostBackTrigger ControlID="btnSearch" />--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="lnkId"  />--%>
                    </Triggers>
                </asp:UpdatePanel>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript" language="javascript">
        /* To collapse and expand page sections */
        function toggleExpansion(image, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                image.src = "../IMAGES/up-arrow.png";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                image.src = "../IMAGES/down-arrow.png";
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$Lvmodule$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$Lvmodule$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_txtSearchCandidateProgram').on('keydown', function (event) {
                debugger;
                var key = event.key;
                var regex = /[!@#$%^&*()_+\=\[\]{};':"\\|,.<>\/?]/;
                if (regex.test(key)) {
                    event.preventDefault();
                }
            });
        });

        $('#ctl00_ContentPlaceHolder1_txtSearchCandidateProgram').on('paste', function (event) {
            var clipboardData = event.originalEvent.clipboardData || window.clipboardData;
            var pastedData = clipboardData.getData('text');
            var regex = /[!@#$%^&*()_+\=\[\]{};':"\\|,.<>\/?]/g;
            $(this).val(pastedData.replace(regex, ''));
            event.preventDefault();
        });

    </script>
</asp:Content>

