<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" CodeFile="Followupdate.aspx.cs" Inherits="ACADEMIC_Followupdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/myfilterOpt.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/semantic.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/Sematic/JS/semantic.min.js")%>"></script>
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
        /*#showright-sidebar8 {
         display:none;
        }*/
    </style>

    <%-- <script>
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lkUpcoming").click(function () {
                alert("The paragraph was clicked.");
                $("#pageright-wrapper8").css('display' , 'none');
            });
        });
</script>--%>

    <%-- <script>
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lkUpcoming").onclick(function () {
                $("#showright-sidebar8").css("display", "none");
            });
        });

    </script>--%>


    <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Panel4 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_pnllst .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">

        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">

                <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                    <li class="nav-item active" id="divlkfollowdate" runat="server">
                        <asp:LinkButton ID="lkfollowdate" runat="server" OnClick="lkfollowdate_Click" CssClass="nav-link" TabIndex="1">Today'sFolllow-up</asp:LinkButton></li>
                    <li class="nav-item" id="divlkUpcoming" runat="server">
                        <asp:LinkButton ID="lkUpcoming" runat="server" OnClick="lkUpcoming_Click" CssClass="nav-link" TabIndex="2">Upcoming</asp:LinkButton></li>
                    <li class="nav-item" id="divlkOverdue" runat="server">
                        <asp:LinkButton ID="lkOverdue" runat="server" OnClick="lkOverdue_Click" CssClass="nav-link" TabIndex="3">Overdue</asp:LinkButton></li>
                    <li class="nav-item" id="divlkCompleted" runat="server">
                        <asp:LinkButton ID="lkCompleted" runat="server" OnClick="lkCompleted_Click" CssClass="nav-link" TabIndex="4">Completed</asp:LinkButton></li>
                    <li class="nav-item" id="divlkAll" runat="server">
                        <asp:LinkButton ID="lkAll" runat="server" OnClick="lkAll_Click" CssClass="nav-link" TabIndex="5">All</asp:LinkButton></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="todaydate" role="tabpanel" runat="server" aria-labelledby="Today'sFolllow-up-tab-tab">
                        <div id="pageright-wrapper" class="pageright-wrapper chiller-theme ">
                            <div id="sidebar" class="right-wrapper">
                                <a id="showright-sidebar" class="sidebtn" style="cursor: pointer">
                                    <asp:Image ID="ImLogo" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                </a>
                                <div class="right-content" style="background-color: #fff;">
                                    <div class="right-brand">
                                        <div class="filter-heading">
                                            <a href="#">FILTERS</a>
                                            <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle"></i>

                                            <div class="filter-text input-group  mt-3">
                                                <div class="input-group-prepend input-filter">
                                                    <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                    <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="sidebar-header scrollbar-right">
                                                <div class="form-group">
                                                    <label>Select Topics</label>

                                                    <asp:ListBox ID="ddlMainLeadLabel" runat="server" AppendDataBoundItems="true" class="form-control"
                                                        data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Application Not Started</asp:ListItem>
                                                        <asp:ListItem Value="2">Application Started But Payment Pending</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="sidebar-footer">
                                                    <asp:Button ID="btnApplyFilter" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnApplyFilter_Click" />&nbsp;&nbsp;
                                                     <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btnClearFilter_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlMainLeadLabel" />
                                            <asp:PostBackTrigger ControlID="btnApplyFilter" />
                                            <asp:PostBackTrigger ControlID="btnClearFilter" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updfollowup"
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
                        <asp:UpdatePanel ID="updfollowup" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">

                                        <asp:Panel ID="pnllst" runat="server" Visible="false">
                                            <asp:ListView ID="lvtodaysdate" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Today'sFolllow-up</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Send Email</th>
                                                                <th style="text-align: center">SrNo
                                                                </th>
                                                                <th style="text-align: center">Username
                                                                </th>
                                                                <th style="text-align: center">Name
                                                                </th>
                                                                <th style="text-align: center">Emailid
                                                                </th>
                                                                <th style="text-align: center">Mobile
                                                                </th>
                                                                <th style="text-align: center">Registration Date
                                                                </th>
                                                                <th style="text-align: center">Followup Date
                                                                </th>
                                                                <th style="text-align: center">Mark as Completed
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
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="chktodayemail" runat="server" />
                                                        </th>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lblusername" CssClass="PopUp" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>' CommandArgument='<%# Eval("USERNO")%>' OnClick="lblusername_Click" AutoPostBack="true"></asp:LinkButton>
                                                            <%--<asp:Label ID="lbluser" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>' Visible="false" ></asp:Label>--%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblregdate" runat="server" Text='<%# Eval("REGDATE")%>' ToolTip='<%# Eval("REGDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("NEXTDATE")%>' ToolTip='<%# Eval("NEXTDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="chk" runat="server" />
                                                        </td>

                                                    </tr>

                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" ToolTip="Submit"
                                            OnClick="btnsubmit_Click" CssClass="btn btn-outline-info" Visible="false" />
                                        <asp:Button ID="btnTodayEmail" runat="server" CssClass="btn btn-outline-info" Text="Send Bulk Email" ValidationGroup="email" Visible="false" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" Visible="false" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="email"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div id="upcomindate" runat="server" visible="false" role="tabpanel" aria-labelledby="Upcoming-tab">
                        <div id="pageright-wrapper8" class="pageright-wrapper chiller-theme ">
                            <div id="sidebar8" class="right-wrapper">
                                <a id="showright-sidebar8" class="sidebtn" style="cursor: pointer">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                </a>
                                <div class="right-content" style="background-color: #fff;">
                                    <div class="right-brand">
                                        <div class="filter-heading">
                                            <a href="#">FILTERS 2</a>
                                            <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle8"></i>

                                            <div class="filter-text input-group  mt-3">
                                                <div class="input-group-prepend input-filter">
                                                    <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                    <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel4"
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

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="sidebar-header scrollbar-right">
                                                <div class="form-group">
                                                    <label>Select Topics</label>

                                                    <asp:ListBox ID="ddlmainleadupc" runat="server" AppendDataBoundItems="true" class="form-control"
                                                        data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Application Not Started</asp:ListItem>
                                                        <asp:ListItem Value="2">Application Started But Payment Pending</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="sidebar-footer">
                                                    <asp:Button ID="btnfilterupc" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnfilterupc_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnclearupc" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btnclearupc_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlmainleadupc" />
                                            <asp:PostBackTrigger ControlID="btnfilterupc" />
                                            <asp:PostBackTrigger ControlID="btnclearupc" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updupcoming"
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
                        <asp:UpdatePanel ID="updupcoming" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                                            <asp:ListView ID="lvupcomings" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Upcoming Dates</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Send Email</th>
                                                                <th style="text-align: center">SrNo
                                                                </th>
                                                                <th style="text-align: center">Username
                                                                </th>
                                                                <th style="text-align: center">Name
                                                                </th>
                                                                <th style="text-align: center">Emailid
                                                                </th>
                                                                <th style="text-align: center">Mobile
                                                                </th>
                                                                <th style="text-align: center">Registration Date
                                                                </th>
                                                                <th style="text-align: center">Followup Date
                                                                </th>
                                                                <th style="text-align: center">Mark as Completed
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
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="chkemailupc" runat="server" />
                                                        </th>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                                           
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbluuser" CssClass="PopUp" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>' CommandArgument='<%# Eval("USERNO")%>' OnClick="lbluuser_Click" AutoPostBack="true"></asp:LinkButton>
                                                            <%--<asp:Label ID="lblupuname" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>'></asp:Label>--%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblupname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblupemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblupmobile" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>
                                                            <%--            <%# Eval("MOBILENO")%> --%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblregupdate" runat="server" Text='<%# Eval("REGDATE")%>' ToolTip='<%# Eval("REGDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblupdate" runat="server" Text='<%# Eval("NEXTDATE")%>' ToolTip='<%# Eval("NEXTDATE")%>'></asp:Label>
                                                            <%-- <%# Eval("date")%> --%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="upchk" runat="server" />
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsave" runat="server" Text="Submit" ToolTip="Submit"
                                            OnClick="btnsave_Click" CssClass="btn btn-outline-info" Visible="false" />
                                        <asp:Button ID="btncancelup" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            OnClick="btncancelup_Click" CssClass="btn btn-outline-danger" Visible="false" />
                                        <asp:Button ID="btnemailoupc" runat="server" CssClass="btn btn-outline-info" Text="Send Bulk Email" ValidationGroup="email" Visible="false" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="overduedate" runat="server" visible="false" role="tabpanel" aria-labelledby="overduedate-tab">
                        <div id="pageright-wrapper2" class="pageright-wrapper chiller-theme ">
                            <div id="sidebar2" class="right-wrapper">
                                <a id="showright-sidebar2" class="sidebtn" style="cursor: pointer">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                </a>
                                <div class="right-content" style="background-color: #fff;">
                                    <div class="right-brand">
                                        <div class="filter-heading">
                                            <a href="#">FILTERS 3 </a>
                                            <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle2"></i>

                                            <div class="filter-text input-group  mt-3">
                                                <div class="input-group-prepend input-filter">
                                                    <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                    <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel6"
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

                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="sidebar-header scrollbar-right">
                                                <div class="form-group">
                                                    <label>Select Topics</label>

                                                    <asp:ListBox ID="ddlmainover" runat="server" AppendDataBoundItems="true" class="form-control"
                                                        data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Application Not Started</asp:ListItem>
                                                        <asp:ListItem Value="2">Application Started But Payment Pending</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="sidebar-footer">
                                                    <asp:Button ID="btnfilover" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnfilover_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btncanover" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btncanover_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlmainover" />
                                            <asp:PostBackTrigger ControlID="btnfilover" />
                                            <asp:PostBackTrigger ControlID="btncanover" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updoverdue"
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
                        <asp:UpdatePanel ID="updoverdue" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <asp:ListView ID="lvoverdue" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Overdue Date</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%-- <th style="text-align: center"><asp:CheckBox ID="chkoverall" runat="server" OnClick="checkboxover(this)" /></th>--%>
                                                                <th style="text-align: center">Send Email
                                                                </th>
                                                                <th style="text-align: center">SrNo
                                                                </th>
                                                                <th style="text-align: center">Username
                                                                </th>
                                                                <th style="text-align: center">Name
                                                                </th>
                                                                <th style="text-align: center">Emailid
                                                                </th>
                                                                <th style="text-align: center">Mobile
                                                                </th>
                                                                <th style="text-align: center">Registration Date
                                                                </th>
                                                                <th style="text-align: center">Followup Date
                                                                </th>
                                                                <th style="text-align: center">Mark as Completed
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
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="chkemailove" runat="server" />
                                                        </th>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                                           
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbloduname" CssClass="PopUp" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>' CommandArgument='<%# Eval("USERNO")%>' OnClick="lbloduname_Click" AutoPostBack="true"></asp:LinkButton>
                                                            <%--  <asp:Label ID="lbloduname" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>'></asp:Label>--%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblodname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblodemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblodmobile" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>
                                                            <%--<%# Eval("MOBILENO")%>--%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblregoverdate" runat="server" Text='<%# Eval("REGDATE")%>' ToolTip='<%# Eval("REGDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbloddate" runat="server" Text='<%# Eval("NEXTDATE")%>' ToolTip='<%# Eval("NEXTDATE")%>'></asp:Label>
                                                            <%--<%# Eval("date")%>--%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="odchk" runat="server" />
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnodsave" runat="server" Text="Submit" ToolTip="Submit"
                                                OnClick="btnodsave_Click" CssClass="btn btn-outline-info" Visible="false" />
                                            <asp:Button ID="btnodcancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                OnClick="btnodcancel_Click" CssClass="btn btn-outline-danger" Visible="false" />
                                            <asp:Button ID="btnemailove" runat="server" Text="Send Bulk Email" ToolTip="Send Bulk Email"
                                                CssClass="btn btn-outline-info" Visible="false" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="completedate" runat="server" visible="false" role="tabpanel" aria-labelledby="completedate-tab">
                        <div id="pageright-wrapper3" class="pageright-wrapper chiller-theme ">
                            <div id="sidebar3" class="right-wrapper">
                                <a id="showright-sidebar3" class="sidebtn" style="cursor: pointer">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                </a>
                                <div class="right-content" style="background-color: #fff;">
                                    <div class="right-brand">
                                        <div class="filter-heading">
                                            <a href="#">FILTERS 4</a>
                                            <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle3"></i>

                                            <div class="filter-text input-group  mt-3">
                                                <div class="input-group-prepend input-filter">
                                                    <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                    <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="UpdatePanel33"
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

                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                        <ContentTemplate>
                                            <div class="sidebar-header scrollbar-right">
                                                <div class="form-group">
                                                    <label>Select Topics</label>

                                                    <asp:ListBox ID="ddlmaincompl" runat="server" AppendDataBoundItems="true" class="form-control"
                                                        data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Application Not Started</asp:ListItem>
                                                        <asp:ListItem Value="2">Application Started But Payment Pending</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="sidebar-footer">
                                                    <asp:Button ID="btnsubcomp" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnsubcomp_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnclearcomp" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btnclearcomp_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlmaincompl" />
                                            <asp:PostBackTrigger ControlID="btnsubcomp" />
                                            <asp:PostBackTrigger ControlID="btnclearcomp" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updcomplete"
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
                        <asp:UpdatePanel ID="updcomplete" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                            <asp:ListView ID="lvcomplete" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Completed Dates</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Send Email</th>
                                                                <th style="text-align: center">SrNo
                                                                </th>
                                                                <th style="text-align: center">Username
                                                                </th>
                                                                <th style="text-align: center">Name
                                                                </th>
                                                                <th style="text-align: center">Emailid
                                                                </th>
                                                                <th style="text-align: center">Mobile
                                                                </th>
                                                                <th style="text-align: center">Registration Date
                                                                </th>
                                                                <th style="text-align: center">Followup Date
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
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="chkemailcompl" runat="server" />
                                                        </th>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                                           
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcomplusername" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>'></asp:Label>
                                                            <%-- <%# Eval("USERNAME")%> --%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblcompname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>
                                                            <%--<%# Eval("NAME")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblcompemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                            <%--<%# Eval("EMAILID")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblcompmobile" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>
                                                            <%--<%# Eval("MOBILENO")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblregcompdate" runat="server" Text='<%# Eval("REGDATE")%>' ToolTip='<%# Eval("REGDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblcompdate" runat="server" Text='<%# Eval("NEXTDATE")%>' ToolTip='<%# Eval("NEXTDATE")%>'></asp:Label>
                                                            <%--<%# Eval("date")%>--%> 
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnemailcompl" runat="server" Text="Send Bulk Email" ToolTip="Send Bulk Email" CssClass="btn btn-outline-info" Visible="false" />
                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="alldate" runat="server" visible="false" role="tabpanel" aria-labelledby="upcomindate-tab">
                        <div id="pageright-wrapper4" class="pageright-wrapper chiller-theme ">
                            <div id="sidebar4" class="right-wrapper">
                                <a id="showright-sidebar4" class="sidebtn" style="cursor: pointer">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                </a>
                                <div class="right-content" style="background-color: #fff;">
                                    <div class="right-brand">
                                        <div class="filter-heading">
                                            <a href="#">FILTERS 5</a>
                                            <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle4"></i>

                                            <div class="filter-text input-group  mt-3">
                                                <div class="input-group-prepend input-filter">
                                                    <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                    <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel44"
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

                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                        <ContentTemplate>
                                            <div class="sidebar-header scrollbar-right">
                                                <div class="form-group">
                                                    <label>Select Topics</label>

                                                    <asp:ListBox ID="ddlmainall" runat="server" AppendDataBoundItems="true" class="form-control"
                                                        data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Application Not Started</asp:ListItem>
                                                        <asp:ListItem Value="2">Application Started But Payment Pending</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="sidebar-footer">
                                                    <asp:Button ID="btnsuball" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnsuball_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="clearall" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="clearall_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlmainall" />
                                            <asp:PostBackTrigger ControlID="btnsuball" />
                                            <asp:PostBackTrigger ControlID="clearall" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updall"
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
                        <asp:UpdatePanel ID="updall" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                                            <asp:ListView ID="lvall" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>All Dates</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                        <thead class="bg-light-blue">

                                                            <tr>
                                                                <th style="text-align: center">
                                                                    <asp:CheckBox ID="chkallot" runat="server" OnClick="checkbox(this)" />
                                                                </th>
                                                                <th style="text-align: center">SrNo
                                                                </th>
                                                                <th style="text-align: center">Username
                                                                </th>
                                                                <th style="text-align: center">Name
                                                                </th>
                                                                <th style="text-align: center">Emailid
                                                                </th>
                                                                <th style="text-align: center">Mobile
                                                                </th>
                                                                <th style="text-align: center">Registration Date
                                                                </th>
                                                                <th style="text-align: center">Followup Date
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
                                                            <asp:CheckBox ID="chkemail" runat="server" />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                                           
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="alluser" CssClass="PopUp" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>' CommandArgument='<%# Eval("USERNO")%>' OnClick="alluser_Click" AutoPostBack="true"></asp:LinkButton>
                                                            <%--   <%# Eval("USERNAME")%> --%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblallname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>
                                                            <%--<%# Eval("NAME")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblallemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                            <%--<%# Eval("EMAILID")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblallmobileno" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>
                                                            <%--<%# Eval("MOBILENO")%>--%> 
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblregalldate" runat="server" Text='<%# Eval("REGDATE")%>' ToolTip='<%# Eval("REGDATE")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblalldate" runat="server" Text='<%# Eval("NEXTDATE")%>' ToolTip='<%# Eval("NEXTDATE")%>'></asp:Label>
                                                            <%--<%# Eval("date")%>--%> 
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                    </div>

                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnBulkEmail" runat="server" CssClass="btn btn-outline-info" Text="Send Bulk Email" ValidationGroup="email" Visible="false" />

                                        <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="email"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade beauty" id="myApplicationModal" role="dialog">
        <asp:UpdatePanel ID="updLeadStatus" runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-xl">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">FollowUp  Status</h4>
                            <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                        </div>
                        <div class="modal-body clearfix">
                            <div class="row">
                                <div class="form-group col-lg-6 col-md-6 col-12">

                                    <label>Followup Status </label>
                                    <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnEnqueryno" runat="server" />
                                    <%--  <asp:HiddenField ID="" runat="server" />--%>
                                </div>

                                <div class="form-group col-lg-6 col-md-6 col-12">

                                    <label>Remark </label>
                                    <asp:TextBox ID="txt_Remark" runat="server" TabIndex="9" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine"></asp:TextBox>

                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                        TargetControlID="txt_Remark" WatermarkText="Enter Remark" />
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">

                                        <label>Next Followup date</label>
                                    </div>

                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4"
                                        ToolTip="Please Enter Next Followup Date" CssClass="form-control" />
                                    <i class="fa fa-calendar input-prefix" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 10px;"></i>
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Next Followup Date" ControlToValidate="txtEndDate" Display="None"
                                        ValidationGroup="ubmit" />

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btn_SubmitModal" runat="server" TabIndex="11" Text="Submit" CssClass="btn btn-outline-primary" OnClick="btn_SubmitModal_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" TabIndex="12" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btn_Cancel_Click" />

                                </div>

                            </div>
                            <div class="modal-footer">
                                <div class="col-12">
                                    <asp:Panel ID="pnlSession" runat="server">
                                        <asp:ListView ID="lvLeadList" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Application Status List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Action </th>
                                                            <th>Lead Stage</th>
                                                            <th>Remark</th>
                                                            <th>Date</th>
                                                            <th>Next Folloup Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <%--<asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" 
                                                        CommandArgument='<%# Eval("ENQUIRYNO") %>' ImageUrl="~/IMAGES/delete.gif" 
                                                        OnClick="btnDel_Click" OnClientClick="return UserDeleteConfirmation();" TabIndex="6" 
                                                        ToolTip='<%# Eval("ENQUIRYNO") %>' /> --%>

                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                            AlternateText="Edit Record" ToolTip='<%# Eval("ENQUIRYNO") %>' CommandArgument='<%# Eval("ENQUIRYNO") %>' OnClick="btnEdit_Click" />

                                                    </td>
                                                    <td><%# Eval("LEAD_STAGE_NAME") %></td>
                                                    <td><%# Eval("REMARKS") %></td>
                                                    <td><%# Eval("UPD_ENQUIRYSTATUS_DATE") %></td>
                                                    <td><%# Eval("NEXTDATE") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Cancel" />
                <asp:AsyncPostBackTrigger ControlID="btn_SubmitModal" />
                <asp:AsyncPostBackTrigger ControlID="lvLeadList" />
                <%-- <asp:PostBackTrigger ControlID="btn_Cancel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--tab1--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_btnTodayEmail").click(function () {
                $("#modalemailtoday").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_btnTodayEmail").click(function () {
                    $("#modalemailtoday").modal();

                });
            });
        });
    </script>

    <div class="modal fade" id="modalemailtoday">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="updEmailSendtoday"
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
                <asp:UpdatePanel ID="updEmailSendtoday" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <%-- <label>Subject</label>--%>
                                <asp:RadioButtonList ID="rbtodayselect" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtodayselect_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div id="todaymau" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtsubtoday" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtsubtoday" Display="None"
                                        ValidationGroup="EmailSubmittoday" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtmestoday" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtmestoday" Display="None"
                                        ValidationGroup="EmailSubmittoday" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fileuptoday" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="todayauto" runat="server" visible="false">
                                <asp:RadioButtonList ID="rbtodaytemplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnemailtoday" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmittoday" OnClick="btnemailtoday_Click" />
                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="EmailSubmittoday" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnemailtoday" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <%--tab2--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_btnemailoupc").click(function () {
                $("#EmailSendupc").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_btnemailoupc").click(function () {
                    $("#EmailSendupc").modal();

                });
            });
        });
    </script>
    <div class="modal fade" id="EmailSendupc">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="updEmailSendupc"
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
                <asp:UpdatePanel ID="updEmailSendupc" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <%-- <label>Subject</label>--%>
                                <asp:RadioButtonList ID="rbupcselect" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rbupcselect_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div id="manuupc" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtsubupc" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtsubupc" Display="None"
                                        ValidationGroup="EmailSubmitupc" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtmesupc" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtmesupc" Display="None"
                                        ValidationGroup="EmailSubmitupc" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuupc" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="autoupc" runat="server" visible="false">
                                <%-- <label><b>Use Filteration</b></label>--%>
                                <asp:RadioButtonList ID="rbupctemplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsendemailupc" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmitupc" OnClick="btnsendemailupc_Click" />
                                <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="EmailSubmitupc" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsendemailupc" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--tab3--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_btnemailove").click(function () {
                $("#EmailSendove").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_btnemailove").click(function () {
                    $("#EmailSendove").modal();

                });
            });
        });
    </script>

    <div class="modal fade" id="EmailSendove">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="updEmailSendove"
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
                <asp:UpdatePanel ID="updEmailSendove" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <%-- <label>Subject</label>--%>
                                <asp:RadioButtonList ID="rboverselect" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rboverselect_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div id="manuover" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtsubove" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtsubove" Display="None"
                                        ValidationGroup="EmailSubmitove" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtmegove" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtmegove" Display="None"
                                        ValidationGroup="EmailSubmitove" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuove" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div id="autoover" runat="server" visible="false">
                                <asp:RadioButtonList ID="rbovertemplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsendemailove" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmitove" OnClick="btnsendemailove_Click" />
                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="EmailSubmitove" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsendemailove" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <%--tab4--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_btnemailcompl").click(function () {
                $("#EmailSendcomp").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_btnemailcompl").click(function () {
                    $("#EmailSendcomp").modal();

                });
            });
        });
    </script>
    <div class="modal fade" id="EmailSendcomp">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress13" runat="server" AssociatedUpdatePanelID="updEmailSendcomp"
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
                <asp:UpdatePanel ID="updEmailSendcomp" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <%-- <label>Subject</label>--%>
                                <asp:RadioButtonList ID="rbcompselect" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rbcompselect_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div id="manucomp" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtsubcomp" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtsubcomp" Display="None"
                                        ValidationGroup="EmailSubmitcomp" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtmegcomp" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtmegcomp" Display="None"
                                        ValidationGroup="EmailSubmitcomp" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fucomp" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="autocomp" runat="server" visible="false">
                                <asp:RadioButtonList ID="rbcomtemplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsendemailcomp" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmitcomp" OnClick="btnsendemailcomp_Click" />
                                <asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="EmailSubmitcomp" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsendemailcomp" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%--tab5--%>
    <div class="modal fade" id="ModelEmailPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress19" runat="server" AssociatedUpdatePanelID="updEmailSend"
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
                <asp:UpdatePanel ID="updEmailSend" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <%-- <label>Subject</label>--%>
                                <asp:RadioButtonList ID="rbselect" runat="server"
                                    RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rbselect_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div id="auto" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtEmailSubject" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtEmailMessage" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtEmailMessage" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" CssClass="form-control" />
                                </div>

                            </div>
                            <div id="manu" runat="server" visible="false">
                                <%-- <label><b>Use Filteration</b></label>--%>
                                <asp:RadioButtonList ID="rbtamplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSendBulkEmail" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmit" OnClick="btnSendBulkEmail_Click" />
                                <asp:ValidationSummary ID="emailsummary" runat="server" ValidationGroup="EmailSubmit" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendBulkEmail" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_btnBulkEmail").click(function () {
                $("#ModelEmailPopup").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_btnBulkEmail").click(function () {
                    $("#ModelEmailPopup").modal();

                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".PopUp").click(function () {
                $("#myApplicationModal").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $(".PopUp").click(function () {
                    $("#myApplicationModal").modal();

                });
            });
        });
    </script>

    <%--filters script--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                //alert('hii')
                $("#OnlineAdmissionPopUp").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                    // alert('bye')
                    $("#OnlineAdmissionPopUp").modal();
                    $("#OnlineAdmissionPopUp").fadeIn();

                });
            });
        });
    </script>
    <script>
        $(function () {
            $('#ctl00_ContentPlaceHolder1_txtEndDate').daterangepicker({
                singleDatePicker: true,
                locale: {
                    format: 'DD-MM-YYYY'
                },
                //<!-- ========= Disable dates before today ========== -->
                minDate: new Date(),
                //<!-- ========= Disable dates before today END ========== -->
            });
            //$('#ctl00_ContentPlaceHolder1_txtEndDate').val('');
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('#ctl00_ContentPlaceHolder1_txtEndDate').daterangepicker({
                    singleDatePicker: true,
                    locale: {
                        format: 'DD-MM-YYYY'
                    },
                    //<!-- ========= Disable dates before today ========== -->
                    minDate: new Date(),
                    //<!-- ========= Disable dates before today END ========== -->
                });
                //$('#ctl00_ContentPlaceHolder1_txtEndDate').val('');
            });
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                //alert('hii')
                $("#ModelEmailPopup").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                    // alert('bye')
                    $("#ModelEmailPopup").modal();
                    $("#ModelEmailPopup").fadeIn();

                });
            });
        });
    </script>

    <%--<script>
        function checkboxover(headchk) {
            var items = document.getElementsByName('ctl00_ContentPlaceHolder1_lvoverdue_ctrl0_chkemailove');
            alert("aashna")
            for (var i = 0; i < items.length; i++) {
                if (items[i].type == 'checkbox')
                    items[i].checked = true;
            }
        }
        </script>--%>
    <script type="text/javascript" language="javascript">

        function checkbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvall$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvall$ctrl';
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
    <script type="text/javascript">

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;

        }
    </script>
    <script>
        $(document).ready(function () {
            "use strict";
            $('.label.ui.dropdown')
         .dropdown();
            $('.no.label.ui.dropdown')
              .dropdown({
                  useLabels: false
              });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                  .dropdown('restore defaults')
            })
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                "use strict";
                $('.label.ui.dropdown')
             .dropdown();
                $('.no.label.ui.dropdown')
                  .dropdown({
                      useLabels: false
                  });
                $('.ui.button').on('click', function () {
                    $('.ui.dropdown')
                      .dropdown('restore defaults')
                })
            });
        });
    </script>

    <script>

        function CmbChange(obj) {
            var cmbValue = document.getElementById("slMainLeadLabel").value;
            __doPostBack('slMainLeadLabel', cmbValue);
        }

    </script>
    <script>

        function CmbChange(obj) {
            var cmbValue = document.getElementById("slMainLeadLabel").value;
            __doPostBack('slMainLeadLabel', cmbValue);
        }

    </script>

    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar").click(function () {
            // alert('hi');
            $("#pageright-wrapper").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#showright-sidebar").click(function () {
                // alert('hi');
                $("#pageright-wrapper").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#filter-toggle").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar8").click(function () {
            // alert('hi');
            $("#pageright-wrapper8").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#showright-sidebar8").click(function () {
                // alert('hi');
                $("#pageright-wrapper8").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle8").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#filter-toggle8").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper8").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper8").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper8").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper8").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper8").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper8").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper8").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper8").removeClass("toggleed");
                }
            });
        });
    </script>


    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar2").click(function () {
            // alert('hi');
            $("#pageright-wrapper2").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#showright-sidebar2").click(function () {
                // alert('hi');
                $("#pageright-wrapper2").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle2").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#filter-toggle2").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper2").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper2").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper2").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper2").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper2").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper2").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper2").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper2").removeClass("toggleed");
                }
            });
        });
    </script>


    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar3").click(function () {
            // alert('hi');
            $("#pageright-wrapper3").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#showright-sidebar3").click(function () {
                // alert('hi');
                $("#pageright-wrapper3").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle3").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#filter-toggle3").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper3").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper3").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper3").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper3").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper3").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper3").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper3").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper3").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar4").click(function () {
            // alert('hi');
            $("#pageright-wrapper4").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#showright-sidebar4").click(function () {
                    // alert('hi');
                    $("#pageright-wrapper4").toggleClass("toggleed");
                    //  $(".btnsidebar").toggleClass('rotated')
                });
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle4").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#filter-toggle4").click(function () {
                    $(".filter-text").toggle();
                    $(".input-filter").addClass('inputfilter')

                });
            });
        });

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper4").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper4").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper4").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper4").removeClass("toggleed");
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $("#pageright-wrapper4").addClass("toggleed");
            }
            else {
                $("#pageright-wrapper4").removeClass("toggleed");
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlintaketwo option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $("#pageright-wrapper4").addClass("toggleed");
                }
                else {
                    $("#pageright-wrapper4").removeClass("toggleed");
                }
            });
        });
    </script>

</asp:Content>
