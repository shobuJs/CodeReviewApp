<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GradeGeneration.aspx.cs" Inherits="CourseWise_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .myCss>thead>tr>th, .myCss>tbody>tr>th, .myCss>tfoot>tr>th, .myCss>thead>tr>td, .myCss>tbody>tr>td, .myCss>tfoot>tr>td {
            border: 1px solid #ddd;
        }

        .fixed {
          position: fixed;
          top:0; 
        }
    </style>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                var stickyOffset = $('.sticky').offset().top;

                $(window).scroll(function () {
                    var sticky = $('.sticky'),
                        scroll = $(window).scrollTop();

                    if (scroll >= stickyOffset) {
                        sticky.addClass('fixed');
                        $('.td1').width($('.th1').width());
                        $('.th2').width($('.td2').width());
                        $('.th3').width($('.td3').width());
                        $('.td4').width($('.th4').width());
                        $('.td5').width($('.th5').width());
                        $('.td6').width($('.th6').width());
                        $('.td7').width($('.th7').width());
                        $('.td8').width($('.th8').width());
                        $('.th9').width($('.td9').width());
                        $('.th10').width($('.td10').width());
                    }
                    else {
                        sticky.removeClass('fixed');
                    }
                });
            });
        });
    </script>
    
    <%--onunload="crViewer_Unload"--%>
    <div style="z-index: 1; position: relative;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position:fixed;margin-left:40%;margin-top:18%">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">GRADE ALLOTMENT</h3>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="form-group col-md-3">
                                    <label><span style="color: red;">*</span> Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                        CssClass="form-control" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span style="color: red;">*</span> College /School Name</label>
                                    <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        ValidationGroup="Show" ToolTip="College Name" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged" TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollegeName"
                                        Display="None" ErrorMessage="Please Select College Name" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span style="color: red;">*</span> Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" TabIndex="3">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span id="spbr" runat="server" style="color: red;">*</span> Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" TabIndex="4">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                        ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span id="spsc" runat="server" style="color: red;">*</span> Regulation</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" TabIndex="5">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvProgram" runat="server" InitialValue="0" SetFocusOnError="true"
                                        ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Regulation"
                                        ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span id="spse" runat="server" style="color: red;">*</span> Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        ValidationGroup="Show" CssClass="form-control" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="6">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>Subject</label>
                                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="7">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3" id="trsection" runat="server">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddlSection" CssClass="form-control" runat="server" ToolTip="Section" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="8" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSection" runat="server" Enabled="false" ControlToValidate="ddlSection"
                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>
                                </div>
                                
                            </div>
                            <div class="col-md-12">
                                <center>
                                    <asp:LinkButton ID="lbtn_Done" runat="server" CssClass="btn btn-outline-info" OnClick="lbtn_Done_Click" TabIndex="9">Show Students</asp:LinkButton>
                                    <asp:LinkButton ID="btn_Process" runat="server" CssClass="btn btn-outline-info" OnClick="btn_Process_Click" TabIndex="10" Enabled="false">Process Grades</asp:LinkButton>
                                    <asp:LinkButton ID="lbnt_Lock" runat="server" CssClass="btn btn-danger" OnClick="lbnt_Lock_Click" TabIndex="11" Enabled="false">Lock</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_Print" runat="server" CssClass="btn btn-outline-primary" OnClick="lbtn_Print_Click" TabIndex="12" Enabled="false">Print</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_Cancel" runat="server" CssClass="btn btn-outline-danger" OnClick="lbtn_Cancel_Click" TabIndex="13">Cancel</asp:LinkButton>
                                <center>
                            </div>
                        </div>
                        <hr  style="margin-bottom:0px"/>
                        <div>
                            <div class="row" style="margin: 5px 5px">
                                <div class="col-md-12" id="div1_Alert" runat="server" visible="false">
                                    <%--Geretaed Table is Here--%>
                                </div>
                            </div>
                        </div>
                        <div id="div_Result" runat="server" visible="false">
                            <div class="row" style="margin: 5px 5px">
                                <div class="col-md-12">
                                    <h3>
                                       <span class="label label-default pull-left">Grade Allotment</span> 
                                       <span class="pull-right">Total Records : <asp:Label ID="lbl_cnt" runat="server" Text="" CssClass="label label-default"></asp:Label></span>
                                   </h3>
                                </div>
                            </div>
                            <div class="row" style="margin: 5px 5px">
                                <div class="col-md-12">
                                    <table class="table table-hover table-bordered myCss">
                                        <thead style="background-color:#3c8dbc;color:white;" class="sticky">
                                          <tr>
                                            <th class="th1">STATUS</th>
                                            <th class="th2">UNIV.REG NO.</th>
                                            <th class="th3">SUBJECT NAME</th>
                                            <th class="th4">SEC</th>
                                            <%--<th>CAT 1</th>
                                            <th>CAT 2</th>
                                            <th class="h_cat3">CAT 3</th>--%>
                                            <th class="th5">INTERNAL</th>
                                            <th class="th6">EXTERNAL</th>
                                            <th class="th7">TOTAL</th>
                                            <th class="th8">GRADE</th>
                                            <th class="th9">TEACHER NAME</th>
                                            <th class="th10">UPDATE DATE</th>
                                            <%--<th>STATUS</th>--%>
                                          </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt_Success" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="td1"><%#Eval("LOCK_STATUS")%></td>
                                                        <td class="td2"><%#Eval("REGNO")%></td>
                                                        <td class="td3"><%#Eval("COURSE_NAME")%></td>
                                                        <td class="td4"><%#Eval("SECTIONNAME")%></td>
                                                        <%--<td><%#Eval("CAT1")%></td>
                                                        <td><%#Eval("CAT2") %></td>
                                                        <td class="v_cat3"><%#Eval("CAT3")%></td>--%>
                                                        <td class="td5"><%#Eval("INTERNAL_MARK") %></td>
                                                        <td class="td6"><%#Eval("EXTERNAL_MARK") %></td>
                                                        <td class="td7"><%#Eval("MARK") %></td>
                                                        <td class="td8"><%#Eval("GRADE") %></td>
                                                        <td class="td9"><%#Eval("TEACHER_NAME") %></td>
                                                        <td class="td10"><%#Eval("UPDATE_DATE") %></td>
                                                        <%--<td><%#Eval("CAT1") %></td>--%>
                                                      </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtn_Done" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function onreport() {

            var a = document.getElementById("ctl00_ContentPlaceHolder1_rdbReport_4");
            if (a.checked) {
                document.getElementById("ctl00_ContentPlaceHolder1_rfvDegree").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvBranch").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvProgram").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = false;
            }
        }
    </script>


</asp:Content>
