<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentFeedBackAns.aspx.cs" Inherits="ACADEMIC_StudentFeedBackAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .myTable tbody tr td, .table-striped tbody tr td label {
            font-weight: bold;
        }

        .rblans td {
            border: 0px solid #dee2e6;
        }

        .table.tbl-new > tbody > tr > td {
            padding: 5px 8px 0px;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.myTable.table-bordered > tbody > tr:nth-of-type(odd)').addClass("df");

            //var myColors = [
            //    '#c7d8ea', '#b4da72', '#f7e76e', '#f2b78d', '#abd3bc', '#f5a5a3', '#96e8e1', '#e2dfa2', '#d9d8da', '#ccccb3', '#e6b3b3', '#b3e6cc'
            //];
            var i = 0;
            $('.df').each(function () {
                $(this).css('background-color', myColors[i]);
                i = (i + 1) % myColors.length;
            });

            $('.myTable.table-bordered > tbody > tr:nth-of-type(even)').addClass("gf");

            //var myColors = [
            //      '#c7d8ea', '#b4da72', '#f7e76e', '#f2b78d', '#abd3bc', '#f5a5a3', '#96e8e1', '#e2dfa2', '#d9d8da', '#ccccb3', '#e6b3b3', '#b3e6cc'
            //];
            var i = 0;
            $('.gf').each(function () {
                $(this).css('background-color', myColors[i]);
                i = (i + 1) % myColors.length;
            });
        });
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row" id="divnotemsg" runat="server">
                            <%--<div class="col-12 btn-footer">
                                <h5 style="color: red; font-weight: bold">Note: MT - Main Teacher || ADT - Additional Teacher</h5>
                            </div>--%>
                        </div>
                        <%--  <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        Reg.No.
                                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" ToolTip="Please Enter Student Registration No. " Font-Bold="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Please Enter User Id" SetFocusOnError="True" ValidationGroup="Search" Display="None"></asp:RequiredFieldValidator>
                                        &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search"
                                            ValidationGroup="Search" OnClick="btnSearch_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                            ValidationGroup="Search" />
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </asp:Panel>--%>

                        <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-md-4 form-group">
                                    <%-- <div class="col-md-10">--%>
                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            Student Name 
                                        </div>

                                        <div class="col-md-8 form-group">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            Session 
                                        </div>

                                        <div class="col-md-8 form-group">
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            Curriculum<%--Regulation--%>
                                        </div>

                                        <div class="col-md-8 form-group">
                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            Semester/Year
                                        </div>

                                        <div class="col-md-8 form-group">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            Faculty/School Name
                                        </div>
                                        <div class="col-md-8 form-group">
                                            <asp:Label ID="lblcollege" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                        </div>

                                    </div>
                                    <%-- </div>--%>
                                    <%-- <div class="form-group col-md-2">
                                        <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                    </div>--%>



                                    <div class="row" id="examrow" runat="server" visible="false">
                                        <div class="col-md-4 form-group">
                                            <span style="color: red">* </span>Exam 
                                        </div>
                                        <div class="col-md-8 form-group">
                                            <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="true"
                                                Visible="false" CssClass="form-control" ToolTip="Please Select Exam Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="ddlExam" Display="None"
                                                ErrorMessage="Please Select Exam Name" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>



                                </div>

                                <div class="form-group col-md-8">
                                    <asp:Panel ID="pnlSubject" runat="server"
                                        Width="100%" ScrollBars="Vertical" Height="200 px">
                                        <asp:ListView ID="lvSelected" runat="server">
                                            <LayoutTemplate>
                                                <div style="color: black">
                                                </div>
                                                <table class="table table-bordered table-hover table-fixed">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>SrNo
                                                            </th>
                                                            <th>Module Code - Module Name - Teacher Name - Teacher Type
                                                            </th>
                                                            <th>Feedback Status
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
                                                    <td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td style="width: 80%">
                                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" CommandArgument='<%# Eval("COURSENO")%>'
                                                            OnClick="lnkbtnCourse_Click" Text='<%#(String.IsNullOrEmpty(Eval("UA_FULLNAME").ToString()))?GetCourseName(Eval("COURSENAME"),"No FACULTY","No FACULTY TYPE"):GetCourseName(Eval("COURSENAME"),Eval("UA_FULLNAME"),Eval("TEACHER")) %>'
                                                            ToolTip='<%# Eval("ua_no")%>' />
                                                        <%--ToolTip='<%# (Convert.ToInt32(Eval("SUBID"))==1 ||Convert.ToInt32(Eval("SUBID"))==3 || 
                                                                  Convert.ToInt32(Eval("SUBID"))==13)?Eval("ad_teacher_th"):Eval("ad_teacher_pr")%>'--%>
                                                        <asp:HiddenField ID="hdnSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblComplete" Text='<%# Eval("Status")%>' runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="form-group col-md-12">
                                <asp:Button ID="btnReport" runat="server"
                                    Text="Report" ValidationGroup="Report" Visible="false" />
                                <asp:Button ID="btnClear" runat="server" Text="Cancel"
                                    Visible="false" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="Report" />
                            </div>

                            <div class="form-group col-md-12">
                                <asp:Label ID="lblMsg" runat="server" Visible="false"> <span ID="spMsg" 
                                            style="color:Red;"></span></asp:Label>
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="pnlFeedback" runat="server" Visible="false">
                            <div class="form-group col-md-12" hidden="hidden">
                                <b>Ratings :</b>&nbsp;&nbsp;&nbsp;5 = Excellent &nbsp;&nbsp; 4 = Great &nbsp;&nbsp; 3 = Good &nbsp;&nbsp; 2 = Fair &nbsp;&nbsp; 1 = Poor
                            </div>

                            <div class="form-group col-md-12">
                                <u><b>
                                    <asp:Label ID="lblcrse" runat="server" Visible="false"></asp:Label></b></u>

                                <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                            </div>
                                            <table class="myTable table table-bordered table-hover table-fixed tbl-new">
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="sub-heading">
                                                    <h5>
                                                        <asp:Label ID="lblCTType" runat="server" Text='<%# Eval("FEEDBACK_NAME")%>' ToolTip='<%# Eval("FEEDBACK_NO")%>'></asp:Label>
                                                    </h5>
                                                    <%-- <div>
                                                        <div class="label-dynamic">                                                         
                                                            <asp:Label ID="lblcom" runat="server" Font-Bold="true" Text="Comments"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="TxtComments" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>--%>
                                                </div>
                                                <asp:HiddenField ID="hdnCTType" runat="server" Value='<%# Eval("FEEDBACK_NO") %>' />
                                                <asp:ListView ID="lvQuestion" runat="server" OnItemDataBound="lvQuestion_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Q&nbsp;<%# Eval("SLNO")%><asp:Label ID="lblQuestion" runat="server" Text='  <%# Eval("QUESTIONID")%>' Visible="false"></asp:Label>.</td>
                                                            <td>
                                                                <%# Eval("QUESTIONNAME")%>
                                                                <asp:RadioButtonList ID="rblAnswer" runat="server" Class="spaced" CssClass="RadioButtonWidth rblans" Style="font-family: Arial, helvetica !important; margin-left: -10px" RepeatDirection="Horizontal" ToolTip='<%# Eval("QUESTIONID") %>'>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdnAnswer" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                                                <asp:RequiredFieldValidator ID="rfvc" runat="server"
                                                                    ErrorMessage='<%# "Please Answer Q[" + Eval("SLNO") + "] Of " + Eval("FEEDBACK_NAME") + "." %>' ControlToValidate="rblAnswer"
                                                                    Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <tr>
                                            <td>
                                                <div class="col-12">
                                                    <%--<div class="label-dynamic">--%>
                                                    <sup>*</sup>
                                                        <asp:Label ID="lblcom" runat="server" Style="font-weight: bold;" ></asp:Label>
                                                   <%-- </div>--%>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvc" runat="server"
                                                                    ErrorMessage='<%# "Please Write additional Comments Of " + Eval("FEEDBACK_NAME") + "." %>' ControlToValidate="TxtComments"
                                                                    Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%--<br /><br />--%>
                            </div>

                            <%--<div class="form-group col-md-12">
                                <u><b>
                                    <asp:Label ID="lblteacher" runat="server" Visible="false"></asp:Label></b></u>
                                <asp:ListView ID="lvTeacher" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                            </div>
                                            <table class="table table-bordered table-hover table-fixed">
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>Q&nbsp;<%# Container.DataItemIndex + 1%><asp:Label ID="lblTeacher" runat="server" Text='<%# Eval("QUESTIONID") %>' Visible="false"></asp:Label>.</td>
                                            <td><%# Eval("QUESTIONNAME") %></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40px">Ans:&nbsp; </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblTeacher" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" ToolTip='<%# Eval("QUESTIONID") %>' Style="margin-left: -10px;">
                                                </asp:RadioButtonList>
                                                <asp:HiddenField ID="hdnTeacher" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>--%>


                            <div class="form-group col-md-12" style="display: block">
                                <sup>*</sup>
                                <asp:Label ID="lblWhatOtherChanges" Style="font-weight: bold;" runat="server" Text="What Other Changes would you like to suggest to improve the curriculum / course?"></asp:Label>
                                <asp:TextBox ID="txtWhatOtherChanges" runat="server" TextMode="MultiLine"
                                    placeholder="Please enter other changes (Max. 200 char) ." CssClass="form-control"
                                    oncopy="return false;" onpaste="return false;" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWhatOtherChanges"
                                    ErrorMessage="Please Enter Other Changes" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-12" style="display: block">
                                <sup>*</sup>
                                <asp:Label ID="lblAnyComments" runat="server" Style="font-weight: bold;" Text="Any additional comments (write briefly)?"></asp:Label>
                                <asp:TextBox ID="txtAnyComments" runat="server" TextMode="MultiLine"
                                    placeholder="Please enter comments (Max. 200 char) ." CssClass="form-control"
                                    oncopy="return false;" onpaste="return false;" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnyComments"
                                    ErrorMessage="Please Enter Additional Comments" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                            </div>


                            <div class="form-group col-md-12" style="display: none;">
                                <asp:Label ID="Label1" runat="server" Text="Any additional Remarks (write briefly)?"></asp:Label>
                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" placeholder="Please enter comments (Max. 100 char) ." Width="100 %" Height="70 px" MaxLength="100"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-12 text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-outline-info"
                                    ValidationGroup="Submit" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="Submit" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                            <tr>
                                <td align="center" valign="middle">
                                    <span style="font-size: large; color: Red;">
                                        <b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                            Please Contact Administrator! </b>
                                    </span>
                                </td>
                            </tr>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .RadioButtonWidth input {
            margin-left: 10px;
        }
    </style>

    <div id="divMsg" runat="server"></div>

    <p class="page_help_text">
        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    </p>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <script>
        $(<%=txtAnyComments.ClientID%>).on('keypress', function () {
            if ($(this).val().length > 200) {
                alert("Allowed Only Max(200) Char. ");
                return false;
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(<%=txtAnyComments.ClientID%>).on('keypress', function () {
                if ($(this).val().length > 200) {
                    alert("Allowed Only Max(200) Char. ");
                    return false;
                }

            });
        });



        $(<%=txtWhatOtherChanges.ClientID%>).on('keypress', function () {
            if ($(this).val().length > 200) {
                alert("Allowed Only Max(200) Char. ");
                return false;
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(<%=txtWhatOtherChanges.ClientID%>).on('keypress', function () {
                if ($(this).val().length > 200) {
                    alert("Allowed Only Max(200) Char. ");
                    return false;
                }

            });
        });

    </script>
</asp:Content>

